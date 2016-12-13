open System.IO

let rec filesUnder basePath =
  seq {
    yield! Directory.GetFiles(basePath)
    for subDir in Directory.GetDirectories(basePath) do
      yield! filesUnder subDir
  }

let NewImageFolder @"C:\dev\junk"

// get error here walk_dir.fsx(12,1): error FS0010: Incomplete structured construct at or before this point in binding. Expected '=' or other token.
__SOURCE_DIRECTORY__
|> filesUnder
|> Seq.filter(fun filePath -> filePath.ToUpper().EndsWith("JPG"))
|> Seq.iter(fun filePath -> let fileName = Path.GetFileName(filePath)
                            let destPath = Path.Combine(NewImageFolder, fileName)
                            File.Copy(filePath, destPath))
