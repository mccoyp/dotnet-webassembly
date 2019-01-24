using System;

namespace WasmClass
{
    public abstract class Sample
    {
	//Sometimes you can use C# dynamic instead of building an abstract class like this.
	public abstract int foo(int value1, int value2);
    }
}
