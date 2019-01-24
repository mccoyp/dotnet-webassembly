namespace WebAssembly.Instructions
{
    /// <summary>
    /// Signed less than.
    /// </summary>
    public class Int32LessThanSigned : ValueTwoToOneInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int32LessThanSigned"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int32LessThanSigned;

	private protected sealed override ValueType ValueType => ValueType.Int32;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Clt;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Clt;

	/// <summary>
	/// Creates a new  <see cref="Int32LessThanSigned"/> instance.
	/// </summary>
	public Int32LessThanSigned()
	{
	}
    }
}