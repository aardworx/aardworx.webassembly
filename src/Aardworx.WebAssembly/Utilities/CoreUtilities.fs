namespace Aardworx.WebAssembly

open System.Runtime.InteropServices

module CoreUtilities =
    [<DllImport("CoreUtilities")>]
    extern void installScript(string script)
    
    [<DllImport("CoreUtilities")>]
    extern void installStyle(string script)

