# Ahead-of-Time WebAssembly Compilation
 This document outlines the development approach and usage of the new wasm compiler developed with IKVM.Reflection dependency. This compiler allows AOT wasm compilation and produces a compiled .NET assembly, saved as a DLL.
 
 ## Use of IKVM.Reflection
 The new compiler, contained in WebAssembly/CompileIKVM.cs, is a refactored version of the original JIT compiler from WebAssembly/Compile.cs. Instead of using System.Reflection, a version of IKVM.Reflection derived from Mono's fork ([here](https://github.com/lambdageek/ikvm-fork/tree/netstandard2.0-support)) is used in order to create an assembly that can be saved to disk.
 
 In order to use IKVM.Reflection, all of the reflection used in Compile.cs had to be refactored to support IKVM.Reflection types - additionally, a new CompilationContext had to be created (WebAssembly/IKVMCompilationContext.cs). Refactoring the code to support IKVM.Reflection also involves adding support for a CompileIKVM method in each WebAssembly instruction. This hasn't been completed yet for all instructions, but a number of wasm programs are still able to be compiled and the work required to support all instructions should be very manageable.
