
// Define a very simple function.
let square x = x * x

// Use pipe forward to combine two functions to achive one result
let sum numbers =
    numbers
    |> Seq.map square
    |> Seq.sum

let n = [1..10] |> sum

// -------------------

// Define a function that simply changes the sign
let negate x = -x

let negated = List.map negate [1.. 10]

// -------------------

// Use lambda expression as a parameter to a function. (This function is higher-order function)
let minues = List.map (fun i -> -1) [1..10]

// -------------------

open System.IO

let appendFile (fileName: string) (text: string) =
    use file = new StreamWriter(fileName, true)
    file.WriteLine(text)
    file.Close()

appendFile @"C:\dev\junk.txt" "Written by F# sample app"

let curriedAppendFile = appendFile @"C:\dev\junk2.txt"
curriedAppendFile "line 1"
curriedAppendFile "line 2"

// -------------------

let generatePowerOfFunc (bs:float) = (fun (exponent:float) -> bs ** exponent)
let powerOfTwo = generatePowerOfFunc 2.0
powerOfTwo 8.0 // 2 to the power of 8
let powerOfThree = generatePowerOfFunc 3.0
powerOfThree 2.0

// --------------------

let rec isOdd n = (n = 1) || isEven (n - 1)
and isEven n = (n = 0) || isOdd(n - 1)
isOdd 2
isEven 2

// --------------------

open System.Text.RegularExpressions

let (===) str (regex: string) =
    Regex.Match(str, regex).Success

let regexResult = "The quick brown fox" === "The (.*) fox"

// --------------------

open System


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
