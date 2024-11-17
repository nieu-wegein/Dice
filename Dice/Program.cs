using Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

while (true)
{
	Console.Clear();
	try
	{
		new Game(args).Play();
	}
	catch (Exception e)
	{
		Console.WriteLine(e.Message);
		Console.WriteLine("\nPress any key to exit...");
		Console.ReadKey();
		Console.Clear();
		break;
	}
	Console.ReadLine();
}
