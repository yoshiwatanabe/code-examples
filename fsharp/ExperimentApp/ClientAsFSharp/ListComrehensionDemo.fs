module ListComrehensionDemo
// https://en.wikipedia.org/wiki/List_comprehension
// https://en.wikipedia.org/wiki/Set-builder_notation

let demoListComprehension =
    let list1 = [1 .. 10] // 1 2 3 ... 10
    list1 |> Seq.iter (fun x -> printfn "%d" x)

    let list2 = [10..10..100] // 10 20 30 ... 100
    list2 |> Seq.iter (printfn "%d")

    let list3 = [for a in 1..10 do yield (a * a) ] // 1 4 9 ... 100
    list3 |> Seq.iter (printfn "%d")

    let list4 = [for a in 1..10 -> (a * a)]
    list4 |> Seq.iter (printfn "%d")
    
    ()