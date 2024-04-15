module Examensarbete.Output.Wallet

open System
open System.Text.Json
open Examensarbete.Projection

type Wallet =
    { id: Guid
      userId: Guid
      balance: decimal }

    static member Empty =
        { id = Guid.Empty
          userId = Guid.Empty
          balance = 0m }

type HistoricWallet =
    { id: Guid
      userId: Guid
      withdrawl: decimal
      deposit: decimal }

    static member Empty =
        { id = Guid.Empty
          userId = Guid.Empty
          withdrawl = 0m
          deposit = 0m }

let mapWallet (wallet: WalletProjection.Wallet) : Wallet =
    { id = wallet.id.value
      userId = wallet.owner.value
      balance = wallet.balance }

let mapWallet' (wallet: WalletProjection.HistoricWallet) : HistoricWallet =
    { id = wallet.id.value
      userId = wallet.owner.value
      withdrawl = wallet.withdrawl
      deposit = wallet.deposit }

let toJson (wallet: WalletProjection.Wallet) =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(mapWallet wallet, options)

let toJson' (wallet: WalletProjection.HistoricWallet) =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(mapWallet' wallet, options)
