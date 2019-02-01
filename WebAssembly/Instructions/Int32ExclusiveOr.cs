namespace WebAssembly.Instructions
{
    /// <summary>
    /// Sign-agnostic bitwise exclusive or.
    /// </summary>
    public class Int32ExclusiveOr : ValueTwoToOneInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int32ExclusiveOr"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int32ExclusiveOr;

	private protected sealed override ValueType ValueType => ValueType.Int32;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Xor;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Xor;

	/// <summary>
	/// Creates a new  <see cref="Int32ExclusiveOr"/> instance.
	/// </summary>
	public Int32ExclusiveOr()
	{
	}
    }
}