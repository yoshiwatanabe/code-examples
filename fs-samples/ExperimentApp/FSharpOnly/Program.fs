module MainProgram

open System
open Shapes

module Utility =    
    let functionWithTwoArgs key value = printfn "Arg1: %A, Arg2: %A" key value
    let functionWithSingleArg(key, value) = printfn "Key: %A, Value: %A" key value
    let functionWithTwoArgsTyped key (value:float) = printfn "Arg1: %A, Arg2: %A" key value

open Utility

[<EntryPoint>]
let main argv =

    if argv.Length = 1 then
        let mutable intValue = 0        
        if Int32.TryParse(argv.[0], &intValue) then 
            printfn "An integer argument is specified  %A" intValue
        else
            printfn "A non-integer argument is specified  %A" argv.[0]
    else 
        printfn "No argument is specified"

    functionWithSingleArg ("Pi", 3.14)
    functionWithTwoArgs "Pi" 3.14
    functionWithTwoArgsTyped "Pi" 3.14

    // for loop
    let n = 10
    for i = 1 to n do
        printf "%d " i
    for j = n downto 1 do
        printf "%d " j

    // foreach array
    let states = [|"WA"; "OR"; "CA"|]
    for state in states do
       printfn "%A" state

    // foreach list
    let stateList = ["WA"; "OR"; "CA"]
    for state in stateList do
       printfn "%A" state

    // while loop
    let mutable counter = 10
    while (counter < 20) do
       printfn "value of counter: %d" counter
       counter <- counter + 1

    // object instance
    let r = Rectangle(2, 3)
    printfn "The width is %i. The area is %i" r.Width r.BoundingArea
    r.Print()

    let c1 = Circle()
    printfn "The width is %i" c1.Width
    let c2 = Circle(2)
    printfn "The width is %i" c2.Width

    c2.Radius <- 3
    printfn "The width is %i" c2.Width

    0 // return an integer exit code