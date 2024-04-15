module Examensarbete.Handlers.DeleteHandlers.DeleteWalletHandler

open Vortex.Api
open Vortex.Http
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler
open Microsoft.AspNetCore.Http

let handler: RequestResponse =
    fun context ->
        let walletId = WalletId(context.GetQueryGuid "walletid")

        RemoveWallet { id = walletId }
        |> handle
        |> function
            | Ok -> Results.Ok("Removed wallet")
            | Error message -> Results.BadRequest message
