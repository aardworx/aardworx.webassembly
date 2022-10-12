namespace Aardvark.Base

open System.Runtime.InteropServices

module Logging =


    [<DllImport("CoreUtilities")>]
    extern void private emBegin(string str)

    [<DllImport("CoreUtilities")>]
    extern void private emEnd()
    
    [<DllImport("CoreUtilities")>]
    extern void private emLog(string str)

    [<DllImport("CoreUtilities")>]
    extern void private emWarn(string str)
    
    [<DllImport("CoreUtilities")>]
    extern void private emError(string str)
    
    [<DllImport("CoreUtilities")>]
    extern void private emDebug(string str)

    let init() =
        let target =
            new TextLogTarget(fun _ typ v msg ->
                match typ with
                | LogType.Debug -> emDebug msg
                | LogType.Info -> emLog msg
                | LogType.Warn -> emWarn msg
                | LogType.Error | LogType.Fatal -> emError msg
                | _ -> ()
            )
        target.LogCompleteLinesOnly <- true
        target.Verbosity <- 10
        Report.RootTarget <- target
