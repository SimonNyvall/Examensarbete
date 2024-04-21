module Examensarbete.Handlers.PutHandlers.DepositWalletHandler

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

        DepositWallet
            { id = walletId
              amount = amount
              owner = userId }
        |> validateCommand
        |> handle
        |> function
            | Ok -> Results.Ok("Deposited")
            | Error message -> Results.BadRequest message
