﻿using System.Reflection.Emit;
using static System.Diagnostics.Debug;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// Sign-agnostic count trailing zero bits.  All zero bits are considered trailing if the value is zero.
    /// </summary>
    public class Int32CountTrailingZeroes : SimpleInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int32CountTrailingZeroes"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int32CountTrailingZeroes;

	/// <summary>
	/// Creates a new  <see cref="Int32CountTrailingZeroes"/> instance.
	/// </summary>
	public Int32CountTrailingZeroes()
	{
	}

	internal sealed override void Compile(CompilationContext context)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Int32CountTrailingZeroes, 1, stack.Count);

	    var type = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Int32CountTrailingZeroes, ValueType.Int32, type);

	    context.Emit(OpCodes.Call, context[HelperMethod.Int32CountTrailingZeroes, (helper, c) =>
	    {
		Assert(c != null);

		var result = context.ExportsBuilder.DefineMethod(
				    "☣ Int32CountTrailingZeroes",
				    CompilationContext.HelperMethodAttributes,
				    typeof(int),
				    new[] { typeof(uint)
			    });

			    //All modern CPUs have a fast instruction specifically for this process, but there's no way to use it from .NET.
			    //Based on the algorithm found here: http://aggregate.org/MAGIC/#Trailing%20Zero%20Count
			    var il = result.GetILGenerator();
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Neg);
		il.Emit(OpCodes.And);
		il.Emit(OpCodes.Ldc_I4_1);
		il.Emit(OpCodes.Sub);
		il.Emit(OpCodes.Call, c[HelperMethod.Int32CountOneBits, Int32CountOneBits.CreateHelper]);
		il.Emit(OpCodes.Ret);

		return result;
	    }
	    ]);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Int32CountTrailingZeroes, 1, stack.Count);

	    var type = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Int32CountTrailingZeroes, ValueType.Int32, type);

	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, context[HelperMethod.Int32CountTrailingZeroes, (helper, c) =>
	    {
		Assert(c != null);

		var result = context.ExportsBuilder.DefineMethod(
				    "☣ Int32CountTrailingZeroes",
				    IKVMCompilationContext.HelperMethodAttributes,
				    universe.Import(typeof(int)),
				    new[] { universe.Import(typeof(uint))
			    });

		//All modern CPUs have a fast instruction specifically for this process, but there's no way to use it from .NET.
		//Based on the algorithm found here: http://aggregate.org/MAGIC/#Trailing%20Zero%20Count
		var il = result.GetILGenerator();
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Neg);
		il.Emit(IKVM.Reflection.Emit.OpCodes.And);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_1);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Sub);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Call, Int32CountOneBits.IKVMCreateHelper(HelperMethod.Int32CountOneBits, context, universe));
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);

		return result;
	    }
	    ]);
	}
    }
}