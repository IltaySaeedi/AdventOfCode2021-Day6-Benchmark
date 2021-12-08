[<Measure>]
type day

[<Literal>]
let ResetSpawnTimer = 6uy<day>

[<Literal>]
let NewFishSpawnTimer = 8uy<day>

type FishSpawnTimes = ResizeArray<byte<day>>

let fishSpawnTimes (input: string) : FishSpawnTimes = 

    input.Split(',')
    |> Array.choose
        (fun days ->

            let success, result = System.Byte.TryParse(days)
            
            if success then
                Some (result * 1uy<day>)
            else
                None)
    |> ResizeArray

let anotherDay (fishSpawnTimes : FishSpawnTimes inref) =

    let n = fishSpawnTimes.Count

    for i in 0..n - 1 do

        let days = fishSpawnTimes[i]

        if days <> 0uy<day> then
            fishSpawnTimes[i] <- fishSpawnTimes[i] - 1uy<day>
        else
            fishSpawnTimes[i] <- ResetSpawnTimer
            fishSpawnTimes.Add(NewFishSpawnTimer)

let mutable input = fishSpawnTimes "3,4,3,1,2" // (System.Console.ReadLine())

let n = 175

let stopwatch = System.Diagnostics.Stopwatch()

stopwatch.Start()

for i in 1..n do
    anotherDay &input

stopwatch.Stop()

printfn $"%3i{n} days after we have got {input.Count} fish"

let timeElapsed = stopwatch.ElapsedMilliseconds

printfn $"Time Elappsed : {timeElapsed} ms"