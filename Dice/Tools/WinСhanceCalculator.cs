using Dice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal static class WinСhanceCalculator
	{
		public static double Calculate(DiceModel d1, DiceModel d2)
		{
			double winCount = 0;

			for (int i = 0; i < 6; i++)
				for (int j = 0; j < 6; j++)
					if (d1[i] > d2[j])
						winCount++;

			return winCount / 36;
		}
	}
}
