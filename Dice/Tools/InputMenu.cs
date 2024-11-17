using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal class InputMenu
	{
		public event Action HelpRequested;
		public event Action ExitRequested;

		public (int, T) GetInput<T>(string message, T[] values)
		{
			int index = -1;
			T value = default;
			bool isValid = false;

			while (!isValid)
			{
				string buffer = string.Join("\n", values.Select((v, i) => $"{i} - {v}")
					.Prepend(message).Append("? - Help\nx - Exit\nYour choice: "));

				Console.Write(buffer);
				string key = Console.ReadLine();
				switch (key)
				{
					case "?": HelpRequested(); break;
					case "x": ExitRequested(); break;
					default:
						try
						{
							index = int.Parse(key);
							value = values[index];
							isValid = true;
						}
						catch
						{
							ConsoleTools.WriteError("\nInvalid value!\n");
						}; break;
				}
			}
			return (index, value);
		}
	}
}
