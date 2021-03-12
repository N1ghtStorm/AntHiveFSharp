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

    [<Struct>]
    type Ant = { id: int; }

    [<Struct>]
    type Order = { antId: int; act: string; dir: string }

    [<Struct>]
    type Request = { ants: Ant[] }

    [<Struct>]
    type Responce = { orders: Order[] }

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.Configure(fun app ->
                     app.UseRouting()
                        .UseEndpoints(fun endpoints ->
                            endpoints.MapGet("/", fun context -> 
                                  let reader = new StreamReader(context.Request.BodyReader.AsStream(), Encoding.Default)
                                  let inputStr = reader.ReadToEnd()
                                  let request = JsonSerializer.Deserialize<Request>(inputStr)
                                  let randNum = rand.Next(directions.Length)
                                  let orders = request.ants.Select(fun a -> {antId = a.id; dir = directions.[randNum]; act = "move"})
                                  context.Response.WriteAsJsonAsync(orders)
                            ) |> ignore
                        ) |> ignore
                ) |> ignore
            )


    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode