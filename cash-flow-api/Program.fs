namespace cash_flow_api

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Vortex.Api
open Examensarbete.Handlers.GetHandlers
open Examensarbete.Handlers.PostHandlers
open Examensarbete.Handlers.PutHandlers
open Examensarbete.Handlers.DeleteHandlers

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let app = builder.Build()

        app
        |> mapGet "/api/wallet/current" CurrentWalletHandler.handler
        |> mapGet "/api/wallet/historical" HistoricalWalletHandler.handler
        |> mapPost "/api/wallet" CreateWalletHandler.handler
        |> mapPut "/api/wallet/withdraw" WithdrawWalletHandler.handler
        |> mapPut "/api/wallet/deposit" DepositWalletHandler.handler
        |> mapDelete "/api/wallet" DeleteWalletHandler.handler


        app.Run()

        exitCode
