open LibraryAsCSharp
open DataAccessFSharp
open Newtonsoft.Json

let add a b = a + b
let increment n = add 1 n
let result = increment 9

[<EntryPoint>]
let main argv =         
    //Basics.BasicDemo   
    //ListComrehensionDemo.demoListComprehension
    //Examples.factorial 4 |> ignore

    //let x = SkuDataAccess.testFunc "5F4-00012"
    let manifestInJson = "{'Id':432,'Description':'Manifest for launching Xbox One!','Products': [{'Id':12, 'Title':'Xbox One'}]}"
    let manifest = Newtonsoft.Json.JsonConvert.DeserializeObject(manifestInJson)

    let c1 = Class1()
    c1.CountProperty <- 10
    let p1 = c1.CountProperty
    let count = c1.GetCountMethod()    
    0
