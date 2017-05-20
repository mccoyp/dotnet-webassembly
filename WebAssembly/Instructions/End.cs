﻿namespace WebAssembly.Instructions
{
	/// <summary>
	/// An instruction that marks the end of a block, loop, if, or function.
	/// </summary>
	public class End : SimpleInstruction
	{
		/// <summary>
		/// Always <see cref="OpCode.End"/>.
		/// </summary>
		public sealed override OpCode OpCode => OpCode.End;

		/// <summary>
		/// Creates a new <see cref="End"/> instance.
		/// </summary>
		public End()
		{
		}
	}
}