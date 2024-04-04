module Examensarbete.Extensions.MinimalApi

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

let mapGet (pattern: string) (app: WebApplication) func =
    app.MapGet(pattern, Func<HttpContext, IResult> func) |> ignore

    app

let mapPost (pattern: string) (app: WebApplication) func =
    app.MapPost(pattern, Func<HttpContext, IResult> func) |> ignore

    app

let mapPut (pattern: string) (app: WebApplication) func =
    app.MapPut(pattern, Func<HttpContext, Guid, Guid, IResult> func) |> ignore

    app

let mapDelete (pattern: string) (app: WebApplication) func =
    app.MapDelete(pattern, Func<HttpContext, Guid, IResult> func) |> ignore

    app
