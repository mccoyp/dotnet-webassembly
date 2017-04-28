namespace WebAssembly.Instructions
{
	/// <summary>
	/// (Placeholder) Instruction for Int64Add.
	/// </summary>
	public class Int64Add : Instruction
	{
		/// <summary>
		/// Always <see cref="OpCode.Int64Add"/>.
		/// </summary>
		public sealed override OpCode OpCode => OpCode.Int64Add;

		/// <summary>
		/// Creates a new  <see cref="Int64Add"/> instance.
		/// </summary>
		public Int64Add()
		{
		}
	}
}