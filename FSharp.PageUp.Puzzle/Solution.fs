module FSharp.PageUp.Puzzle.Solution

open System

//
// Domain Model
//
type ParcelCategory = | Rejected | HeavyParcel | SmallParcel | MediumParcel | LargeParcel
type ParcelSpecification = { Weight : int; Height : int; Width : int; Depth : int }

//
// Parcel Volume Functions
//
let getParcelVolume (parcel : ParcelSpecification) = parcel.Height * parcel.Width * parcel.Depth

// 
// Parcel Category Functions
//
let rejectParcel (parcel : ParcelSpecification) = parcel.Weight > 50 
let isHeavyParcel (parcel : ParcelSpecification) = parcel.Weight > 10
let isSmallParcel (parcel : ParcelSpecification ) = getParcelVolume(parcel) < 1500
let isMediumParcel (parcel : ParcelSpecification ) = getParcelVolume(parcel) < 2500
let getParcelCategory parcel = 
        if rejectParcel(parcel) then Rejected
        else if isHeavyParcel(parcel) then HeavyParcel
        else if isSmallParcel(parcel) then SmallParcel
        else if isMediumParcel(parcel) then MediumParcel
        else LargeParcel        

//
// Delivery Cost Functions
//
let getHeavyParcelCost (parcel : ParcelSpecification) = float (parcel.Weight * 15)
let getSmallParcelCost (parcel : ParcelSpecification) = (float (getParcelVolume(parcel))) * 0.05
let getMeduimParcel (parcel : ParcelSpecification) = (float (getParcelVolume(parcel))) * 0.04
let getLargeParcel (parcel : ParcelSpecification) = (float (getParcelVolume(parcel))) * 0.03
let getDeliveryCostWithCategory (parcelCategory : ParcelCategory) (parcel) = 
         match parcelCategory with       
         | Rejected -> None        
         | HeavyParcel -> Some(getHeavyParcelCost parcel)
         | SmallParcel -> Some(getSmallParcelCost parcel)
         | MediumParcel -> Some(getMeduimParcel parcel)
         | LargeParcel -> Some(getLargeParcel parcel)       

let getDeliveryCost (parcel : ParcelSpecification) =                   
          let parcelCategory = getParcelCategory parcel 
          let costOfDelivery  = getDeliveryCostWithCategory parcelCategory parcel   
          parcelCategory, costOfDelivery
            
//
// Console Output Functions
//
let getUserInput() =
        printfn "Enter Weight in kg:"
        let parsedWeightSuccess, weight = Int32.TryParse(Console.ReadLine())                
        printfn "Enter Height in cm:"
        let parsedHeightSucsess, height = Int32.TryParse(Console.ReadLine())
        printfn "Enter Width in cm:"
        let parsedWidthSuccess, width = Int32.TryParse(Console.ReadLine())
        printfn "Enter Depth:"
        let parsedDepthSuccess, depth = Int32.TryParse(Console.ReadLine())
        
        if parsedWeightSuccess = true && 
           parsedHeightSucsess = true &&
           parsedWidthSuccess = true && 
           parsedDepthSuccess = true then
           Choice1Of2({ ParcelSpecification.Weight = weight; Height = height; Width = width; Depth = depth })
        else
           Choice2Of2("Invalid Input") 

let printPuzzleSolution (parcelCategory : ParcelCategory, costOfDelivery : float Option)=         
        match parcelCategory with
        | Rejected -> printfn "Category: Rejected" 
                      printfn "Cost: N/A"
        | HeavyParcel -> printfn "Category: Heavy Parcel"
                         printfn "Cost: $%d" (int (truncate(costOfDelivery.Value)))
        | SmallParcel -> printfn "Category: Small Parcel"  
                         printfn "Cost: $%d" (int (truncate(costOfDelivery.Value)))
        | MediumParcel -> printfn "Category: Medium Parcel"  
                          printfn "Cost: $%d" (int (truncate(costOfDelivery.Value)))
        | LargeParcel -> printfn "Category: Rejected"
                         printfn "Cost: $%d" (int (truncate(costOfDelivery.Value)))
 
//
// Solve problem
//       
let solve() =
        match getUserInput() with 
        | Choice1Of2(parcel) -> parcel |> getDeliveryCost |> printPuzzleSolution
        | Choice2Of2(parcel) -> printfn "Invalid input"                     