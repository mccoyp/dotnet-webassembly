using System;
using WasmClass;

namespace WasmCompiler
{
    class Program
    {
	// Command line interface 1:
	//     WasmCompiler.exe <input.wasm> <outputAssemblyName>
	//
	//  Interface 2:
	//     WasmCompiler.exe <input.wasm> <outputAssemblyName> <interface.dll> <className>
	//
	//  in WasmClass:
	//    public abstract class Sample { ... }
	//
	//  run with:   WasmCompiler.exe test.wasm CompiledWebAssembly \...\WasmClass.dll WasmClass.Sample

	// Command line arguments:
	// WasmCompiler.exe [wasm file location] [assemblyname] [optional: abstract class location] [optional: abstract class name]
	static void Main(string[] args)
	{
	    var filename = args[0];
	    var assemblyname = args[1];
	    var module = WebAssembly.Module.ReadFromBinary(filename);

	    if (args.Length < 4)  // if no abstract class specified, uses WasmClass default
	    {
		IKVM.Reflection.Emit.AssemblyBuilder assembly = module.CompileIKVM<Sample>(assemblyname);
		assembly.Save(assemblyname + ".dll");
	    }
	    else
	    {
		var interfaceDllName = args[2];
		var classNameFromDll = args[3];
		var asm = System.Reflection.Assembly.LoadFrom(interfaceDllName);
		var t = asm.GetType(classNameFromDll);

		var wasmModule = typeof(WebAssembly.Module);
		var genericCompileIKVMMethod = wasmModule.GetMethod("CompileIKVM");
		var compileIKVMMethod = genericCompileIKVMMethod.MakeGenericMethod(t);
		var assembly = (IKVM.Reflection.Emit.AssemblyBuilder)compileIKVMMethod.Invoke(module, new object[] { assemblyname, null });
		assembly.Save(assemblyname + ".dll");
	    }
	}
    }
}
