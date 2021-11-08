module AST


type VName = Var of string


type Regex =
    | RSmb of char
    | RVar of VName
    | Alt of Regex * Regex
    | Seq of Regex * Regex
    | Opt of Regex
    | Star of Regex
    | Intersect of Regex * Regex


type Expr =
    | RegExp of Regex
    | FindAll of string * Regex
    | IsAcceptable of string * Regex


type Stmt =
    | Print of VName
    | PrintToDot of VName * string
    | VDecl of VName * Expr


type Program = List<Stmt>
