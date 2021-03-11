namespace FSharpRestApi

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

module Program =
    let exitCode = 0

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
                            endpoints.MapGet("/", fun req -> 
                                //let cn : ComplexNumber= {real = 1; imaginary = 1}
                                //req.Response.WriteAsJsonAsync(cn)
                            ) |> ignore
                        ) |> ignore
                ) |> ignore
            )


    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode