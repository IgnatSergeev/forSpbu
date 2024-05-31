module Crawler

open FSharp.Data
open System.Text.RegularExpressions

/// <summary>
/// Creates async task to fetch page length
/// </summary>
/// <param name"url"> Page url </param>
/// <returns> Page lenght </returns> 
let fetchSize url = 
    async {
        let! page = Http.AsyncRequestString(url)
        return page.Length
    } |> Async.Catch

/// <summary>
/// Creates async task to fetch size of every url on the page
/// </summary>
/// <param name"url"> Page url </param>
/// <returns> Async task of whether the result was successful or not </returns> 
let crawlAsync url =
    let regex = new Regex("<a href=\"(http://[\w-/.]+)\">")
    async {
        let! pageOrExc = Http.AsyncRequestString(url) |> Async.Catch
        match pageOrExc with
        | Choice2Of2 exc -> return Error(exc)
        | Choice1Of2 page ->
            let links = regex.Matches(page) |> Seq.map (fun x -> x.Groups[1].Value) 
            let results = links |> Seq.map fetchSize |> Async.Parallel |> Async.RunSynchronously 
            return Ok(Array.map2 (fun x y -> (x, y)) (Seq.toArray links) results)
    } 

let printCrawl url =
    let result = crawlAsync url |> Async.RunSynchronously
    match result with
    | Error e -> printfn "Incorrect url: %s" e.Message
    | Ok array -> 
        let printUrl (url, result) = 
            match result  with
            | Choice1Of2 length -> printfn "%s - %d" url length
            | Choice2Of2 _ -> printfn "Incorrect url ignored: %s" url
        array |> Array.iter printUrl
