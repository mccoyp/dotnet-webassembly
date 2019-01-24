using System;

namespace RunWasm
{
    class Program
    {
	// C code:
	// int maxCounters = 0;
	// int* counters[10];
	//
	// int create_counter (void)
	// {
	//    if (maxCounters >= 10)
	//       return -1; // can't have more than 10 counters
	//    int c = maxCounters;
	//    maxCounters++;
	//    int *p = malloc(sizeof(int));
	//    counters[c] = p;
	//    return c;
	// }
	// int add_to_counter(int counterNum)
	// {
	//    int oldValue = counters[counterNum];
	//    counters[counterNum]++;
	//    return oldValue;
	// }
	// void delete_counter (int counterNum)
	// {
	//    free (counters[counterNum]);
	//    counters[counterNum] = (int*)0;
	// }

	static void Main(string[] args)
	{
	    /*
	    var o = new CompiledInstance();
	    int i = o.Exports.foo(2, 3);
	    Console.WriteLine(i);
	    */
	}
    }
}
