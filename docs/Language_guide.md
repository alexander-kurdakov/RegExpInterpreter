# Language guide

RegExpInterpreter uses simple programming language to work with regular expressions.

## Grammar

Each regular expression is defined as variable which can be used in other expressions. Typical program consists of statements with expressions and variables' names associated with them.

## Statements

There are three types of statements supported in the language: 

*	`print [<vname>]`
*	`let [<vname>] = <expression>`
*   `printToDot [<vname>: RegExp] <string>`

`print` prints the regular expression corresponding to the given variable, `let` defines a variable and `printToDot` outputs regular expression in .dot format.

* `<vname>` starts with a Latin character, which can be followed by numbers or other letters.
* `<expression>` consists of Latin letters, digits, characters `'-' '.' '/' '+' '~' ';'` and other variables. As operators characters `'(' ')' '*' '|' '?' '&'` can be used.

## RegExp type

*    `RSmb of <char>`
*    `RVar of <VName>`
*    `Alt of <Regex * Regex>`
*    `Seq of <Regex * Regex>`
*    `Opt of <Regex>`
*    `Star of <Regex>`
*    `Intersect of <Regex * Regex>`

`RVar` case allows variables to be used inside regular expressions.

## Expressions

*    `RegExp of <Regex>`
*    `FindAll of <string * Regex>`
*    `IsAcceptable of <string * Regex>`

`FindAll` searches for all substrings satisfying the specified regular expression,

`IsAcceptable` checks whether the string belongs to the specified expression.

## Operators

Available operators in Interpreter-RegEx:

* `*` - Kleene star
* `|` - alternation
* `?` - repeat 0 or 1 times
* `&` - intersect
* `(`, `)` - brackets for grouping


## Code example
	let [x] = (a|b)caba
	let [c] = isAcceptable "acaba" [x]
	let [d] = isAcceptable "1" (1*)&(1?)
	let [e] = findAll "muxa" (x|a)

	print [c] # True
	print [d] # True
	print [e] # [(2, 3); (3, 4)]
