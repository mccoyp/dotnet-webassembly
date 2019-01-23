namespace WebAssembly.Instructions
{
    /// <summary>
    /// Compare ordered and greater than.
    /// </summary>
    public class Float32GreaterThan : ValueTwoToInt32Instruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Float32GreaterThan"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Float32GreaterThan;

	private protected sealed override ValueType ValueType => ValueType.Float32;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Cgt;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Cgt;

	/// <summary>
	/// Creates a new  <see cref="Float32GreaterThan"/> instance.
	/// </summary>
	public Float32GreaterThan()
	{
	}
    }
}