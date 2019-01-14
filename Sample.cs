﻿using WebAssembly; //Acquire from https://www.nuget.org/packages/WebAssembly
using WebAssembly.Instructions;
using System;

//We need this later to call the code we're generating.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public abstract class Sample
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{
    //Sometimes you can use C# dynamic instead of building an abstract class like this.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public abstract int Demo(int value);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

static class Program
{
    static void Main()
    {
        //Module can be used to create, read, modify, and write WebAssembly files.
        var module = new Module(); //In this case, we're creating a new one.

        //Types are function signatures: the list of parameters and returns.
        module.Types.Add(new WebAssembly.Type //The first added type gets index 0.
        {
            Parameters = new[]
            {
                WebAssembly.ValueType.Int32, //This sample takes a single Int32 as input.
				//Complex types can be passed by sending them in pieces.
			},
            Returns = new[]
            {
				//Multiple returns are supported by the binary format.
				//Standard currently allows a count of 0 or 1, though.
				WebAssembly.ValueType.Int32,
            },
        });
        //Types can be re-used for multiple functions to reduce WASM size.

        //The function list associates a function index to a type index.
        module.Functions.Add(new Function //The first added function gets index 0.
        {
            Type = 0, //The index for the "type" value added above.
        });

        //Code must be passed in the exact same order as the Functions above.
        module.Codes.Add(new FunctionBody
        {
            Code = new Instruction[]
            {
                new GetLocal(0), //The parameters are the first locals, in order.
				//We defined the first parameter as Int32, so now an Int32 is at the top of the stack.
				new Int32CountOneBits(), //Returns the number of binary bits set to 1.
				//It takes the Int32 from the top of the stack, and pushes the return value.
				//So, in the end, there is still a single Int32 on the stack, but it's now the result of Int32CountOneBits.
				new End(), //All functions must end with "End".
				//The final "End" also delivers the returned value, if the type says that a value is returned.
			},
        });

        //Exports enable features to be accessed by external code.
        //Typically this means JavaScript, but this library adds .NET execution capability, too.
        module.Exports.Add(new Export
        {
            Kind = ExternalKind.Function,
            Index = 0, //This should match the function index from above.
            Name = "Demo", //Anything legal in Unicode is legal in an export name.
        });

        //We now have enough for a usable WASM file, which we could save with module.WriteToBinary().
        //Below, we show how the Compile feature can be used for .NET-based execution.
        //For stream-based compilation, WebAssembly.Compile should be used.
        var instanceCreator = module.Compile<Sample>();

        //Instances should be wrapped in a "using" block for automatic disposal.
        using (var instance = instanceCreator())
        {
            //FYI, instanceCreator can be used multiple times to create independant instances.
            Console.WriteLine(instance.Exports.Demo(0)); //Binary 0, result 0
            Console.WriteLine(instance.Exports.Demo(1)); //Binary 1, result 1,
            Console.WriteLine(instance.Exports.Demo(42));  //Binary 101010, result 3
        } //Automatically release the WebAssembly instance here.
    }
}
