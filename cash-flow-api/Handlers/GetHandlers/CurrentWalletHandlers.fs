module Examensarbete.Handlers.GetHandlers.CurrentWalletHandler

open Vortex.Api
open Vortex.Http
open Examensarbete.Core.Ids
open Examensarbete.Output.Wallet
open Examensarbete.Storage.Repository
open Examensarbete.Projection.WalletBalanceProjector
open Microsoft.AspNetCore.Http

let handler: RequestResponse =
    fun context ->
        let walletId = WalletId(context.GetQueryGuid "walletid")

        let walletEventStream = getEventStream walletId

        let wallet = buildState walletEventStream

        Results.Ok(toJson wallet)
