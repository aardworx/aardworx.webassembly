# Aardworx.WebAssembly

[![Windows](https://github.com/aardworx/aardworx.webassembly/actions/workflows/windows.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/windows.yml)
[![Mac](https://github.com/aardworx/aardworx.webassembly/actions/workflows/mac.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/mac.yml)
[![Linux](https://github.com/aardworx/aardworx.webassembly/actions/workflows/linux.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/linux.yml)
[![NuGet](https://badgen.net/nuget/v/Aardworx.Rendering.WebGL)](https://www.nuget.org/packages/Aardworx.Rendering.WebGL/)

Aardworx.WebAssembly provides a WebGL backend for [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering) and tools for working with [Aardvark.Dom](https://github.com/aardvark-community/aardvark.dom) in a WebAssembly environment.

This library is work-in-progress and several rendering functionalities have not been implemented yet.

### Check out the [Live Demo](https://georg.haaser.net/WASM/simple/)
<img width="820" alt="Screenshot 2024-10-19 at 10 39 11" src="https://github.com/user-attachments/assets/db8370c9-dac0-4e0d-a1b3-d01d70d1d643">



## Building

### Requirements
* dotnet sdk `8.0.401`
* `wasm-tools` workload (can be installed via `dotnet workload install wasm-tools`)

### Steps
* run `build.cmd` or `build.sh`
* whenever you change `Model.fs` in `src/Examples/Dom/` run `adapt` to update the generated file next to it.
