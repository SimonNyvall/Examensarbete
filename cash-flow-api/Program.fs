namespace cash_flow_api

#nowarn "20"

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.MapPost(
            "/api/wallet",
            Func<Http.HttpContext, Http.IResult>(fun context ->
                let createCommand =
                    CreateWallet
                        { id = WalletId(Guid.NewGuid())
                          owner = UserId(Guid.NewGuid()) }

                handle createCommand |> ignore

                Http.Results.Ok())
        )
        |> ignore

        app.MapPut(
            "/api/wallet/{id: Guid}/deposit/{userId: Guid}",
            Func<Http.HttpContext, Guid, Guid, Http.IResult>(fun context id userId ->
                let amount = Decimal.Parse(context.Request.Query.["amount"].ToString())

                let depositCommand =
                    DepositWallet
                        { id = WalletId(id)
                          owner = UserId(userId)
                          amount = amount }

                handle depositCommand |> ignore

                Http.Results.Ok())
        )


        app.Run()

        exitCode
