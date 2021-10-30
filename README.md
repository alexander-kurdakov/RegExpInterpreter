# About RegExpInterpreter


RegExpInterpreter is an interpreter for the simple programming language designed to work with regular expressions. It also has a libraries for a work with quadtree-matrices and NFA. Last two modules can be used for developers. Interpreter has console interface for users.

# Builds
||Badge|
|------|:------:|
|**Build Status**|[![GitHub Actions](https://github.com/alexander-kurdakov/RegExpInterpreter/workflows/Build/badge.svg?branch=master)](https://github.com/alexander-kurdakov/RegExpInterpreter/actions?query=branch%3Amaster) |
|**Build History**|[![Build History](https://buildstats.info/github/chart/alexander-kurdakov/RegExpInterpreter)](https://github.com/alexander-kurdakov/RegExpInterpreter/actions?query=branch%3Amaster) |
|**Target Framework**|[![Targets](https://img.shields.io/badge/.NET%20-5-green.svg)](https://docs.microsoft.com/ru-ru/dotnet/core/introduction)|


# Getting Started

You can install the package with dotnet by following this steps:

* Add a source and PAT in your nuget.config file (global or you can make local copy in project directory).
#
	dotnet nuget add source --username <YOUR_USERNAME> --password <PAT> --store-password-in-clear-text --name github "https://nuget.pkg.github.com/alexander-kurdakov/index.json"

* Install the package by using this command or do it in IDE Nuget manager.
#
	dotnet add PROJECT package RegExpInterpreter --version <version>

# Usage and Examples

A regular expression can be defined as variable, which can be used in other expressions. Typical program consists of statements with expressions and variables' names associated with them.

Three statements are supported:

	let [var] = expr # Variable declaration; var = { 'a'..'z' | 'A'..'Z' | '0' - '9' }
	print [var] # Statement for outputting variables
	printToDot [var: RegExp] # Statement for outputting regular expressions to .dot file

Example:

	let [x] = (a|b)caba
	let [c] = isAcceptable "acaba" [x]
	let [d] = isAcceptable "1" (1*)&(1?)
	let [e] = findAll "muxa" (x|a)

	print [c] # True
	print [d] # True
	print [e] # [(2, 3); (3, 4)]

# Documents

Visit [docs](https://alexander-kurdakov.github.io/RegExpInterpreter/) for full overview of tool.

# Directory structure

```
RegExpInterpreter
├── .config - dotnet tools config
├── .github - GitHub Actions setup 
├── docs - documentation files
├── src - projects directory
│  ├── Interpreter - interpreter of regular expressions
|  ├── ATMLibrary - library for NFA, uses quadtrees as transitions
|  └── QuadTree - quadtrees library
├── tests 
|	├── Interpreter.Tests - tests for Interpreter
|	├── QuadTree.Tests - tests for QuadTree
|	└── ATMLibrary.Tests - tests for ATMLibrary
├── fsharplint.json - linter config
├── RegExpInterpreter.sln - solution file
└── build.fsx - configuration of build
```
	