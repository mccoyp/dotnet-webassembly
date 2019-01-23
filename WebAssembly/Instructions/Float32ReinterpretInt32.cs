using System.Reflection.Emit;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// Reinterpret the bits of a 32-bit integer as a 32-bit float.
    /// </summary>
    public class Float32ReinterpretInt32 : SimpleInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Float32ReinterpretInt32"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Float32ReinterpretInt32;

	/// <summary>
	/// Creates a new  <see cref="Float32ReinterpretInt32"/> instance.
	/// </summary>
	public Float32ReinterpretInt32()
	{
	}

	internal sealed override void Compile(CompilationContext context)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Float32ReinterpretInt32, 1, stack.Count);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Float32ReinterpretInt32, ValueType.Int32, type);

	    stack.Push(ValueType.Float32);

	    context.Emit(OpCodes.Call, context[HelperMethod.Float32ReinterpretInt32, (helper, c) =>
	    {
		var builder = c.ExportsBuilder.DefineMethod(
				    "☣ Float32ReinterpretInt32",
				    CompilationContext.HelperMethodAttributes,
				    typeof(float),
				    new[]
				    {
							typeof(int),
			    }
				    );

		var il = builder.GetILGenerator();
		il.Emit(OpCodes.Ldarga_S, 0);
		il.Emit(OpCodes.Ldind_R4);
		il.Emit(OpCodes.Ret);
		return builder;
	    }
	    ]);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    var stack = context.Stack;
	    if (stack.Count < 1)
		throw new StackTooSmallException(OpCode.Float32ReinterpretInt32, 1, stack.Count);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Float32ReinterpretInt32, ValueType.Int32, type);

	    stack.Push(ValueType.Float32);

	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, context[HelperMethod.Float32ReinterpretInt32, (helper, c) =>
	    {
		var builder = c.ExportsBuilder.DefineMethod(
				    "☣ Float32ReinterpretInt32",
				    IKVMCompilationContext.HelperMethodAttributes,
				    universe.Import(typeof(float)),
				    new[]
				    {
					universe.Import(typeof(int)),
				    }
				    );

		var il = builder.GetILGenerator();
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarga_S, 0);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ldind_R4);
		il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);
		return builder;
	    }
	    ]);
	}
    }
}