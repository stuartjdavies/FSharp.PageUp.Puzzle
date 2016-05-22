module FSharp.PageUp.Puzzle.Tests.SolutionTests

open System
open FsUnit
open NUnit.Framework
open FSharp.PageUp.Puzzle.Solution
open Microsoft.FSharp.Reflection

let unionToString (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

[<TestCase(10, 20, 5, 20, "MediumParcel", "80")>]
[<TestCase(22, 5, 5, 5, "HeavyParcel", "330")>]
[<TestCase(2, 3, 10, 12, "SmallParcel", "18")>]
[<TestCase(110, 20, 55, 120, "Rejected", "")>]
let ``Verify provided examples``(weight : int, height : int, width : int, depth : int, category : string, cost : string) =                                        
        let parcel = { FSharp.PageUp.Puzzle.Solution.ParcelSpecification.Weight = weight; Height=height; Width=width;Depth=depth }
        let solCategory, solCost = FSharp.PageUp.Puzzle.Solution.getDeliveryCost(parcel)                                 
        let solCostAsString = match(solCost) with | Some(x) -> x.ToString() | None -> String.Empty        
        (unionToString(solCategory), solCostAsString) |> should equal (category, cost)  
        ()