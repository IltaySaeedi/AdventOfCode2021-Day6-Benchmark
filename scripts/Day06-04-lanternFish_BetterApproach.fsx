[<Measure>]
type day

type FishSpawnTimes = (byte<day> * int) array

let fishSpawnTimes (input: string) : FishSpawnTimes = 

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
            Array.tryFind (fun (days, count) -> days = i * 1uy<day>) fish

        if result.IsSome then
            result.Value
        else
            i * 1uy<day>, 0
    |]

let anotherDay (fishSpawnTimes : FishSpawnTimes inref) =

    let n = Array.length fishSpawnTimes

    let mutable newFish = 0

    let mutable resetFish = 0

    let mutable temp = 0

    for i in 0..n-1 do

        let days, count = fishSpawnTimes[i]

        temp <- 
            if i = n-1 then
                0
            elif fst fishSpawnTimes[i+1] = days + 1uy<day> then
                snd fishSpawnTimes[i+1]
            else
                0

        if days = 0uy<day> then
            resetFish <- count
            newFish <- count
            fishSpawnTimes[i] <- days, temp
        elif days = 6uy<day> then
            fishSpawnTimes[i] <- days, temp + resetFish
        elif days < 8uy<day> then
            fishSpawnTimes[i] <- days, temp
        elif days = 8uy<day> then
            fishSpawnTimes[i] <- days, resetFish
        else
            ()

let mutable input = fishSpawnTimes "3,4,3,1,2" // (System.Console.ReadLine())

let n = 175

let stopwatch = System.Diagnostics.Stopwatch()

stopwatch.Start()

Array.sortInPlaceBy (fun (days, _) -> days) input

for i in 1..n do
    anotherDay &input

stopwatch.Stop()

let fishCount = Array.sumBy (fun (_, count) -> count) input

printfn $"%3i{n} days after we have got {fishCount} fish"

let timeElapsed = stopwatch.ElapsedMilliseconds

printfn $"Time Elappsed : {timeElapsed} ms"