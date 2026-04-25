namespace Aardworx.Rendering.WebGL.Tests

open System
open System.Diagnostics
open Aardworx.WebAssembly
open Aardworx.Rendering.WebGL
open Aardvark.Rendering

type TestResult =
    {
        Name : string
        Passed : bool
        Skipped : bool
        DurationMs : float
        Error : string option
        /// Optional (name, base64DataUrl) pairs surfaced to the Playwright report.
        Attachments : (string * string) list
    }

type TestCtx =
    {
        Device : Device
        Runtime : IRuntime
    }

exception PendingTestException of string

module TestRunner =

    let pending (reason : string) : 'a =
        raise (PendingTestException reason)

    let private currentAttachments = ResizeArray<string * string>()

    /// Attach a (name, base64-data-URL) pair to the currently-running test.
    /// Surfaced via window.__aardworx_test_results__ for Playwright pickup.
    let attach (name : string) (dataUrl : string) =
        currentAttachments.Add(name, dataUrl)

    let run (tests : (string * (TestCtx -> unit)) list) (ctx : TestCtx) : TestResult list =
        tests
        |> List.map (fun (name, body) ->
            currentAttachments.Clear()
            let sw = Stopwatch.StartNew()
            let snapshotAttachments() = currentAttachments |> List.ofSeq
            try
                try
                    body ctx
                    sw.Stop()
                    { Name = name; Passed = true; Skipped = false; DurationMs = sw.Elapsed.TotalMilliseconds; Error = None; Attachments = snapshotAttachments() }
                with
                | PendingTestException reason ->
                    sw.Stop()
                    { Name = name; Passed = false; Skipped = true; DurationMs = sw.Elapsed.TotalMilliseconds; Error = Some reason; Attachments = snapshotAttachments() }
                | e ->
                    sw.Stop()
                    let msg =
                        let inner = if isNull e.InnerException then "" else "\n--- inner ---\n" + e.InnerException.ToString()
                        e.GetType().Name + ": " + e.Message + "\n" + (e.StackTrace |> Option.ofObj |> Option.defaultValue "") + inner
                    { Name = name; Passed = false; Skipped = false; DurationMs = sw.Elapsed.TotalMilliseconds; Error = Some msg; Attachments = snapshotAttachments() }
            with e ->
                // safety net for anything thrown outside of the try above
                { Name = name; Passed = false; Skipped = false; DurationMs = 0.0; Error = Some (e.ToString()); Attachments = snapshotAttachments() }
        )

    let renderResults (target : HTMLElement) (results : TestResult list) =
        let doc = Window.Document
        target.InnerHTML <- ""

        let total = List.length results
        let passed = results |> List.filter (fun r -> r.Passed) |> List.length
        let skipped = results |> List.filter (fun r -> r.Skipped) |> List.length
        let failed = total - passed - skipped

        let summary = doc.CreateElement "div"
        summary.InnerHTML <-
            sprintf "<strong>%d / %d passed</strong> &nbsp; <span class='fail'>%d failed</span> &nbsp; <span class='skip'>%d skipped</span>"
                passed total failed skipped
        target.AppendChild summary

        let ul = doc.CreateElement "ul"
        ul.Id <- "test-list"

        for r in results do
            let li = doc.CreateElement "li"
            let cls, mark =
                if r.Skipped then "skip", "~"
                elif r.Passed then "pass", "✓"
                else "fail", "✗"
            li.ClassName <- cls

            let head = doc.CreateElement "span"
            head.InnerHTML <- sprintf "<span class='%s'>%s</span> %s <small>(%.1fms)</small>" cls mark r.Name r.DurationMs
            li.AppendChild head

            match r.Error with
            | Some err ->
                let pre = doc.CreateElement "pre"
                pre.ClassName <- "error"
                pre.InnerText <- err
                li.AppendChild pre
            | None -> ()

            ul.AppendChild li

        target.AppendChild ul

    let private escapeJs (s : string) =
        if isNull s then "null"
        else
            let b = System.Text.StringBuilder()
            b.Append('"') |> ignore
            for c in s do
                match c with
                | '\\' -> b.Append "\\\\" |> ignore
                | '"' -> b.Append "\\\"" |> ignore
                | '\n' -> b.Append "\\n" |> ignore
                | '\r' -> b.Append "\\r" |> ignore
                | '\t' -> b.Append "\\t" |> ignore
                | c when int c < 32 -> b.AppendFormat("\\u{0:x4}", int c) |> ignore
                | c -> b.Append c |> ignore
            b.Append('"') |> ignore
            b.ToString()

    let private toJson (results : TestResult list) =
        let sb = System.Text.StringBuilder()
        sb.Append '[' |> ignore
        let mutable first = true
        for r in results do
            if not first then sb.Append ',' |> ignore
            first <- false
            sb.Append '{' |> ignore
            sb.AppendFormat("\"name\":{0},", escapeJs r.Name) |> ignore
            sb.AppendFormat("\"passed\":{0},", (if r.Passed then "true" else "false")) |> ignore
            sb.AppendFormat("\"skipped\":{0},", (if r.Skipped then "true" else "false")) |> ignore
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "\"durationMs\":{0:0.###},", r.DurationMs) |> ignore
            let errStr =
                match r.Error with
                | Some e -> escapeJs e
                | None -> "null"
            sb.AppendFormat("\"error\":{0},", errStr) |> ignore
            sb.Append "\"attachments\":[" |> ignore
            let mutable first2 = true
            for (an, av) in r.Attachments do
                if not first2 then sb.Append ',' |> ignore
                first2 <- false
                sb.AppendFormat("{{\"name\":{0},\"dataUrl\":{1}}}", escapeJs an, escapeJs av) |> ignore
            sb.Append ']' |> ignore
            sb.Append '}' |> ignore
        sb.Append ']' |> ignore
        sb.ToString()

    let exportToWindow (results : TestResult list) =
        let total = List.length results
        let passed = results |> List.filter (fun r -> r.Passed) |> List.length
        let skipped = results |> List.filter (fun r -> r.Skipped) |> List.length
        let failed = total - passed - skipped
        let json = toJson results
        let summaryJson =
            sprintf "{ \"total\": %d, \"passed\": %d, \"failed\": %d, \"skipped\": %d, \"done\": true }"
                total passed failed skipped
        let code =
            sprintf "(function(){ window.__aardworx_test_results__ = %s; window.__aardworx_test_summary__ = %s; })();"
                json summaryJson
        JsObj.InstallScript code

    let exportFatal (msg : string) =
        let code =
            sprintf "(function(){ window.__aardworx_test_summary__ = { done: true, fatal: %s }; window.__aardworx_test_results__ = []; })();"
                (escapeJs msg)
        JsObj.InstallScript code
