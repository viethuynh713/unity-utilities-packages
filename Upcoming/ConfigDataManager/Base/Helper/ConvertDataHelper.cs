using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Kozits.ZooCoffee.Config
{
    public static class ConvertDataHelper
    {
	    private static readonly Regex reg_color32 = new Regex(@"^[A-Fa-f0-9]{8}$");
	    private static readonly Regex reg_color24 = new Regex(@"^[A-Fa-f0-9]{6}$");
	    public static int[] GetIntsFromString(string str) {
			str = TrimBracket(str);
			if (string.IsNullOrEmpty(str)) { return new int[0]; }
			string[] splits = str.Split(',');
			int[] ints = new int[splits.Length];
			for (int i = 0, imax = splits.Length; i < imax; i++) {
				int intValue;
				if (int.TryParse(splits[i].Trim(), out intValue)) {
					ints[i] = intValue;
				} else {
					ints[i] = 0;
				}
			}
			return ints;
		}

	    public static long[] GetLongsFromString(string str) {
			str = TrimBracket(str);
			if (string.IsNullOrEmpty(str)) { return new long[0]; }
			string[] splits = str.Split(',');
			long[] longs = new long[splits.Length];
			for (int i = 0, imax = splits.Length; i < imax; i++) {
				long longValue;
				if (long.TryParse(splits[i].Trim(), out longValue)) {
					longs[i] = longValue;
				} else {
					longs[i] = 0L;
				}
			}
			return longs;
		}

	    public static float[] GetFloatsFromString(string str) {
			str = TrimBracket(str);
			if (string.IsNullOrEmpty(str)) { return new float[0]; }
			string[] splits = str.Split(',');
			float[] floats = new float[splits.Length];
			for (int i = 0, imax = splits.Length; i < imax; i++) {
				float floatValue;
				if (float.TryParse(splits[i].Trim(), out floatValue)) {
					floats[i] = floatValue;
				} else {
					floats[i] = 0;
				}
			}
			return floats;
		}

	    public static string[] GetStringsFromString(string str) {
			str = TrimBracket(str);
			if (string.IsNullOrEmpty(str)) { return new string[0]; }
			return str.Split(',');
		}

	    public static Color GetColorFromString(string str) {
			if (string.IsNullOrEmpty(str)) { return Color.clear; }
			uint colorUInt;
			if (GetColorUIntFromString(str, out colorUInt)) {
				uint r = (colorUInt >> 24) & 0xffu;
				uint g = (colorUInt >> 16) & 0xffu;
				uint b = (colorUInt >> 8) & 0xffu;
				uint a = colorUInt & 0xffu;
				return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
			}
			str = TrimBracket(str);
			string[] splits = str.Split(',');
			if (splits.Length == 4) {
				int r, g, b, a;
				if (int.TryParse(splits[0].Trim(), out r) && int.TryParse(splits[1].Trim(), out g) &&
					int.TryParse(splits[2].Trim(), out b) && int.TryParse(splits[3].Trim(), out a)) {
					return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
				}
			} else if (splits.Length == 3) {
				int r, g, b;
				if (int.TryParse(splits[0].Trim(), out r) && int.TryParse(splits[1].Trim(), out g) &&
					int.TryParse(splits[2].Trim(), out b)) {
					return new Color(r / 255f, g / 255f, b / 255f);
				}
			}
			return Color.clear;
		}
	    public static bool GetColorUIntFromString(string str, out uint color) {
			if (reg_color32.IsMatch(str)) {
				color = Convert.ToUInt32(str, 16);
			} else if (reg_color24.IsMatch(str)) {
				color = (Convert.ToUInt32(str, 16) << 8) | 0xffu;
			} else {
				color = 0u;
				return false;
			}
			return true;
		}
		public static bool CheckClassName(string str) {
			return Regex.IsMatch(str, @"^[A-Z][A-Za-z0-9_]*$");
		}

		public static bool CheckFieldName(string name) {
			return Regex.IsMatch(name, @"^[A-Za-z_][A-Za-z0-9_]*$");
		}

		public static string CapitalFirstChar(string str) {
			return str[0].ToString().ToUpper() + str.Substring(1);
		}

		public static string TrimBracket(string str) {
			if (str.StartsWith("[") && str.EndsWith("]")) {
				return str.Substring(1, str.Length - 2);
			}
			return str;
		}

		public static bool CheckIsNameSpaceValid(string ns) {
			if (string.IsNullOrEmpty(ns)) { return true; }
			return Regex.IsMatch(ns, @"(\S+\s*\.\s*)*\S+");
		}
    }
}