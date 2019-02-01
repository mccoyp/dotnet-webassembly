using System;

namespace WasmClass
{
    public abstract class Sample
    {
	public abstract int create_counter();
	public abstract int add_to_counter(int value);
	public abstract void delete_counter(int value);
    }
}
