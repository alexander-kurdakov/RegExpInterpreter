module Main


open Argu
open Interpreter


type CLIArguments =
    | InputFile of file: string
    | InputString of code: string
    | Compute

    
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | InputFile _ -> "File with code"
            | InputString _ -> "String of code" 
            | Compute -> "Return the result of interpretation of given code"


[<EntryPoint>]
let main (argv: string array) =
    let parser = ArgumentParser.Create<CLIArguments>(programName = "Interpreter")
    let results = parser.Parse(argv)
    let p = parser.ParseCommandLine argv
    if argv.Length = 0 || results.IsUsageRequested then parser.PrintUsage() |> printfn "%s"
    else
        let input =
            if p.Contains(InputFile) then System.IO.File.ReadAllText (results.GetResult InputFile)
            elif p.Contains(InputString) then results.GetResult InputString
            else failwith "No input code given"
        let ast = textToAST input
        if p.Contains(Compute)
            then
                let _, _, pD = run ast
                printfn "%s" pD.["print"] 
    0
    