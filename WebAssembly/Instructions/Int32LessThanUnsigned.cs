namespace WebAssembly.Instructions
{
	/// <summary>
	/// (Placeholder) Instruction for Int32LessThanUnsigned.
	/// </summary>
	public class Int32LessThanUnsigned : Instruction
	{
		/// <summary>
		/// Always <see cref="OpCode.Int32LessThanUnsigned"/>.
		/// </summary>
		public sealed override OpCode OpCode => OpCode.Int32LessThanUnsigned;

		/// <summary>
		/// Creates a new  <see cref="Int32LessThanUnsigned"/> instance.
		/// </summary>
		public Int32LessThanUnsigned()
		{
		}
	}
}