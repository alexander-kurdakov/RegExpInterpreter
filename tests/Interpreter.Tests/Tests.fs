module Tests


open System.Collections.Generic
open Interpreter
open Expecto


let evaluateExpr (ast: AST.Stmt list) =
    match ast.[0] with
    | AST.VDecl (_, e) -> processExpr (Dictionary<_,_>()) e
    | _ -> failwith "unexpected statement"


[<Tests>]
let testInterpreter =
    testList "Interpreter tests" [
        testCase "isAcceptable test #1" <| fun _ ->
            let x = "let [x] = isAcceptable \"12121\" (1|2)*"
            Expect.equal "True" (evaluateExpr (textToAST x)).ToString ""
        
        
        testCase "False isAcceptable test" <| fun _ ->
            let x = "let [x] = isAcceptable \"12521\" (1|2)*"
            Expect.equal "False" (evaluateExpr (textToAST x)).ToString ""
            
            
        testCase "First findAll test" <| fun _ ->
            let x = "let [x] = findAll \"yyx\" x|y"
            Expect.equal "[(0, 1); (1, 2); (2, 3)]" (evaluateExpr (textToAST x)).ToString ""
            
            
        testCase "Second findAll test" <| fun _ ->
            let x = "let [x] = findAll \"byx\" a|y"
            Expect.equal "[(1, 2)]" (evaluateExpr (textToAST x)).ToString ""
            
            
        testCase "Empty findAll test" <| fun _ ->
            let x = "let [x] = findAll \"abb\" x|y"
            Expect.equal "[]" (evaluateExpr (textToAST x)).ToString ""
    ]
    