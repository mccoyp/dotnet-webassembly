﻿using System.Reflection.Emit;
using static System.Diagnostics.Debug;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// Sign-agnostic count number of one bits.
    /// </summary>
    public class Int64CountOneBits : SimpleInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int64CountOneBits"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int64CountOneBits;

	/// <summary>
	/// Creates a new  <see cref="Int64CountOneBits"/> instance.
	/// </summary>
	public Int64CountOneBits()
	{
	}

	internal sealed override void Compile(CompilationContext context)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Int64CountOneBits, 1, stack.Count);

	    var type = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (type != ValueType.Int64)
		throw new StackTypeInvalidException(OpCode.Int64CountOneBits, ValueType.Int64, type);

	    context.Emit(OpCodes.Call, context[HelperMethod.Int64CountOneBits, CreateHelper]);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Int64CountOneBits, 1, stack.Count);

	    var type = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (type != ValueType.Int64)
		throw new StackTypeInvalidException(OpCode.Int64CountOneBits, ValueType.Int64, type);

	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, IKVMCreateHelper(HelperMethod.Int64CountOneBits, context, universe));
	}

	internal static MethodBuilder CreateHelper(HelperMethod helper, CompilationContext context)
	{
	    Assert(context != null);

	    var result = context.ExportsBuilder.DefineMethod(
		    "☣ Int64CountOneBits",
		    CompilationContext.HelperMethodAttributes,
		    typeof(ulong),
		    new[] { typeof(ulong)
		    });

	    //All modern CPUs have a fast instruction specifically for this process, but there's no way to use it from .NET.
	    //This algorithm is from https://stackoverflow.com/questions/2709430/count-number-of-bits-in-a-64-bit-long-big-integer
	    var il = result.GetILGenerator();

	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldc_I4_1);
	    il.Emit(OpCodes.Shr_Un);
	    il.Emit(OpCodes.Ldc_I8, 0x5555555555555555);
	    il.Emit(OpCodes.And);
	    il.Emit(OpCodes.Sub);
	    il.Emit(OpCodes.Starg_S, 0);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldc_I8, 0x3333333333333333);
	    il.Emit(OpCodes.And);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldc_I4_2);
	    il.Emit(OpCodes.Shr_Un);
	    il.Emit(OpCodes.Ldc_I8, 0x3333333333333333);
	    il.Emit(OpCodes.And);
	    il.Emit(OpCodes.Add);
	    il.Emit(OpCodes.Starg_S, 0);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ldc_I4_4);
	    il.Emit(OpCodes.Shr_Un);
	    il.Emit(OpCodes.Add);
	    il.Emit(OpCodes.Ldc_I8, 0x0f0f0f0f0f0f0f0f);
	    il.Emit(OpCodes.And);
	    il.Emit(OpCodes.Ldc_I8, 0x0101010101010101);
	    il.Emit(OpCodes.Mul);
	    il.Emit(OpCodes.Ldc_I4_S, 56);
	    il.Emit(OpCodes.Shr_Un);
	    il.Emit(OpCodes.Ret);

	    return result;
	}

	internal static IKVM.Reflection.Emit.MethodBuilder IKVMCreateHelper(HelperMethod helper, IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    Assert(context != null);

	    var result = context.ExportsBuilder.DefineMethod(
		    "☣ Int64CountOneBits",
		    IKVMCompilationContext.HelperMethodAttributes,
		    universe.Import(typeof(ulong)),
		    new[] { universe.Import(typeof(ulong))
		    });

	    //All modern CPUs have a fast instruction specifically for this process, but there's no way to use it from .NET.
	    //This algorithm is from https://stackoverflow.com/questions/2709430/count-number-of-bits-in-a-64-bit-long-big-integer
	    var il = result.GetILGenerator();

	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_1);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Shr_Un);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I8, 0x5555555555555555);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.And);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Sub);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Starg_S, 0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I8, 0x3333333333333333);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.And);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_2);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Shr_Un);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I8, 0x3333333333333333);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.And);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Add);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Starg_S, 0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_4);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Shr_Un);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Add);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I8, 0x0f0f0f0f0f0f0f0f);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.And);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I8, 0x0101010101010101);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Mul);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_S, 56);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Shr_Un);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);

	    return result;
	}
    }
}