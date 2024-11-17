using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Models
{
	internal class DiceModel
	{
		private readonly uint[] _dice;

		public DiceModel(uint[] dice)
		{
			_dice = dice;
		}

		public override string ToString() => $"[{String.Join(",", _dice)}]";

		public uint this[int side] => _dice[side];

	}
}
