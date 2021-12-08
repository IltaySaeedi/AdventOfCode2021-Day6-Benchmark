[<Measure>]
type day

[<Literal>]
let ResetSpawnTimer = 6uy<day>

[<Literal>]
let NewFishSpawnTimer = 8uy<day>

type FishSpawnTimes = byte<day> array

let fishSpawnTimes (input: string) : FishSpawnTimes = 
    input.Split(',')
    |> Array.choose
        (fun days ->
            let success, result = System.Byte.TryParse(days)
            if success then
                Some (result * 1uy<day>)
            else
                None)

let anotherDay (fishSpawnTimes : FishSpawnTimes) =

    let nromalFish, readyFish =
        fishSpawnTimes
        |> Array.partition
            (fun spawnTime -> spawnTime > 0uy<day>)

    let n = Array.length readyFish
    
    (Array.map (fun days -> days - 1uy<day>) nromalFish)
    |> Array.append (Array.map (fun _ -> ResetSpawnTimer) readyFish)
    |> Array.append (Array.create n NewFishSpawnTimer)
    
let mutable input = fishSpawnTimes "3,4,3,1,2" // (System.Console.ReadLine())

let n = 175

let stopwatch = System.Diagnostics.Stopwatch()

stopwatch.Start()

for i in 1..n do
    input <- anotherDay input

stopwatch.Stop()

printfn $"%3i{n} days after we have got {Array.length input} fish"

let timeElapsed = stopwatch.ElapsedMilliseconds

printfn $"Time Elappsed : {timeElapsed} ms"