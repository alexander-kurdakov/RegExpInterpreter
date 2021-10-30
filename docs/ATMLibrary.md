# ATMLibrary

ATMLibrary is a library used in Interpreter as main instrument to compute expressions and part of statements.

## QuadTree
### Type
An instance of `QuadTree<'t>` is a discriminated union of:
* `Node(QuadTree<'t>, QuadTree<'t>, QuadTree<'t>, QuadTree<'t>)`
* `Leaf<'t>`
* `None`
