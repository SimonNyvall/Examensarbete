module Examensarbete.Events.Events

open System
open Examensarbete.Core.Ids

type Created = { id: WalletId ; owner: UserId }

type Removed = { id: WalletId }

type Deposited =
    { id: WalletId
      owner: UserId
      amount: decimal }

type Withdrawn =
    { id: WalletId
      owner: UserId
      amount: decimal }

type WalletEvent =
    | Created of Created
    | Removed of Removed
    | Deposited of Deposited
    | Withdrawn of Withdrawn
