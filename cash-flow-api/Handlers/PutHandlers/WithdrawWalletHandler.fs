module Examensarbete.Handlers.PutHandlers.WithdrawWalletHandler

open Vortex.Api
open Vortex.Http
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler
open Examensarbete.Commands.Validation
open Microsoft.AspNetCore.Http

let handler: RequestResponse =
    fun context ->
        let walletId = WalletId(context.GetQueryGuid "walletid")
        let amount = context.GetQueryDecimal "amount"
        let userId = UserId(context.GetQueryGuid "userid")

        WithdrawWallet
            { id = walletId
              amount = amount
              owner = userId }
        |> validateCommand
        |> handle
        |> function
            | Ok -> Results.Ok("Withdrawn")
            | Error message -> Results.BadRequest message
