module Evaluations

open LibraryAsCSharp

let rec fib n =
    match n with
    | 1 | 2 -> 1
    | n -> fib(n-1) + fib(n-2)

        
