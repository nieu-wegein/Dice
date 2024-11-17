using ConsoleTables;
using Dice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal static class TableGenerator
	{
		public static void Display(List<DiceModel> diceList)
		{
			var table = new ConsoleTable().AddColumn(diceList.ConvertAll(d => d.ToString()).Prepend("Dice"));

			foreach (var dice in diceList)
			{
				string[] row = new string[diceList.Count + 1];
				row[0] = dice.ToString();

				for (int j = 0; j < diceList.Count; j++)
				{
					row[j + 1] = WinСhanceCalculator.Calculate(dice, diceList[j]).ToString();
				}

				table.Rows.Add(row);
			}

			ConsoleTools.WriteInfo("\nHere is the table of probabilities of winning for each dice");
			Console.WriteLine(table.ToString().Replace($"\n Count: {diceList.Count}", ""));
		}
	}
}
