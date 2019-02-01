namespace WebAssembly.Instructions
{
    /// <summary>
    /// Sign-agnostic subtraction.
    /// </summary>
    public class Int32Subtract : ValueTwoToOneInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Int32Subtract"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Int32Subtract;

	private protected sealed override ValueType ValueType => ValueType.Int32;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Sub;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Sub;

	/// <summary>
	/// Creates a new  <see cref="Int32Subtract"/> instance.
	/// </summary>
	public Int32Subtract()
	{
	}
    }
}