﻿using System.Reflection.Emit;

namespace WebAssembly.Instructions
{
    /// <summary>
    /// Provides shared functionality for instructions that read from linear memory.
    /// </summary>
    public abstract class MemoryReadInstruction : MemoryImmediateInstruction
    {
	private protected MemoryReadInstruction()
		: base()
	{
	}

	private protected MemoryReadInstruction(Reader reader)
		: base(reader)
	{
	}

	private protected virtual System.Reflection.Emit.OpCode ConversionOpCode => OpCodes.Nop;

	private protected virtual IKVM.Reflection.Emit.OpCode IKVMConversionOpCode => IKVM.Reflection.Emit.OpCodes.Nop;

	internal sealed override void Compile(CompilationContext context)
	{
	    var stack = context.Stack;
	    if (stack.Count == 0)
		throw new StackTooSmallException(this.OpCode, 1, 0);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(this.OpCode, ValueType.Int32, type);

	    if (this.Offset != 0)
	    {
		Int32Constant.Emit(context, (int)this.Offset);
		context.Emit(OpCodes.Add_Ovf_Un);
	    }

	    this.EmitRangeCheck(context);

	    context.EmitLoadThis();
	    context.Emit(OpCodes.Ldfld, context.Memory);
	    context.Emit(OpCodes.Call, Runtime.UnmanagedMemory.StartGetter);
	    context.Emit(OpCodes.Add);

	    byte alignment;
	    switch (this.Flags & Options.Align8)
	    {
		default: //Impossible to hit, but needed to avoid compiler error the about alignment variable.
		case Options.Align1: alignment = 1; break;
		case Options.Align2: alignment = 2; break;
		case Options.Align4: alignment = 4; break;
		case Options.Align8: alignment = 8; break;
	    }

	    if (alignment != 4)
		context.Emit(OpCodes.Unaligned, alignment);

	    context.Emit(this.EmittedOpCode);
	    var conversion = this.ConversionOpCode;
	    if (conversion != OpCodes.Nop)
		context.Emit(conversion);

	    stack.Push(this.Type);
	}

	internal sealed override void CompileIKVM(IKVMCompilationContext context, IKVM.Reflection.Universe universe)
	{
	    var stack = context.Stack;
	    if (stack.Count == 0)
		throw new StackTooSmallException(this.OpCode, 1, 0);

	    var type = stack.Pop();
	    if (type != ValueType.Int32)
		throw new StackTypeInvalidException(this.OpCode, ValueType.Int32, type);

	    if (this.Offset != 0)
	    {
		Int32Constant.Emit(context, (int)this.Offset);
		context.Emit(IKVM.Reflection.Emit.OpCodes.Add_Ovf_Un);
	    }

	    this.IKVMEmitRangeCheck(context, universe);

	    context.EmitLoadThis();
	    context.Emit(IKVM.Reflection.Emit.OpCodes.Ldfld, context.Memory);
	    context.Emit(IKVM.Reflection.Emit.OpCodes.Call, Runtime.UnmanagedMemory.IKVMStartGetter(universe));
	    context.Emit(IKVM.Reflection.Emit.OpCodes.Add);

	    byte alignment;
	    switch (this.Flags & Options.Align8)
	    {
		default: //Impossible to hit, but needed to avoid compiler error the about alignment variable.
		case Options.Align1: alignment = 1; break;
		case Options.Align2: alignment = 2; break;
		case Options.Align4: alignment = 4; break;
		case Options.Align8: alignment = 8; break;
	    }

	    if (alignment != 4)
		context.Emit(IKVM.Reflection.Emit.OpCodes.Unaligned, alignment);

	    context.Emit(this.IKVMEmittedOpCode);
	    var conversion = this.IKVMConversionOpCode;
	    if (conversion != IKVM.Reflection.Emit.OpCodes.Nop)
		context.Emit(conversion);

	    stack.Push(this.Type);
	}
    }
}