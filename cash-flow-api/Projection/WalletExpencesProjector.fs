module Examensarbete.Projection.WalletExpencesProjector

open Examensarbete.Events.Events
open Examensarbete.Projection.WalletProjection

let applyEvent (state: HistoricWallet) (event: WalletEvent) =
    match event with
    | WalletEvent.Created e ->
        { state with
            id = e.id
            owner = e.owner }
    | WalletEvent.Removed _ -> HistoricWallet.Empty
    | WalletEvent.Deposited e ->
        { state with
            deposit = state.deposit + e.amount }
    | WalletEvent.Withdrawn e ->
        { state with
            withdrawl = state.withdrawl + e.amount }

let buildState events =
    List.fold applyEvent HistoricWallet.Empty events
