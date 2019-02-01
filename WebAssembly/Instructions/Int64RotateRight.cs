using System.Reflection.Emit;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// Sign-agnostic rotate right.
    /// </summary>
    public class Int64RotateRight : SimpleInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int64RotateRight"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int64RotateRight;

	/// <summary>
	/// Creates a new  <see cref="Int64RotateRight"/> instance.
	/// </summary>
	public Int64RotateRight()
	{
	}

	internal sealed override void Compile(CompilationContext context)
	{
	    var stack = context.Stack;
	    if (stack.Count < 2)
		throw new StackTooSmallException(OpCode.Int64RotateRight, 2, stack.Count);

	    var typeB = stack.Pop();
	    var typeA = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (typeA != ValueType.Int64)
		throw new StackTypeInvalidException(OpCode.Int64RotateRight, ValueType.Int64, typeA);

	    if (typeA != typeB)
		throw new StackParameterMismatchException(OpCode.Int64RotateRight, typeA, typeB);

	    context.Emit(OpCodes.Call, context[HelperMethod.Int64RotateRight, (helper, c) =>
	    {
		var builder = c.ExportsBuilder.DefineMethod(
				    "☣ Int64RotateRight",
				    CompilationContext.HelperMethodAttributes,
				    typeof(ulong),
				    new[]
				    {
							typeof(ulong),
							typeof(long),
			    }
				    );

		var il = builder.GetILGenerator();
		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Ldarg_1);
		il.Emit(OpCodes.Conv_I4);
		il.Emit(OpCodes.Ldc_I4_S, 63);
		il.Emit(OpCodes.And);
		il.Emit(OpCodes.Shr_Un);

		il.Emit(OpCodes.Ldarg_0);
		il.Emit(OpCodes.Ldc_I4_S, 64);
		il.Emit(OpCodes.Ldarg_1);
		il.Emit(OpCodes.Conv_I4);
		il.Emit(OpCodes.Sub);
		il.Emit(OpCodes.Ldc_I4_S, 63);
		il.Emit(OpCodes.And);
		il.Emit(OpCodes.Shl);
		il.Emit(OpCodes.Or);

		il.Emit(OpCodes.Ret);
		return builder;
	    }
	    ]);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    var stack = context.Stack;
	    if (stack.Count < 2)
		throw new StackTooSmallException(OpCode.Int64RotateRight, 2, stack.Count);

	    var typeB = stack.Pop();
	    var typeA = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (typeA != ValueType.Int64)
		throw new StackTypeInvalidException(OpCode.Int64RotateRight, ValueType.Int64, typeA);

	    if (typeA != typeB)
		throw new StackParameterMismatchException(OpCode.Int64RotateRight, typeA, typeB);

	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, context[HelperMethod.Int64RotateRight, (helper, c) =>
	    {
		var builder = c.ExportsBuilder.DefineMethod(
				    "☣ Int64RotateRight",
				    IKVMCompilationContext.HelperMethodAttributes,
				    universe.Import(typeof(ulong)),
				    new[]
				    {
					universe.Import(typeof(ulong)),
					universe.Import(typeof(long)),
				    }
				    );

		var il = builder.GetILGenerator();
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_1);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Conv_I4);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_S, 63);
		il.Emit(IKVM.Reflection.Emit.OpCodes.And);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Shr_Un);

		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_S, 64);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_1);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Conv_I4);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Sub);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldc_I4_S, 63);
		il.Emit(IKVM.Reflection.Emit.OpCodes.And);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Shl);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Or);

		il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);
		return builder;
	    }
	    ]);
	}
    }
}