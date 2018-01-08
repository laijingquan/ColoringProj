using System;
using System.Collections;
using System.Text;

namespace GameAnalyticsSDK.Utilities
{
	// Token: 0x0200003F RID: 63
	public class GA_MiniJSON
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000C094 File Offset: 0x0000A494
		public static object JsonDecode(string json)
		{
			GA_MiniJSON.instance.lastDecode = json;
			if (json != null)
			{
				char[] json2 = json.ToCharArray();
				int num = 0;
				bool flag = true;
				object result = GA_MiniJSON.instance.ParseValue(json2, ref num, ref flag);
				if (flag)
				{
					GA_MiniJSON.instance.lastErrorIndex = -1;
				}
				else
				{
					GA_MiniJSON.instance.lastErrorIndex = num;
				}
				return result;
			}
			return null;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C0F4 File Offset: 0x0000A4F4
		public static string JsonEncode(object json)
		{
			StringBuilder stringBuilder = new StringBuilder(2000);
			bool flag = GA_MiniJSON.instance.SerializeValue(json, stringBuilder);
			return (!flag) ? null : stringBuilder.ToString();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C12B File Offset: 0x0000A52B
		public static bool LastDecodeSuccessful()
		{
			return GA_MiniJSON.instance.lastErrorIndex == -1;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C13A File Offset: 0x0000A53A
		public static int GetLastErrorIndex()
		{
			return GA_MiniJSON.instance.lastErrorIndex;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C148 File Offset: 0x0000A548
		public static string GetLastErrorSnippet()
		{
			if (GA_MiniJSON.instance.lastErrorIndex == -1)
			{
				return string.Empty;
			}
			int num = GA_MiniJSON.instance.lastErrorIndex - 5;
			int num2 = GA_MiniJSON.instance.lastErrorIndex + 15;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 >= GA_MiniJSON.instance.lastDecode.Length)
			{
				num2 = GA_MiniJSON.instance.lastDecode.Length - 1;
			}
			return GA_MiniJSON.instance.lastDecode.Substring(num, num2 - num + 1);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C1CC File Offset: 0x0000A5CC
		protected Hashtable ParseObject(char[] json, ref int index)
		{
			Hashtable hashtable = new Hashtable();
			this.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				int num = this.LookAhead(json, index);
				if (num == 0)
				{
					return null;
				}
				if (num == 6)
				{
					this.NextToken(json, ref index);
				}
				else
				{
					if (num == 2)
					{
						this.NextToken(json, ref index);
						return hashtable;
					}
					string text = this.ParseString(json, ref index);
					if (text == null)
					{
						return null;
					}
					num = this.NextToken(json, ref index);
					if (num != 5)
					{
						return null;
					}
					bool flag2 = true;
					object value = this.ParseValue(json, ref index, ref flag2);
					if (!flag2)
					{
						return null;
					}
					hashtable[text] = value;
				}
			}
			return hashtable;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C274 File Offset: 0x0000A674
		protected ArrayList ParseArray(char[] json, ref int index)
		{
			ArrayList arrayList = new ArrayList();
			this.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				int num = this.LookAhead(json, index);
				if (num == 0)
				{
					return null;
				}
				if (num == 6)
				{
					this.NextToken(json, ref index);
				}
				else
				{
					if (num == 4)
					{
						this.NextToken(json, ref index);
						break;
					}
					bool flag2 = true;
					object value = this.ParseValue(json, ref index, ref flag2);
					if (!flag2)
					{
						return null;
					}
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C2FC File Offset: 0x0000A6FC
		protected object ParseValue(char[] json, ref int index, ref bool success)
		{
			switch (this.LookAhead(json, index))
			{
			case 1:
				return this.ParseObject(json, ref index);
			case 3:
				return this.ParseArray(json, ref index);
			case 7:
				return this.ParseString(json, ref index);
			case 8:
				return this.ParseNumber(json, ref index);
			case 9:
				this.NextToken(json, ref index);
				return bool.Parse("TRUE");
			case 10:
				this.NextToken(json, ref index);
				return bool.Parse("FALSE");
			case 11:
				this.NextToken(json, ref index);
				return null;
			}
			success = false;
			return null;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C3C0 File Offset: 0x0000A7C0
		protected string ParseString(char[] json, ref int index)
		{
			string text = string.Empty;
			this.EatWhitespace(json, ref index);
			char c = json[index];
			index++;
			bool flag = false;
			while (!flag)
			{
				if (index == json.Length)
				{
					break;
				}
				c = json[index];
				index++;
				if (c == '"')
				{
					flag = true;
					break;
				}
				if (c == '\\')
				{
					if (index == json.Length)
					{
						break;
					}
					c = json[index];
					index++;
					if (c == '"')
					{
						text += '"';
					}
					else if (c == '\\')
					{
						text += '\\';
					}
					else if (c == '/')
					{
						text += '/';
					}
					else if (c == 'b')
					{
						text += '\b';
					}
					else if (c == 'f')
					{
						text += '\f';
					}
					else if (c == 'n')
					{
						text += '\n';
					}
					else if (c == 'r')
					{
						text += '\r';
					}
					else if (c == 't')
					{
						text += '\t';
					}
					else if (c == 'u')
					{
						int num = json.Length - index;
						if (num < 4)
						{
							break;
						}
						char[] array = new char[4];
						for (int i = 0; i < 4; i++)
						{
							array[i] = json[index + i];
						}
						text = text + "&#x" + new string(array) + ";";
						index += 4;
					}
				}
				else
				{
					text += c.ToString();
				}
			}
			if (!flag)
			{
				return null;
			}
			return text;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000C5A0 File Offset: 0x0000A9A0
		protected float ParseNumber(char[] json, ref int index)
		{
			this.EatWhitespace(json, ref index);
			int lastIndexOfNumber = this.GetLastIndexOfNumber(json, index);
			int num = lastIndexOfNumber - index + 1;
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = json[index + i];
			}
			index = lastIndexOfNumber + 1;
			return float.Parse(new string(array));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000C5F8 File Offset: 0x0000A9F8
		protected int GetLastIndexOfNumber(char[] json, int index)
		{
			int i;
			for (i = index; i < json.Length; i++)
			{
				if ("0123456789+-.eE".IndexOf(json[i]) == -1)
				{
					break;
				}
			}
			return i - 1;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C634 File Offset: 0x0000AA34
		protected void EatWhitespace(char[] json, ref int index)
		{
			while (index < json.Length)
			{
				if (" \t\n\r".IndexOf(json[index]) == -1)
				{
					break;
				}
				index++;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C664 File Offset: 0x0000AA64
		protected int LookAhead(char[] json, int index)
		{
			int num = index;
			return this.NextToken(json, ref num);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C67C File Offset: 0x0000AA7C
		protected int NextToken(char[] json, ref int index)
		{
			this.EatWhitespace(json, ref index);
			if (index == json.Length)
			{
				return 0;
			}
			char c = json[index];
			index++;
			switch (c)
			{
			case ',':
				return 6;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return 8;
			default:
				switch (c)
				{
				case '[':
					return 3;
				default:
					switch (c)
					{
					case '{':
						return 1;
					default:
					{
						if (c == '"')
						{
							return 7;
						}
						index--;
						int num = json.Length - index;
						if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
						{
							index += 5;
							return 10;
						}
						if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
						{
							index += 4;
							return 9;
						}
						if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
						{
							index += 4;
							return 11;
						}
						return 0;
					}
					case '}':
						return 2;
					}
					break;
				case ']':
					return 4;
				}
				break;
			case ':':
				return 5;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C815 File Offset: 0x0000AC15
		protected bool SerializeObjectOrArray(object objectOrArray, StringBuilder builder)
		{
			if (objectOrArray is Hashtable)
			{
				return this.SerializeObject((Hashtable)objectOrArray, builder);
			}
			return objectOrArray is ArrayList && this.SerializeArray((ArrayList)objectOrArray, builder);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C84C File Offset: 0x0000AC4C
		protected bool SerializeObject(Hashtable anObject, StringBuilder builder)
		{
			builder.Append("{");
			IDictionaryEnumerator enumerator = anObject.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string aString = enumerator.Key.ToString();
				object value = enumerator.Value;
				if (!flag)
				{
					builder.Append(", ");
				}
				this.SerializeString(aString, builder);
				builder.Append(":");
				if (!this.SerializeValue(value, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append("}");
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C8D8 File Offset: 0x0000ACD8
		protected bool SerializeArray(ArrayList anArray, StringBuilder builder)
		{
			builder.Append("[");
			bool flag = true;
			for (int i = 0; i < anArray.Count; i++)
			{
				object value = anArray[i];
				if (!flag)
				{
					builder.Append(", ");
				}
				if (!this.SerializeValue(value, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append("]");
			return true;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C944 File Offset: 0x0000AD44
		protected bool SerializeValue(object value, StringBuilder builder)
		{
			if (value == null)
			{
				builder.Append("null");
			}
			else if (value.GetType().IsArray)
			{
				this.SerializeArray(new ArrayList((ICollection)value), builder);
			}
			else if (value is string)
			{
				this.SerializeString((string)value, builder);
			}
			else if (value is char)
			{
				this.SerializeString(value.ToString(), builder);
			}
			else if (value is Hashtable)
			{
				this.SerializeObject((Hashtable)value, builder);
			}
			else if (value is ArrayList)
			{
				this.SerializeArray((ArrayList)value, builder);
			}
			else if (value is bool && (bool)value)
			{
				builder.Append("true");
			}
			else if (value is bool && !(bool)value)
			{
				builder.Append("false");
			}
			else
			{
				if (!(value is float))
				{
					return false;
				}
				this.SerializeNumber((float)value, builder);
			}
			return true;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000CA74 File Offset: 0x0000AE74
		protected void SerializeString(string aString, StringBuilder builder)
		{
			builder.Append("\"");
			foreach (char c in aString.ToCharArray())
			{
				if (c == '"')
				{
					builder.Append("\\\"");
				}
				else if (c == '\\')
				{
					builder.Append("\\\\");
				}
				else if (c == '\b')
				{
					builder.Append("\\b");
				}
				else if (c == '\f')
				{
					builder.Append("\\f");
				}
				else if (c == '\n')
				{
					builder.Append("\\n");
				}
				else if (c == '\r')
				{
					builder.Append("\\r");
				}
				else if (c == '\t')
				{
					builder.Append("\\t");
				}
				else
				{
					int num = (int)c;
					if (num >= 32 && num <= 126)
					{
						builder.Append(c);
					}
				}
			}
			builder.Append("\"");
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000CB80 File Offset: 0x0000AF80
		protected void SerializeNumber(float number, StringBuilder builder)
		{
			builder.Append(number.ToString());
		}

		// Token: 0x04000181 RID: 385
		public const int TOKEN_NONE = 0;

		// Token: 0x04000182 RID: 386
		public const int TOKEN_CURLY_OPEN = 1;

		// Token: 0x04000183 RID: 387
		public const int TOKEN_CURLY_CLOSE = 2;

		// Token: 0x04000184 RID: 388
		public const int TOKEN_SQUARED_OPEN = 3;

		// Token: 0x04000185 RID: 389
		public const int TOKEN_SQUARED_CLOSE = 4;

		// Token: 0x04000186 RID: 390
		public const int TOKEN_COLON = 5;

		// Token: 0x04000187 RID: 391
		public const int TOKEN_COMMA = 6;

		// Token: 0x04000188 RID: 392
		public const int TOKEN_STRING = 7;

		// Token: 0x04000189 RID: 393
		public const int TOKEN_NUMBER = 8;

		// Token: 0x0400018A RID: 394
		public const int TOKEN_TRUE = 9;

		// Token: 0x0400018B RID: 395
		public const int TOKEN_FALSE = 10;

		// Token: 0x0400018C RID: 396
		public const int TOKEN_NULL = 11;

		// Token: 0x0400018D RID: 397
		private const int BUILDER_CAPACITY = 2000;

		// Token: 0x0400018E RID: 398
		protected static GA_MiniJSON instance = new GA_MiniJSON();

		// Token: 0x0400018F RID: 399
		protected int lastErrorIndex = -1;

		// Token: 0x04000190 RID: 400
		protected string lastDecode = string.Empty;
	}
}
