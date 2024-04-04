module Examensarbete.Endpoints.WalletEndpoints

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Examensarbete.Core.Ids
open Examensarbete.Output.Wallet
open Examensarbete.Commands.WalletCommand
open Examensarbete.Commands.WalletHandler
open Examensarbete.Projection.WalletProjector
open Examensarbete.Storage.Repository
open Examensarbete.Extensions.MinimalApi

let private fetchAmount (context: HttpContext) : decimal option =
    let couldParse, amount =
        Decimal.TryParse(context.Request.Query.["amount"].ToString())

    match couldParse with
    | true -> Some amount
    | false -> None

let private execute (answer: string) (command: Command) : IResult =
    handle command
    |> function
        | Ok -> Results.Ok(answer)
        | Error message -> Results.BadRequest message

let mapWalletEndpoints (app: WebApplication) =
    mapGet "/api/wallet" app (fun context ->
        let walletId = WalletId(Guid.Parse(context.Request.Query.["id"].ToString()))

        let walletEventStream = getEventStream walletId

        let wallet = buildState walletEventStream

        printfn "Wallet: %A" wallet

        Results.Ok(toJson wallet))
    |> ignore

    mapPost "/api/wallet" app (fun _ ->
        let walletId = WalletId(Guid.NewGuid())
        let userId = UserId(Guid.NewGuid())

        CreateWallet { id = walletId; owner = userId } |> execute "Wallet created")
    |> ignore

    mapPut "/api/wallet/{id:guid}" app (fun context id userId ->
        let walletId = WalletId id
        let userId = UserId userId

        match fetchAmount context with
        | None -> Results.BadRequest "Missing amount in query string or amount is not a valid decimal"
        | Some amount ->
            (DepositWallet
                { id = walletId
                  owner = userId
                  amount = amount }
             |> execute "Wallet deposited"))
    |> ignore

    mapPut "/api/wallet/{id:guid}/withdraw/{userId:guid}" app (fun context id userId ->
        let walletId = WalletId id
        let userId = UserId userId

        match fetchAmount context with
        | None -> Results.BadRequest "Missing amount in query string or amount is not a valid decimal"
        | Some amount ->
            (WithdrawWallet
                { id = walletId
                  owner = userId
                  amount = amount }
             |> execute "Wallet withdrawn"))
    |> ignore

    mapDelete "/api/wallet/{id:guid}" app (fun _ id ->
        let walletId = WalletId id

        RemoveWallet { id = walletId } |> execute "Wallet removed")
    |> ignore

    app
