#r "System.Windows.Forms.dll"
#r "System.Drawing.dll"

open System
open System.IO
open System.Collection.Generic
open System.Drawing
open System.Windows.Forms

// val images : seq<string * System.Drawigng.Image>
let images =
  let myPictures =
    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

  Directory.GetFiles(myPictures, "*.JPG")
  |> Seq.map(fun filePath),
    Path.GetFileName(filePath),
    BitMap.FromFile(filePath))

let dg = new DataGridView(Dock = DockStyle.Fill)
dg.DataSource <- new List<_>(images)

let f = new Form()
f.Controls.Add(dg)

f.ShowDialog()
