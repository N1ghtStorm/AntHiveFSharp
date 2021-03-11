namespace FSharpRestApi

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open System.Text.Json;
open Microsoft.AspNetCore.Http
open System.IO
open System.Text

module Program =
    let exitCode = 0

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

                                  context.Response.WriteAsJsonAsync("sadsa")
                            ) |> ignore
                        ) |> ignore
                ) |> ignore
            )


    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode