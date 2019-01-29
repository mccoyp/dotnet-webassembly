using System;

namespace RunWasm
{
    class Program
    {
	//C code:
	//int maxCounters = 0;
	//int counters[10];

	//int create_counter(void)
	//{
	//    if (maxCounters >= 10)
	//	return -1; // can't have more than 10 counters
	//    int c = maxCounters;
	//    maxCounters++;
	//    counters[c] = 1;
	//    return c;
	//}

	//int add_to_counter(int counterNum)
	//{
	//    int oldValue = counters[counterNum];
	//    counters[counterNum]++;
	//    return oldValue;
	//}

	//void delete_counter(int counterNum)
	//{
	//    counters[counterNum] = 0;
	//}

	static void Main(string[] args)
	{
	    var o = new CompiledInstance();
	    int i = o.Exports.create_counter();
	    Console.WriteLine(i);
	    int x = o.Exports.add_to_counter(i);
	    int y = o.Exports.add_to_counter(i);
	}
    }
}
