# QuadTree library

QuadTree is a library used in Interpreter as automata transition type.

## QuadTree
### Type
An instance of `QuadTree<'t>` is a discriminated union of:
* `Node(QuadTree<'t>, QuadTree<'t>, QuadTree<'t>, QuadTree<'t>)`
* `Leaf<'t>`
* `None`

### Functions
* member `this.noneCheck (neutral: 't)` -  if `this` is `(None, None, None, None)` or `Leaf(neutral)` returns `None`, else return `this`.
* member `this.plus (sndTree: quadTree<'t>) (algebraStruct: AlgebraicStruct<'t>)` - sum of trees in specified algebraic structure.
* member `this.reduce (need: int) (current: int)` - resize of tree, allows to get rid of `(None, None, None, _)`.
* member `this.scalarMultiply (scalar: 't) (multOp: 't -> 't -> 't) (neutral: 't)` - multiplies to `scalar` each element of tree.

## ExtendedTree 
### Type 
Extended tree is a QuadTree wrapper type that additionally contains size of tree. Consists of vals:
* `colSize: int`
* `lineSize: int`
* `specSize: int`
* `tree: QuadTree<'t>`
### Functions
* member `this.toSparseMatrix` - returns `SparceMatrix` equivalent.
* static member `init (lineSize: int) (colSize:int) (func: int -> int -> 't)` -  initialize ExtendedTree.
* static member `clearNeutral (neutral: int) (tree: extendedTree<'t>)` - analog QuadTree.noneCheck
* member `this.fillNeutral (neutral: 't)` - All `None` to neutral
* member `this.getByIndex (i:int) (j: int) (algStr: AlgebraicStruct<'t>` - getter 
* static member `iteri (func: int -> int -> 't -> unit) (tree: extendedTree<'t>)` - default iteri
* static member `mapi (func: int -> int -> 't -> 'a) (exTree: extendedTree<'t>)` - default mapi
* static member `fold (func: 'a -> 't -> 'a) (acc: 'a) (tree: extendedTree<'t>)` - default fold 
* static member `map (func: 't -> 'a) (exTree: extendedTree<'t>)` - default map 
* member `this.plus (y: extendedTree<'t>) (algStruct: AlgebraicStruct<'t>)` - sum of trees
* static member `createTreeOfSparseMatrix (algStruct: AlgebraicStruct<'t>) (sparseMatrix: SparseMatrix<'t>)` - create tree of SparseMatrix
* member `this.multiply (snd: extendedTree<'t>) (algStruct: AlgebraicStruct<'t>)` - matrices multiplying
* member `this.tensorMultiply (snd: extendedTree<'t>) (algStruct: AlgebraicStruct<'t>)` - matrices tensor multiplying
* member `this.toBoolTree` - return tree, where `None` -> false, `'t` -> true
* member `this.transitiveClosure (algStruct: AlgebraicStruct<'t>)` - transitive closure of matrix in algebraic struct.
* member `this.parallelMultiply (snd: extendedTree<'t>) (algStruct: AlgebraicStruct<'t>) (depth: int)` - parallel multiplying in 4^depth 'threads'.


## IMatrix interface 
ExtendedTree implements IMatrix interface: 

*  `map: ('t -> 'a) -> IMatrix<'a>`

* `iteri: (int -> int -> 't -> unit) -> unit`

* `mapi: (int -> int -> 't -> 'a) -> IMatrix<'a>`

* `transitiveClosure: AlgebraicStruct<'t> -> IMatrix<'t>`

* `toBool: 't -> IMatrix<bool>`

* `fold: ('acc -> 't -> 'acc) -> 'acc -> 'acc`

* `get: int * int * AlgebraicStruct<'t> -> 't`

* `tensorMultiply: IMatrix<'t> -> AlgebraicStruct<'t> -> IMatrix<'t>`

* `lineSize: int`

* `colSize: int`