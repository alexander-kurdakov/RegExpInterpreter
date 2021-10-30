# ATMLibrary

ATMLibrary is a library used in Interpreter as main instrument to compute expressions and part of statements.

## NFA
### Type
NFA consists of vals:

* StartState : HashSet<int>
* FinalState : HashSet<int>
* Transitions : IMatrix<Set<'t>>

### Functions 

* `epsClosure (nfa: NFA<_>) (algStr: AlgebraicStruct<'t>) (algStrForBoolOp: AlgebraicStruct<'t>)
               (matrixBuilder: int -> int -> (int -> int -> Set<_>) -> IMatrix<_>)` - epsilon closure of ATM.
* `intersect (fst: NFA<_>) (snd: NFA<_>) (algStr: AlgebraicStruct<'t>) (algStrForBoolOp: AlgebraicStruct<'t>)
               (matrixBuilder: int -> int -> (int -> int -> Set<_>) -> IMatrix<_>)` - returns intersections of ATM's.
* `toDot (this: NFA<_>) (outFile: string)` - prints ATM to file
* `regexpToNFA (regexp: Regexp<_>) (algStr: AlgebraicStruct<_>)
    (mtxBuilder: int -> int -> (int -> int -> HashSet<_>) -> IMatrix<HashSet<_>>)` - converts regular expression to ATM.
* `seqToNFA (input: list<_>) matrixBuilder` - coverts string to ATM.
* `accept (nfa: NFA<_>) (input: list<_>) (algStr: AlgebraicStruct<'t>) (algStrForBoolOp: AlgebraicStruct<'t>)
               (matrixBuilder: int -> int -> (int -> int -> Set<_>) -> IMatrix<_>)`  - return result of accepting string by ATM.
* `findAll (nfa: NFA<_>) (input: list<_>) (algStr: AlgebraicStruct<'t>) (algStrForBoolOp: AlgebraicStruct<'t>)
               (matrixBuilder: int -> int -> (int -> int -> Set<_>) -> IMatrix<_>)` - returns all searched substrings satisfying ATM.
    
