module Examensarbete.Output.Wallet

open System

type Wallet =
    { id: Guid
      userId: Guid
      balance: decimal }

    static member Empty =
        { id = Guid.Empty
          userId = Guid.Empty
          balance = 0m }
