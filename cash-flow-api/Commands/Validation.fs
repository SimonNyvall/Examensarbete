module Examensarbete.Commands.Validation

open Examensarbete.Core.Ids
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


let validateCreateWallet (create: CreateWallet) =
    let walletProjection = getProjection create.id

    match walletProjection with
    | Some wallet when wallet.owner = create.owner -> Error "Wallet already exists"
    | _ -> Ok


let validateDepositWallet (deposit: DepositWallet) =
    let walletProjection = getProjection deposit.id

    match walletProjection with
    | Some wallet when wallet.owner = deposit.owner -> Ok
    | _ -> Error "Wallet does not exist"


let validateWithdrawWallet (withdraw: WithdrawWallet) =
    let walletProjection = getProjection withdraw.id

    match walletProjection with
    | Some wallet when wallet.owner = withdraw.owner -> Ok
    | _ -> Error "Wallet does not exist"


let validateRemoveWallet (remove: RemoveWallet) =
    let walletProjection = getProjection remove.id

    match walletProjection with
    | Some wallet when wallet.owner = remove.owner -> Ok
    | _ -> Error "Wallet does not exist"


let validateCommand (command: Command) : Validated<Command> =
    match command with
    | CreateWallet create ->
        validateCreateWallet create
        |> function
            | Ok -> { command = command }
            | Error e -> failwith e
    | DepositWallet deposit ->
        validateDepositWallet deposit
        |> function
            | Ok -> { command = command }
            | Error e -> failwith e
    | WithdrawWallet withdraw ->
        validateWithdrawWallet withdraw
        |> function
            | Ok -> { command = command }
            | Error e -> failwith e
    | RemoveWallet remove ->
        validateRemoveWallet remove
        |> function
            | Ok -> { command = command }
            | Error e -> failwith e
