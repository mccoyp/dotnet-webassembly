namespace WebAssembly.Instructions
{
	/// <summary>
	/// (Placeholder) Instruction for Int64TruncateUnsignedFloat32.
	/// </summary>
	public class Int64TruncateUnsignedFloat32 : Instruction
	{
		/// <summary>
		/// Always <see cref="OpCode.Int64TruncateUnsignedFloat32"/>.
		/// </summary>
		public sealed override OpCode OpCode => OpCode.Int64TruncateUnsignedFloat32;

		/// <summary>
		/// Creates a new  <see cref="Int64TruncateUnsignedFloat32"/> instance.
		/// </summary>
		public Int64TruncateUnsignedFloat32()
		{
		}
	}
}