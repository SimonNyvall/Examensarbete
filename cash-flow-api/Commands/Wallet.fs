module Examensarbete.Commands.WalletCommand

open Examensarbete.Core.Ids

type Money = decimal

type CreateWallet = { id: WalletId; owner: UserId }

type RemoveWallet = { id: WalletId }

type DepositWallet =
    { id: WalletId
      owner: UserId
      amount: Money }

type WithdrawWallet =
    { id: WalletId
      owner: UserId
      amount: Money }

type Validated<'T> = { command: 'T }

type Command =
    | CreateWallet of CreateWallet
    | RemoveWallet of RemoveWallet
    | DepositWallet of DepositWallet
    | WithdrawWallet of WithdrawWallet
