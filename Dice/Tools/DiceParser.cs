using Dice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal static class DiceParser
	{
		public static List<DiceModel> Parse(string[] config)
		{
			List<DiceModel> diceList = new List<DiceModel>();
			string format = "\n\nUse the following parameter format to specify dice configuration:" +
			"\nDice.exe 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6 ...";

			if (config.Length == 0)
				throw new Exception("You need to provide the dice configuration!" + format);
			if (config.Length < 3)
				throw new Exception("At least three dice must be provided to play the game!" + format);

			for (int i = 0; i < config.Length; i++)
			{
				uint[] _dice;
				try
				{
					_dice = config[i].Split('\u002C').Select(UInt32.Parse).ToArray();
				}
				catch
				{
					throw new Exception("The value of each side of the dice must be a positive integer!" + format);
				}
				if (_dice.Length != 6)
					throw new Exception("Each dice must have exactly six sides!" + format);

				diceList.Add(new DiceModel(_dice));
			}

			return diceList;
		}
	}
}
