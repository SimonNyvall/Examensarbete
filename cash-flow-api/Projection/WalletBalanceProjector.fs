module Examensarbete.Projection.WalletBalanceProjector

open Examensarbete.Events.Events
open Examensarbete.Projection.WalletProjection

let applyEvent (state: Wallet) (event: WalletEvent) =
    match event with
    | WalletEvent.Created e ->
        { state with
            id = e.id
            owner = e.owner }
    | WalletEvent.Removed _ -> Wallet.Empty
    | WalletEvent.Deposited e ->
        { state with
            balance = state.balance + e.amount }
    | WalletEvent.Withdrawn e ->
        { state with
            balance = state.balance - e.amount }

let buildState events =
    List.fold applyEvent Wallet.Empty events
