module Examensarbete.Commands.WalletHandler

open Examensarbete.Events.Events
open Examensarbete.Core.Ids
open Examensarbete.Commands.WalletCommand
open Examensarbete.Storage.Repository


let private executeCreateWallet (create: Validated<CreateWallet>) : Result =
    [ WalletEvent.Created
          { id = create.command.id
            owner = create.command.owner } ]
    |> tryAppendEvents create.command.id


let private executeRemoveWallet (remove: Validated<RemoveWallet>) =
    [ WalletEvent.Removed { id = remove.command.id } ]
    |> tryAppendEvents remove.command.id


let private executeDepositWallet (deposit: Validated<DepositWallet>) =
    [ WalletEvent.Deposited
          { id = deposit.command.id
            owner = deposit.command.owner
            amount = deposit.command.amount } ]
    |> tryAppendEvents deposit.command.id


let private executeWithdrawWallet (withdraw: Validated<WithdrawWallet>) =
    [ WalletEvent.Withdrawn
          { id = withdraw.command.id
            owner = withdraw.command.owner
            amount = withdraw.command.amount } ]
    |> tryAppendEvents withdraw.command.id


let handle (validated: Validated<Command>) =
    match validated.command with
    | CreateWallet create -> executeCreateWallet { command = create }
    | RemoveWallet remove -> executeRemoveWallet { command = remove }
    | DepositWallet deposit -> executeDepositWallet { command = deposit }
    | WithdrawWallet withdraw -> executeWithdrawWallet { command = withdraw }
