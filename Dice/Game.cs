using Dice.Models;
using Dice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
	internal class Game
	{
		private readonly List<DiceModel> _diceList;
		private DiceModel _userDice;
		private DiceModel _computerDice;
		private InputMenu _menu;
		private uint _userScore;
		private uint _computerScore;
		private bool isUserMove;

		public Game(string[] config)
		{
			_diceList = DiceParser.Parse(config);
			_menu = new InputMenu();
			_menu.ExitRequested += () => { Environment.Exit(1); };
			_menu.HelpRequested += () => { TableGenerator.Display(DiceParser.Parse(config)); };
		}

		public void Play()
		{
			Console.WriteLine("Welcome to the game!\n");
			SetFirstPlayer();

			if (isUserMove)
			{
				SetUserDice();
				SetComputerDice();
			}
			else
			{
				SetComputerDice();
				SetUserDice();
			}

			for (int i = 0; i < 2; i++, isUserMove = !isUserMove)
				Throw();

			int val = _userScore.CompareTo(_computerScore);
			switch (val)
			{
				case > 0: ConsoleTools.WriteInfo($"{_userScore} > {_computerScore}. You win!"); break;
				case < 0: ConsoleTools.WriteInfo($"{_computerScore} > {_userScore}. I win!"); break;
				case 0: ConsoleTools.WriteInfo($"{_userScore} = {_computerScore}. Draw!"); break;
			}

		}

		private void SetFirstPlayer()
		{
			int computerChoice = FairPlayTools.GetFairNumber(0, 2, out string key, out string hmac);

			Console.WriteLine($"""
			Let's determine who will move first.
			I chose a random number from 0 to 1 (HMAC = {hmac}).
			""");

			var (_, value) = _menu.GetInput("Try to guess my choice.", new[] { 0, 1 });
			isUserMove = value == computerChoice;
			Console.WriteLine($"My choice: {computerChoice} (KEY = {key}).");
			Console.WriteLine((isUserMove ? "You" : "I") + " make the first move.\n");
		}

		private void SetComputerDice()
		{
			int diceIndex = FairPlayTools.GetFairNumber(0, _diceList.Count, out _, out _);
			_computerDice = _diceList[diceIndex];
			_diceList.RemoveAt(diceIndex);

			Console.WriteLine($"I choose the {_computerDice} dice.");
		}

		private void SetUserDice()
		{
			var (index, _) = _menu.GetInput("Choose your dice:", _diceList.ToArray());
			_userDice = _diceList[index];
			_diceList.RemoveAt(index);

			Console.WriteLine($"You choose the {_userDice} dice.");
		}

		private void Throw()
		{
			int computerNumber = FairPlayTools.GetFairNumber(0, 5, out string key, out string hmac);
			Console.WriteLine($"\nIt's time for {(isUserMove ? "your" : "my")} throw.");
			Console.WriteLine($"I chose a random number from 0 to 5 (HMAC = {hmac}).");

			var (_, value) = _menu.GetInput("Choose a number from 0 to 5: ", new[] { 0, 1, 2, 3, 4, 5 });
			int result = (computerNumber + value) % 6;
			uint score = isUserMove ? _userDice[result] : _computerDice[result];

			Console.WriteLine($"My selection is {computerNumber} (KEY = {key}).");
			Console.WriteLine($"The result is {computerNumber} + {value} = {result} (mod 6).");
			Console.WriteLine($"{(isUserMove ? "Your" : "My")} throw is {score}.\n");

			if (isUserMove)
				_userScore = score;
			else
				_computerScore = score;
		}
	}
}
