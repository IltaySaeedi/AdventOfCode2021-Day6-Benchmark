[<AutoOpen>]
module Common

[<Measure>]
type day

type FishSpawnTimes = byte<day> array

type FishSpawnTimesResizable = byte<day> ResizeArray

type FishSpawnTimesBetterApproach = (byte<day> * int) array

[<Literal>]
let ResetSpawnTimer = 6uy<day>

[<Literal>]
let NewFishSpawnTimer = 8uy<day>

let fishSpawnTimes (input: string) : FishSpawnTimes = 
    input.Split(',')
    |> Array.choose
        (fun days ->
            let success, result = System.Byte.TryParse(days)
            if success then
                Some (result * 1uy<day>)
            else
                None)

let FishSpawnTimesBetterApproach (input: string) : FishSpawnTimesBetterApproach = 

    let fish =
        input.Split(',')
        |> Array.choose
            (fun days ->
                let success, result = System.Byte.TryParse(days)
                if success then
                    Some (result * 1uy<day>)
                else
                    None)
        |> Array.countBy id
        
    [| for i in 0uy..8uy ->

        let result = 
            Array.tryFind 
                (fun (days, count) -> days = i * 1uy<day>) fish

        if result.IsSome then
            result.Value
        else
            i * 1uy<day>, 0
    |]