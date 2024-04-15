module Examensarbete.Projection.WalletProjection

open System
open Examensarbete.Core.Ids

type Wallet =
    { id: WalletId
      owner: UserId
      balance: decimal }

    static member Empty =
        { id = (WalletId Guid.Empty)
          owner = (UserId Guid.Empty)
          balance = 0m }

type HistoricWallet =
    { id: WalletId
      owner: UserId
      withdrawl: decimal
      deposit: decimal }

    static member Empty =
        { id = (WalletId Guid.Empty)
          owner = (UserId Guid.Empty)
          withdrawl = 0m
          deposit = 0m }
