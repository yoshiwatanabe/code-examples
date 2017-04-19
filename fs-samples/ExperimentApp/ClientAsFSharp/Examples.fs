module Examples

let rec factorial n =
    match n with
    | 0 | 1 -> 1
    | _ -> n * factorial(n-1)

let factorialTailRecursion n =
    let rec loop i acc =
        match i with
        | 0 | 1 -> acc
        | _ -> loop (i-1) (acc * i)
    loop n 1

let factorialWithFold n = [1..n] |> List.fold (*) 1

let factorialWithReduce n = [1..n] |> List.reduce (*)

let rec fib n =
    match n with
    | 1 | 2 -> 1
    | n -> fib(n-1) + fib(n-2)

let fibWithLookup n =
    let lookup = Array.create (n+1) 0
    let rec f = fun x ->
        match x with
        | 1 | 2 -> 1
        | x ->
            match lookup.[x] with
            | 0 ->
                lookup.[x] <- f(x-1) + f(x-2)
                lookup.[x]
            | x -> x
    f(n)