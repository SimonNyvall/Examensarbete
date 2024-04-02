namespace cash_flow_api

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Examensarbete.Endpoints.WalletEndpoints

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let app = builder.Build()

        mapWalletEndpoints app

        app.Run()

        exitCode
