open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes

module Inref =

    let anotherDay (fishSpawnTimes : FishSpawnTimes inref) =

        let n = Array.length fishSpawnTimes

        let mutable newFishes = 0

        for i in 0..n - 1 do

            let days = fishSpawnTimes[i]

            if days <> 0uy<day> then
                fishSpawnTimes[i] <- fishSpawnTimes[i] - 1uy<day>
            else
                fishSpawnTimes[i] <- ResetSpawnTimer
                newFishes <- newFishes + 1
                
        newFishes

    let spawnFish (fish: FishSpawnTimes byref) (newFish : int) =

        Array.append fish (Array.create newFish NewFishSpawnTimer)

module InrefResizeArray =

    let anotherDay (fishSpawnTimes : FishSpawnTimesResizable inref) =

        let n = fishSpawnTimes.Count

        for i in 0..n - 1 do

            let days = fishSpawnTimes[i]

            if days <> 0uy<day> then
                fishSpawnTimes[i] <- fishSpawnTimes[i] - 1uy<day>
            else
                fishSpawnTimes[i] <- ResetSpawnTimer
                fishSpawnTimes.Add(NewFishSpawnTimer)

module BetterApproach =


    let anotherDay (fishSpawnTimes : FishSpawnTimesBetterApproach inref) =

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

type Benchmarks() =

    let input = "3,4,3,1,2"

    [<Literal>]
    let ResetSpawnTimer = 6uy<day>

    [<Literal>]
    let NewFishSpawnTimer = 8uy<day>

    let fishSpawnTimes : FishSpawnTimes = 
        input.Split(',')
        |> Array.choose
            (fun days ->
                let success, result = System.Byte.TryParse(days)
                if success then
                    Some (result * 1uy<day>)
                else
                    None)

    let n = 175

    [<Benchmark>]
    member _.lanternFish() = 

        let mutable fish = fishSpawnTimes
    
        let anotherDay fishSpawnTimes =

            let nromalFish, readyFish =
                fishSpawnTimes
                |> Array.partition
                    (fun spawnTime -> spawnTime > 0uy<day>)

            let n = Array.length readyFish
            
            (Array.map (fun days -> days - 1uy<day>) nromalFish)
            |> Array.append (Array.map (fun _ -> ResetSpawnTimer) readyFish)
            |> Array.append (Array.create n NewFishSpawnTimer)
        
        for i = 1 to n do
            fish <- anotherDay fish

        fish.Length

    [<Benchmark>]
    member _.lanternFishInref() = 

        let mutable fish = fishSpawnTimes

        let mutable newFish = 0

        for i in 1..n do
            newFish <- Inref.anotherDay &fish
            fish <- Inref.spawnFish &fish newFish

        fish.Length

    [<Benchmark>]
    member _.lanternFishInrefResizeArray() =

        let mutable fish = fishSpawnTimes |> ResizeArray

        for i in 1..n do
            InrefResizeArray.anotherDay &fish

        fish.Count

    [<Benchmark>]
    member _.lanternFishBetterApproach() =

        let mutable fish = FishSpawnTimesBetterApproach "3,4,3,1,2" // (System.Console.ReadLine())

        Array.sortInPlaceBy (fun (days, _) -> days) fish

        for i in 1..n do
            BetterApproach.anotherDay &fish

        Array.sumBy (fun (_, count) -> count) fish

[<EntryPoint>]
let main (_: array<string>) =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    0