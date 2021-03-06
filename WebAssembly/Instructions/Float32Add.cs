namespace WebAssembly.Instructions
{
    /// <summary>
    /// Addition.
    /// </summary>
    public class Float32Add : ValueTwoToOneInstruction
    {
	/// <summary>
	/// Always <see cref="OpCode.Float32Add"/>.
	/// </summary>
	public sealed override OpCode OpCode => OpCode.Float32Add;

	private protected sealed override ValueType ValueType => ValueType.Float32;

	private protected sealed override System.Reflection.Emit.OpCode EmittedOpCode =>
		System.Reflection.Emit.OpCodes.Add;

	private protected sealed override IKVM.Reflection.Emit.OpCode IKVMEmittedOpCode =>
		IKVM.Reflection.Emit.OpCodes.Add;

	/// <summary>
	/// Creates a new  <see cref="Float32Add"/> instance.
	/// </summary>
	public Float32Add()
	{
	}
    }
}