using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Tools
{
	internal static class FairPlayTools
	{
		public static int GetFairNumber(int from, int to, out string key, out string hmac)
		{
			int number = RandomNumberGenerator.GetInt32(from, to);
			byte[] numberBytes = Encoding.UTF8.GetBytes(number.ToString());
			byte[] keyBytes = RandomNumberGenerator.GetBytes(32);

			HMac h = new HMac(new Sha3Digest(256));
			h.Init(new KeyParameter(keyBytes));
			h.BlockUpdate(numberBytes, 0, numberBytes.Length);

			byte[] result = new byte[h.GetMacSize()];
			h.DoFinal(result, 0);

			hmac = BitConverter.ToString(result).Replace("-", "");
			key = BitConverter.ToString(keyBytes).Replace("-", "");
			return number;
		}
	}
}
