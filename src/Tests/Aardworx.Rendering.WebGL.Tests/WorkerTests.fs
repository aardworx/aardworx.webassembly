namespace Aardworx.Rendering.WebGL.Tests

open System
open System.Threading.Tasks
open Aardworx.WebAssembly

// ----------------------------------------------------------------------
// Worker classes
//
// Workers have to be public, top-level types with a parameterless ctor
// (Aardworx.WebAssembly.AbstractWorker.RunServer uses Type.GetType +
// reflection to construct them on the worker side). Each test gets its
// own dedicated worker class — that's simpler than carrying a "mode"
// message and dispatching once the worker has booted.
// ----------------------------------------------------------------------

/// Worker that sends a single text message ("ping") once Run() starts,
/// then sits idle so the host can verify exactly one message arrives.
type TextPingWorker() =
    inherit AbstractWorker()
    override _.Run(ctx) =
        task {
            ctx.Send (WorkerMessage.String "ping")
            // Keep the worker alive — the host pulls one message and disposes.
            do! Task.Delay(System.Threading.Timeout.Infinite)
        }

/// Worker that sends a single 4-byte binary payload, then idles.
type BinaryPingWorker() =
    inherit AbstractWorker()
    override _.Run(ctx) =
        task {
            ctx.Send (WorkerMessage.Binary [| 1uy; 2uy; 3uy; 4uy |])
            do! Task.Delay(System.Threading.Timeout.Infinite)
        }

/// Worker that echoes each incoming message straight back to the host.
type EchoWorker() =
    inherit AbstractWorker()
    override _.Run(ctx) =
        task {
            while true do
                let! msg = ctx.Receive()
                ctx.Send msg
        }

/// Worker that does nothing but stay alive. Used by the dispose test —
/// after Terminate() the host should observe no further messages.
type IdleWorker() =
    inherit AbstractWorker()
    override _.Run(_ctx) =
        task {
            do! Task.Delay(System.Threading.Timeout.Infinite)
        }


module WorkerTests =

    let private assertTrue (msg : string) (cond : bool) =
        if not cond then failwithf "assertion failed: %s" msg

    /// Await `t` but bail out with a clear error if it doesn't finish in `ms`.
    let private withTimeout (ms : int) (label : string) (t : Task<'a>) : Task<'a> =
        task {
            let delay = Task.Delay ms
            let! winner = Task.WhenAny(t :> Task, delay)
            if obj.ReferenceEquals(winner, delay) then
                return failwithf "%s timed out after %dms" label ms
            else
                return! t
        }

    // ------------------------------------------------------------------

    let private workerTextMessage (_ctx : TestCtx) =
        task {
            let! w = Worker.start<TextPingWorker>() |> withTimeout 10_000 "worker startup (text)"
            try
                let! msg = w.Receive() |> withTimeout 10_000 "worker text receive"
                match msg with
                | WorkerMessage.String s ->
                    assertTrue (sprintf "expected 'ping', got %A" s) (s = "ping")
                | WorkerMessage.Binary _ ->
                    failwith "expected text message, got binary"
            finally
                try w.Terminate() with _ -> ()
        }

    let private workerBinaryMessage (_ctx : TestCtx) =
        task {
            let! w = Worker.start<BinaryPingWorker>() |> withTimeout 10_000 "worker startup (binary)"
            try
                let! msg = w.Receive() |> withTimeout 10_000 "worker binary receive"
                match msg with
                | WorkerMessage.Binary arr ->
                    let expected = [| 1uy; 2uy; 3uy; 4uy |]
                    assertTrue
                        (sprintf "binary mismatch: expected %A, got %A" expected arr)
                        (arr.Length = expected.Length && Array.forall2 (=) arr expected)
                | WorkerMessage.String s ->
                    failwithf "expected binary message, got string %A" s
            finally
                try w.Terminate() with _ -> ()
        }

    let private workerEcho (_ctx : TestCtx) =
        task {
            let! w = Worker.start<EchoWorker>() |> withTimeout 10_000 "echo worker startup"
            try
                w.Send (WorkerMessage.String "hello")
                let! reply1 = w.Receive() |> withTimeout 10_000 "echo string receive"
                match reply1 with
                | WorkerMessage.String s ->
                    assertTrue (sprintf "echo string mismatch: %A" s) (s = "hello")
                | _ -> failwith "expected string echo"

                let payload = [| 9uy; 8uy; 7uy; 6uy; 5uy |]
                w.Send (WorkerMessage.Binary payload)
                let! reply2 = w.Receive() |> withTimeout 10_000 "echo binary receive"
                match reply2 with
                | WorkerMessage.Binary arr ->
                    assertTrue
                        (sprintf "echo binary mismatch: got %A" arr)
                        (arr.Length = payload.Length && Array.forall2 (=) arr payload)
                | _ -> failwith "expected binary echo"
            finally
                try w.Terminate() with _ -> ()
        }

    let private workerDispose (_ctx : TestCtx) =
        task {
            let! w = Worker.start<IdleWorker>() |> withTimeout 10_000 "idle worker startup"
            // Terminate the worker, then prove no further message is delivered.
            // Two acceptable outcomes:
            //   * Receive throws OperationCanceledException (queue.Completed())
            //   * Receive stays pending past the delay
            // A successfully-returning Receive with a message would be the bug.
            w.Terminate()
            let receive = w.Receive()
            let delay = Task.Delay 500
            let! winner = Task.WhenAny(receive :> Task, delay)
            if not (obj.ReferenceEquals(winner, receive :> Task)) then
                () // Receive still pending → fine, the queue is empty.
            else
                try
                    let! msg = receive
                    failwithf "received unexpected message after dispose: %A" msg
                with
                | :? OperationCanceledException -> ()  // queue completed by Terminate — expected
        }

    // ------------------------------------------------------------------

    let mkAll () : (string * (TestCtx -> Task<unit>)) list =
        [
            "worker text message",   workerTextMessage
            "worker binary message", workerBinaryMessage
            "worker echo",           workerEcho
            "worker dispose",        workerDispose
        ]
