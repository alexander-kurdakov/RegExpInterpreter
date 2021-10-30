# QuadTree

QuadTree is a library used in Interpreter as automata transition type.

## QuadTree 
### Type
An instance of `QuadTree<'t>` is a discriminated union of:
* `Node(QuadTree<'t>, QuadTree<'t>, QuadTree<'t>, QuadTree<'t>)`
* `Leaf<'t>`
* `None`

### Functions
* `this.noneCheck (neutral: 't)` -  if `this` is `(None, None, None, None)` or `Leaf(neutral)` returns `None`, else return `this`.
* `this.plus (sndTree: quadTree<'t>) (algebraStruct: AlgebraicStruct<'t>)` - sum of trees in specified algebraic structure.
* `this.reduce (need: int) (current: int)` - resize of tree, allows to get rid of `(None, None, None, _)`.
* `this.scalarMultiply (scalar: 't) (multOp: 't -> 't -> 't) (neutral: 't)` - multiplies to `scalar` each element of tree.

## ExtendedTree type
Extended tree is a QuadTree wrapper type that additionally contains size of tree. Consists of vals: `colSize: int, lineSize: int, tree: QuadTree<'t>`

## IMatrix interface 
QuadTree implements IMatrix interface: 
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