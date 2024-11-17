using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal static class ConsoleTools
	{

		public static void WriteError(string message) => Write(message, ConsoleColor.Red);
		public static void WriteInfo(string message) => Write(message, ConsoleColor.Cyan);

		private static void Write(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}
