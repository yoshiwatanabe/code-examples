
module test_readFile =
    
    open System.IO
    open System
    
    let baseDirectory = __SOURCE_DIRECTORY__
    let ParseFile = File.ReadAllLines <| Path.Combine(baseDirectory, "us-500.csv")

module test_function1 =
    
    open System.IO
    open System

    let dataFile fileName = 
        let baseDirectory = __SOURCE_DIRECTORY__
        File.ReadAllLines <| Path.Combine(baseDirectory, fileName)

    let ParseFile = dataFile "us-500.csv"

module test_consume_module =

    open test_function1

    let ParseFile = dataFile "us-500.csv"
    ParseFile.Length |> printfn "Length: %d"
    ParseFile.[0]

    let printLine l =
        printfn "Line: %s" l

    Seq.skip 1 ParseFile |> Seq.map printLine

    ParseFile

module test_colorful_output =

    open System

    let cprintfn c fmt = 
        Printf.kprintf
            (fun s ->
                let orig = System.Console.ForegroundColor
                System.Console.ForegroundColor <- c;
                System.Console.WriteLine(s)
                System.Console.ForegroundColor <- orig)
            fmt

    

    cprintfn ConsoleColor.Blue "Hello"


module dialog_test =

    #r "System.Windows.Forms.dll"
    #r "System.Drawing.dll"

    open System
    open System.IO
    open System.Collections.Generic
    open System.Drawing
    open System.Windows.Forms

    // val images : seq<string * System.Drawigng.Image>
    let images =
      let myPictures =
        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

      Directory.GetFiles(myPictures, "*.JPG")
      |> Seq.map(fun filePath ->
        Path.GetFileName(filePath),
        Bitmap.FromFile(filePath))

    let dg = new DataGridView(Dock = DockStyle.Fill)
    dg.DataSource <- new List<_>(images)

    let f = new Form()
    f.Controls.Add(dg)

    f.ShowDialog();;

module test_user =
    open System.IO
    
    let testFunc =        
        use sw = new StreamWriter(@"c:\dev\junk\test.txt")
        fprintf sw "hello from f#"
        sw.Close()

    testFunc;;


module whatisdo =
    
    do printf "do it"
    do (1 + 1 |> ignore)