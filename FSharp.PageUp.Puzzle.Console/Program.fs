// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open FSharp.PageUp.Puzzle.Solution

open System

[<EntryPoint>]
let main argv = 
    FSharp.PageUp.Puzzle.Solution.solve()
    Console.ReadLine() |> ignore    
    0 // return an integer exit code
