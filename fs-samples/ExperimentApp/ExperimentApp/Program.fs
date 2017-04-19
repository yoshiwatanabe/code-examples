// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open ObjectOriented

// interface


// abstract base class with virtual methods
[<AbstractClass>]
type Shape() = 
    //readonly properties
    abstract member Width : int with get
    abstract member Height : int with get
    //non-virtual method
    member this.BoundingArea = this.Height * this.Width
    //virtual method with base implementation
    abstract member Print : unit -> unit 
    default this.Print () = printfn "I'm a shape"

// concrete class that inherits from base class and overrides 
type Rectangle(x:int, y:int) = 
    inherit Shape()
    override this.Width = x
    override this.Height = y
    override this.Print ()  = printfn "I'm a Rectangle"

type Circle(rad:int) = 
    inherit Shape()

    //mutable field
    let mutable radius = rad
    
    //property overrides
    override this.Width = radius * 2
    override this.Height = radius * 2
    
    //alternate constructor with default radius
    new() = Circle(10)      

    //property with get and set
    member this.Radius
         with get() = radius
         and set(value) = radius <- value


[<EntryPoint>]
let main argv =     
    let derived = new DerivedClass(1,2)
    printfn "param1=%O" derived.Param1
    printfn "param2=%O" derived.Param2

    //test
    let r = Rectangle(2,3)
    printfn "The width is %i" r.Width
    printfn "The area is %i" r.BoundingArea
    r.Print()

    // test constructors
    let c1 = Circle()   // parameterless ctor
    printfn "The width is %i" c1.Width
    let c2 = Circle(2)  // main ctor
    printfn "The width is %i" c2.Width

    // test mutable property
    c2.Radius <- 3
    printfn "The width is %i" c2.Width
    0