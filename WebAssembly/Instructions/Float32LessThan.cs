namespace WebAssembly.Instructions
{
	/// <summary>
	/// (Placeholder) Instruction for Float32LessThan.
	/// </summary>
	public class Float32LessThan : Instruction
	{
		/// <summary>
		/// Always <see cref="OpCode.Float32LessThan"/>.
		/// </summary>
		public sealed override OpCode OpCode => OpCode.Float32LessThan;

		/// <summary>
		/// Creates a new  <see cref="Float32LessThan"/> instance.
		/// </summary>
		public Float32LessThan()
		{
		}
	}
}