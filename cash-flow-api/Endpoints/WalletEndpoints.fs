module Examensarbete.Endpoints.WalletEndpoints

open System
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler
open Examensarbete.Projection.WalletProjection
open Examensarbete.Projection.WalletProjector
open Examensarbete.Storage.Repository

open Examensarbete.Extensions.MinimalApi

let mapWalletEndpoints (app: WebApplication) =
    mapGet "/api/wallet/{id:guid}" app (fun _ id ->
        let walletId = WalletId id

        let walletEventStream = getEventStream walletId

        let wallet = buildState walletEventStream

        printfn "%A" wallet

        Results.Ok())
    |> ignore

    mapPost "/api"


    app
