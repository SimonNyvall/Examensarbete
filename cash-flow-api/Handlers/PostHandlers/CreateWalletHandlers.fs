module Examensarbete.Handlers.PostHandlers.CreateWalletHandler

open System
open Vortex.Api
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler
open Examensarbete.Commands.Validation
open Microsoft.AspNetCore.Http

let handler: RequestResponse =
    fun _ ->
        let walletId = WalletId(Guid.NewGuid())
        let userId = UserId(Guid.NewGuid())

        CreateWallet { id = walletId; owner = userId }
        |> validateCommand
        |> handle
        |> function
            | Ok -> Results.Ok("Wallet created")
            | Error message -> Results.BadRequest message
