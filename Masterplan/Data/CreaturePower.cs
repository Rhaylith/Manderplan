using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Masterplan.Data
{
	[Serializable]
	public class CreaturePower : IComparable<CreaturePower>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PowerAction fAction;

		private string fKeywords = "";

		private string fCondition = "";

		private string fRange = "";

		private PowerAttack fAttack;

		private string fDescription = "";

		private string fDetails = "";

		public PowerAction Action
		{
			get
			{
				return this.fAction;
			}
			set
			{
				this.fAction = value;
			}
		}

		public PowerAttack Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public CreaturePowerCategory Category
		{
			get
			{
				if (this.fAction == null)
				{
					return CreaturePowerCategory.Trait;
				}
				if (this.fAction.Trigger != null && this.fAction.Trigger != "")
				{
					return CreaturePowerCategory.Triggered;
				}
				switch (this.fAction.Action)
				{
					case ActionType.Standard:
					{
						return CreaturePowerCategory.Standard;
					}
					case ActionType.Move:
					{
						return CreaturePowerCategory.Move;
					}
					case ActionType.Minor:
					{
						return CreaturePowerCategory.Minor;
					}
					case ActionType.Reaction:
					case ActionType.Interrupt:
					case ActionType.Opportunity:
					{
						return CreaturePowerCategory.Triggered;
					}
					case ActionType.Free:
					{
						return CreaturePowerCategory.Free;
					}
				}
				return CreaturePowerCategory.Other;
			}
		}

		public string Condition
		{
			get
			{
				return this.fCondition;
			}
			set
			{
				this.fCondition = value;
			}
		}

		public string Damage
		{
			get
			{
				return AI.ExtractDamage(this.fDetails);
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public string Range
		{
			get
			{
				return this.fRange;
			}
			set
			{
				this.fRange = value;
			}
		}

		public CreaturePower()
		{
		}

		public List<string> AsHTML(CombatData cd, CardMode mode, bool functional_template)
		{
			bool flag = (mode != CardMode.Combat || cd == null ? false : cd.UsedPowers.Contains(this.fID));
			string str = "Actions";
			switch (this.Category)
			{
				case CreaturePowerCategory.Trait:
				{
					str = "Traits";
					break;
				}
				case CreaturePowerCategory.Standard:
				{
					str = "Standard Actions";
					break;
				}
				case CreaturePowerCategory.Move:
				{
					str = "Move Actions";
					break;
				}
				case CreaturePowerCategory.Minor:
				{
					str = "Minor Actions";
					break;
				}
				case CreaturePowerCategory.Free:
				{
					str = "Free Actions";
					break;
				}
				case CreaturePowerCategory.Triggered:
				{
					str = "Triggered Actions";
					break;
				}
				case CreaturePowerCategory.Other:
				{
					str = "Other Actions";
					break;
				}
			}
			List<string> strs = new List<string>();
			if (mode == CardMode.Build)
			{
				strs.Add("<TR class=creature>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<A href=power:action style=\"color:white\"><B>", str, "</B> (click here to change the action)</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (flag)
			{
				strs.Add("<TR class=shaded_dimmed>");
			}
			else
			{
				strs.Add("<TR class=shaded>");
			}
			strs.Add("<TD colspan=3>");
			strs.Add(this.power_topline(cd, mode));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (flag)
			{
				strs.Add("<TR class=dimmed>");
			}
			else
			{
				strs.Add("<TR>");
			}
			strs.Add("<TD colspan=3>");
			strs.Add(this.power_content(mode));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (mode == CardMode.Combat)
			{
				if (flag)
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					object[] d = new object[] { "<A href=\"refresh:", cd.ID, ";", this.fID, "\">(recharge this power)</A>" };
					strs.Add(string.Concat(d));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				else if (this.fAction != null && (this.fAction.Use == PowerUseType.Encounter || this.fAction.Use == PowerUseType.Daily))
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					object[] objArray = new object[] { "<A href=\"refresh:", cd.ID, ";", this.fID, "\">(use this power)</A>" };
					strs.Add(string.Concat(objArray));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			if (functional_template)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Note</B>: This power is part of a functional template, and so its attack bonus will be increased by the level of the creature it is applied to.");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			return strs;
		}

		public int CompareTo(CreaturePower rhs)
		{
			bool flag = false;
			bool flag1 = false;
			if (this.fAction != null && this.fAction.Use == PowerUseType.Basic)
			{
				flag = true;
			}
			if (rhs.Action != null && rhs.Action.Use == PowerUseType.Basic)
			{
				flag1 = true;
			}
			if (flag != flag1)
			{
				if (flag)
				{
					return -1;
				}
				if (flag1)
				{
					return 1;
				}
			}
			if (flag && flag1)
			{
				bool flag2 = this.fRange.ToLower().Contains("melee");
				bool flag3 = rhs.Range.ToLower().Contains("melee");
				if (flag2 != flag3)
				{
					if (flag2)
					{
						return -1;
					}
					if (flag3)
					{
						return 1;
					}
				}
			}
			if (!flag && !flag1)
			{
				bool flag4 = this.fRange.ToLower().Contains("double");
				bool flag5 = rhs.Range.ToLower().Contains("double");
				if (flag4 != flag5)
				{
					if (flag4)
					{
						return -1;
					}
					if (flag5)
					{
						return 1;
					}
				}
			}
			return this.fName.CompareTo(rhs.Name);
		}

		public CreaturePower Copy()
		{
			PowerAction powerAction;
			PowerAttack powerAttack;
			CreaturePower creaturePower = new CreaturePower()
			{
				ID = this.fID,
				Name = this.fName
			};
			CreaturePower creaturePower1 = creaturePower;
			if (this.fAction != null)
			{
				powerAction = this.fAction.Copy();
			}
			else
			{
				powerAction = null;
			}
			creaturePower1.Action = powerAction;
			creaturePower.Keywords = this.fKeywords;
			creaturePower.Condition = this.fCondition;
			creaturePower.Range = this.fRange;
			CreaturePower creaturePower2 = creaturePower;
			if (this.fAttack != null)
			{
				powerAttack = this.fAttack.Copy();
			}
			else
			{
				powerAttack = null;
			}
			creaturePower2.Attack = powerAttack;
			creaturePower.Description = this.fDescription;
			creaturePower.Details = this.fDetails;
			return creaturePower;
		}

		public void ExtractAttackDetails()
		{
			if (this.fAttack != null)
			{
				return;
			}
			if (!this.fDetails.Contains("vs"))
			{
				return;
			}
			string str = this.fDetails;
			string[] strArrays = new string[] { ";" };
			string[] strArrays1 = str.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			this.fDetails = "";
			string[] strArrays2 = strArrays1;
			for (int i = 0; i < (int)strArrays2.Length; i++)
			{
				string str1 = strArrays2[i].Trim();
				bool flag = false;
				int num = str1.IndexOf("vs");
				if (num != -1 && this.fAttack == null)
				{
					string str2 = str1.Substring(0, num);
					string str3 = str1.Substring(num);
					int num1 = str2.LastIndexOfAny("1234567890".ToCharArray());
					if (num1 != -1)
					{
						int num2 = 0;
						DefenceType defenceType = DefenceType.AC;
						bool flag1 = false;
						bool flag2 = false;
						if (str3.Contains("AC"))
						{
							defenceType = DefenceType.AC;
							flag2 = true;
						}
						if (str3.Contains("Fort"))
						{
							defenceType = DefenceType.Fortitude;
							flag2 = true;
						}
						if (str3.Contains("Ref"))
						{
							defenceType = DefenceType.Reflex;
							flag2 = true;
						}
						if (str3.Contains("Will"))
						{
							defenceType = DefenceType.Will;
							flag2 = true;
						}
						if (flag2)
						{
							try
							{
								num1 = Math.Max(0, num1 - 2);
								num2 = int.Parse(str2.Substring(num1));
								flag1 = true;
							}
							catch
							{
								flag1 = false;
							}
						}
						if (flag1 && flag2)
						{
							this.fAttack = new PowerAttack()
							{
								Bonus = num2,
								Defence = defenceType
							};
							flag = true;
						}
					}
				}
				if (!flag)
				{
					if (this.fDetails != "")
					{
						CreaturePower creaturePower = this;
						creaturePower.fDetails = string.Concat(creaturePower.fDetails, "; ");
					}
					CreaturePower creaturePower1 = this;
					creaturePower1.fDetails = string.Concat(creaturePower1.fDetails, str1);
				}
			}
		}

		private string power_content(CardMode mode)
		{
			string str;
			List<string> strs = new List<string>();
			string str1 = "";
			if (this.fDescription != null)
			{
				str1 = HTML.Process(this.fDescription, true);
			}
			if (str1 == null)
			{
				str1 = "";
			}
			if (mode == CardMode.Build)
			{
				if (str1 == "")
				{
					str1 = "Set read-aloud description (optional)";
				}
				str1 = string.Concat("<A href=power:desc>", str1, "</A>");
			}
			if (str1 != "")
			{
				strs.Add(string.Concat("<I>", str1, "</I>"));
			}
			if (mode == CardMode.Build)
			{
				strs.Add("");
			}
			if (this.fAction != null && this.fAction.Trigger != "")
			{
				ActionType action = this.fAction.Action;
				if (action == ActionType.None)
				{
					str = "no action";
				}
				else
				{
					switch (action)
					{
						case ActionType.Reaction:
						{
							str = "immediate reaction";
							break;
						}
						case ActionType.Interrupt:
						{
							str = "immediate interrupt";
							break;
						}
						default:
						{
							str = string.Concat(this.fAction.Action.ToString().ToLower(), " action");
							break;
						}
					}
				}
				if (mode == CardMode.Build)
				{
					string[] trigger = new string[] { "Trigger (<A href=power:action>", str, "</A>): <A href=power:action>", this.fAction.Trigger, "</A>" };
					strs.Add(string.Concat(trigger));
				}
				else
				{
					strs.Add(string.Concat("Trigger (", str, "): ", this.fAction.Trigger));
				}
			}
			string str2 = HTML.Process(this.fCondition, true);
			if (str2 == "" && mode == CardMode.Build)
			{
				str2 = "No prerequisite";
			}
			if (str2 != "")
			{
				if (mode == CardMode.Build)
				{
					str2 = string.Concat("<A href=power:prerequisite>", str2, "</A>");
				}
				str2 = string.Concat("Prerequisite: ", str2);
				strs.Add(str2);
			}
			string str3 = (this.fRange != null ? this.fRange : "");
			string str4 = (this.fAttack != null ? this.fAttack.ToString() : "");
			if (mode == CardMode.Build)
			{
				str3 = (str3 != "" ? string.Concat("<A href=power:range>", str3, "</A>") : "<A href=power:range>The power's range and its target(s) are not set</A>");
				str4 = (str4 != "" ? string.Concat("<A href=power:attack>", str4, "</A> <A href=power:clearattack>(clear attack)</A>") : "<A href=power:attack>Click here to make this an attack power</A>");
			}
			if (str3 != "")
			{
				strs.Add(string.Concat("Range: ", str3));
			}
			if (str4 != "")
			{
				strs.Add(string.Concat("Attack: ", str4));
			}
			if (mode == CardMode.Build)
			{
				strs.Add("");
			}
			string str5 = HTML.Process(this.fDetails, true) ?? "";
			if (mode == CardMode.Build)
			{
				if (str5 == "")
				{
					str5 = "Specify the power's effects";
				}
				str5 = string.Concat("<A href=power:details>", str5, "</A>");
			}
			if (str5 != "")
			{
				strs.Add(str5);
			}
			if (mode == CardMode.Build)
			{
				strs.Add("");
			}
			if (this.fAction != null && this.fAction.SustainAction != ActionType.None)
			{
				string str6 = this.fAction.SustainAction.ToString();
				if (mode == CardMode.Build)
				{
					str6 = string.Concat("<A href=power:action>", str6, "</A>");
				}
				strs.Add(string.Concat("Sustain: ", str6));
			}
			string str7 = "";
			foreach (string str8 in strs)
			{
				if (str7 != "")
				{
					str7 = string.Concat(str7, "<BR>");
				}
				str7 = string.Concat(str7, str8);
			}
			if (str7 == "")
			{
				str7 = "(no details)";
			}
			return str7;
		}

		private string power_parenthesis(CardMode mode)
		{
			if (this.fCondition == "" && this.fAction == null)
			{
				return "";
			}
			string str = "";
			if (this.fAction != null)
			{
				string str1 = this.fAction.ToString();
				if (mode == CardMode.Build)
				{
					str1 = string.Concat("<A href=power:action>", str1, "</A>");
				}
				str = string.Concat(str, str1);
			}
			return str;
		}

		private string power_topline(CombatData cd, CardMode mode)
		{
			string str = "";
			Image area = null;
			string lower = this.fRange.ToLower();
			if (lower.Contains("melee"))
			{
				area = (this.fAction == null || this.fAction.Use != PowerUseType.Basic ? Resources.Melee : Resources.MeleeBasic);
			}
			if (lower.Contains("ranged"))
			{
				area = (this.fAction == null || this.fAction.Use != PowerUseType.Basic ? Resources.Ranged : Resources.RangedBasic);
			}
			if (lower.Contains("area"))
			{
				area = Resources.Area;
			}
			if (lower.Contains("close"))
			{
				area = Resources.Close;
			}
			if (area == null && this.fAttack != null && this.fAction != null)
			{
				area = (this.fAction.Use != PowerUseType.Basic ? Resources.Melee : Resources.MeleeBasic);
			}
			str = string.Concat(str, "<B>", HTML.Process(this.fName, true), "</B>");
			if (mode == CardMode.Combat && cd != null)
			{
				bool flag = false;
				if (!cd.UsedPowers.Contains(this.fID))
				{
					if (this.fAttack != null)
					{
						flag = true;
					}
					if (this.fAction != null && this.fAction.Use == PowerUseType.Encounter)
					{
						flag = true;
					}
				}
				if (flag)
				{
					object[] d = new object[] { "<A href=\"power:", cd.ID, ";", this.fID, "\">", str, "</A>" };
					str = string.Concat(d);
				}
			}
			if (mode == CardMode.Build)
			{
				str = string.Concat("<A href=power:info>", str, "</A>");
			}
			if (area != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				area.Save(memoryStream, ImageFormat.Png);
				string base64String = Convert.ToBase64String(memoryStream.ToArray());
				if (base64String != null && base64String != "")
				{
					str = string.Concat("<img src=data:image/png;base64,", base64String, ">", str);
				}
			}
			if (this.fKeywords != "")
			{
				string str1 = HTML.Process(this.fKeywords, true);
				if (mode == CardMode.Build)
				{
					str1 = string.Concat("<A href=power:info>", str1, "</A>");
				}
				str = string.Concat(str, " (", str1, ")");
			}
			string str2 = this.power_parenthesis(mode);
			if (str2 != "")
			{
				str = string.Concat(str, " &diams; ", str2);
			}
			return str;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}