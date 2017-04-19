module Basics

let BasicDemo =
    1
    let one = 1

    3.14
    let pi = 3.14

    "hello"
    let msg = "hello"

    "hello" |> ignore

    (1,1)
    let t1 = (1,1)

    (1,'c')
    let t2 = (1,'c')

    (10,"Red")
    let t3 = (10,"Red")
    
    ()

type RGB = Red = 0 | Green = 1 | Blue = 2
type Shape = Circle = 0 | Rectangle =1
let r = RGB.Red
let n = int RGB.Green // convert from Enum to a number
let e:RGB = enum 2 // convert a number to an Enum







