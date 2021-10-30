---
hide:
  - navigation
---
# Github repository structure

```
RegExpInterpreter
├── .config - dotnet tools config
├── .github - GitHub Actions setup 
├── docs - documentation files
├── src - projects directory
│	├── Interpreter - interpreter of regular expressions
|	├── ATMLibrary - library for NFA, uses quadtrees as transitions
|	└── QuadTree - quadtrees library
├── tests 
|	├── Interpreter.Tests - tests for Interpreter
|	├── QuadTree.Tests - tests for QuadTree
|	└── ATMLibrary.Tests - tests for ATMLibrary
├── fsharplint.json - linter config
├── RegExpInterpreter.sln - solution file
└── build.fsx - configuration of build
```