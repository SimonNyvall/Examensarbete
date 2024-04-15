module Examensarbete.Storage.Repository

open Examensarbete.Core.Ids
open Examensarbete.Events.Events

// TODO save this to a sql database
// maybe use this a cache later on


let mutable private storedEvents: Map<WalletId, WalletEvent list> = Map.empty

let getEventStream (id: WalletId) =
    printfn "Getting event stream for wallet %A" id

    match storedEvents.TryFind id with
    | Some events -> events
    | None -> failwith $"Stream with id {id} not wrong or empty"

let tryAppendEvents (id: WalletId) (events: WalletEvent list) =
    printfn "Appending event %A to repository" events

    let appendEvents =
        events
        |> List.iter (fun event ->
            match event with
            | WalletEvent.Created e -> storedEvents <- storedEvents.Add(e.id, [ event ])
            | _ ->
                let events = storedEvents.[id]
                storedEvents <- storedEvents.Add(id, events @ [ event ]))

    try
        appendEvents
        Ok
    with _ ->
        Error "Failed to append events to repository"
