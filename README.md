# Aardworx.WebAssembly

[![Windows](https://github.com/aardworx/aardworx.webassembly/actions/workflows/windows.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/windows.yml)
[![Mac](https://github.com/aardworx/aardworx.webassembly/actions/workflows/mac.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/mac.yml)
[![Linux](https://github.com/aardworx/aardworx.webassembly/actions/workflows/linux.yml/badge.svg)](https://github.com/aardworx/aardworx.webassembly/actions/workflows/linux.yml)
[![NuGet](https://badgen.net/nuget/v/Aardworx.Rendering.WebGL)](https://www.nuget.org/packages/Aardworx.Rendering.WebGL/)

Aardworx.WebAssembly provides a WebGL backend for [Aardvark.Rendering](https://github.com/aardvark-platform/aardvark.rendering) and tools for working with [Aardvark.Dom](https://github.com/aardvark-community/aardvark.dom) in a WebAssembly environment.

This library is work-in-progress and several rendering functionalities have not been implemented yet.

### Check out the [Live Demo](https://demo.aardvarkians.com/wasm/RenderingOnly/)
[![image](https://user-images.githubusercontent.com/6370801/195428227-12172fa3-ea00-4945-ae60-79984112eb05.png)](https://demo.aardvarkians.com/wasm/RenderingOnly/)


## Building

### Requirements
* dotnet sdk `8.0.401`
* `wasm-tools` workload (can be installed via `dotnet workload install wasm-tools`)

### Steps
* run `build.cmd` or `build.sh`
* whenever you change `Model.fs` in `src/Examples/Dom/` run `adapt` to update the generated file next to it.
