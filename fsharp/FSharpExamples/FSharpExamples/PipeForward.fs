﻿module PipeForward

open System.IO

    let sizeOfFolder folder =

        let getFiles folder =
            Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)

        let totalSize =
            folder
            |> getFiles
            |> Array.map (fun file -> new FileInfo(file))
            |> Array.map (fun info -> info.Length)
            |> Array.sum

        totalSize

// sizeOfFolder @"C:\repos\SGO.Tool.MMX"


