using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyncRes {
	static class Parser {
		static string PatternSpaceWiden(string reg) {
			return Regex.Replace(reg, @" +", @"\s*");
		}

		public static string Match(string source, string pattern) {
			Match match = Regex.Match(source, PatternSpaceWiden(pattern));
			if(match.Success) {
				return match.Groups[1].Value;
			} else {
				return null;
			}
		}
	}
}
