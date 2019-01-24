using System;
using WasmClass;

namespace WasmCompiler
{
    class Program
    {

	// Current commandline interface:
	//     WasmCompiler.exe <input.wasm> <outputAssemblyName>
	//
	//  New interfaces:
	//     WasmCompiler.exe <input.wasm> <outputAssemblyName> <interface.dll> <className>
	//
	//  in inteface.dll:
	//    public abstract class Sample { int Demo (int); }
	//
	//  run with:   WasmCompiler.exe test.wasm CompiledWebAssembly interface.dll Sample
	//
	// 
	//
	//    "module.CompileIKVM<Sample>(assemblyname)"
	// var wasmModule = typeof(WebAssembly.Module);

	static void Main(string[] args)
	{
	    var filename = args[0];
	    var assemblyname = args[1];
	    var module = WebAssembly.Module.ReadFromBinary(filename);
#if false
	    
	    IKVM.Reflection.Emit.AssemblyBuilder assembly = module.CompileIKVM<Sample>(assemblyname);

#else
	    var interfaceDllName = args[2];
	    var classNameFromDll = args[3];
	    var asm = System.Reflection.Assembly.LoadFrom(interfaceDllName);
	    var t = asm.GetType (classNameFromDll);

	    var wasmModule = typeof(WebAssembly.Module);
	    var genericCompileIKVMMethod = wasmModule.GetMethod("CompileIKVM");
	    var compileIKVMMethod = genericCompileIKVMMethod.MakeGenericMethod(t);
	    var assembly = (IKVM.Reflection.Emit.AssemblyBuilder)compileIKVMMethod.Invoke(module, new object[] { assemblyname, null });

#endif
	    assembly.Save(assemblyname+".dll");
	}
    }
}
