module Examensarbete.Commands.WalletHandler

open Examensarbete.Events.Events
open Examensarbete.Commands.WalletCommand
open Examensarbete.Storage.Repository

let private executeCreateWallet (create: CreateWallet) : Result =
    [ WalletEvent.Created { id = create.id; owner = create.owner } ]
    |> tryAppendEvents create.id

let private executeRemoveWallet (remove: RemoveWallet) =
    [ WalletEvent.Removed { id = remove.id } ]
    |> tryAppendEvents remove.id

let private executeDepositWallet (deposit: DepositWallet) =
    [ WalletEvent.Deposited
          { id = deposit.id
            owner = deposit.owner
            amount = deposit.amount } ]
    |> tryAppendEvents deposit.id

let private executeWithdrawWallet (withdraw: WithdrawWallet) =
    [ WalletEvent.Withdrawn
          { id = withdraw.id
            owner = withdraw.owner
            amount = withdraw.amount } ]
    |> tryAppendEvents withdraw.id

let handle (command: Command) =
    match command with
    | CreateWallet create -> executeCreateWallet create
    | RemoveWallet remove -> executeRemoveWallet remove
    | DepositWallet deposit -> executeDepositWallet deposit
    | WithdrawWallet withdraw -> executeWithdrawWallet withdraw
