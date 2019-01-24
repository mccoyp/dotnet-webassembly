using System.Reflection.Emit;
using static System.Diagnostics.Debug;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// A ternary operator with two operands, which have the same type as each other, plus a boolean (i32) condition. Returns the first operand if the condition operand is non-zero, or the second otherwise.
    /// </summary>
    public class Select : SimpleInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Select"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Select;

	/// <summary>
	/// Creates a new  <see cref="Select"/> instance.
	/// </summary>
	public Select()
	{
	}

	internal sealed override void Compile(CompilationContext context)
	{
	    Assert(context != null);

	    var stack = context.Stack;
	    Assert(stack != null);
	    if (stack.Count < 3)
		throw new StackTooSmallException(OpCode.Select, 3, stack.Count);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Select, ValueType.Int32, type);

	    var typeB = stack.Pop();
	    var typeA = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (typeA != typeB)
		throw new StackParameterMismatchException(OpCode.Select, typeA, typeB);

	    HelperMethod helper;
	    switch (typeA)
	    {
		default: //This shouldn't be possible due to previous validations.
		    Fail("Unknown ValueType.");
		    return;
		case ValueType.Int32: helper = HelperMethod.SelectInt32; break;
		case ValueType.Int64: helper = HelperMethod.SelectInt64; break;
		case ValueType.Float32: helper = HelperMethod.SelectFloat32; break;
		case ValueType.Float64: helper = HelperMethod.SelectFloat64; break;
	    }
	    context.Emit(OpCodes.Call, context[helper, CreateSelectHelper]);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    Assert(context != null);

	    var stack = context.Stack;
	    Assert(stack != null);
	    if (stack.Count < 3)
		throw new StackTooSmallException(OpCode.Select, 3, stack.Count);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(OpCode.Select, ValueType.Int32, type);

	    var typeB = stack.Pop();
	    var typeA = stack.Peek(); //Assuming validation passes, the remaining type will be this.

	    if (typeA != typeB)
		throw new StackParameterMismatchException(OpCode.Select, typeA, typeB);

	    HelperMethod helper;
	    switch (typeA)
	    {
		default: //This shouldn't be possible due to previous validations.
		    Fail("Unknown ValueType.");
		    return;
		case ValueType.Int32: helper = HelperMethod.SelectInt32; break;
		case ValueType.Int64: helper = HelperMethod.SelectInt64; break;
		case ValueType.Float32: helper = HelperMethod.SelectFloat32; break;
		case ValueType.Float64: helper = HelperMethod.SelectFloat64; break;
	    }
	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, IKVMCreateSelectHelper(helper, context, universe));
	}

	static MethodBuilder CreateSelectHelper(HelperMethod helper, CompilationContext context)
	{
	    Assert(context != null);

	    MethodBuilder builder;
	    switch (helper)
	    {
		default:
		    Fail("Attempted to obtain an unknown helper method.");
		    return null;
		case HelperMethod.SelectInt32:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Int32",
			    CompilationContext.HelperMethodAttributes,
			    typeof(int),
			    new[]
			    {
								typeof(int),
								typeof(int),
								typeof(int),
			    }
			    );
		    break;

		case HelperMethod.SelectInt64:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Int64",
			    CompilationContext.HelperMethodAttributes,
			    typeof(long),
			    new[]
			    {
								typeof(long),
								typeof(long),
								typeof(int),
			    }
			    );
		    break;

		case HelperMethod.SelectFloat32:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Float32",
			    CompilationContext.HelperMethodAttributes,
			    typeof(float),
			    new[]
			    {
								typeof(float),
								typeof(float),
								typeof(int),
			    }
			    );
		    break;

		case HelperMethod.SelectFloat64:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Float64",
			    CompilationContext.HelperMethodAttributes,
			    typeof(double),
			    new[]
			    {
								typeof(double),
								typeof(double),
								typeof(int),
			    }
			    );
		    break;
	    }

	    var il = builder.GetILGenerator();
	    il.Emit(OpCodes.Ldarg_2);
	    var @true = il.DefineLabel();
	    il.Emit(OpCodes.Brtrue_S, @true);
	    il.Emit(OpCodes.Ldarg_1);
	    il.Emit(OpCodes.Ret);
	    il.MarkLabel(@true);
	    il.Emit(OpCodes.Ldarg_0);
	    il.Emit(OpCodes.Ret);
	    return builder;
	}

	static IKVM.Reflection.Emit.MethodBuilder IKVMCreateSelectHelper(HelperMethod helper, IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    Assert(context != null);

	    IKVM.Reflection.Emit.MethodBuilder builder;
	    switch (helper)
	    {
		default:
		    Fail("Attempted to obtain an unknown helper method.");
		    return null;
		case HelperMethod.SelectInt32:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Int32",
			    IKVMCompilationContext.HelperMethodAttributes,
			    universe.Import(typeof(int)),
			    new[]
			    {
				universe.Import(typeof(int)),
				universe.Import(typeof(int)),
				universe.Import(typeof(int)),
			    }
			    );
		    break;

		case HelperMethod.SelectInt64:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Int64",
			    IKVMCompilationContext.HelperMethodAttributes,
			    universe.Import(typeof(long)),
			    new[]
			    {
				universe.Import(typeof(long)),
				universe.Import(typeof(long)),
				universe.Import(typeof(int)),
			    }
			    );
		    break;

		case HelperMethod.SelectFloat32:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Float32",
			    IKVMCompilationContext.HelperMethodAttributes,
			    universe.Import(typeof(float)),
			    new[]
			    {
				universe.Import(typeof(float)),
				universe.Import(typeof(float)),
				universe.Import(typeof(int)),
			    }
			    );
		    break;

		case HelperMethod.SelectFloat64:
		    builder = context.ExportsBuilder.DefineMethod(
			    "☣ Select Float64",
			    IKVMCompilationContext.HelperMethodAttributes,
			    universe.Import(typeof(double)),
			    new[]
			    {
				universe.Import(typeof(double)),
				universe.Import(typeof(double)),
				universe.Import(typeof(int)),
			    }
			    );
		    break;
	    }

	    var il = builder.GetILGenerator();
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_2);
	    var @true = il.DefineLabel();
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Brtrue_S, @true);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_1);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);
	    il.MarkLabel(@true);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ldarg_0);
	    il.Emit(IKVM.Reflection.Emit.OpCodes.Ret);
	    return builder;
	}
    }
}