using Masterplan;
using Masterplan.Data;
using Masterplan.Properties;
using Masterplan.Tools.Generators;
using Masterplan.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Utils;
using Utils.Text;

namespace Masterplan.Tools
{
	internal class HTML
	{
		private static Markdown fMarkdown;

		private string fRelativePath = "";

		private string fFullPath = "";

		private List<Pair<string, Plot>> fPlots = new List<Pair<string, Plot>>();

		private Dictionary<Guid, List<Guid>> fMaps = new Dictionary<Guid, List<Guid>>();

		private static Dictionary<DisplaySize, List<string>> fStyles;

		static HTML()
		{
			HTML.fMarkdown = new Markdown();
			HTML.fStyles = new Dictionary<DisplaySize, List<string>>();
		}

		public HTML()
		{
		}

		private void add_map(Guid map_id, Guid area_id)
		{
			if (map_id == Guid.Empty)
			{
				return;
			}
			if (!this.fMaps.ContainsKey(map_id))
			{
				this.fMaps[map_id] = new List<Guid>();
			}
			if (!this.fMaps[map_id].Contains(area_id))
			{
				this.fMaps[map_id].Add(area_id);
			}
		}

		public static string Artifact(Artifact artifact, DisplaySize size, bool builder, bool wrapper)
		{
			List<string> strs = new List<string>();
			if (wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetHead(null, null, size));
			}
			strs.Add("<BODY>");
			if (artifact == null)
			{
				strs.Add("<P class=instruction>(no item selected)</P>");
			}
			else
			{
				strs.AddRange(HTML.get_artifact(artifact, builder));
			}
			if (wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string Background(Background bg, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (bg == null)
			{
				strs.Add("<P class=instruction>(no background selected)</P>");
			}
			else
			{
				string str = HTML.Process(bg.Details, false);
				if (str == "")
				{
					strs.Add("<P class=instruction>Press <A href=\"background:edit\">Edit</A> to add information to this item.</P>");
				}
				else
				{
					str = HTML.fMarkdown.Transform(str);
					str = str.Replace("<p>", "<p class=background>");
					strs.Add(str);
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string Background(List<Background> backgrounds, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			foreach (Background background in backgrounds)
			{
				string str = HTML.Process(background.Title, false);
				string str1 = HTML.Process(background.Details, false);
				if (!(str != "") || !(str1 != ""))
				{
					continue;
				}
				strs.Add(string.Concat("<H3>", str, "</H3>"));
				str1 = HTML.fMarkdown.Transform(str1);
				str1 = str1.Replace("<p>", "<p class=background>");
				strs.Add(str1);
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string Concatenate(List<string> lines)
		{
			string str = "";
			foreach (string line in lines)
			{
				if (str != "")
				{
					str = string.Concat(str, Environment.NewLine);
				}
				str = string.Concat(str, line);
			}
			return str;
		}

		public static string CreatureTemplate(CreatureTemplate template, DisplaySize size, bool builder)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=template>");
			strs.Add("<TD colspan=2>");
			strs.Add(string.Concat("<B>", HTML.Process(template.Name, true), "</B>"));
			strs.Add("</TD>");
			strs.Add("<TD>");
			strs.Add(string.Concat("<B>", HTML.Process(template.Info, true), "</B>"));
			strs.Add("</TD>");
			if (builder)
			{
				strs.Add("<TR class=template>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TR>");
			string str = template.Initiative.ToString();
			if (template.Initiative >= 0)
			{
				str = string.Concat("+", str);
			}
			if (builder)
			{
				str = string.Concat("<A href=build:combat>", str, "</A>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>Initiative</B> ", str));
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str1 = HTML.Process(template.Movement, true);
			if (str1 != "" || builder)
			{
				if (builder)
				{
					if (str1 == "")
					{
						str1 = "no additional movement modes";
					}
					str1 = string.Concat("<A href=build:movement>", str1, "</A>");
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Movement</B> ", str1));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (template.Senses != "" || builder)
			{
				int num = 2;
				if (template.Resist != "" || template.Vulnerable != "" || template.Immune != "" || template.DamageModifierTemplates.Count != 0)
				{
					num++;
				}
				string str2 = HTML.Process(template.Senses, true);
				if (builder)
				{
					if (str2 == "")
					{
						str2 = "no additional senses";
					}
					str2 = string.Concat("<A href=build:senses>", str2, "</A>");
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Senses</B> ", str2));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str3 = "";
			if (template.AC != 0)
			{
				string str4 = (template.AC > 0 ? "+" : "");
				object obj = str3;
				object[] aC = new object[] { obj, str4, template.AC, " AC" };
				str3 = string.Concat(aC);
			}
			if (template.Fortitude != 0)
			{
				if (str3 != "")
				{
					str3 = string.Concat(str3, "; ");
				}
				string str5 = (template.Fortitude > 0 ? "+" : "");
				object obj1 = str3;
				object[] fortitude = new object[] { obj1, str5, template.Fortitude, " Fort" };
				str3 = string.Concat(fortitude);
			}
			if (template.Reflex != 0)
			{
				if (str3 != "")
				{
					str3 = string.Concat(str3, "; ");
				}
				string str6 = (template.Reflex > 0 ? "+" : "");
				object obj2 = str3;
				object[] reflex = new object[] { obj2, str6, template.Reflex, " Ref" };
				str3 = string.Concat(reflex);
			}
			if (template.Will != 0)
			{
				if (str3 != "")
				{
					str3 = string.Concat(str3, "; ");
				}
				string str7 = (template.Will > 0 ? "+" : "");
				object obj3 = str3;
				object[] will = new object[] { obj3, str7, template.Will, " Will" };
				str3 = string.Concat(will);
			}
			if (str3 != "" || builder)
			{
				if (builder)
				{
					if (str3 == "")
					{
						str3 = "no defence bonuses";
					}
					str3 = string.Concat("<A href=build:combat>", str3, "</A>");
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Defences</B> ", str3));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str8 = HTML.Process(template.Resist, true);
			string str9 = HTML.Process(template.Vulnerable, true);
			string str10 = HTML.Process(template.Immune, true);
			if (str8 == null)
			{
				str8 = "";
			}
			if (str9 == null)
			{
				str9 = "";
			}
			if (str10 == null)
			{
				str10 = "";
			}
			foreach (DamageModifierTemplate damageModifierTemplate in template.DamageModifierTemplates)
			{
				int heroicValue = damageModifierTemplate.HeroicValue + damageModifierTemplate.ParagonValue + damageModifierTemplate.EpicValue;
				if (heroicValue == 0)
				{
					if (str10 != "")
					{
						str10 = string.Concat(str10, ", ");
					}
					str10 = string.Concat(str10, damageModifierTemplate.Type.ToString().ToLower());
				}
				if (heroicValue > 0)
				{
					if (str9 != "")
					{
						str9 = string.Concat(str9, ", ");
					}
					object obj4 = str9;
					object[] objArray = new object[] { obj4, damageModifierTemplate.HeroicValue, "/", damageModifierTemplate.ParagonValue, "/", damageModifierTemplate.EpicValue, " ", damageModifierTemplate.Type.ToString().ToLower() };
					str9 = string.Concat(objArray);
				}
				if (heroicValue >= 0)
				{
					continue;
				}
				if (str8 != "")
				{
					str8 = string.Concat(str8, ", ");
				}
				int num1 = Math.Abs(damageModifierTemplate.HeroicValue);
				int num2 = Math.Abs(damageModifierTemplate.ParagonValue);
				int num3 = Math.Abs(damageModifierTemplate.EpicValue);
				object obj5 = str8;
				object[] lower = new object[] { obj5, num1, "/", num2, "/", num3, " ", damageModifierTemplate.Type.ToString().ToLower() };
				str8 = string.Concat(lower);
			}
			string str11 = "";
			if (str10 != "")
			{
				if (str11 != "")
				{
					str11 = string.Concat(str11, " ");
				}
				str11 = string.Concat(str11, "<B>Immune</B> ", str10);
			}
			if (str8 != "")
			{
				if (str11 != "")
				{
					str11 = string.Concat(str11, " ");
				}
				str11 = string.Concat(str11, "<B>Resist</B> ", str8);
			}
			if (str9 != "")
			{
				if (str11 != "")
				{
					str11 = string.Concat(str11, " ");
				}
				str11 = string.Concat(str11, "<B>Vulnerable</B> ", str9);
			}
			if (str11 != "" || builder)
			{
				if (builder)
				{
					if (str11 == "")
					{
						str11 = "Set resistances / vulnerabilities / immunities";
					}
					str11 = string.Concat("<A href=build:damage>", str11, "</A>");
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(str11);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Saving Throws</B> +2");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Action Point</B> 1");
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str12 = string.Concat("+", template.HP, " per level + Constitution score");
			if (builder)
			{
				str12 = string.Concat("<A href=build:combat>", str12, "</A>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>HP</B> ", str12));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (builder)
			{
				strs.Add("<TR class=template>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Powers and Traits</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=power:addtrait>add a trait</A>");
				strs.Add("|");
				strs.Add("<A href=power:addpower>add a power</A>");
				strs.Add("|");
				strs.Add("<A href=power:addaura>add an aura</A>");
				if (template.Regeneration == null)
				{
					strs.Add("|");
					strs.Add("<A href=power:regenedit>add regeneration</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			Dictionary<CreaturePowerCategory, List<CreaturePower>> creaturePowerCategories = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
			Array values = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory value in values)
			{
				creaturePowerCategories[value] = new List<CreaturePower>();
			}
			foreach (CreaturePower creaturePower in template.CreaturePowers)
			{
				creaturePowerCategories[creaturePower.Category].Add(creaturePower);
			}
			foreach (CreaturePowerCategory creaturePowerCategory in values)
			{
				int count = creaturePowerCategories[creaturePowerCategory].Count;
				if (creaturePowerCategory == CreaturePowerCategory.Trait)
				{
					count += template.Auras.Count;
					if (template.Regeneration != null)
					{
						count++;
					}
				}
				if (count == 0)
				{
					continue;
				}
				string str13 = "";
				switch (creaturePowerCategory)
				{
					case CreaturePowerCategory.Trait:
					{
						str13 = "Traits";
						break;
					}
					case CreaturePowerCategory.Standard:
					case CreaturePowerCategory.Move:
					case CreaturePowerCategory.Minor:
					case CreaturePowerCategory.Free:
					{
						str13 = string.Concat(creaturePowerCategory, " Actions");
						break;
					}
					case CreaturePowerCategory.Triggered:
					{
						str13 = "Triggered Actions";
						break;
					}
					case CreaturePowerCategory.Other:
					{
						str13 = "Other Actions";
						break;
					}
				}
				strs.Add("<TR class=creature>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", str13, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (creaturePowerCategory == CreaturePowerCategory.Trait)
				{
					foreach (Aura aura in template.Auras)
					{
						string str14 = HTML.Process(aura.Details.Trim(), true);
						str14 = (str14.StartsWith("aura", StringComparison.OrdinalIgnoreCase) ? string.Concat("A", str14.Substring(1)) : string.Concat("Aura ", str14));
						MemoryStream memoryStream = new MemoryStream();
						Resources.Aura.Save(memoryStream, ImageFormat.Png);
						string base64String = Convert.ToBase64String(memoryStream.ToArray());
						strs.Add("<TR class=shaded>");
						strs.Add("<TD colspan=3>");
						strs.Add(string.Concat("<img src=data:image/png;base64,", base64String, ">"));
						strs.Add(string.Concat("<B>", HTML.Process(aura.Name, true), "</B>"));
						if (aura.Keywords != "")
						{
							strs.Add(string.Concat("(", aura.Keywords, ")"));
						}
						if (aura.Radius > 0)
						{
							strs.Add(string.Concat(" &diams; Aura ", aura.Radius));
						}
						strs.Add("</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD colspan=3>");
						strs.Add(str14);
						strs.Add("</TD>");
						strs.Add("</TR>");
						if (!builder)
						{
							continue;
						}
						strs.Add("<TR>");
						strs.Add("<TD colspan=3 align=center>");
						strs.Add(string.Concat("<A href=auraedit:", aura.ID, ">edit</A>"));
						strs.Add("|");
						strs.Add(string.Concat("<A href=auraremove:", aura.ID, ">remove</A>"));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					if (template.Regeneration != null)
					{
						strs.Add("<TR class=shaded>");
						strs.Add("<TD colspan=3>");
						strs.Add("<B>Regeneration</B>");
						strs.Add("</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD colspan=3>");
						strs.Add(string.Concat("Regeneration ", HTML.Process(template.Regeneration.ToString(), true)));
						strs.Add("</TD>");
						strs.Add("</TR>");
						if (builder)
						{
							strs.Add("<TR>");
							strs.Add("<TD colspan=3 align=center>");
							strs.Add("<A href=power:regenedit>edit</A>");
							strs.Add("|");
							strs.Add("<A href=power:regenremove>remove</A>");
							strs.Add("</TD>");
							strs.Add("</TR>");
						}
					}
				}
				foreach (CreaturePower item in creaturePowerCategories[creaturePowerCategory])
				{
					strs.AddRange(item.AsHTML(null, CardMode.View, false));
					if (!builder)
					{
						continue;
					}
					strs.Add("<TR>");
					strs.Add("<TD colspan=3 align=center>");
					strs.Add(string.Concat("<A href=\"poweredit:", item.ID, "\">edit this power</A>"));
					strs.Add("|");
					strs.Add(string.Concat("<A href=\"powerremove:", item.ID, "\">remove this power</A>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			if (template.Tactics != "" || builder)
			{
				string str15 = HTML.Process(template.Tactics, true);
				if (builder)
				{
					if (str15 == "")
					{
						str15 = "no tactics specified";
					}
					str15 = string.Concat("<A href=build:tactics>", str15, "</A>");
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Tactics</B> ", str15));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			Library library = Session.FindLibrary(template);
			if (library != null && library.Name != "" && (Session.Project == null || library != Session.Project.Library))
			{
				string str16 = HTML.Process(library.Name, true);
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(str16);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string CustomMapToken(CustomToken ct, bool drag, bool include_wrapper, DisplaySize size)
		{
			List<string> strs = new List<string>();
			if (include_wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetStyle(size));
				strs.Add("<BODY>");
			}
			if (drag)
			{
				strs.Add("<P class=instruction>Drag this item from the list onto the map.</P>");
			}
			strs.AddRange(HTML.get_custom_token(ct));
			if (include_wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string EncounterNote(EncounterNote en, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (en == null)
			{
				strs.Add("<P class=instruction>(no note selected)</P>");
			}
			else
			{
				strs.Add(string.Concat("<H3>", HTML.Process(en.Title, true), "</H3>"));
				string str = HTML.Process(en.Contents, false);
				if (str == "")
				{
					strs.Add("<P class=instruction>This note has no contents.</P>");
					strs.Add("<P class=instruction>Press <A href=\"note:edit\">Edit</A> to add information to this note.</P>");
				}
				else
				{
					str = HTML.fMarkdown.Transform(str);
					str = str.Replace("<p>", "<p class=encounter_note>");
					strs.Add(str);
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string EncounterReportTable(ReportTable table, DisplaySize size)
		{
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead("Encounter Log", "", DisplaySize.Small));
			strs.Add("<BODY>");
			string str = "";
			switch (table.ReportType)
			{
				case ReportType.Time:
				{
					str = "Time Taken";
					break;
				}
				case ReportType.DamageToEnemies:
				{
					str = "Damage (to opponents)";
					break;
				}
				case ReportType.DamageToAllies:
				{
					str = "Damage (to allies)";
					break;
				}
				case ReportType.Movement:
				{
					str = "Movement";
					break;
				}
			}
			switch (table.BreakdownType)
			{
				case BreakdownType.Controller:
				{
					str = string.Concat(str, " (by controller)");
					break;
				}
				case BreakdownType.Faction:
				{
					str = string.Concat(str, " (by faction)");
					break;
				}
			}
			strs.Add("<H3>");
			strs.Add(str);
			strs.Add("</H3>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE class=wide>");
			strs.Add("<TR class=encounterlog>");
			strs.Add("<TD align=center>");
			strs.Add("<B>Combatant</B>");
			strs.Add("</TD>");
			for (int i = 1; i <= table.Rounds; i++)
			{
				strs.Add("<TD align=right>");
				strs.Add(string.Concat("<B>Round ", i, "</B>"));
				strs.Add("</TD>");
			}
			strs.Add("<TD align=right>");
			strs.Add("<B>Total</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			foreach (ReportRow row in table.Rows)
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", row.Heading, "</B>"));
				strs.Add("</TD>");
				for (int j = 0; j <= table.Rounds - 1; j++)
				{
					strs.Add("<TD align=right>");
					switch (table.ReportType)
					{
						case ReportType.Time:
						{
							TimeSpan timeSpan = new TimeSpan(0, 0, row.Values[j]);
							if (timeSpan.TotalSeconds < 1)
							{
								strs.Add("-");
								break;
							}
							else
							{
								strs.Add(HTML.get_time(timeSpan));
								break;
							}
						}
						case ReportType.DamageToEnemies:
						case ReportType.DamageToAllies:
						case ReportType.Movement:
						{
							int item = row.Values[j];
							if (item == 0)
							{
								strs.Add("-");
								break;
							}
							else
							{
								strs.Add(item.ToString());
								break;
							}
						}
					}
					strs.Add("</TD>");
				}
				strs.Add("<TD align=right>");
				switch (table.ReportType)
				{
					case ReportType.Time:
					{
						TimeSpan timeSpan1 = new TimeSpan(0, 0, row.Total);
						if (timeSpan1.TotalSeconds < 1)
						{
							strs.Add("-");
							break;
						}
						else
						{
							strs.Add(HTML.get_time(timeSpan1));
							break;
						}
					}
					case ReportType.DamageToEnemies:
					case ReportType.DamageToAllies:
					case ReportType.Movement:
					{
						int total = row.Total;
						if (total == 0)
						{
							strs.Add("-");
							break;
						}
						else
						{
							strs.Add(total.ToString());
							break;
						}
					}
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add("<B>Totals</B>");
			strs.Add("</TD>");
			for (int k = 0; k <= table.Rounds - 1; k++)
			{
				strs.Add("<TD align=right>");
				switch (table.ReportType)
				{
					case ReportType.Time:
					{
						TimeSpan timeSpan2 = new TimeSpan(0, 0, table.Rows[k].Total);
						if (timeSpan2.TotalSeconds < 1)
						{
							strs.Add("-");
							break;
						}
						else
						{
							strs.Add(HTML.get_time(timeSpan2));
							break;
						}
					}
					case ReportType.DamageToEnemies:
					case ReportType.DamageToAllies:
					case ReportType.Movement:
					{
						int num = table.Rows[k].Total;
						if (num == 0)
						{
							strs.Add("-");
							break;
						}
						else
						{
							strs.Add(num.ToString());
							break;
						}
					}
				}
				strs.Add("</TD>");
			}
			strs.Add("<TD align=right>");
			switch (table.ReportType)
			{
				case ReportType.Time:
				{
					TimeSpan timeSpan3 = new TimeSpan(0, 0, table.GrandTotal);
					if (timeSpan3.TotalSeconds < 1)
					{
						strs.Add("-");
						break;
					}
					else
					{
						strs.Add(HTML.get_time(timeSpan3));
						break;
					}
				}
				case ReportType.DamageToEnemies:
				case ReportType.DamageToAllies:
				case ReportType.Movement:
				{
					int grandTotal = table.GrandTotal;
					if (grandTotal == 0)
					{
						strs.Add("-");
						break;
					}
					else
					{
						strs.Add(grandTotal.ToString());
						break;
					}
				}
			}
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string EncyclopediaEntry(EncyclopediaEntry entry, Encyclopedia encyclopedia, DisplaySize size, bool include_dm_info, bool include_entry_links, bool include_attachment, bool include_picture_links)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (entry != null)
			{
				strs.Add(string.Concat("<H4>", HTML.Process(entry.Name, true), "</H4>"));
				strs.Add("<HR>");
			}
			if (entry == null)
			{
				strs.Add("<P class=instruction>(no entry selected)</P>");
			}
			else
			{
				if (include_attachment && entry.AttachmentID != Guid.Empty)
				{
					MapLocation mapLocation = null;
					List<RegionalMap>.Enumerator enumerator = Session.Project.RegionalMaps.GetEnumerator();
					try
					{
						do
						{
							if (!enumerator.MoveNext())
							{
								break;
							}
							mapLocation = enumerator.Current.FindLocation(entry.AttachmentID);
						}
						while (mapLocation == null);
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					if (mapLocation != null)
					{
						strs.Add(string.Concat("<P class=instruction><A href=\"map:", entry.AttachmentID, "\">View location on map</A>.</P>"));
						strs.Add("<HR>");
					}
				}
				string str = HTML.process_encyclopedia_info(entry.Details, encyclopedia, include_entry_links, include_dm_info);
				string str1 = HTML.process_encyclopedia_info(entry.DMInfo, encyclopedia, include_entry_links, include_dm_info);
				if (str == "" && str1 == "")
				{
					strs.Add("<P class=instruction>Press <A href=\"entry:edit\">Edit</A> to add information to this entry.</P>");
				}
				if (str != "")
				{
					strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(str, false), "</P>"));
				}
				if (include_dm_info && str1 != "")
				{
					strs.Add("<H4>For DMs Only</H4>");
					strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(str1, false), "</P>"));
				}
				if (include_picture_links && entry.Images.Count != 0)
				{
					strs.Add("<H4>Pictures</H4>");
					strs.Add("<UL>");
					foreach (EncyclopediaImage image in entry.Images)
					{
						strs.Add("<LI>");
						object[] d = new object[] { "<A href=picture:", image.ID, ">", image.Name, "</A>" };
						strs.Add(string.Concat(d));
						strs.Add("</LI>");
					}
					strs.Add("</UL>");
				}
				if (include_attachment && entry.AttachmentID != Guid.Empty)
				{
					Hero hero = Session.Project.FindHero(entry.AttachmentID);
					if (hero != null)
					{
						strs.AddRange(HTML.get_hero(hero, null, false, false));
					}
					ICreature creature = Session.FindCreature(entry.AttachmentID, SearchType.Global);
					if (creature != null)
					{
						EncounterCard encounterCard = new EncounterCard(creature.ID);
						strs.Add("<P class=table>");
						strs.AddRange(encounterCard.AsText(null, CardMode.View, true));
						strs.Add("</P>");
					}
					IPlayerOption playerOption = Session.Project.FindPlayerOption(entry.AttachmentID);
					if (playerOption != null)
					{
						strs.AddRange(HTML.get_player_option(playerOption));
					}
				}
				if (include_entry_links && encyclopedia != null)
				{
					List<EncyclopediaLink> encyclopediaLinks = new List<EncyclopediaLink>();
					foreach (EncyclopediaLink link in encyclopedia.Links)
					{
						if (!link.EntryIDs.Contains(entry.ID))
						{
							continue;
						}
						encyclopediaLinks.Add(link);
					}
					if (encyclopediaLinks.Count != 0)
					{
						strs.Add("<HR>");
						strs.Add("<P><B>See also</B>:</P>");
						strs.Add("<UL>");
						foreach (EncyclopediaLink encyclopediaLink in encyclopediaLinks)
						{
							foreach (Guid entryID in encyclopediaLink.EntryIDs)
							{
								if (entryID == entry.ID)
								{
									continue;
								}
								EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(entryID);
								object[] objArray = new object[] { "<LI><A href=\"entry:", entryID, "\">", HTML.Process(encyclopediaEntry.Name, true), "</A></LI>" };
								strs.Add(string.Concat(objArray));
							}
						}
						strs.Add("</UL>");
					}
					List<EncyclopediaGroup> encyclopediaGroups = new List<EncyclopediaGroup>();
					foreach (EncyclopediaGroup group in encyclopedia.Groups)
					{
						if (!group.EntryIDs.Contains(entry.ID))
						{
							continue;
						}
						encyclopediaGroups.Add(group);
					}
					if (encyclopediaGroups.Count != 0)
					{
						strs.Add("<HR>");
						strs.Add("<P><B>Groups</B>:</P>");
						foreach (EncyclopediaGroup encyclopediaGroup in encyclopediaGroups)
						{
							strs.Add("<P class=table>");
							strs.Add("<TABLE class=wide>");
							strs.Add("<TR class=shaded align=center>");
							strs.Add("<TD>");
							object[] d1 = new object[] { "<B><A href=\"group:", encyclopediaGroup.ID, "\">", HTML.Process(encyclopediaGroup.Name, true), "</A></B>" };
							strs.Add(string.Concat(d1));
							strs.Add("</TD>");
							strs.Add("</TR>");
							strs.Add("<TR>");
							strs.Add("<TD>");
							List<EncyclopediaEntry> encyclopediaEntries = new List<EncyclopediaEntry>();
							foreach (Guid guid in encyclopediaGroup.EntryIDs)
							{
								EncyclopediaEntry encyclopediaEntry1 = encyclopedia.FindEntry(guid);
								if (encyclopediaEntry1 == null)
								{
									continue;
								}
								encyclopediaEntries.Add(encyclopediaEntry1);
							}
							encyclopediaEntries.Sort();
							foreach (EncyclopediaEntry encyclopediaEntry2 in encyclopediaEntries)
							{
								if (encyclopediaEntry2 == entry)
								{
									strs.Add(string.Concat("<B>", HTML.Process(encyclopediaEntry2.Name, true), "</B>"));
								}
								else
								{
									object[] objArray1 = new object[] { "<A href=\"entry:", encyclopediaEntry2.ID, "\">", HTML.Process(encyclopediaEntry2.Name, true), "</A>" };
									strs.Add(string.Concat(objArray1));
								}
							}
							strs.Add("</TD>");
							strs.Add("</TR>");
							strs.Add("</TABLE>");
						}
					}
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string EncyclopediaGroup(EncyclopediaGroup group, Encyclopedia encyclopedia, DisplaySize size, bool include_dm_info, bool include_entry_links)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (group == null)
			{
				strs.Add("<P class=instruction>(no group selected)</P>");
			}
			else if (encyclopedia != null)
			{
				List<EncyclopediaEntry> encyclopediaEntries = new List<EncyclopediaEntry>();
				foreach (Guid entryID in group.EntryIDs)
				{
					EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(entryID);
					if (encyclopediaEntry == null)
					{
						continue;
					}
					encyclopediaEntries.Add(encyclopediaEntry);
				}
				if (encyclopediaEntries.Count == 0)
				{
					strs.Add("<P class=instruction>(no entries in group)</P>");
				}
				else
				{
					foreach (EncyclopediaEntry encyclopediaEntry1 in encyclopediaEntries)
					{
						strs.Add(string.Concat("<H3>", HTML.Process(encyclopediaEntry1.Name, true), "</H3>"));
						string str = HTML.process_encyclopedia_info(encyclopediaEntry1.Details, encyclopedia, include_entry_links, include_dm_info);
						strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(str, false), "</P>"));
					}
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public bool ExportProject(string filename)
		{
			bool flag;
			try
			{
				string str = FileName.Directory(filename);
				this.fRelativePath = string.Concat(FileName.Name(filename), " Files", Path.DirectorySeparatorChar);
				this.fFullPath = string.Concat(str, this.fRelativePath);
				StreamWriter streamWriter = new StreamWriter(filename);
				foreach (string _content in this.get_content())
				{
					streamWriter.WriteLine(_content);
				}
				streamWriter.Close();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
				flag = false;
				return flag;
			}
			if (this.fPlots.Count != 0 || this.fMaps.Keys.Count != 0)
			{
				Directory.CreateDirectory(this.fFullPath);
			}
			foreach (Pair<string, Plot> fPlot in this.fPlots)
			{
				try
				{
					Bitmap bitmap = Screenshot.Plot(fPlot.Second, new Size(800, 600));
					string _filename = this.get_filename(fPlot.First, "jpg", true);
					bitmap.Save(_filename, ImageFormat.Jpeg);
				}
				catch (Exception exception1)
				{
					LogSystem.Trace(exception1);
					flag = false;
					return flag;
				}
			}
			Dictionary<Guid, List<Guid>>.KeyCollection.Enumerator enumerator = this.fMaps.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Guid current = enumerator.Current;
					try
					{
						Map map = Session.Project.FindTacticalMap(current);
						List<Guid>.Enumerator enumerator1 = this.fMaps[current].GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								Guid guid = enumerator1.Current;
								Rectangle empty = Rectangle.Empty;
								if (guid != Guid.Empty)
								{
									empty = map.FindArea(guid).Region;
								}
								Bitmap bitmap1 = Screenshot.Map(map, empty, null, null, null);
								string mapName = this.get_map_name(current, guid);
								string _filename1 = this.get_filename(mapName, "jpg", true);
								bitmap1.Save(_filename1, ImageFormat.Jpeg);
							}
						}
						finally
						{
							((IDisposable)enumerator1).Dispose();
						}
					}
					catch (Exception exception2)
					{
						LogSystem.Trace(exception2);
						flag = false;
						return flag;
					}
				}
				return true;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private static List<string> get_artifact(Artifact artifact, bool builder)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=artifact>",
				"<TD colspan=2>",
				string.Concat("<B>", HTML.Process(artifact.Name, true), "</B>"),
				"</TD>",
				"<TD align=center>",
				string.Concat(artifact.Tier, " tier"),
				"</TD>",
				"</TR>"
			};
			if (builder)
			{
				strs.Add("<TR class=artifact>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str = HTML.Process(artifact.Description, true);
			if (builder)
			{
				if (str == "")
				{
					str = "click to set description";
				}
				str = string.Concat("<A href=build:description>", str, "</A>");
			}
			if (str != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD class=readaloud colspan=3>");
				strs.Add(str);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str1 = HTML.Process(artifact.Details, true);
			if (builder)
			{
				if (str1 == "")
				{
					str1 = "click to set details";
				}
				str1 = string.Concat("<A href=build:details>", str1, "</A>");
			}
			if (str1 != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(str1);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			foreach (MagicItemSection section in artifact.Sections)
			{
				int num = artifact.Sections.IndexOf(section);
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", HTML.Process(section.Header, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent colspan=3>");
				strs.Add(HTML.Process(section.Details, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (!builder)
				{
					continue;
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add(string.Concat("<A href=sectionedit:", num, ">edit</A>"));
				strs.Add("|");
				strs.Add(string.Concat("<A href=sectionremove:", num, ">remove</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=section:new>add a section</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str2 = HTML.Process(artifact.Goals, true);
			if (builder)
			{
				if (str2 == "")
				{
					str2 = "click to set goals";
				}
				str2 = string.Concat("<A href=build:goals>", str2, "</A>");
			}
			if (str2 != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Goals of ", HTML.Process(artifact.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(str2);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str3 = HTML.Process(artifact.RoleplayingTips, true);
			if (builder)
			{
				if (str3 == "")
				{
					str3 = "click to set roleplaying tips";
				}
				str3 = string.Concat("<A href=build:rp>", str3, "</A>");
			}
			if (str3 != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Roleplaying ", HTML.Process(artifact.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(str3);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Concordance</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=2>Starting score</TD>");
			strs.Add("<TD align=center>5</TD>");
			strs.Add("</TR>");
			foreach (Pair<string, string> concordanceRule in artifact.ConcordanceRules)
			{
				int num1 = artifact.ConcordanceRules.IndexOf(concordanceRule);
				strs.Add("<TR>");
				strs.Add("<TD colspan=2>");
				strs.Add(concordanceRule.First);
				if (builder)
				{
					strs.Add(string.Concat("<A href=ruleedit:", num1, ">edit</A>"));
					strs.Add("|");
					strs.Add(string.Concat("<A href=ruleremove:", num1, ">remove</A>"));
				}
				strs.Add("</TD>");
				strs.Add("<TD align=center>");
				strs.Add(concordanceRule.Second);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=rule:new>add a concordance rule</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			foreach (ArtifactConcordance concordanceLevel in artifact.ConcordanceLevels)
			{
				int num2 = artifact.ConcordanceLevels.IndexOf(concordanceLevel);
				string str4 = HTML.Process(concordanceLevel.Name, true);
				if (concordanceLevel.ValueRange != "")
				{
					str4 = string.Concat(str4, " (", HTML.Process(concordanceLevel.ValueRange, true), ")");
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", str4, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				string str5 = HTML.Process(concordanceLevel.Quote, true);
				if (builder)
				{
					if (str5 == "")
					{
						str5 = "click to set a quote for this concordance level";
					}
					object[] objArray = new object[] { "<A href=quote:", num2, ">", str5, "</A>" };
					str5 = string.Concat(objArray);
				}
				if (str5 != "")
				{
					strs.Add("<TR class=readaloud>");
					strs.Add("<TD colspan=3>");
					strs.Add(str5);
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				string str6 = HTML.Process(concordanceLevel.Description, true);
				if (builder)
				{
					if (str6 == "")
					{
						str6 = "click to set concordance details";
					}
					object[] objArray1 = new object[] { "<A href=desc:", num2, ">", str6, "</A>" };
					str6 = string.Concat(objArray1);
				}
				if (str6 != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					strs.Add(str6);
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (concordanceLevel.ValueRange == "")
				{
					continue;
				}
				foreach (MagicItemSection magicItemSection in concordanceLevel.Sections)
				{
					int num3 = artifact.Sections.IndexOf(magicItemSection);
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=3>");
					strs.Add(string.Concat("<B>", HTML.Process(magicItemSection.Header, true), "</B>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					strs.Add(HTML.Process(magicItemSection.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
					if (!builder)
					{
						continue;
					}
					strs.Add("<TR>");
					strs.Add("<TD colspan=3 align=center>");
					object[] objArray2 = new object[] { "<A href=sectionedit:", num2, ",", num3, ">edit</A>" };
					strs.Add(string.Concat(objArray2));
					strs.Add("|");
					object[] objArray3 = new object[] { "<A href=sectionremove:", num2, ",", num3, ">remove</A>" };
					strs.Add(string.Concat(objArray3));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (!builder)
				{
					continue;
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add(string.Concat("<A href=section:", num2, ",new>add a section</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private List<string> get_backgrounds()
		{
			List<string> strs = new List<string>();
			foreach (Background background in Session.Project.Backgrounds)
			{
				if (background.Details == "")
				{
					continue;
				}
				strs.Add(HTML.wrap(HTML.Process(background.Title, true), "h3"));
				strs.Add(string.Concat("<P class=background>", HTML.Process(background.Details, false), "</P>"));
			}
			return strs;
		}

		private List<string> get_body()
		{
			List<string> strs = new List<string>()
			{
				"<BODY>",
				string.Concat("<H1>", Session.Project.Name, "</H1>"),
				string.Concat("<P class=description>", this.get_description(), "</P>")
			};
			if (Session.Project.Author != "")
			{
				strs.Add(string.Concat("<P class=description>By ", HTML.Process(Session.Project.Author, true), "</P>"));
			}
			if (Session.Project.Backgrounds.Count != 0)
			{
				strs.AddRange(this.get_backgrounds());
			}
			if (Session.Project.Plot.Points.Count != 0)
			{
				strs.Add("<HR>");
				strs.AddRange(this.get_full_plot());
			}
			if (Session.Project.NPCs.Count != 0)
			{
				strs.Add("<HR>");
				strs.AddRange(this.get_npcs());
			}
			if (Session.Project.Encyclopedia.Entries.Count != 0)
			{
				strs.Add("<HR>");
				strs.AddRange(this.get_encyclopedia());
			}
			if (Session.Project.Notes.Count != 0)
			{
				strs.Add("<HR>");
				strs.AddRange(this.get_notes());
			}
			strs.Add("<HR>");
			strs.Add("<P class=signature>Designed using <A href=\"http://www.habitualindolence.net/masterplan\">Masterplan</A></P>");
			strs.Add("</BODY>");
			return strs;
		}

		private static List<string> get_combat_data(CombatData cd, int max_hp, Encounter enc, bool initiative_holder)
		{
			int maxHp = max_hp / 2;
			int num = max_hp - cd.Damage;
			bool flag = (max_hp == 0 ? false : num <= maxHp);
			bool flag1 = (max_hp == 0 ? false : num <= 0);
			List<string> strs = new List<string>();
			if (cd.Conditions.Count != 0 || flag || flag1 || initiative_holder || cd.Altitude != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add("<B>Information</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (initiative_holder)
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat(cd.DisplayName, " is the current initiative holder"));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (cd.Altitude != 0)
				{
					string str = (Math.Abs(cd.Altitude) == 1 ? "square" : "squares");
					string str1 = (cd.Altitude > 0 ? "up" : "down");
					strs.Add("<TR>");
					strs.Add("<TD>");
					object[] displayName = new object[] { cd.DisplayName, " is ", Math.Abs(cd.Altitude), " ", str, " <B>", str1, "</B>" };
					strs.Add(string.Concat(displayName));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (flag1)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Hit Points</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat(cd.DisplayName, " is <B>dead</B>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				else if (flag)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Hit Points</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat(cd.DisplayName, " is <B>bloodied</B>"));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (cd.Conditions.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add("<B>Effects</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (OngoingCondition condition in cd.Conditions)
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(condition.ToString(enc, true));
						int num1 = cd.Conditions.IndexOf(condition);
						object[] d = new object[] { "<A href=\"effect:", cd.ID, ":", num1, "\">(remove)</A>" };
						strs.Add(string.Concat(d));
						if (condition.Type != OngoingType.Condition)
						{
							continue;
						}
						strs.Add("</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD class=indent>");
						List<string> conditionInfo = Conditions.GetConditionInfo(condition.Data);
						if (conditionInfo.Count != 0)
						{
							strs.AddRange(conditionInfo);
						}
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			return strs;
		}

		private List<string> get_content()
		{
			List<string> strs = new List<string>();
			string str = string.Concat(Session.Project.Name, ": ", this.get_description());
			strs.Add("<HTML>");
			strs.AddRange(HTML.GetHead(Session.Project.Name, str, DisplaySize.Small));
			strs.AddRange(this.get_body());
			strs.Add("</HTML>");
			return strs;
		}

		private static List<string> get_custom_token(CustomToken ct)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD>",
				string.Concat("<B>", HTML.Process(ct.Name, true), "</B>"),
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD>",
				(ct.Details != "" ? HTML.Process(ct.Details, true) : "(no details)"),
				"</TD>",
				"</TR>"
			};
			if (ct.TerrainPower != null)
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(HTML.Process(string.Concat("Includes the terrain power \"", ct.TerrainPower.Name, "\""), true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			if (ct.TerrainPower != null)
			{
				strs.Add("<BR>");
				strs.AddRange(HTML.get_terrain_power(ct.TerrainPower));
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			strs.Add("</P>");
			return strs;
		}

		private string get_description()
		{
			object[] size = new object[] { "An adventure for ", Session.Project.Party.Size, " characters of level ", Session.Project.Party.Level, "." };
			return string.Concat(size);
		}

		private static List<string> get_encounter(Encounter enc)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD colspan=2>",
				"<B>Encounter</B>",
				"</TD>",
				"<TD>",
				string.Concat(enc.GetXP(), " XP"),
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD colspan=3>",
				string.Concat("<B>Level</B> ", enc.GetLevel(Session.Project.Party.Size)),
				"</TD>",
				"</TR>"
			};
			if (enc.Slots.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=2>");
				strs.Add("<B>Combatants</B>");
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", enc.Count, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (EncounterSlot slot in enc.Slots)
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(slot.Card.Title);
					strs.Add("</TD>");
					strs.Add("<TD>");
					if (slot.CombatData.Count > 1)
					{
						strs.Add(string.Concat("x", slot.CombatData.Count));
					}
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			foreach (EncounterWave wafe in enc.Waves)
			{
				if (wafe.Count == 0)
				{
					continue;
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=2>");
				strs.Add(string.Concat("<B>", wafe.Name, "</B>"));
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", wafe.Count, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (EncounterSlot encounterSlot in wafe.Slots)
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(encounterSlot.Card.Title);
					strs.Add("</TD>");
					strs.Add("<TD>");
					if (encounterSlot.CombatData.Count > 1)
					{
						strs.Add(string.Concat("x", encounterSlot.CombatData.Count));
					}
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			if (enc.Traps.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=2>");
				strs.Add("<B>Traps / Hazards</B>");
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", enc.Traps.Count, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (Trap trap in enc.Traps)
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					strs.Add(HTML.Process(trap.Name, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			if (enc.SkillChallenges.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=2>");
				strs.Add("<B>Skill Challenges</B>");
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", enc.SkillChallenges.Count, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (SkillChallenge skillChallenge in enc.SkillChallenges)
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					strs.Add(HTML.Process(skillChallenge.Name, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			foreach (EncounterNote note in enc.Notes)
			{
				if (note.Contents == "")
				{
					continue;
				}
				strs.Add("<P class=encounter_note>");
				strs.Add(string.Concat("<B>", HTML.Process(note.Title, true), "</B>"));
				strs.Add("</P>");
				strs.Add("<P class=encounter_note>");
				strs.Add(HTML.Process(note.Contents, false));
				strs.Add("</P>");
			}
			List<string> strs1 = new List<string>();
			foreach (EncounterSlot allSlot in enc.AllSlots)
			{
				if (strs1.Contains(allSlot.Card.Title))
				{
					continue;
				}
				strs.Add("<P class=table>");
				strs.AddRange(allSlot.Card.AsText(null, CardMode.View, true));
				strs.Add("</P>");
				strs1.Add(allSlot.Card.Title);
			}
			foreach (Trap trap1 in enc.Traps)
			{
				strs.AddRange(HTML.get_trap(trap1, null, false, false));
			}
			foreach (SkillChallenge skillChallenge1 in enc.SkillChallenges)
			{
				strs.AddRange(HTML.get_skill_challenge(skillChallenge1, false));
			}
			foreach (CustomToken customToken in enc.CustomTokens)
			{
				if (customToken.Type == CustomTokenType.Token)
				{
					continue;
				}
				strs.AddRange(HTML.get_custom_token(customToken));
			}
			return strs;
		}

		private List<string> get_encyclopedia()
		{
			List<string> strs = new List<string>()
			{
				HTML.wrap("Encyclopedia", "h2")
			};
			foreach (EncyclopediaEntry entry in Session.Project.Encyclopedia.Entries)
			{
				strs.Add(HTML.wrap(HTML.Process(entry.Name, true), "h3"));
				strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(entry.Details, false), "</P>"));
			}
			return strs;
		}

		private string get_filename(string item_name, string extension, bool full_path)
		{
			string itemName = item_name;
			List<string> strs = new List<string>()
			{
				"\\",
				"/",
				":",
				"*",
				"?",
				"\"",
				"<",
				">",
				"|"
			};
			foreach (string str in strs)
			{
				itemName = itemName.Replace(str, "");
			}
			string str1 = string.Concat((full_path ? this.fFullPath : this.fRelativePath), itemName, ".", extension);
			if (!full_path)
			{
				str1 = str1.Replace(" ", "%20");
			}
			return str1;
		}

		private List<string> get_full_plot()
		{
			List<string> strs = new List<string>()
			{
				HTML.wrap(HTML.Process(Session.Project.Name, true), "h2")
			};
			strs.AddRange(this.get_plot(Session.Project.Name, Session.Project.Plot));
			return strs;
		}

		private static List<string> get_hero(Hero hero, Encounter enc, bool initiative_holder, bool show_effects)
		{
			List<string> strs = new List<string>();
			if (enc != null)
			{
				strs.AddRange(HTML.get_combat_data(hero.CombatData, hero.HP, enc, initiative_holder));
			}
			if (show_effects && hero.Effects.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading><TD colspan=3><B>Effects</B></TD></TR>");
				foreach (OngoingCondition effect in hero.Effects)
				{
					int num = hero.Effects.IndexOf(effect);
					strs.Add(string.Concat("<TR><TD colspan=2>", effect.ToString(enc, true), "</TD>"));
					strs.Add(string.Concat("<TD align=right><A href=addeffect:", num, ">Apply &#8658</A></TD></TR>"));
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=hero>");
			strs.Add("<TD colspan=2>");
			strs.Add(string.Concat("<B>", HTML.Process(hero.Name, true), "</B>"));
			strs.Add("</TD>");
			strs.Add("<TD align=right>");
			strs.Add(HTML.Process(hero.Player, true));
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(HTML.Process(hero.Info, true));
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Combat</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str = hero.InitBonus.ToString();
			if (hero.InitBonus >= 0)
			{
				str = string.Concat("+", str);
			}
			if (hero.CombatData != null && hero.CombatData.Initiative != -2147483648)
			{
				object[] initiative = new object[] { hero.CombatData.Initiative, " (", str, ")" };
				str = string.Concat(initiative);
			}
			if (enc != null)
			{
				object[] d = new object[] { "<A href=init:", hero.CombatData.ID, ">", str, "</A>" };
				str = string.Concat(d);
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>Initiative</B> ", str));
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str1 = hero.HP.ToString();
			if (hero.CombatData != null && hero.CombatData.Damage != 0)
			{
				int hP = hero.HP - hero.CombatData.Damage;
				str1 = string.Concat(hP, " of ", hero.HP);
			}
			string str2 = string.Concat("<B>HP</B> ", str1);
			if (enc != null)
			{
				object[] objArray = new object[] { "<A href=hp:", hero.ID, ">", str2, "</A>" };
				str2 = string.Concat(objArray);
			}
			str2 = string.Concat(str2, "; <B>Bloodied</B> ", hero.HP / 2);
			if (hero.CombatData != null && hero.CombatData.TempHP > 0)
			{
				str2 = string.Concat(str2, "; <B>Temp HP</B> ", hero.CombatData.TempHP);
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(str2);
			strs.Add("</TD>");
			strs.Add("</TR>");
			int aC = hero.AC;
			int fortitude = hero.Fortitude;
			int reflex = hero.Reflex;
			int will = hero.Will;
			if (hero.CombatData != null)
			{
				foreach (OngoingCondition condition in hero.CombatData.Conditions)
				{
					if (condition.Type != OngoingType.DefenceModifier)
					{
						continue;
					}
					if (condition.Defences.Contains(DefenceType.AC))
					{
						aC += condition.DefenceMod;
					}
					if (condition.Defences.Contains(DefenceType.Fortitude))
					{
						fortitude += condition.DefenceMod;
					}
					if (condition.Defences.Contains(DefenceType.Reflex))
					{
						reflex += condition.DefenceMod;
					}
					if (!condition.Defences.Contains(DefenceType.Will))
					{
						continue;
					}
					will += condition.DefenceMod;
				}
			}
			string str3 = aC.ToString();
			if (aC != hero.AC)
			{
				str3 = string.Concat("<B><I>", str3, "</I></B>");
			}
			string str4 = fortitude.ToString();
			if (fortitude != hero.Fortitude)
			{
				str4 = string.Concat("<B><I>", str4, "</I></B>");
			}
			string str5 = reflex.ToString();
			if (reflex != hero.Reflex)
			{
				str5 = string.Concat("<B><I>", str5, "</I></B>");
			}
			string str6 = will.ToString();
			if (will != hero.Will)
			{
				str6 = string.Concat("<B><I>", str6, "</I></B>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			string[] strArrays = new string[] { "<B>AC</B> ", str3, "; <B>Fort</B> ", str4, "; <B>Ref</B> ", str5, "; <B>Will</B> ", str6 };
			strs.Add(string.Concat(strArrays));
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Skills</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>Passive Insight</B> ", hero.PassiveInsight));
			strs.Add("<BR>");
			strs.Add(string.Concat("<B>Passive Perception</B> ", hero.PassivePerception));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (hero.Languages != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Languages</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(HTML.Process(hero.Languages, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private static List<string> get_magic_item(MagicItem item, bool builder)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=item>",
				"<TD colspan=2>",
				string.Concat("<B>", HTML.Process(item.Name, true), "</B>"),
				"</TD>",
				"<TD>",
				HTML.Process(item.Type, true),
				"</TD>",
				"</TR>"
			};
			if (builder)
			{
				strs.Add("<TR class=item>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str = HTML.Process(item.Description, true);
			if (builder && str == "")
			{
				str = "(no description set)";
			}
			if (str != "")
			{
				if (builder)
				{
					str = string.Concat("<A href=build:desc>", str, "</A>");
				}
				strs.Add("<TR>");
				strs.Add("<TD class=readaloud colspan=3>");
				strs.Add(str);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str1 = item.Rarity.ToString();
			if (builder)
			{
				str1 = string.Concat("<A href=build:profile>", str1, "</A>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>Availability</B> ", str1));
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str2 = item.Level.ToString();
			if (builder)
			{
				str2 = string.Concat("<A href=build:profile>", str2, "</A>");
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>Level</B> ", str2));
			strs.Add("</TD>");
			strs.Add("</TR>");
			foreach (MagicItemSection section in item.Sections)
			{
				int num = item.Sections.IndexOf(section);
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", HTML.Process(section.Header, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent colspan=3>");
				strs.Add(HTML.Process(section.Details, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (!builder)
				{
					continue;
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add(string.Concat("<A href=edit:", num, ">edit</A>"));
				strs.Add("|");
				strs.Add(string.Concat("<A href=remove:", num, ">remove</A>"));
				if (item.Sections.Count > 1)
				{
					if (num != 0)
					{
						strs.Add("|");
						strs.Add(string.Concat("<A href=moveup:", num, ">move up</A>"));
					}
					if (num != item.Sections.Count - 1)
					{
						strs.Add("|");
						strs.Add(string.Concat("<A href=movedown:", num, ">move down</A>"));
					}
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (builder)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Sections</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (item.Sections.Count == 0)
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					strs.Add("No details set");
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=section:new>add a new section</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			Library library = Session.FindLibrary(item);
			if (library != null)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(HTML.Process(library.Name, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private static List<string> get_map_area_details(PlotPoint pp)
		{
			List<string> strs = new List<string>();
			Map map = null;
			MapArea mapArea = null;
			pp.GetTacticalMapArea(ref map, ref mapArea);
			if (map != null && mapArea != null && mapArea.Details != "")
			{
				strs.Add(string.Concat("<P><B>", HTML.Process(mapArea.Name, true), "</B>:</P>"));
				strs.Add(string.Concat("<P>", HTML.Process(mapArea.Details, true), "</P>"));
			}
			return strs;
		}

		private string get_map_name(Guid map_id, Guid area_id)
		{
			Map map = Session.Project.FindTacticalMap(map_id);
			if (map == null)
			{
				return "";
			}
			if (area_id == Guid.Empty)
			{
				return map.Name;
			}
			MapArea mapArea = map.FindArea(area_id);
			return string.Concat(map.Name, " - ", mapArea.Name);
		}

		private List<string> get_notes()
		{
			List<string> strs = new List<string>()
			{
				HTML.wrap("Notes", "h2")
			};
			foreach (Note note in Session.Project.Notes)
			{
				strs.Add(string.Concat("<P class=note>", HTML.Process(note.Content, true), "</P>"));
			}
			return strs;
		}

		private List<string> get_npcs()
		{
			List<string> strs = new List<string>()
			{
				HTML.wrap("Encyclopedia", "h2")
			};
			foreach (NPC nPC in Session.Project.NPCs)
			{
				strs.Add(HTML.wrap(HTML.Process(nPC.Name, true), "h3"));
				string str = HTML.Process(nPC.Details, true);
				if (str != "")
				{
					strs.Add(string.Concat("<P>", str, "</P>"));
				}
				strs.Add("<P class=table>");
				EncounterCard encounterCard = new EncounterCard()
				{
					CreatureID = nPC.ID
				};
				strs.AddRange(encounterCard.AsText(null, CardMode.View, true));
				strs.Add("</P>");
			}
			return strs;
		}

		private static List<string> get_parcels(PlotPoint pp, bool links)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD>",
				"<B>Treasure Parcels</B>",
				"</TD>",
				"</TR>"
			};
			foreach (Parcel parcel in pp.Parcels)
			{
				MagicItem magicItem = null;
				if (parcel.MagicItemID != Guid.Empty)
				{
					magicItem = Session.FindMagicItem(parcel.MagicItemID, SearchType.Global);
				}
				string str = (parcel.Name != "" ? HTML.Process(parcel.Name, true) : "(undefined parcel)");
				if (links && magicItem != null)
				{
					object[] d = new object[] { "<A href=\"item:", magicItem.ID, "\">", str, "</A>" };
					str = string.Concat(d);
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", str, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (parcel.Details != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(HTML.Process(parcel.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (!(parcel.MagicItemID != Guid.Empty) || magicItem == null)
				{
					continue;
				}
				Library library = Session.FindLibrary(magicItem);
				if (library == null || Session.Project != null && Session.Project.Library == library)
				{
					continue;
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(HTML.Process(library.Name, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private static List<string> get_player_option(IPlayerOption option)
		{
			object[] first;
			List<string> strs = new List<string>();
			if (option is Race)
			{
				Race race = option as Race;
				if (race.Quote != null && race.Quote != "")
				{
					strs.Add("<P class=readaloud>");
					strs.Add(HTML.Process(race.Quote, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(race.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (race.HeightRange != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Average Height</B> ", HTML.Process(race.HeightRange, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (race.WeightRange != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Average Weight</B> ", HTML.Process(race.WeightRange, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (race.AbilityScores != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Ability Scores</B> ", HTML.Process(race.AbilityScores, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Size</B> ", race.Size));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (race.Speed != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Speed</B> ", HTML.Process(race.Speed, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (race.Vision != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Vision</B> ", HTML.Process(race.Vision, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (race.Languages != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Languages</B> ", HTML.Process(race.Languages, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (race.SkillBonuses != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Skill Bonuses</B> ", HTML.Process(race.SkillBonuses, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				foreach (Feature feature in race.Features)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>", HTML.Process(feature.Name, true), "</B> ", HTML.Process(feature.Details, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				foreach (PlayerPower power in race.Powers)
				{
					strs.AddRange(HTML.get_player_power(power, 0));
				}
				if (race.Details != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(race.Details, true));
					strs.Add("</P>");
				}
			}
			if (option is Class)
			{
				Class @class = option as Class;
				if (@class.Quote != null && @class.Quote != "")
				{
					strs.Add("<P class=readaloud>");
					strs.Add(HTML.Process(@class.Quote, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(@class.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (@class.Role != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Role</B> ", HTML.Process(@class.Role, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.PowerSource != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Power Source</B> ", HTML.Process(@class.PowerSource, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.KeyAbilities != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Key Abilities</B> ", HTML.Process(@class.KeyAbilities, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.ArmourProficiencies != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Armour Proficiencies</B> ", HTML.Process(@class.ArmourProficiencies, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.WeaponProficiencies != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Weapon Proficiencies</B> ", HTML.Process(@class.WeaponProficiencies, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.Implements != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Implements</B> ", HTML.Process(@class.Implements, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (@class.DefenceBonuses != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Defence Bonuses</B> ", HTML.Process(@class.DefenceBonuses, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Hit Points at 1st Level</B> ", @class.HPFirst, " + Constitution score"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>HP per Level Gained</B> ", @class.HPSubsequent));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Healing Surges per Day</B> ", @class.HealingSurges, " + Constitution modifier"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (@class.TrainedSkills != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Trained Skills</B> ", HTML.Process(@class.TrainedSkills, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				string str = "";
				foreach (Feature feature1 in @class.FeatureData.Features)
				{
					if (str != "")
					{
						str = string.Concat(str, ", ");
					}
					str = string.Concat(str, feature1.Name);
				}
				if (str != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Class Features</B> ", HTML.Process(str, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				if (@class.Description != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(@class.Description, true));
					strs.Add("</P>");
				}
				if (@class.OverviewCharacteristics != "" || @class.OverviewReligion != "" || @class.OverviewRaces != "")
				{
					strs.Add("<P class=table>");
					strs.Add("<TABLE>");
					strs.Add("<TR class=heading>");
					strs.Add("<TD>");
					strs.Add("<B>Overview</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					if (@class.OverviewCharacteristics != "")
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>Characteristics</B> ", HTML.Process(@class.OverviewCharacteristics, true)));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					if (@class.OverviewReligion != "")
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>Religion</B> ", HTML.Process(@class.OverviewReligion, true)));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					if (@class.OverviewRaces != "")
					{
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>Races</B> ", HTML.Process(@class.OverviewRaces, true)));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					strs.Add("</TABLE>");
					strs.Add("</P>");
				}
				if (@class.FeatureData.Features.Count != 0)
				{
					strs.Add("<H4>Class Features</H4>");
					foreach (Feature feature2 in @class.FeatureData.Features)
					{
						strs.Add("<P class=table>");
						strs.Add("<TABLE>");
						strs.Add("<TR class=heading>");
						strs.Add("<TD>");
						strs.Add(string.Concat("<B>", HTML.Process(feature2.Name, true), "</B>"));
						strs.Add("</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD>");
						strs.Add(HTML.Process(feature2.Details, true));
						strs.Add("</TD>");
						strs.Add("</TR>");
						strs.Add("</TABLE>");
						strs.Add("</P>");
					}
				}
				foreach (PlayerPower playerPower in @class.FeatureData.Powers)
				{
					strs.AddRange(HTML.get_player_power(playerPower, 0));
				}
				foreach (LevelData level in @class.Levels)
				{
					if (level.Powers.Count == 0)
					{
						continue;
					}
					strs.Add(string.Concat("<H4>Level ", level.Level, " Powers</H4>"));
					foreach (PlayerPower power1 in level.Powers)
					{
						strs.AddRange(HTML.get_player_power(power1, level.Level));
					}
				}
			}
			if (option is Theme)
			{
				Theme theme = option as Theme;
				if (theme.Quote != null && theme.Quote != "")
				{
					strs.Add("<P class=readaloud>");
					strs.Add(HTML.Process(theme.Quote, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(theme.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (theme.Prerequisites != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Prerequisites</B> ", HTML.Process(theme.Prerequisites, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (theme.SecondaryRole != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Secondary Role</B> ", HTML.Process(theme.SecondaryRole, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (theme.PowerSource != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Power Source</B> ", HTML.Process(theme.PowerSource, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Granted Power</B> ", HTML.Process(theme.GrantedPower.Name, true)));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (LevelData levelDatum in theme.Levels)
				{
					foreach (Feature feature3 in levelDatum.Features)
					{
						strs.Add("<TR class=shaded>");
						strs.Add("<TD>");
						first = new object[] { "<B>", HTML.Process(feature3.Name, true), " (level ", levelDatum.Level, ")</B> ", HTML.Process(feature3.Details, true) };
						strs.Add(string.Concat(first));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				strs.AddRange(HTML.get_player_power(theme.GrantedPower, 0));
				foreach (LevelData level1 in theme.Levels)
				{
					foreach (PlayerPower playerPower1 in level1.Powers)
					{
						strs.AddRange(HTML.get_player_power(playerPower1, level1.Level));
					}
				}
				if (theme.Details != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(theme.Details, true));
					strs.Add("</P>");
				}
			}
			if (option is ParagonPath)
			{
				ParagonPath paragonPath = option as ParagonPath;
				if (paragonPath.Quote != null && paragonPath.Quote != "")
				{
					strs.Add("<P class=readaloud>");
					strs.Add(HTML.Process(paragonPath.Quote, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(paragonPath.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (paragonPath.Prerequisites != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Prerequisites</B> ", HTML.Process(paragonPath.Prerequisites, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				foreach (LevelData levelDatum1 in paragonPath.Levels)
				{
					foreach (Feature feature4 in levelDatum1.Features)
					{
						strs.Add("<TR class=shaded>");
						strs.Add("<TD>");
						object[] objArray = new object[] { "<B>", HTML.Process(feature4.Name, true), " (level ", levelDatum1.Level, ")</B> ", HTML.Process(feature4.Details, true) };
						strs.Add(string.Concat(objArray));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				foreach (LevelData level2 in paragonPath.Levels)
				{
					foreach (PlayerPower power2 in level2.Powers)
					{
						strs.AddRange(HTML.get_player_power(power2, level2.Level));
					}
				}
				if (paragonPath.Details != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(paragonPath.Details, true));
					strs.Add("</P>");
				}
			}
			if (option is EpicDestiny)
			{
				EpicDestiny epicDestiny = option as EpicDestiny;
				if (epicDestiny.Quote != null && epicDestiny.Quote != "")
				{
					strs.Add("<P class=readaloud>");
					strs.Add(HTML.Process(epicDestiny.Quote, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(epicDestiny.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (epicDestiny.Prerequisites != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Prerequisites</B> ", HTML.Process(epicDestiny.Prerequisites, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				foreach (LevelData levelDatum2 in epicDestiny.Levels)
				{
					foreach (Feature feature5 in levelDatum2.Features)
					{
						strs.Add("<TR class=shaded>");
						strs.Add("<TD>");
						object[] objArray1 = new object[] { "<B>", HTML.Process(feature5.Name, true), " (level ", levelDatum2.Level, ")</B> ", HTML.Process(feature5.Details, true) };
						strs.Add(string.Concat(objArray1));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				foreach (LevelData level3 in epicDestiny.Levels)
				{
					foreach (PlayerPower playerPower2 in level3.Powers)
					{
						strs.AddRange(HTML.get_player_power(playerPower2, level3.Level));
					}
				}
				if (epicDestiny.Details != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(epicDestiny.Details, true));
					strs.Add("</P>");
				}
				if (epicDestiny.Immortality != "")
				{
					strs.Add("<P>");
					strs.Add(string.Concat("<B>Immortality</B> ", HTML.Process(epicDestiny.Immortality, true)));
					strs.Add("</P>");
				}
			}
			if (option is PlayerBackground)
			{
				PlayerBackground playerBackground = option as PlayerBackground;
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(playerBackground.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (playerBackground.Details != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(HTML.Process(playerBackground.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (playerBackground.AssociatedSkills != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Associated Skills</B> ", HTML.Process(playerBackground.AssociatedSkills, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (playerBackground.RecommendedFeats != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Recommended Feats</B> ", HTML.Process(playerBackground.RecommendedFeats, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (option is Feat)
			{
				Feat feat = option as Feat;
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(feat.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (feat.Prerequisites != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Prerequisites</B> ", HTML.Process(feat.Prerequisites, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (feat.Benefits != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Benefit</B> ", HTML.Process(feat.Benefits, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (option is Weapon)
			{
				Weapon weapon = option as Weapon;
				string str1 = string.Concat(weapon.Type, " ", weapon.Category);
				str1 = string.Concat(str1, (weapon.TwoHanded ? " two-handed weapon" : " one-handed weapon"));
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=item>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(weapon.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(str1);
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Proficiency</B> +", weapon.Proficiency));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (weapon.Damage != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Damage</B> ", weapon.Damage));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (weapon.Range != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Range</B> ", weapon.Range));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (weapon.Price != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Price</B> ", weapon.Price));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (weapon.Weight != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Weight</B> ", weapon.Weight));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (weapon.Group != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Group</B> ", weapon.Group));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (weapon.Properties != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Properties</B> ", weapon.Properties));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				if (weapon.Description != "")
				{
					strs.Add(string.Concat("<P>", HTML.Process(weapon.Description, true), "</P>"));
				}
			}
			if (option is Ritual)
			{
				Ritual ritual = option as Ritual;
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(ritual.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (ritual.ReadAloud != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD class=readaloud>");
					strs.Add(HTML.Process(ritual.ReadAloud, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Level</B> ", ritual.Level));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Category</B> ", ritual.Category));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (ritual.Time != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Time</B> ", HTML.Process(ritual.Time, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (ritual.Duration != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Duration</B> ", HTML.Process(ritual.Duration, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (ritual.ComponentCost != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Component Cost</B> ", HTML.Process(ritual.ComponentCost, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (ritual.MarketPrice != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Market Price</B> ", HTML.Process(ritual.MarketPrice, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (ritual.KeySkill != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD>");
					strs.Add(string.Concat("<B>Key Skill</B> ", HTML.Process(ritual.KeySkill, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (ritual.Details != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD>");
					strs.Add(HTML.Process(ritual.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (option is CreatureLore)
			{
				CreatureLore creatureLore = option as CreatureLore;
				strs.Add(string.Concat("<H3>", HTML.Process(creatureLore.Name, true), " Lore</H3>"));
				strs.Add("<P>");
				strs.Add(string.Concat("A character knows the following information with a successful <B>", creatureLore.SkillName, "</B> check:"));
				strs.Add("</P>");
				strs.Add("<UL>");
				foreach (Pair<int, string> information in creatureLore.Information)
				{
					strs.Add("<LI>");
					first = new object[] { "<B>DC ", information.First, "</B>: ", information.Second };
					strs.Add(string.Concat(first));
					strs.Add("</LI>");
				}
				strs.Add("</UL>");
			}
			if (option is Disease)
			{
				Disease disease = option as Disease;
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=trap>");
				strs.Add("<TD colspan=2>");
				strs.Add(string.Concat("<B>", HTML.Process(disease.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("<TD>");
				if (disease.Level != "")
				{
					strs.Add(string.Concat("Level ", disease.Level, " Disease"));
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (disease.Details != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD class=readaloud colspan=3>");
					strs.Add(HTML.Process(disease.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (disease.Levels.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Stages</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Cured</B>: The target is cured.");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (string str2 in disease.Levels)
					{
						strs.Add("<TR>");
						strs.Add("<TD colspan=3>");
						if (disease.Levels.Count > 1)
						{
							int num = disease.Levels.IndexOf(str2);
							if (num == 0)
							{
								strs.Add("<B>Initial state</B>:");
							}
							if (num == disease.Levels.Count - 1)
							{
								strs.Add("<B>Final state</B>:");
							}
						}
						strs.Add(HTML.Process(str2, true));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Check</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Maintain</B>: DC ", HTML.Process(disease.MaintainDC, true)));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Improve</B>: DC ", HTML.Process(disease.ImproveDC, true)));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (option is Poison)
			{
				Poison poison = option as Poison;
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=trap>");
				strs.Add("<TD>");
				first = new object[] { "<B>", HTML.Process(poison.Name, true), "</B> (level ", poison.Level, ")" };
				strs.Add(string.Concat(first));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (poison.Details != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD class=readaloud>");
					strs.Add(HTML.Process(poison.Details, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				int itemValue = Treasure.GetItemValue(poison.Level) / 4;
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>Price</B>: ", itemValue, " gp"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (PlayerPowerSection section in poison.Sections)
				{
					strs.Add("<TR>");
					if (section.Indent != 0)
					{
						int indent = section.Indent * 15;
						strs.Add(string.Concat("<TD style=\"padding-left=", indent, "px\">"));
					}
					else
					{
						strs.Add("<TD>");
					}
					strs.Add(string.Concat("<B>", HTML.Process(section.Header, true), "</B> ", HTML.Process(section.Details, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			return strs;
		}

		private static List<string> get_player_power(PlayerPower power, int level)
		{
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>"
			};
			string str = HTML.Process(power.Name, true);
			if (str == "")
			{
				str = "(unnamed power)";
			}
			string str1 = string.Concat("<B>", str, "</B>");
			if (level != 0)
			{
				object obj = str1;
				object[] objArray = new object[] { obj, " (level ", level, ")" };
				str1 = string.Concat(objArray);
			}
			switch (power.Type)
			{
				case PlayerPowerType.AtWill:
				{
					strs.Add("<TR class=atwill>");
					break;
				}
				case PlayerPowerType.Encounter:
				{
					strs.Add("<TR class=encounter>");
					break;
				}
				case PlayerPowerType.Daily:
				{
					strs.Add("<TR class=daily>");
					break;
				}
			}
			strs.Add("<TD>");
			strs.Add(str1);
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (power.ReadAloud != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD class=readaloud>");
				strs.Add(HTML.Process(power.ReadAloud, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str2 = power.Type.ToString();
			if (power.Keywords != "")
			{
				str2 = string.Concat(str2, " &diams; ", HTML.Process(power.Keywords, true));
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add(str2);
			strs.Add("</TD>");
			strs.Add("</TR>");
			string str3 = string.Concat("<B>Action</B> ", power.Action);
			if (power.Range != "")
			{
				str3 = string.Concat(str3, "; <B>Range</B> ", HTML.Process(power.Range, true));
			}
			strs.Add("<TR>");
			strs.Add("<TD>");
			strs.Add(str3);
			strs.Add("</TD>");
			strs.Add("</TR>");
			foreach (PlayerPowerSection section in power.Sections)
			{
				strs.Add("<TR>");
				if (section.Indent != 0)
				{
					int indent = section.Indent * 15;
					strs.Add(string.Concat("<TD style=\"padding-left=", indent, "px\">"));
				}
				else
				{
					strs.Add("<TD>");
				}
				strs.Add(string.Concat("<B>", HTML.Process(section.Header, true), "</B> ", HTML.Process(section.Details, true)));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private List<string> get_plot(string plot_name, Plot p)
		{
			List<string> strs = new List<string>();
			if (p.Points.Count > 1)
			{
				this.fPlots.Add(new Pair<string, Plot>(plot_name, p));
				string _filename = this.get_filename(plot_name, "jpg", false);
				string[] strArrays = new string[] { "<P class=figure><A href=\"", _filename, "\"><IMG src=\"", _filename, "\" alt=\"", HTML.Process(plot_name, true), "\" height=200></A></P>" };
				strs.Add(string.Concat(strArrays));
			}
			foreach (List<PlotPoint> plotPoints in Workspace.FindLayers(p))
			{
				foreach (PlotPoint plotPoint in plotPoints)
				{
					strs.AddRange(this.get_plot_point(plotPoint));
				}
			}
			return strs;
		}

		private List<string> get_plot_point(PlotPoint pp)
		{
			List<string> strs = new List<string>()
			{
				HTML.wrap(HTML.Process(pp.Name, true), "h3")
			};
			if (pp.ReadAloud != "")
			{
				strs.Add(string.Concat("<P class=readaloud>", HTML.Process(pp.ReadAloud, false), "</P>"));
			}
			if (pp.Details != "")
			{
				strs.Add(string.Concat("<P>", HTML.Process(pp.Details, false), "</P>"));
			}
			if (pp.Date != null)
			{
				strs.Add(string.Concat("<P>Date: ", pp.Date, "</P>"));
			}
			Encounter element = pp.Element as Encounter;
			if (element != null)
			{
				strs.AddRange(HTML.get_encounter(element));
				if (element.MapID != Guid.Empty)
				{
					this.add_map(element.MapID, element.MapAreaID);
					string mapName = this.get_map_name(element.MapID, element.MapAreaID);
					string _filename = this.get_filename(mapName, "jpg", false);
					string[] strArrays = new string[] { "<P class=figure><A href=\"", _filename, "\"><IMG src=\"", _filename, "\" alt=\"", HTML.Process(pp.Name, true), "\" height=200></A></P>" };
					strs.Add(string.Concat(strArrays));
				}
			}
			TrapElement trapElement = pp.Element as TrapElement;
			if (trapElement != null)
			{
				strs.AddRange(HTML.get_trap(trapElement.Trap, null, false, false));
			}
			SkillChallenge skillChallenge = pp.Element as SkillChallenge;
			if (skillChallenge != null)
			{
				strs.AddRange(HTML.get_skill_challenge(skillChallenge, false));
			}
			Quest quest = pp.Element as Quest;
			if (quest != null)
			{
				strs.AddRange(HTML.get_quest(quest));
			}
			MapElement mapElement = pp.Element as MapElement;
			if (mapElement != null && mapElement.MapID != Guid.Empty)
			{
				this.add_map(mapElement.MapID, mapElement.MapAreaID);
				string str = this.get_map_name(mapElement.MapID, mapElement.MapAreaID);
				string _filename1 = this.get_filename(str, "jpg", false);
				string[] strArrays1 = new string[] { "<P class=figure><A href=\"", _filename1, "\"><IMG src=\"", _filename1, "\" alt=\"", HTML.Process(str, true), "\" height=200></A></P>" };
				strs.Add(string.Concat(strArrays1));
			}
			if (pp.Parcels.Count != 0)
			{
				strs.AddRange(HTML.get_parcels(pp, false));
			}
			if (pp.Subplot.Points.Count != 0)
			{
				strs.Add("<BLOCKQUOTE>");
				strs.AddRange(this.get_plot(pp.Name, pp.Subplot));
				strs.Add("</BLOCKQUOTE>");
			}
			return strs;
		}

		private static List<string> get_quest(Quest q)
		{
			string str = (q.Type == QuestType.Major ? "Major Quest" : "Minor Quest");
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD colspan=2>",
				string.Concat("<B>", str, "</B>"),
				"</TD>",
				"<TD>",
				string.Concat(q.GetXP(), " XP"),
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD colspan=3>",
				string.Concat("<B>Level</B> ", q.Level),
				"</TD>",
				"</TR>",
				"</TABLE>",
				"</P>"
			};
			return strs;
		}

		private static List<string> get_skill(SkillChallengeData scd, int level, bool include_details, bool include_links)
		{
			List<string> strs = new List<string>();
			string str = string.Concat("<B>", scd.SkillName, "</B>");
			if (include_details)
			{
				int skillDC = AI.GetSkillDC(scd.Difficulty, level) + scd.DCModifier;
				object obj = str;
				object[] objArray = new object[] { obj, " (DC ", skillDC, ")" };
				str = string.Concat(objArray);
			}
			if (scd.Details != "")
			{
				str = string.Concat(str, ": ", scd.Details);
			}
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add(HTML.Process(str, false));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (include_details)
			{
				if (scd.Success != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					strs.Add(string.Concat("<B>Success</B>: ", HTML.Process(scd.Success, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (scd.Failure != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD class=indent colspan=3>");
					strs.Add(string.Concat("<B>Failure</B>: ", HTML.Process(scd.Failure, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			strs.Add("<TR>");
			strs.Add("<TD class=indent colspan=3>");
			if (include_links)
			{
				strs.Add(string.Concat("Add a <A href=\"success:", scd.SkillName, "\">success</A>"));
				if (scd.Results.Successes > 0)
				{
					strs.Add(string.Concat("(", scd.Results.Successes, ")"));
				}
				strs.Add(string.Concat("or <A href=\"failure:", scd.SkillName, "\">failure</A>"));
				if (scd.Results.Fails > 0)
				{
					strs.Add(string.Concat("(", scd.Results.Fails, ")"));
				}
			}
			strs.Add("</TD>");
			strs.Add("</TR>");
			return strs;
		}

		private static List<string> get_skill_challenge(SkillChallenge sc, bool include_links)
		{
			List<SkillChallengeData> skillChallengeDatas = new List<SkillChallengeData>();
			List<SkillChallengeData> skillChallengeDatas1 = new List<SkillChallengeData>();
			List<SkillChallengeData> skillChallengeDatas2 = new List<SkillChallengeData>();
			foreach (SkillChallengeData skill in sc.Skills)
			{
				switch (skill.Type)
				{
					case SkillType.Primary:
					{
						skillChallengeDatas.Add(skill);
						continue;
					}
					case SkillType.Secondary:
					{
						skillChallengeDatas1.Add(skill);
						continue;
					}
					case SkillType.AutoFail:
					{
						skillChallengeDatas2.Add(skill);
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
			List<string> strs = new List<string>()
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=trap>",
				"<TD colspan=2>",
				string.Concat("<B>", HTML.Process(sc.Name, true), "</B>"),
				"</TD>",
				"<TD>",
				string.Concat(sc.GetXP(), " XP"),
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD colspan=3>",
				string.Concat("<B>Level</B> ", sc.Level),
				"<BR>"
			};
			object[] complexity = new object[] { "<B>Complexity</B> ", sc.Complexity, " (requires ", sc.Successes, " successes before 3 failures)" };
			strs.Add(string.Concat(complexity));
			strs.Add("</TD>");
			strs.Add("</TR>");
			SkillChallengeResult results = sc.Results;
			if (results.Successes + results.Fails != 0)
			{
				string str = "In Progress";
				if (results.Fails >= 3)
				{
					str = "Failed";
				}
				else if (results.Successes >= sc.Successes)
				{
					str = "Succeeded";
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", str, "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				object[] successes = new object[] { "<B>Successes</B> ", results.Successes, " <B>Failures</B> ", results.Fails };
				strs.Add(string.Concat(successes));
				if (include_links)
				{
					strs.Add("(<A href=\"sc:reset\">reset</A>)");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (skillChallengeDatas.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Primary Skills</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (SkillChallengeData skillChallengeData in skillChallengeDatas)
				{
					strs.AddRange(HTML.get_skill(skillChallengeData, sc.Level, true, include_links));
				}
			}
			if (skillChallengeDatas1.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Other Skills</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (SkillChallengeData skillChallengeDatum in skillChallengeDatas1)
				{
					strs.AddRange(HTML.get_skill(skillChallengeDatum, sc.Level, true, false));
				}
			}
			if (skillChallengeDatas2.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Automatic Failure</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (SkillChallengeData skillChallengeData1 in skillChallengeDatas2)
				{
					strs.AddRange(HTML.get_skill(skillChallengeData1, sc.Level, false, false));
				}
			}
			if (sc.Success != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Victory</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(HTML.Process(sc.Success, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (sc.Failure != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Defeat</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(HTML.Process(sc.Failure, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (sc.Notes != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Notes</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(HTML.Process(sc.Notes, true));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			SkillChallenge skillChallenge = Session.FindSkillChallenge(sc.ID, SearchType.External);
			if (skillChallenge != null)
			{
				Library library = Session.FindLibrary(skillChallenge);
				if (library != null)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=3>");
					strs.Add(HTML.Process(library.Name, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private static List<string> get_terrain_power(TerrainPower tp)
		{
			List<string> strs = new List<string>();
			if (tp == null)
			{
				strs.Add("<P class=instruction>(none)</P>");
			}
			else
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", HTML.Process(tp.Name, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add((tp.Type == TerrainPowerType.AtWill ? "At-Will Terrain" : "Single-Use Terrain"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (tp.FlavourText != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD class=readaloud colspan=2>");
					strs.Add(HTML.Process(tp.FlavourText, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Requirement != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Requirement</B> ", HTML.Process(tp.Requirement, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Check != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Check</B> ", HTML.Process(tp.Check, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Success != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Success</B> ", HTML.Process(tp.Success, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Failure != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Failure</B> ", HTML.Process(tp.Failure, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Attack != "")
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Attack</B> ", HTML.Process(tp.Attack, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Target != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Target</B> ", HTML.Process(tp.Target, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Hit != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Hit</B> ", HTML.Process(tp.Hit, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Miss != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Miss</B> ", HTML.Process(tp.Miss, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				if (tp.Effect != "")
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=2>");
					strs.Add(string.Concat("<B>Effect</B> ", HTML.Process(tp.Effect, true)));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			return strs;
		}

		private static string get_time(TimeSpan ts)
		{
			string[] str = new string[] { ts.Hours.ToString("00"), ":", ts.Minutes.ToString("00"), ":", ts.Seconds.ToString("00") };
			return string.Concat(str);
		}

		private static List<string> get_trap(Trap trap, CombatData cd, bool initiative_holder, bool builder)
		{
			List<string> strs = new List<string>();
			if (initiative_holder)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD>");
				strs.Add("<B>Information</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat(HTML.Process(trap.Name, true), " is the current initiative holder"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=trap>");
			strs.Add("<TD colspan=2>");
			strs.Add(string.Concat("<B>", HTML.Process(trap.Name, true), "</B>"));
			strs.Add("<BR>");
			strs.Add(HTML.Process(trap.Info, true));
			strs.Add("</TD>");
			strs.Add("<TD>");
			strs.Add(string.Concat(trap.XP, " XP"));
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (builder)
			{
				strs.Add("<TR class=trap>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3 align=center>");
				strs.Add("<A href=build:addskill>add a skill</A>");
				strs.Add("|");
				strs.Add("<A href=build:addattack>add an attack</A>");
				strs.Add("|");
				strs.Add("<A href=build:addcm>add a countermeasure</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str = HTML.Process(trap.ReadAloud, true);
			if (builder)
			{
				str = (str != "" ? string.Concat(str, " <A href=build:readaloud>(edit)</A>") : "<A href=build:readaloud>Click here to enter read-aloud text</A>");
			}
			if (str != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD class=readaloud colspan=3>");
				strs.Add(str);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str1 = HTML.Process(trap.Description, true);
			if (builder)
			{
				str1 = (str1 != "" ? string.Concat(str1, " <A href=build:desc>(edit)</A>") : "<A href=build:desc>Click here to enter a description</A>");
			}
			if (str1 != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(str1);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			string str2 = HTML.Process(trap.Details, true);
			if (builder)
			{
				str2 = (str2 != "" ? string.Concat(str2, " <A href=build:details>(edit)</A>") : "<A href=build:details>(no trap details entered)</A>");
			}
			if (str2 != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", trap.Type, "</B>: "));
				strs.Add(str2);
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			List<string> strs1 = new List<string>();
			Dictionary<string, List<TrapSkillData>> trapSkillDatas = new Dictionary<string, List<TrapSkillData>>();
			foreach (TrapSkillData skill in trap.Skills)
			{
				if (skill.Details == "")
				{
					continue;
				}
				if (skill.SkillName != "Perception" && !strs1.Contains(skill.SkillName))
				{
					strs1.Add(skill.SkillName);
				}
				if (!trapSkillDatas.ContainsKey(skill.SkillName))
				{
					trapSkillDatas[skill.SkillName] = new List<TrapSkillData>();
				}
				trapSkillDatas[skill.SkillName].Add(skill);
			}
			strs1.Sort();
			if (trapSkillDatas.ContainsKey("Perception"))
			{
				strs1.Insert(0, "Perception");
			}
			foreach (string str3 in strs1)
			{
				List<TrapSkillData> item = trapSkillDatas[str3];
				item.Sort();
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>", HTML.Process(str3, true), "</B>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (TrapSkillData trapSkillDatum in item)
				{
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					if (trapSkillDatum.DC != 0)
					{
						strs.Add(string.Concat("<B>DC ", trapSkillDatum.DC, "</B>:"));
					}
					strs.Add(HTML.Process(trapSkillDatum.Details, true));
					if (builder)
					{
						object[] d = new object[] { "(<A href=skill:", trapSkillDatum.ID, ">edit</A> | <A href=skillremove:", trapSkillDatum.ID, ">remove</A>)" };
						strs.Add(string.Concat(d));
					}
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			if (trap.Initiative != -2147483648)
			{
				string str4 = trap.Initiative.ToString();
				if (trap.Initiative >= 0)
				{
					str4 = string.Concat("+", str4);
				}
				if (cd != null)
				{
					object[] initiative = new object[] { cd.Initiative, " (", str4, ")" };
					str4 = string.Concat(initiative);
				}
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Initiative</B>:");
				if (builder)
				{
					strs.Add("<A href=build:profile>");
				}
				if (cd != null)
				{
					strs.Add(string.Concat("<A href=init:", cd.ID, ">"));
				}
				strs.Add(str4);
				if (cd != null)
				{
					strs.Add("</A>");
				}
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Initiative</B>: <A href=build:profile>The trap does not roll initiative</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap.Trigger != "")
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Trigger</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				if (builder)
				{
					strs.Add("<A href=build:trigger>");
				}
				strs.Add(HTML.Process(trap.Trigger, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Trigger</B>: <A href=build:trigger>Set trigger</A>");
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			foreach (TrapAttack attack in trap.Attacks)
			{
				strs.AddRange(HTML.get_trap_attack(attack, cd != null, builder));
			}
			if (trap.Countermeasures.Count != 0)
			{
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Countermeasures</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				for (int i = 0; i != trap.Countermeasures.Count; i++)
				{
					string item1 = trap.Countermeasures[i];
					strs.Add("<TR>");
					strs.Add("<TD colspan=3>");
					if (builder)
					{
						strs.Add(string.Concat("<A href=cm:", i, ">"));
					}
					strs.Add(HTML.Process(item1, true));
					if (builder)
					{
						strs.Add("</A>");
					}
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			Trap trap1 = Session.FindTrap(trap.ID, SearchType.External);
			if (trap1 != null)
			{
				Library library = Session.FindLibrary(trap1);
				if (library != null)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=3>");
					strs.Add(HTML.Process(library.Name, true));
					strs.Add("</TD>");
					strs.Add("</TR>");
				}
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			return strs;
		}

		private static List<string> get_trap_attack(TrapAttack trap_attack, bool links, bool builder)
		{
			List<string> strs = new List<string>();
			string name = trap_attack.Name;
			if (name == "")
			{
				name = "Attack";
			}
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=3>");
			strs.Add(string.Concat("<B>", name, "</B>"));
			if (builder)
			{
				strs.Add(string.Concat("<A href=attackaction:", trap_attack.ID, ">"));
				strs.Add("(edit)");
				strs.Add("</A>");
				strs.Add(string.Concat("<A href=attackremove:", trap_attack.ID, ">"));
				strs.Add("(remove)");
				strs.Add("</A>");
			}
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Action</B>:");
			if (builder)
			{
				strs.Add(string.Concat("<A href=attackaction:", trap_attack.ID, ">"));
			}
			strs.Add(trap_attack.Action.ToString().ToLower());
			if (builder)
			{
				strs.Add("</A>");
			}
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (trap_attack.Range != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Range</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackaction:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.Range, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Range</B>: <A href=attackaction:", trap_attack.ID, ">Set range</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.Target != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Target</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackaction:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.Target, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Target</B>: <A href=attackaction:", trap_attack.ID, ">Set target</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.Attack != null)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Attack</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackattack:", trap_attack.ID, ">"));
				}
				if (links)
				{
					strs.Add(string.Concat("<A href=power:", trap_attack.ID, ">"));
				}
				strs.Add(trap_attack.Attack.ToString());
				if (links)
				{
					strs.Add("</A>");
				}
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Attack</B>: <A href=attackattack:", trap_attack.ID, ">Set attack</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.OnHit != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Hit</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackhit:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.OnHit, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Hit</B>: <A href=attackhit:", trap_attack.ID, ">Set hit</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.OnMiss != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Miss</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackmiss:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.OnMiss, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Miss</B>: <A href=attackmiss:", trap_attack.ID, ">Set miss</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.Effect != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Effect</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attackeffect:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.Effect, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Effect</B>: <A href=attackeffect:", trap_attack.ID, ">Set effect</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			if (trap_attack.Notes != "")
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Notes</B>:");
				if (builder)
				{
					strs.Add(string.Concat("<A href=attacknotes:", trap_attack.ID, ">"));
				}
				strs.Add(HTML.Process(trap_attack.Notes, true));
				if (builder)
				{
					strs.Add("</A>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			else if (builder)
			{
				strs.Add("<TR>");
				strs.Add("<TD colspan=3>");
				strs.Add(string.Concat("<B>Notes</B>: <A href=attacknotes:", trap_attack.ID, ">Set notes</A>"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			return strs;
		}

		public static List<string> GetHead(string title, string description, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HEAD>"
			};
			if (title != null)
			{
				strs.Add(HTML.wrap(title, "title"));
			}
			if (description != null)
			{
				strs.Add(string.Concat("<META name=\"Description\" content=\"", description, "\">"));
			}
			strs.Add("<META name=\"Generator\" content=\"Masterplan\">");
			strs.Add("<META name=\"Originator\" content=\"Masterplan\">");
			strs.AddRange(HTML.GetStyle(size));
			strs.Add("</HEAD>");
			return strs;
		}

		public static List<string> GetStyle(DisplaySize size)
		{
			if (HTML.fStyles.ContainsKey(size))
			{
				return HTML.fStyles[size];
			}
			Dictionary<int, int> nums = new Dictionary<int, int>();
			switch (size)
			{
				case DisplaySize.Small:
				{
					nums[8] = 8;
					nums[9] = 9;
					nums[12] = 12;
					nums[16] = 16;
					nums[24] = 24;
					break;
				}
				case DisplaySize.Medium:
				{
					nums[8] = 16;
					nums[9] = 18;
					nums[12] = 24;
					nums[16] = 32;
					nums[24] = 48;
					break;
				}
				case DisplaySize.Large:
				{
					nums[8] = 25;
					nums[9] = 30;
					nums[12] = 40;
					nums[16] = 50;
					nums[24] = 72;
					break;
				}
			}
			Dictionary<int, int> nums1 = new Dictionary<int, int>();
			switch (size)
			{
				case DisplaySize.Small:
				{
					nums1[15] = 15;
					nums1[300] = 300;
					break;
				}
				case DisplaySize.Medium:
				{
					nums1[15] = 30;
					nums1[300] = 600;
					break;
				}
				case DisplaySize.Large:
				{
					nums1[15] = 45;
					nums1[300] = 1000;
					break;
				}
			}
			List<string> strs = new List<string>()
			{
				"<STYLE type=\"text/css\">"
			};
			bool flag = false;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				object[] objArray = new object[] { FileName.Directory(entryAssembly.Location), "Style.", size, ".css" };
				string str = string.Concat(objArray);
				if (File.Exists(str))
				{
					strs.AddRange(File.ReadAllLines(str));
					flag = true;
				}
			}
			if (!flag)
			{
				strs.Add(string.Concat("body                 { font-family: Arial; font-size: ", nums[9], "pt }"));
				strs.Add("h1, h2, h3, h4       { color: #000060 }");
				strs.Add(string.Concat("h1                   { font-size: ", nums[24], "pt; font-weight: bold; text-align: center }"));
				strs.Add(string.Concat("h2                   { font-size: ", nums[16], "pt; font-weight: bold; text-align: center }"));
				strs.Add(string.Concat("h3                   { font-size: ", nums[12], "pt }"));
				strs.Add(string.Concat("h4                   { font-size: ", nums[9], "pt }"));
				strs.Add("p                    { }");
				strs.Add(string.Concat("p.instruction        { color: #666666; text-align: center; font-size: ", nums[8], "pt }"));
				strs.Add("p.description        { }");
				strs.Add("p.signature          { color: #666666; text-align: center }");
				object[] item = new object[] { "p.readaloud          { padding-left: ", nums1[15], "px; padding-right: ", nums1[15], "px; font-style: italic }" };
				strs.Add(string.Concat(item));
				strs.Add("p.background         { }");
				strs.Add("p.encounter_note     { }");
				strs.Add("p.encyclopedia_entry { }");
				strs.Add("p.note               { }");
				strs.Add("p.table              { text-align: center }");
				strs.Add("p.figure             { text-align: center }");
				object[] item1 = new object[] { "table                { font-size: ", nums[8], "pt; border-color: #BBBBBB; border-style: solid; border-width: 1px; border-collapse: collapse; table-layout: fixed; width: ", nums1[300], "px }" };
				strs.Add(string.Concat(item1));
				strs.Add("table.clear          { border-style: none; table-layout: fixed; width: 99% }");
				strs.Add("table.wide           { width: 99% }");
				strs.Add("table.initiative     { table-layout: auto; border-style: none; width=99% }");
				strs.Add("tr                   { background-color: #E1E7C5 }");
				strs.Add("tr.clear             { background-color: #FFFFFF }");
				strs.Add("tr.heading           { background-color: #143D5F; color: #FFFFFF }");
				strs.Add("tr.trap              { background-color: #5B1F34; color: #FFFFFF }");
				strs.Add("tr.template          { background-color: #5B1F34; color: #FFFFFF }");
				strs.Add("tr.creature          { background-color: #364F27; color: #FFFFFF }");
				strs.Add("tr.hero              { background-color: #143D5F; color: #FFFFFF }");
				strs.Add("tr.item              { background-color: #D06015; color: #FFFFFF }");
				strs.Add("tr.artifact          { background-color: #5B1F34; color: #FFFFFF }");
				strs.Add("tr.encounterlog      { background-color: #303030; color: #FFFFFF }");
				strs.Add("tr.shaded            { background-color: #9FA48D }");
				strs.Add("tr.dimmed            { color: #666666; text-decoration: line-through }");
				strs.Add("tr.shaded_dimmed     { background-color: #9FA48D; color: #666666 }");
				strs.Add("tr.atwill            { background-color: #238E23; color: #FFFFFF }");
				strs.Add("tr.encounter         { background-color: #8B0000; color: #FFFFFF }");
				strs.Add("tr.daily             { background-color: #000000; color: #FFFFFF }");
				strs.Add("tr.warning           { background-color: #E5A0A0; color: #000000; text-align: center }");
				strs.Add("td                   { padding-top: 2px; padding-bottom: 2px; vertical-align: top }");
				strs.Add("td.clear             { vertical-align: top }");
				strs.Add(string.Concat("td.indent            { padding-left: ", nums1[15], "px }"));
				strs.Add("td.readaloud         { font-style: italic }");
				strs.Add("td.dimmed            { color: #666666 }");
				strs.Add("td.pvlogentry        { color: lightgrey; background-color: #000000 }");
				strs.Add(string.Concat("td.pvlogindent       { color: #FFFFFF; background-color: #000000; padding-left: ", nums1[15], "px }"));
				strs.Add(string.Concat("ul, ol               { font-size: ", nums[8], "pt }"));
				strs.Add("a                    { text-decoration: none }");
				strs.Add("a:link               { color: #0000C0 }");
				strs.Add("a:visited            { color: #0000C0 }");
				strs.Add("a:active             { color: #FF0000 }");
				strs.Add("a.missing            { color: #FF0000 }");
				strs.Add("a:hover              { text-decoration: underline }");
			}
			strs.Add("</STYLE>");
			HTML.fStyles[size] = strs;
			return strs;
		}

		public static string Goal(Goal goal)
		{
			List<string> strs = new List<string>();
			if (goal == null)
			{
				strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				strs.Add("<BODY>");
				strs.Add("<P>On this screen you can define <B>party goals</B>.</P>");
				strs.Add("<P>Party goals specify the challenges the party will face during the adventure - for example, <I>rescuing the princess</I>.</P>");
				strs.Add("<P>Goals can have sub-goals - for example, <I>finding where the princess is being held</I>, <I>cracking the code that unlocks the door</I>, <I>obtaining the right tools</I>, and so on. This can go as many levels deep as you like, and you can reorder your goals by dragging them around.</P>");
				strs.Add("<P>When you have finished, press <B>OK</B>; an outline plot will be built for you.</P>");
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			else
			{
				strs.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				strs.Add(string.Concat("<H3>", HTML.Process(goal.Name, true), "</H3>"));
				if (goal.Details != "")
				{
					string str = HTML.Process(goal.Details, true);
					strs.Add(HTML.fMarkdown.Transform(str));
				}
				else
				{
					strs.Add("<P class=instruction>(no details)</P>");
				}
				if (goal.Prerequisites.Count != 0)
				{
					strs.Add("<P><B>Prerequisite Goals</B>:</P>");
					strs.Add("<UL>");
					foreach (Goal prerequisite in goal.Prerequisites)
					{
						strs.Add(string.Concat("<LI>", HTML.Process(prerequisite.Name, true), "</LI>"));
					}
					strs.Add("</UL>");
				}
			}
			return HTML.Concatenate(strs);
		}

		public static string Handout(List<object> items, DisplaySize size, bool include_dm_info)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(Session.Project.Name, "Handout", size));
			strs.Add("<BODY>");
			if (items.Count == 0)
			{
				strs.Add("<P class=instruction>(no items selected)</P>");
			}
			else
			{
				foreach (object item in items)
				{
					if (item is Background)
					{
						Background background = item as Background;
						string str = HTML.Process(background.Details, false);
						str = HTML.fMarkdown.Transform(str);
						str = str.Replace("<p>", "<p class=background>");
						strs.Add(string.Concat("<H3>", HTML.Process(background.Title, true), "</H3>"));
						strs.Add(str);
					}
					if (item is EncyclopediaEntry)
					{
						EncyclopediaEntry encyclopediaEntry = item as EncyclopediaEntry;
						strs.Add(string.Concat("<H3>", HTML.Process(encyclopediaEntry.Name, true), "</H3>"));
						string str1 = HTML.process_encyclopedia_info(encyclopediaEntry.Details, Session.Project.Encyclopedia, false, include_dm_info);
						strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(str1, false), "</P>"));
						if (include_dm_info && encyclopediaEntry.DMInfo != "")
						{
							string str2 = HTML.process_encyclopedia_info(encyclopediaEntry.DMInfo, Session.Project.Encyclopedia, false, include_dm_info);
							strs.Add("<H4>For DMs Only</H4>");
							strs.Add(string.Concat("<P class=encyclopedia_entry>", HTML.Process(str2, false), "</P>"));
						}
					}
					if (!(item is IPlayerOption))
					{
						continue;
					}
					IPlayerOption playerOption = item as IPlayerOption;
					strs.Add(string.Concat("<H3>", HTML.Process(playerOption.Name, true), "</H3>"));
					strs.AddRange(HTML.get_player_option(playerOption));
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string MagicItem(MagicItem item, DisplaySize size, bool builder, bool wrapper)
		{
			List<string> strs = new List<string>();
			if (wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetHead(null, null, size));
			}
			strs.Add("<BODY>");
			if (item == null)
			{
				strs.Add("<P class=instruction>(no item selected)</P>");
			}
			else
			{
				strs.AddRange(HTML.get_magic_item(item, builder));
			}
			if (wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string MapArea(MapArea area, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (area != null)
			{
				string str = HTML.Process(area.Name, true);
				strs.Add(string.Concat("<H3>", str, "</H3>"));
				if (area.Details != "")
				{
					strs.Add("<P>");
					strs.Add(HTML.Process(area.Details, true));
					strs.Add("</P>");
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD><B>Options</B></TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"maparea:edit\">View information</A> about this map area.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"maparea:create\">Create a plot point</A> here.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maparea:encounter\">combat encounter</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maparea:trap\">trap or hazard</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maparea:challenge\">skill challenge</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			return HTML.Concatenate(strs);
		}

		public static string MapLocation(MapLocation loc, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (loc != null)
			{
				string str = HTML.Process(loc.Name, true);
				strs.Add(string.Concat("<H3>", str, "</H3>"));
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD><B>Options</B></TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"maploc:edit\">View information</A> about this map location.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add("<A href=\"maploc:create\">Create a plot point</A> here.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maploc:encounter\">combat encounter</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maploc:trap\">trap or hazard</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD class=indent>");
				strs.Add("... containing a <A href=\"maploc:challenge\">skill challenge</A>.");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			return HTML.Concatenate(strs);
		}

		public static string PartyBreakdown(DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead("Party", null, size));
			strs.Add("<BODY>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=heading>");
			strs.Add("<TD colspan=2>");
			strs.Add("<B>Party</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=2>");
			strs.Add("<B>PCs</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			Dictionary<HeroRoleType, int> heroRoleTypes = new Dictionary<HeroRoleType, int>();
			foreach (HeroRoleType value in Enum.GetValues(typeof(HeroRoleType)))
			{
				heroRoleTypes[value] = 0;
			}
			foreach (Hero hero in Session.Project.Heroes)
			{
				string str = string.Concat("<B>", hero.Name, "</B>");
				if (hero.Player != "")
				{
					str = string.Concat(str, " (", hero.Player, ")");
				}
				string race = hero.Race;
				if (hero.Class != null && hero.Class != "")
				{
					race = string.Concat(race, " ", hero.Class);
				}
				if (hero.ParagonPath != null && hero.ParagonPath != "")
				{
					race = string.Concat(race, " / ", hero.ParagonPath);
				}
				if (hero.EpicDestiny != null && hero.EpicDestiny != "")
				{
					race = string.Concat(race, " / ", hero.EpicDestiny);
				}
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(str);
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(race);
				strs.Add("</TD>");
				strs.Add("</TR>");
				Dictionary<HeroRoleType, int> item = heroRoleTypes;
				Dictionary<HeroRoleType, int> heroRoleTypes1 = item;
				HeroRoleType role = hero.Role;
				item[role] = heroRoleTypes1[role] + 1;
			}
			strs.Add("<TR class=shaded>");
			strs.Add("<TD colspan=2>");
			strs.Add("<B>Roles</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			foreach (HeroRoleType key in heroRoleTypes.Keys)
			{
				strs.Add("<TR>");
				strs.Add("<TD>");
				strs.Add(string.Concat("<B>", key, "</B>"));
				strs.Add("</TD>");
				strs.Add("<TD>");
				strs.Add(heroRoleTypes[key].ToString());
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string PCs(string secondary, DisplaySize size)
		{
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (Session.Project == null)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(no project loaded)");
				strs.Add("</P>");
			}
			else if (Session.Project.Heroes.Count != 0)
			{
				int num = 2147483647;
				int num1 = 2147483647;
				int num2 = 2147483647;
				int num3 = 2147483647;
				int num4 = -2147483648;
				int num5 = -2147483648;
				int num6 = -2147483648;
				int num7 = -2147483648;
				int num8 = 2147483647;
				int num9 = 2147483647;
				int num10 = -2147483648;
				int num11 = -2147483648;
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (Hero hero in Session.Project.Heroes)
				{
					num = Math.Min(num, hero.AC);
					num1 = Math.Min(num1, hero.Fortitude);
					num2 = Math.Min(num2, hero.Reflex);
					num3 = Math.Min(num3, hero.Will);
					num4 = Math.Max(num4, hero.AC);
					num5 = Math.Max(num5, hero.Fortitude);
					num6 = Math.Max(num6, hero.Reflex);
					num7 = Math.Max(num7, hero.Will);
					num8 = Math.Min(num8, hero.PassivePerception);
					num9 = Math.Min(num9, hero.PassiveInsight);
					num10 = Math.Max(num10, hero.PassivePerception);
					num11 = Math.Max(num11, hero.PassiveInsight);
					string str = hero.Languages.Replace(".", "");
					str = str.Replace(",", "");
					str = str.Replace(";", "");
					str = str.Replace(":", "");
					string[] strArrays = str.Replace("/", "").Split(null);
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string str1 = strArrays[i];
						if (str1 != "")
						{
							binarySearchTree.Add(str1);
						}
					}
				}
				strs.Add("<P class=table>");
				strs.Add("<TABLE class=clear>");
				strs.Add("<TR class=clear>");
				strs.Add("<TD class=clear>");
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				strs.Add("<TR class=heading>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Party Breakdown</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>The Party</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				foreach (Hero hero1 in Session.Project.Heroes)
				{
					strs.Add("<TR>");
					object[] d = new object[] { "<TD><A href=show:", hero1.ID, ">", hero1.Name, "</A></TD>" };
					strs.Add(string.Concat(d));
					strs.Add(string.Concat("<TD colspan=2>", hero1.Info, "</TD>"));
					strs.Add("</TR>");
				}
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Defences</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:ac>Armour Class</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num != num4)
				{
					strs.Add(string.Concat(num, " - ", num4));
				}
				else
				{
					strs.Add(num.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:fort>Fortitude</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num1 != num5)
				{
					strs.Add(string.Concat(num1, " - ", num5));
				}
				else
				{
					strs.Add(num1.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:ref>Reflex</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num2 != num6)
				{
					strs.Add(string.Concat(num2, " - ", num6));
				}
				else
				{
					strs.Add(num2.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:will>Will</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num3 != num7)
				{
					strs.Add(string.Concat(num3, " - ", num7));
				}
				else
				{
					strs.Add(num3.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR class=shaded>");
				strs.Add("<TD colspan=3>");
				strs.Add("<B>Passive Skills</B>");
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:passiveinsight>Insight</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num9 != num11)
				{
					strs.Add(string.Concat(num9, " - ", num11));
				}
				else
				{
					strs.Add(num9.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("<TR>");
				strs.Add("<TD><A href=show:passiveperception>Perception</A></TD>");
				strs.Add("<TD colspan=2>");
				if (num8 != num10)
				{
					strs.Add(string.Concat(num8, " - ", num10));
				}
				else
				{
					strs.Add(num8.ToString());
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				if (binarySearchTree.Count != 0)
				{
					strs.Add("<TR class=shaded>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Known Languages</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (string sortedList in binarySearchTree.SortedList)
					{
						string str2 = "";
						foreach (Hero hero2 in Session.Project.Heroes)
						{
							if (!hero2.Languages.Contains(sortedList))
							{
								continue;
							}
							if (str2 != "")
							{
								str2 = string.Concat(str2, ", ");
							}
							str2 = string.Concat(str2, hero2.Name);
						}
						strs.Add("<TR>");
						strs.Add(string.Concat("<TD>", sortedList, "</TD>"));
						strs.Add(string.Concat("<TD colspan=2>", str2, "</TD>"));
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
				strs.Add("</TD>");
				strs.Add("<TD class=clear>");
				if (secondary != "")
				{
					Guid empty = Guid.Empty;
					try
					{
						empty = new Guid(secondary);
					}
					catch
					{
						empty = Guid.Empty;
					}
					if (empty == Guid.Empty)
					{
						string str3 = "";
						Dictionary<int, string> nums = new Dictionary<int, string>();
						if (secondary == "ac")
						{
							str3 = "Armour Class";
						}
						if (secondary == "fort")
						{
							str3 = "Fortitude";
						}
						if (secondary == "ref")
						{
							str3 = "Reflex";
						}
						if (secondary == "will")
						{
							str3 = "Will";
						}
						if (secondary == "passiveinsight")
						{
							str3 = "Passive Insight";
						}
						if (secondary == "passiveperception")
						{
							str3 = "Passive Perception";
						}
						foreach (Hero hero3 in Session.Project.Heroes)
						{
							int aC = 0;
							if (secondary == "ac")
							{
								aC = hero3.AC;
							}
							if (secondary == "fort")
							{
								aC = hero3.Fortitude;
							}
							if (secondary == "ref")
							{
								aC = hero3.Reflex;
							}
							if (secondary == "will")
							{
								aC = hero3.Will;
							}
							if (secondary == "passiveinsight")
							{
								aC = hero3.PassiveInsight;
							}
							if (secondary == "passiveperception")
							{
								aC = hero3.PassivePerception;
							}
							object[] objArray = new object[] { "<A href=show:", hero3.ID, ">", hero3.Name, "</A>" };
							string str4 = string.Concat(objArray);
							if (!nums.ContainsKey(aC))
							{
								nums[aC] = str4;
							}
							else
							{
								Dictionary<int, string> nums1 = nums;
								Dictionary<int, string> nums2 = nums1;
								int num12 = aC;
								int num13 = num12;
								nums1[num12] = string.Concat(nums2[num13], ", ", str4);
							}
						}
						strs.Add("<P class=table>");
						strs.Add("<TABLE>");
						strs.Add("<TR class=heading>");
						strs.Add("<TD colspan=3>");
						strs.Add(string.Concat("<B>", str3, "</B>"));
						strs.Add("</TD>");
						strs.Add("</TR>");
						List<int> nums3 = new List<int>(nums.Keys);
						nums3.Sort();
						nums3.Reverse();
						foreach (int num14 in nums3)
						{
							strs.Add("<TR>");
							strs.Add(string.Concat("<TD>", num14, "</TD>"));
							strs.Add(string.Concat("<TD colspan=2>", nums[num14], "</TD>"));
							strs.Add("</TR>");
						}
						strs.Add("</TABLE>");
						strs.Add("</P>");
					}
					else
					{
						Hero hero4 = Session.Project.FindHero(empty);
						strs.Add(HTML.StatBlock(hero4, null, false, false, false, size));
					}
				}
				else
				{
					strs.Add("<P class=instruction>");
					strs.Add("Click on a link to the right to show details here");
					strs.Add("</P>");
				}
				strs.Add("</TD>");
				strs.Add("</TR>");
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			else
			{
				strs.Add("<P class=instruction>");
				strs.Add("No PC details have been entered; click <A href=\"party:edit\">here</A> to do this now.");
				strs.Add("</P>");
				strs.Add("<P class=instruction>");
				strs.Add("When PCs have been entered, you will see a useful breakdown of their defences, passive skills and known languages here.");
				strs.Add("</P>");
			}
			strs.Add("</BODY>");
			return HTML.Concatenate(strs);
		}

		public static string PlayerOption(IPlayerOption option, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (option == null)
			{
				strs.Add("<P class=instruction>(no item selected)</P>");
			}
			else
			{
				strs.AddRange(HTML.get_player_option(option));
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string PlotPoint(PlotPoint pp, Plot plot, int party_level, bool links, MainForm.ViewType view, DisplaySize size)
		{
			object[] d;
			if (Session.Project == null)
			{
				return null;
			}
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			if (pp == null)
			{
				PlotPoint plotPoint = Session.Project.FindParent(plot);
				string str = (plotPoint != null ? plotPoint.Name : Session.Project.Name);
				strs.Add(string.Concat("<H2>", HTML.Process(str, true), "</H2>"));
				if (plotPoint == null)
				{
					if (Session.Project.Author != "")
					{
						strs.Add(string.Concat("<P class=instruction>by ", Session.Project.Author, "</P>"));
					}
					int num = Session.Project.Party.Size;
					int level = Session.Project.Party.Level;
					int xP = Session.Project.Party.XP;
					int heroXP = Experience.GetHeroXP(level);
					object[] objArray = new object[] { "<B>", HTML.Process(Session.Project.Name, true), "</B> is designed for a party of ", num, " characters at level ", level };
					string str1 = string.Concat(objArray);
					if (xP != heroXP)
					{
						object obj = str1;
						object[] objArray1 = new object[] { obj, ", starting with ", xP, " XP" };
						str1 = string.Concat(objArray1);
					}
					str1 = string.Concat(str1, ".");
					strs.Add(string.Concat("<P>", str1, "</P>"));
				}
				else
				{
					if (plotPoint.Date != null)
					{
						strs.Add(string.Concat("<P>", plotPoint.Date, "</P>"));
					}
					if (plotPoint.Details != "")
					{
						strs.Add(string.Concat("<P>", HTML.Process(plotPoint.Details, false), "</P>"));
					}
				}
				int layerXP = 0;
				foreach (List<PlotPoint> plotPoints in Workspace.FindLayers(plot))
				{
					layerXP += Workspace.GetLayerXP(plotPoints);
				}
				if (layerXP != 0)
				{
					string str2 = string.Concat("XP available: ", layerXP, ".");
					if (plot == Session.Project.Plot)
					{
						int level1 = Session.Project.Party.Level;
						int heroXP1 = Experience.GetHeroXP(level1);
						int num1 = heroXP1 + layerXP / Session.Project.Party.Size;
						int heroLevel = Experience.GetHeroLevel(num1);
						if (heroLevel != -1 && heroLevel != level1)
						{
							str2 = string.Concat(str2, "<BR>");
							object obj1 = str2;
							object[] objArray2 = new object[] { obj1, "The party will reach level ", heroLevel, "." };
							str2 = string.Concat(objArray2);
						}
					}
					strs.Add(string.Concat("<P>", str2, "</P>"));
				}
				if (links)
				{
					strs.Add("<P class=table>");
					strs.Add("<TABLE>");
					strs.Add("<TR class=heading>");
					strs.Add("<TD><B>Options</B></TD>");
					strs.Add("</TR>");
					if (view == MainForm.ViewType.Flowchart)
					{
						if (plot.Points.Count == 0)
						{
							strs.Add("<TR>");
							strs.Add("<TD>This plot is empty:</TD>");
							strs.Add("</TR>");
							strs.Add("<TR>");
							strs.Add("<TD class=indent>Add a <A href=\"plot:add\">plot point</A>.</TD>");
							strs.Add("</TR>");
							strs.Add("<TR>");
							strs.Add("<TD class=indent>Add a <A href=\"plot:encounter\">combat encounter</A>.</TD>");
							strs.Add("</TR>");
							strs.Add("<TR>");
							strs.Add("<TD class=indent>Add a <A href=\"plot:challenge\">skill challenge</A>.</TD>");
							strs.Add("</TR>");
							strs.Add("<TR>");
							strs.Add("<TD>Build a plot by setting the <A href=\"plot:goals\">party goals</A>.</TD>");
							strs.Add("</TR>");
						}
						if (plotPoint != null)
						{
							strs.Add("<TR>");
							strs.Add("<TD>Move up <A href=\"plot:up\">one plot level</A>.</TD>");
							strs.Add("</TR>");
						}
						List<Guid> guids = plot.FindTacticalMaps();
						if (guids.Count == 0)
						{
							if (Session.Project.Maps.Count != 0)
							{
								strs.Add("<TR>");
								strs.Add("<TD>Use a tactical map as the basis of this plot:</TD>");
								strs.Add("</TR>");
								strs.Add("<TR>");
								strs.Add("<TD class=indent>Build a <A href=\"delveview:build\">new map</A>.</TD>");
								strs.Add("</TR>");
								strs.Add("<TR>");
								strs.Add("<TD class=indent>Select an <A href=\"delveview:select\">existing map</A>.</TD>");
								strs.Add("</TR>");
							}
							else
							{
								strs.Add("<TR>");
								strs.Add("<TD>Create a <A href=\"delveview:build\">tactical map</A> to use as the basis of this plot.</TD>");
								strs.Add("</TR>");
							}
						}
						else if (guids.Count != 1)
						{
							strs.Add("<TR>");
							strs.Add("<TD>Switch to delve view using one of the following maps:</TD>");
							strs.Add("</TR>");
							foreach (Guid guid in guids)
							{
								if (guid == Guid.Empty)
								{
									continue;
								}
								Map map = Session.Project.FindTacticalMap(guid);
								if (map == null)
								{
									continue;
								}
								strs.Add("<TR>");
								d = new object[] { "<TD class=indent><A href=\"delveview:", guid, "\">", HTML.Process(map.Name, true), "</A></TD>" };
								strs.Add(string.Concat(d));
								strs.Add("</TR>");
							}
							strs.Add("<TR>");
							strs.Add("<TD class=indent><A href=\"delveview:select\">Select (or create) a map</A></TD>");
							strs.Add("</TR>");
						}
						else
						{
							strs.Add("<TR>");
							strs.Add(string.Concat("<TD>Switch to <A href=\"delveview:", guids[0], "\">delve view</A>.</TD>"));
							strs.Add("</TR>");
						}
						List<Guid> guids1 = plot.FindRegionalMaps();
						if (guids1.Count == 0)
						{
							if (Session.Project.RegionalMaps.Count != 0)
							{
								strs.Add("<TR>");
								strs.Add("<TD>Use a regional map as the basis of this plot:</TD>");
								strs.Add("</TR>");
								strs.Add("<TR>");
								strs.Add("<TD class=indent>Build a <A href=\"mapview:build\">new map</A>.</TD>");
								strs.Add("</TR>");
								strs.Add("<TR>");
								strs.Add("<TD class=indent>Select an <A href=\"mapview:select\">existing map</A>.</TD>");
								strs.Add("</TR>");
							}
							else
							{
								strs.Add("<TR>");
								strs.Add("<TD>Create a <A href=\"mapview:build\">regional map</A> to use as the basis of this plot.</TD>");
								strs.Add("</TR>");
							}
						}
						else if (guids1.Count != 1)
						{
							strs.Add("<TR>");
							strs.Add("<TD>Switch to map view using one of the following maps:</TD>");
							strs.Add("</TR>");
							foreach (Guid guid1 in guids1)
							{
								if (guid1 == Guid.Empty)
								{
									continue;
								}
								RegionalMap regionalMap = Session.Project.FindRegionalMap(guid1);
								if (regionalMap == null)
								{
									continue;
								}
								strs.Add("<TR>");
								d = new object[] { "<TD class=indent><A href=\"mapview:", guid1, "\">", HTML.Process(regionalMap.Name, true), "</A></TD>" };
								strs.Add(string.Concat(d));
								strs.Add("</TR>");
							}
							strs.Add("<TR>");
							strs.Add("<TD class=indent><A href=\"mapview:select\">Select (or create) a map</A></TD>");
							strs.Add("</TR>");
						}
						else
						{
							strs.Add("<TR>");
							strs.Add(string.Concat("<TD>Switch to <A href=\"mapview:", guids1[0], "\">map view</A>.</TD>"));
							strs.Add("</TR>");
						}
						if (plotPoint == null)
						{
							strs.Add("<TR>");
							strs.Add("<TD>Edit the <A href=\"plot:properties\">project properties</A>.</TD>");
							strs.Add("</TR>");
						}
					}
					else if (view == MainForm.ViewType.Delve)
					{
						strs.Add("<TR>");
						strs.Add("<TD>Switch to <A href=\"delveview:off\">flowchart view</A>.</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD><A href=\"delveview:edit\">Edit this map</A>.</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD>Send this map to the <A href=\"delveview:playerview\">player view</A>.</TD>");
						strs.Add("</TR>");
					}
					else if (view == MainForm.ViewType.Map)
					{
						strs.Add("<TR>");
						strs.Add("<TD>Switch to <A href=\"mapview:off\">flowchart view</A>.</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD><A href=\"mapview:edit\">Edit this map</A>.</TD>");
						strs.Add("</TR>");
						strs.Add("<TR>");
						strs.Add("<TD>Send this map to the <A href=\"mapview:playerview\">player view</A>.</TD>");
						strs.Add("</TR>");
					}
					strs.Add("</TR>");
					strs.Add("</TABLE>");
					strs.Add("</P>");
				}
			}
			else
			{
				strs.Add(string.Concat("<H3>", HTML.Process(pp.Name, true), "</H3>"));
				switch (pp.State)
				{
					case PlotPointState.Completed:
					{
						strs.Add("<P class=instruction>(completed)</P>");
						break;
					}
					case PlotPointState.Skipped:
					{
						strs.Add("<P class=instruction>(skipped)</P>");
						break;
					}
				}
				if (links)
				{
					List<string> strs1 = new List<string>();
					if (view == MainForm.ViewType.Flowchart)
					{
						strs1.Add("<A href=\"plot:edit\">Open</A> this plot point.");
					}
					if (pp.Element == null)
					{
						strs1.Add("Turn this point into a <A href=plot:encounter>combat encounter</A>.");
						strs1.Add("Turn this point into a <A href=plot:challenge>skill challenge</A>.");
					}
					if (pp.Subplot.Points.Count == 0)
					{
						strs1.Add("Create a <A href=\"plot:explore\">subplot</A> for this point.");
					}
					else
					{
						strs1.Add("This plot point has a <A href=\"plot:explore\">subplot</A>.");
					}
					if (pp.Element is Encounter)
					{
						strs1.Add("This plot point contains an <A href=plot:element>encounter</A> (<A href=plot:run>run it</a>).");
					}
					if (pp.Element is SkillChallenge)
					{
						strs1.Add("This plot point contains a <A href=plot:element>skill challenge</A>.");
					}
					TrapElement element = pp.Element as TrapElement;
					if (element != null)
					{
						strs1.Add(string.Concat("This plot point contains a <A href=plot:element>", (element.Trap.Type == TrapType.Trap ? "trap" : "hazard"), "</A>."));
					}
					Map map1 = null;
					MapArea mapArea = null;
					pp.GetTacticalMapArea(ref map1, ref mapArea);
					if (map1 != null && mapArea != null)
					{
						string str3 = HTML.Process(mapArea.Name, true);
						strs1.Add(string.Concat("This plot point occurs in <A href=plot:maparea>", str3, "</A>."));
					}
					RegionalMap regionalMap1 = null;
					MapLocation mapLocation = null;
					pp.GetRegionalMapArea(ref regionalMap1, ref mapLocation, Session.Project);
					if (regionalMap1 != null && mapLocation != null)
					{
						string str4 = HTML.Process(mapLocation.Name, true);
						strs1.Add(string.Concat("This plot point occurs at <A href=plot:maploc>", str4, "</A>."));
					}
					if (strs1.Count != 0)
					{
						strs.Add("<P class=table>");
						strs.Add("<TABLE>");
						strs.Add("<TR class=heading>");
						strs.Add("<TD><B>Options</B></TD>");
						strs.Add("</TR>");
						for (int i = 0; i != strs1.Count; i++)
						{
							strs.Add("<TR>");
							strs.Add("<TD>");
							strs.Add(strs1[i]);
							strs.Add("</TD>");
							strs.Add("</TR>");
						}
						strs.Add("</TABLE>");
						strs.Add("</P>");
					}
				}
				string str5 = HTML.Process(pp.ReadAloud, false);
				if (str5 != "")
				{
					str5 = HTML.fMarkdown.Transform(str5);
					str5 = str5.Replace("<p>", "<p class=readaloud>");
					strs.Add(str5);
				}
				string str6 = HTML.Process(pp.Details, false);
				if (str6 != "")
				{
					str6 = HTML.fMarkdown.Transform(str6);
					strs.Add(str6);
				}
				if (party_level != Session.Project.Party.Level)
				{
					strs.Add(string.Concat("<P><B>Party level</B>: ", party_level, "</P>"));
				}
				if (pp.Date != null)
				{
					strs.Add(string.Concat("<P><B>Date</B>: ", pp.Date, "</P>"));
				}
				strs.AddRange(HTML.get_map_area_details(pp));
				if (links)
				{
					BinarySearchTree<EncyclopediaEntry> binarySearchTree = new BinarySearchTree<EncyclopediaEntry>();
					foreach (Guid encyclopediaEntryID in pp.EncyclopediaEntryIDs)
					{
						EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(encyclopediaEntryID);
						if (encyclopediaEntry == null)
						{
							continue;
						}
						binarySearchTree.Add(encyclopediaEntry);
					}
					if (pp.MapLocationID != Guid.Empty)
					{
						EncyclopediaEntry encyclopediaEntry1 = Session.Project.Encyclopedia.FindEntryForAttachment(pp.MapLocationID);
						if (encyclopediaEntry1 != null)
						{
							binarySearchTree.Add(encyclopediaEntry1);
						}
					}
					if (pp.Element != null && pp.Element is Encounter)
					{
						Encounter encounter = pp.Element as Encounter;
						foreach (NPC nPC in Session.Project.NPCs)
						{
							EncyclopediaEntry encyclopediaEntry2 = Session.Project.Encyclopedia.FindEntryForAttachment(nPC.ID);
							if (encyclopediaEntry2 == null || !encounter.Contains(nPC.ID))
							{
								continue;
							}
							binarySearchTree.Add(encyclopediaEntry2);
						}
					}
					List<EncyclopediaEntry> sortedList = binarySearchTree.SortedList;
					if (sortedList.Count != 0)
					{
						strs.Add("<P><B>See also</B>:</P>");
						strs.Add("<UL>");
						foreach (EncyclopediaEntry encyclopediaEntry3 in sortedList)
						{
							d = new object[] { "<LI><A href=\"entry:", encyclopediaEntry3.ID, "\">", encyclopediaEntry3.Name, "</A></LI>" };
							strs.Add(string.Concat(d));
						}
						strs.Add("</UL>");
					}
				}
				if (pp.Element != null)
				{
					Encounter element1 = pp.Element as Encounter;
					if (element1 != null)
					{
						strs.AddRange(HTML.get_encounter(element1));
					}
					TrapElement trapElement = pp.Element as TrapElement;
					if (trapElement != null)
					{
						strs.AddRange(HTML.get_trap(trapElement.Trap, null, false, false));
					}
					SkillChallenge skillChallenge = pp.Element as SkillChallenge;
					if (skillChallenge != null)
					{
						strs.AddRange(HTML.get_skill_challenge(skillChallenge, links));
					}
					Quest quest = pp.Element as Quest;
					if (quest != null)
					{
						strs.AddRange(HTML.get_quest(quest));
					}
				}
				if (pp.Parcels.Count != 0)
				{
					strs.AddRange(HTML.get_parcels(pp, links));
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string Process(string raw_text, bool strip_html)
		{
			List<Pair<string, string>> pairs = new List<Pair<string, string>>()
			{
				new Pair<string, string>("&", "&amp;"),
				new Pair<string, string>("Á", "&Aacute;"),
				new Pair<string, string>("á", "&aacute;"),
				new Pair<string, string>("À", "&Agrave;"),
				new Pair<string, string>("Â", "&Acirc;"),
				new Pair<string, string>("à", "&agrave;"),
				new Pair<string, string>("Â", "&Acirc;"),
				new Pair<string, string>("â", "&acirc;"),
				new Pair<string, string>("Ä", "&Auml;"),
				new Pair<string, string>("ä", "&auml;"),
				new Pair<string, string>("Ã", "&Atilde;"),
				new Pair<string, string>("ã", "&atilde;"),
				new Pair<string, string>("Å", "&Aring;"),
				new Pair<string, string>("å", "&aring;"),
				new Pair<string, string>("Æ", "&Aelig;"),
				new Pair<string, string>("æ", "&aelig;"),
				new Pair<string, string>("Ç", "&Ccedil;"),
				new Pair<string, string>("ç", "&ccedil;"),
				new Pair<string, string>("Ð", "&Eth;"),
				new Pair<string, string>("ð", "&eth;"),
				new Pair<string, string>("É", "&Eacute;"),
				new Pair<string, string>("é", "&eacute;"),
				new Pair<string, string>("È", "&Egrave;"),
				new Pair<string, string>("è", "&egrave;"),
				new Pair<string, string>("Ê", "&Ecirc;"),
				new Pair<string, string>("ê", "&ecirc;"),
				new Pair<string, string>("Ë", "&Euml;"),
				new Pair<string, string>("ë", "&euml;"),
				new Pair<string, string>("Í", "&Iacute;"),
				new Pair<string, string>("í", "&iacute;"),
				new Pair<string, string>("Ì", "&Igrave;"),
				new Pair<string, string>("ì", "&igrave;"),
				new Pair<string, string>("Î", "&Icirc;"),
				new Pair<string, string>("î", "&icirc;"),
				new Pair<string, string>("Ï", "&Iuml;"),
				new Pair<string, string>("ï", "&iuml;"),
				new Pair<string, string>("Ñ", "&Ntilde;"),
				new Pair<string, string>("ñ", "&ntilde;"),
				new Pair<string, string>("Ó", "&Oacute;"),
				new Pair<string, string>("ó", "&oacute;"),
				new Pair<string, string>("Ò", "&Ograve;"),
				new Pair<string, string>("ò", "&ograve;"),
				new Pair<string, string>("Ô", "&Ocirc;"),
				new Pair<string, string>("ô", "&ocirc;"),
				new Pair<string, string>("Ö", "&Ouml;"),
				new Pair<string, string>("ö", "&ouml;"),
				new Pair<string, string>("Õ", "&Otilde;"),
				new Pair<string, string>("õ", "&otilde;"),
				new Pair<string, string>("Ø", "&Oslash;"),
				new Pair<string, string>("ø", "&oslash;"),
				new Pair<string, string>("ß", "&szlig;"),
				new Pair<string, string>("Þ", "&Thorn;"),
				new Pair<string, string>("þ", "&thorn;"),
				new Pair<string, string>("Ú", "&Uacute;"),
				new Pair<string, string>("ú", "&uacute;"),
				new Pair<string, string>("Ù", "&Ugrave;"),
				new Pair<string, string>("ù", "&ugrave;"),
				new Pair<string, string>("Û", "&Ucirc;"),
				new Pair<string, string>("û", "&ucirc;"),
				new Pair<string, string>("Ü", "&Uuml;"),
				new Pair<string, string>("ü", "&uuml;"),
				new Pair<string, string>("Ý", "&Yacute;"),
				new Pair<string, string>("ý", "&yacute;"),
				new Pair<string, string>("ÿ", "&yuml;"),
				new Pair<string, string>("©", "&copy;"),
				new Pair<string, string>("®", "&reg;"),
				new Pair<string, string>("™", "&trade;"),
				new Pair<string, string>("€", "&euro;"),
				new Pair<string, string>("¢", "&cent;"),
				new Pair<string, string>("£", "&pound;"),
				new Pair<string, string>("‘", "&lsquo;"),
				new Pair<string, string>("’", "&rsquo;"),
				new Pair<string, string>("“", "&ldquo;"),
				new Pair<string, string>("”", "&rdquo;"),
				new Pair<string, string>("«", "&laquo;"),
				new Pair<string, string>("»", "&raquo;"),
				new Pair<string, string>("—", "&mdash;"),
				new Pair<string, string>("–", "&ndash;"),
				new Pair<string, string>("°", "&deg;"),
				new Pair<string, string>("±", "&plusmn;"),
				new Pair<string, string>("¼", "&frac14;"),
				new Pair<string, string>("½", "&frac12;"),
				new Pair<string, string>("¾", "&frac34;"),
				new Pair<string, string>("×", "&times;"),
				new Pair<string, string>("÷", "&divide;"),
				new Pair<string, string>("α", "&alpha;"),
				new Pair<string, string>("β", "&beta;"),
				new Pair<string, string>("∞", "&infin;")
			};
			if (strip_html)
			{
				pairs.Add(new Pair<string, string>("\"", "&quot;"));
				pairs.Add(new Pair<string, string>("<", "&lt;"));
				pairs.Add(new Pair<string, string>(">", "&gt;"));
			}
			string rawText = raw_text;
			foreach (Pair<string, string> pair in pairs)
			{
				rawText = rawText.Replace(pair.First, pair.Second);
			}
			return rawText;
		}

		private static string process_encyclopedia_info(string details, Encyclopedia encyclopedia, bool include_entry_links, bool include_dm_info)
		{
			while (true)
			{
				string str = "[[DM]]";
				int num = details.IndexOf(str);
				if (num == -1)
				{
					break;
				}
				int num1 = details.IndexOf(str, num + str.Length);
				if (num1 == -1)
				{
					break;
				}
				int length = num + str.Length;
				string str1 = details.Substring(length, num1 - length);
				if (!include_dm_info)
				{
					details = string.Concat(details.Substring(0, num), details.Substring(num1 + str.Length));
				}
				else
				{
					string[] strArrays = new string[] { details.Substring(0, num), "<B>", str1, "</B>", details.Substring(num1 + str.Length) };
					details = string.Concat(strArrays);
				}
			}
			while (true)
			{
				string str2 = "[[";
				string str3 = "]]";
				int num2 = details.IndexOf(str2);
				if (num2 == -1)
				{
					break;
				}
				int num3 = details.IndexOf(str3, num2 + str2.Length);
				if (num3 == -1)
				{
					break;
				}
				int length1 = num2 + str2.Length;
				string str4 = details.Substring(length1, num3 - length1);
				string str5 = str4;
				string str6 = str4;
				if (str4.Contains("|"))
				{
					int num4 = str4.IndexOf("|");
					str5 = str4.Substring(0, num4);
					str6 = str4.Substring(num4 + 1);
					str6 = str6.Trim();
				}
				string str7 = "";
				if (!include_entry_links)
				{
					str7 = str6;
				}
				else
				{
					EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(str5);
					if (encyclopediaEntry != null)
					{
						object[] d = new object[] { "<A href=\"entry:", encyclopediaEntry.ID, "\" title=\"", encyclopediaEntry.Name, "\">", str6, "</A>" };
						str7 = string.Concat(d);
					}
					else
					{
						string[] strArrays1 = new string[] { "<A class=\"missing\" href=\"missing:", str5, "\" title=\"Create entry '", str5, "'\">", str6, "</A>" };
						str7 = string.Concat(strArrays1);
					}
				}
				details = string.Concat(details.Substring(0, num2), str7, details.Substring(num3 + str3.Length));
			}
			details = HTML.fMarkdown.Transform(details);
			details = details.Replace("<p>", "<p class=encyclopedia_entry>");
			return details;
		}

		public static string SkillChallenge(SkillChallenge challenge, bool include_links, bool include_wrapper, DisplaySize size)
		{
			List<string> strs = new List<string>();
			if (include_wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetStyle(DisplaySize.Small));
				strs.Add("<BODY>");
			}
			if (challenge == null)
			{
				strs.Add("<P class=instruction>(no skill challenge selected)</P>");
			}
			else
			{
				strs.AddRange(HTML.get_skill_challenge(challenge, include_links));
			}
			if (include_wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string StatBlock(EncounterCard card, CombatData data, Encounter enc, bool include_wrapper, bool initiative_holder, bool full, CardMode mode, DisplaySize size)
		{
			List<string> strs = new List<string>();
			if (include_wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetStyle(DisplaySize.Small));
				strs.Add("<BODY>");
			}
			if (full)
			{
				if (data != null && data.Location == CombatData.NoPoint && enc != null && enc.MapID != Guid.Empty)
				{
					strs.Add("<P class=instruction>Drag this creature from the list onto the map.</P>");
				}
				if (data != null)
				{
					strs.AddRange(HTML.get_combat_data(data, card.HP, enc, initiative_holder));
				}
			}
			if (card == null)
			{
				strs.Add("<P class=instruction>(no creature selected)</P>");
			}
			else
			{
				strs.Add("<P class=table>");
				strs.AddRange(card.AsText(data, mode, full));
				strs.Add("</P>");
			}
			if (include_wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string StatBlock(Hero hero, Encounter enc, bool include_wrapper, bool initiative_holder, bool show_effects, DisplaySize size)
		{
			List<string> strs = new List<string>();
			if (include_wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetStyle(DisplaySize.Small));
				strs.Add("<BODY>");
			}
			if (enc != null)
			{
				if (enc.MapID == Guid.Empty && hero.CombatData.Initiative == -2147483648)
				{
					strs.Add("<P class=instruction>Double-click this character on the list to set its initiative score.</P>");
				}
				else if (enc.MapID != Guid.Empty && hero.CombatData.Location == CombatData.NoPoint)
				{
					strs.Add("<P class=instruction>Drag this character from the list onto the map.</P>");
				}
			}
			strs.AddRange(HTML.get_hero(hero, enc, initiative_holder, show_effects));
			if (include_wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		public static string TerrainPower(TerrainPower tp, DisplaySize size)
		{
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY>");
			strs.AddRange(HTML.get_terrain_power(tp));
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string Text(string str, bool strip_html, bool centred, DisplaySize size)
		{
			List<string> strs = new List<string>()
			{
				"<HTML>"
			};
			strs.AddRange(HTML.GetHead(null, null, size));
			strs.Add("<BODY style=\"background-color=black\">");
			string str1 = HTML.Process(str, strip_html);
			if (str1 != "")
			{
				if (!centred)
				{
					strs.Add(string.Concat("<P style=\"color=white\">", str1, "</P>"));
				}
				else
				{
					strs.Add(string.Concat("<P class=instruction style=\"color=white\">", str1, "</P>"));
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			return HTML.Concatenate(strs);
		}

		public static string Trap(Trap trap, CombatData cd, bool include_wrapper, bool initiative_holder, bool builder, DisplaySize size)
		{
			List<string> strs = new List<string>();
			if (include_wrapper)
			{
				strs.Add("<HTML>");
				strs.AddRange(HTML.GetStyle(DisplaySize.Small));
				strs.Add("<BODY>");
			}
			if (trap == null)
			{
				strs.Add("<P class=instruction>(no trap / hazard selected)</P>");
			}
			else
			{
				strs.AddRange(HTML.get_trap(trap, cd, initiative_holder, builder));
			}
			if (include_wrapper)
			{
				strs.Add("</BODY>");
				strs.Add("</HTML>");
			}
			return HTML.Concatenate(strs);
		}

		private static string wrap(string content, string tag)
		{
			string str = string.Concat("<", tag.ToUpper(), ">");
			string str1 = string.Concat("</", tag.ToUpper(), ">");
			return string.Concat(str, content, str1);
		}
	}
}