[<AutoOpen>]
module Common

[<Measure>]
type day

type FishSpawnTimes = byte<day> array

type FishSpawnTimesResizable = byte<day> ResizeArray

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