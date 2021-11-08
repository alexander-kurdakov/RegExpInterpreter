---
hide:
  - navigation
---
# Installation


You can install the package with dotnet by following this steps:

* Add a source and PAT in your nuget.config file (global or you can make local copy in project directory).
#
	dotnet nuget add source --username <YOUR_USERNAME> --password <PAT> --store-password-in-clear-text --name github "https://nuget.pkg.github.com/alexander-kurdakov/index.json"

* Install the package by using this command or do it in IDE Nuget manager.
#
	dotnet add PROJECT package RegExpInterpreter --version <version>