namespace WebAssembly.Instructions
{
    /// <summary>
    /// Division.
    /// </summary>
    public class Float64Divide : ValueTwoToOneInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Float64Divide"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Float64Divide;

	private protected sealed override ValueType ValueType => ValueType.Float64;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Div;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Div;

	/// <summary>
	/// Creates a new  <see cref="Float64Divide"/> instance.
	/// </summary>
	public Float64Divide()
	{
	}
    }
}