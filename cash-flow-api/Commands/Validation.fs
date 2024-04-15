module Examensarbete.Commands.Validation

open System
open Examensarbete.Core.Ids
open FsToolkit.ErrorHandling
open Examensarbete.Storage.Repository
open Examensarbete.Projection.WalletProjection
open Examensarbete.Projection.WalletBalanceProjector
open WalletCommand

let private getProjection (walletId: WalletId) : Wallet option =
    try
        let walletEventStream = getEventStream walletId

        Some(buildState walletEventStream)

    with _ ->
        None

let private validateCreateWallet (create: CreateWallet) : Result<Validated<CreateWallet>, string> =
    match create.owner with
    | ownerId when ownerId = UserId(Guid.Empty) -> Error "Owner id cannot be empty"
    | _ -> Ok { command = create }
