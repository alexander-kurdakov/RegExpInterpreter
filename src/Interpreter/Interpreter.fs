module Interpreter


open System.Collections.Generic
open FSharp.Text.Lexing
open Regexp
open ATM
open MatrixBuilder
open AlgebraicStructsForATM


let private runtimeException = Event<string>()

let NewRuntimeException = runtimeException.Publish


type VType =
    | RE of Regexp<char>
    | Bool of bool
    | Lst of list<int * int>
    
    // for tests
    member this.ToString =
        match this with
        | RE x -> x.ToString()
        | Bool x -> x.ToString()
        | Lst x -> x.ToString()
    

let rec processRE (valueDict: Dictionary<_, _>) re =
    match re with
    | AST.RVar v ->
        let data =
            try
                valueDict.[v]
            with
            | _ ->
                $"Variable %A{v} is not declared."
                |> runtimeException.Trigger

                failwith $"Variable %A{v} is not declared."

        match data with
        | RE r -> r
        
        | Bool _ ->
            $"Variable %A{v} has type bool, but regexp is expected."
            |> runtimeException.Trigger

            failwithf $"Variable %A{v} has type bool, but regexp is expected."
            
        | Lst _ ->
            $"Variable %A{v} has type list, but regexp is expected."
            |> runtimeException.Trigger

            failwithf $"Variable %A{v} has type list, but regexp is expected."

    | AST.RSmb smb -> RSmb smb
    
    | AST.Alt (l, r) ->
        let left = processRE valueDict l
        let right = processRE valueDict r
        Alt(left, right)
        
    | AST.Seq (l, r) ->
        let l = processRE valueDict l
        let r = processRE valueDict r
        Seq(l, r)
        
    | AST.Star r ->
        let r = processRE valueDict r
        Star r
        
    | AST.Opt r ->
        let r = processRE valueDict r
        Alt(REps, r)
        
    | AST.Intersect (l, r) ->
        let l = processRE valueDict l
        let r = processRE valueDict r
        Intersect(l, r)


let processExpr vDict expression =
    let makeAtm astRegex =
        let regex = processRE vDict astRegex
        let nfa = regexpToNFA regex algStrForHashSetsOp (matrixBuilder QuadTree)
        epsClosure nfa algStrForSetsOp algStrForBoolOp (matrixBuilder QuadTree)

    match expression with
    | AST.FindAll (str, re) ->
        Lst(findAll (makeAtm re) (str.ToCharArray() |> List.ofArray) algStrForSetsOp algStrForBoolOp (matrixBuilder QuadTree))
                                            
    | AST.IsAcceptable (str, re) ->
        Bool(accept (makeAtm re) (str.ToCharArray() |> List.ofArray) algStrForSetsOp algStrForBoolOp (matrixBuilder QuadTree))
    
    | AST.RegExp re -> RE(processRE vDict re)


let processStmt (vDict: Dictionary<_, _>) stmt =
    match stmt with
    | AST.Print value ->
        let varData =
            try
                vDict.[value]
            with
            | _ ->
                $"Variable {value} is not declared."
                |> runtimeException.Trigger

                failwithf $"Variable {value} is not declared."

        let printConst = "print"
        match varData with
        | RE reVal -> printfn $"{reVal.ToString()}"
        | Bool boolVal ->printfn $"{boolVal.ToString()}"
        | Lst lValues -> printfn $"{lValues.ToString()}"
    
    | AST.VDecl (value, expr) ->
        if vDict.ContainsKey value then
            vDict.[value] <- processExpr vDict expr
        else
            vDict.Add(value, processExpr vDict expr)
            
    | AST.PrintToDot (var, path) ->
        let processVar reVar fullPath =
            match reVar with
            | RE regEx ->
                let mtxNFA = regexpToNFA regEx algStrForHashSetsOp (matrixBuilder QuadTree)
                toDot mtxNFA fullPath
                Ok $"printToDot: written to {fullPath}"
                
            | _ as other -> Error $"Error: 'printToDot' 1 arg. should be a var with regex; instead got: %A{other}"
        
        match (processVar vDict.[var] path) with
        | Ok msg -> printfn $"{msg}" 
        | Error msg ->
            let _msg = $"Processing statement error: {msg}"
            runtimeException.Trigger msg // generate event
            failwith _msg

    vDict


let run program =
    let vDict = Dictionary<_, _>()
    List.fold processStmt vDict program |> ignore


let textToAST text =
    let lexBuffer = LexBuffer<char>.FromString text
    let parsed = Parser.start Lexer.tokenStream lexBuffer
    parsed
        
