namespace FSharpRestApi

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open System.Text.Json;
open System.IO
open System.Text
open System
open System.Linq

module Program =
    let exitCode = 0

    let rand = new Random(DateTime.Now.Millisecond);
    let actions = [| "move"; "eat"; "take"; "put" |];
    let directions = [| "up"; "down"; "right"; "left" |];

    type Ant = { id: int; }

    type Order = { antId: int; act: string; dir: string }

    type Request = { ants: Ant[] }

    type Responce = { orders: Order[] }

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseUrls("http://*:7070").Configure(fun app ->
                     app.UseRouting()
                        .UseEndpoints(fun endpoints ->
                            endpoints.MapGet("/", fun context -> 
                                  let reader = new StreamReader(context.Request.BodyReader.AsStream(), Encoding.Default)
                                  let inputStr = reader.ReadToEnd()
                                  let request = JsonSerializer.Deserialize(inputStr)
                                  let orders = request.ants.Select(fun ant -> {antId = ant.id; dir = directions.[rand.Next(directions.Length)]; act = "move"}).ToArray()
                                  let responce = {orders = orders}
                                  context.Response.WriteAsJsonAsync(responce)
                            ) |> ignore
                        ) |> ignore
                ) |> ignore
            )


    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode