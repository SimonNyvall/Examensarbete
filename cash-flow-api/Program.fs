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
open Examensarbete.Projection.WalletProjection
open Examensarbete.Projection.WalletProjector
open Examensarbete.Storage.Repository
open Examensarbete.Output.Wallet

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let app = builder.Build()

        app.MapGet(
            "/api/wallet/{id:guid}",
            Func<Http.HttpContext, Guid, Http.IResult>(fun _ id ->
                let walletId = WalletId(id)

                let walletEventStream = getEventStream walletId

                let wallet = buildState walletEventStream

                printfn "%A" wallet

                Http.Results.Ok())
        )

        app.MapPost(
            "/api/wallet",
            Func<Http.HttpContext, Http.IResult>(fun _ ->
                let walletId = WalletId(Guid.NewGuid())
                let userId = UserId(Guid.NewGuid())

                let createCommand = CreateWallet { id = walletId; owner = userId }

                handle createCommand |> ignore

                let output =
                    { id = walletId
                      owner = userId
                      balance = 0m }

                printfn "%A" output

                Http.Results.Ok())
        )
        |> ignore

        app.MapPut(
            "/api/wallet/{id:guid}/deposit/{userId:guid}",
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

        app.MapPut(
            "/api/wallet/{id:guid}/withdraw/{userId:guid}",
            Func<Http.HttpContext, Guid, Guid, Http.IResult>(fun context id userId ->
                let amount = Decimal.Parse(context.Request.Query.["amount"].ToString())

                let withdrawCommand =
                    WithdrawWallet
                        { id = WalletId(id)
                          owner = UserId(userId)
                          amount = amount }

                handle withdrawCommand |> ignore

                Http.Results.Ok())
        )


        app.Run()

        exitCode
