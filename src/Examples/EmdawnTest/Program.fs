namespace EmdawnTest

open System
open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Aardworx.WebAssembly

module Program =

    [<EntryPoint>]
    let main args =
        printfn "EmdawnTest - WebGPU Integration Test"
        printfn "======================================"

        // Test WebGPU integration
        let success = WebGPU.testWebGPU()

        if success then
            printfn "\n✓ WebGPU integration test PASSED"
        else
            printfn "\n✗ WebGPU integration test FAILED"

        // Create minimal Blazor host
        let builder = WebAssemblyHostBuilder.CreateDefault(args)
        let host = builder.Build()

        printfn "\nBlazor host created. Check browser console for WebGPU stub logs."

        host.RunAsync() |> ignore
        0
