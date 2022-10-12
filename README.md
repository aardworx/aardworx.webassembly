# Aardworx.WebAssembly

Aardworx.WebAssembly provides a WebGL backend for [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering) and tools for working with [Aardvark.Dom](https://github.com/aardvark-community/aardvark.dom) in a WebAssembly environment.

This library is work-in-progress and several rendering functionalities have not been implemented yet.

## Building

### Requirements
* dotnet sdk `6.0.401`
* `wasm-tools` workload (can be installed via `dotnet workload install wasm-tools`)

### Steps
* run `build.cmd` or `build.sh`
* whenever you change `Model.fs` in `src/Examples/Dom/` run `adapt` to update the generated file next to it.