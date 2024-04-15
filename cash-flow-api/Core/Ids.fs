module Examensarbete.Core.Ids

open System

type UserId =
    | UserId of Guid

    member this.value =
        let (UserId value) = this
        value

type WalletId =
    | WalletId of Guid

    member this.value =
        let (WalletId value) = this
        value

type Result =
    | Ok
    | Error of string
