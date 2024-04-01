module Examensarbete.Commands.WalletHandler

open Microsoft.AspNetCore.Http
open Examensarbete.Events.Events
open Examensarbete.Commands.WalletCommand
open Examensarbete.Storage.Repository


let private executeCreateWallet (create: CreateWallet) : IResult =
    let createEvent =
        [ WalletEvent.Created { id = create.id; owner = create.owner } ]
        |> tryAppendEvents create.id

    match createEvent with
    | Ok -> Results.Ok()
    | Error e -> Results.BadRequest e

let private executeRemoveWallet (remove: RemoveWallet) =
    let removeEvent =
        [ WalletEvent.Removed { id = remove.id } ]
        |> tryAppendEvents remove.id

    match removeEvent with
    | Ok -> Results.Ok()
    | Error e -> Results.BadRequest e

let private executeDepositWallet (deposit: DepositWallet) =
    let depositEvent =
        [ WalletEvent.Deposited
              { id = deposit.id
                owner = deposit.owner
                amount = deposit.amount } ]
        |> tryAppendEvents deposit.id

    match depositEvent with
    | Ok -> Results.Ok()
    | Error e -> Results.BadRequest e

let private executeWithdrawWallet (withdraw: WithdrawWallet) =
    let withdrawEvent =
        [ WalletEvent.Withdrawn
              { id = withdraw.id
                owner = withdraw.owner
                amount = withdraw.amount } ]
        |> tryAppendEvents withdraw.id

    match withdrawEvent with
    | Ok -> Results.Ok()
    | Error e -> Results.BadRequest e

let handle (command: Command) =
    match command with
    | CreateWallet create -> executeCreateWallet create
    | RemoveWallet remove -> executeRemoveWallet remove
    | DepositWallet deposit -> executeDepositWallet deposit
    | WithdrawWallet withdraw -> executeWithdrawWallet withdraw
