module Shapes

[<AbstractClass>]
type Shape() = 
    abstract member Width : int with get
    abstract member Height : int with get

    member this.BoundingArea = this.Height * this.Width

    abstract member Print : unit -> unit 
    default this.Print () = printfn "I'm a shape"

type Rectangle(x:int, y:int) = 
    inherit Shape()
    override this.Width = x
    override this.Height = y
    override this.Print ()  = printfn "I'm a Rectangle"

type Circle(rad:int) = 
    inherit Shape()

    let mutable radius = rad
    
    override this.Width = radius * 2
    override this.Height = radius * 2
    
    new() = Circle(10)      

    member this.Radius
         with get() = radius
         and set(value) = radius <- value