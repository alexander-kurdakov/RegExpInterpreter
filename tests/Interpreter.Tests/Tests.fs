module Tests


open System.Collections.Generic
open Interpreter
open Expecto


let evaluateExpr (ast: AST.Stmt list) =
    match ast.[0] with
    | AST.VDecl (_, e) -> processExpr (Dictionary<_,_>()) e
    | _ -> failwith "unexpected statement"
    
   
let testTemplate (func: string) (str: string) (re: string) (expectation: string) =
    let x = "let [x]" + $" = %s{func} \"%s{str}\" %s{re}"
    let ast = textToAST x
    Expect.equal $"{expectation}" (evaluateExpr ast).ToString <| $"%A{ast}"


[<Tests>]
let testInterpreter =
    testList "Interpreter tests" [
        testCase "isAcceptable test #1" <| fun _ ->
            testTemplate "isAcceptable" "12121" "(1|2)*" "True"
        
        
        testCase "False isAcceptable test" <| fun _ ->
            testTemplate "isAcceptable" "12521" "(1|2)*" "False"
            
            
        testCase "First findAll test" <| fun _ ->
            testTemplate "findAll" "yyx" "x|y" "[(0, 1); (1, 2); (2, 3)]"
            
            
        testCase "Second findAll test" <| fun _ ->
            testTemplate "findAll" "byx" "a|y" "[(1, 2)]"
            
            
        testCase "Empty findAll test" <| fun _ ->
            testTemplate "findAll" "abb" "x|y" "[]"
            
            
        testCase "fail test #1" <| fun _ ->
            let x = "let x] = findAll \"abb\" x|y"
            Expect.throws (fun () -> (evaluateExpr (textToAST x)) |> ignore) ""
            
            
        testCase "fail test #2" <| fun _ ->
            let x = "let [x] =, isAcceptable \"12521\" (1|2)*"
            Expect.throws (fun () -> (evaluateExpr (textToAST x)) |> ignore) ""
            
            
        testCase "Intersection #1" <| fun _ ->
            testTemplate "isAcceptable" "1" "(1*)&(1|0)*" "True"
            
            
        testCase "Intersection #2" <| fun _ ->
            testTemplate "isAcceptable" "101" "(1*)&(1|0)*" "False"
            
            
        testCase "Intersection #3" <| fun _ ->
            testTemplate "isAcceptable" "1" "(1|0)&(1|2)*" "True"
            
          
        testCase "Intersection #4" <| fun _ ->
            testTemplate "isAcceptable" "0" "(1|0)&(1|2)*" "False"
            
            
        testCase "Intersection #5" <| fun _ ->
             testTemplate "isAcceptable" "2" "(1|0)&(1|2)*" "False"
            
            
        testCase "Intersection #6" <| fun _ ->
            testTemplate "isAcceptable" "rak" "(ra(k)*)&(r(a)*(k)*)*" "True"
            
            
        testCase "Intersection #7" <| fun _ ->
            testTemplate "isAcceptable" "raak" "(ra(k)*)&(r(a)*(k)*)" "False"
    ]
    