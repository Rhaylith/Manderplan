using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class PowerStatisticsForm : Form
	{
		private List<CreaturePower> fPowers;

		private int fCreatures;

		private WebBrowser Browser;

		public PowerStatisticsForm(List<CreaturePower> powers, int creatures)
		{
			this.InitializeComponent();
			this.fPowers = powers;
			this.fCreatures = creatures;
			this.update_table();
		}

		private int count_powers(string text)
		{
			if (text == "Immobilised")
			{
				text = "Immobilized";
			}
			int num = 0;
			foreach (CreaturePower fPower in this.fPowers)
			{
				if (!fPower.Details.ToLower().Contains(text.ToLower()))
				{
					continue;
				}
				num++;
			}
			return num;
		}

		private Dictionary<string, double> get_category_breakdown()
		{
			Dictionary<string, double> strs = new Dictionary<string, double>();
			foreach (CreaturePowerCategory value in Enum.GetValues(typeof(CreaturePowerCategory)))
			{
				int num = 0;
				foreach (CreaturePower fPower in this.fPowers)
				{
					if (fPower.Category != value)
					{
						continue;
					}
					num++;
				}
				strs[value.ToString()] = (double)num / (double)this.fPowers.Count;
			}
			return strs;
		}

		private Dictionary<string, int> get_condition_breakdown()
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			foreach (string condition in Conditions.GetConditions())
			{
				int num = this.count_powers(condition);
				if (num == 0)
				{
					continue;
				}
				strs[condition] = num;
			}
			return strs;
		}

		private Dictionary<string, int> get_damage_expression_breakdown()
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			foreach (CreaturePower fPower in this.fPowers)
			{
				DiceExpression diceExpression = DiceExpression.Parse(fPower.Damage);
				if (diceExpression == null || diceExpression.Maximum == 0)
				{
					continue;
				}
				string str = diceExpression.ToString();
				if (!strs.ContainsKey(str))
				{
					strs[str] = 0;
				}
				Dictionary<string, int> item = strs;
				Dictionary<string, int> strs1 = item;
				string str1 = str;
				item[str1] = strs1[str1] + 1;
			}
			return strs;
		}

		private Dictionary<string, int> get_damage_type_breakdown()
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			foreach (DamageType value in Enum.GetValues(typeof(DamageType)))
			{
				if (value == DamageType.Untyped)
				{
					continue;
				}
				string str = value.ToString();
				int num = this.count_powers(str);
				if (num == 0)
				{
					continue;
				}
				strs[str] = num;
			}
			return strs;
		}

		private Dictionary<string, int> get_keyword_breakdown()
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			foreach (CreaturePower fPower in this.fPowers)
			{
				string keywords = fPower.Keywords;
				string[] strArrays = new string[] { ",", ";" };
				string[] strArrays1 = keywords.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str = strArrays1[i].Trim();
					if (!strs.ContainsKey(str))
					{
						strs[str] = 0;
					}
					Dictionary<string, int> item = strs;
					Dictionary<string, int> strs1 = item;
					string str1 = str;
					item[str1] = strs1[str1] + 1;
				}
			}
			return strs;
		}

		private void InitializeComponent()
		{
			this.Browser = new WebBrowser();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(364, 362);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(364, 362);
			base.Controls.Add(this.Browser);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerStatisticsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Statistics";
			base.ResumeLayout(false);
		}

		private List<Pair<string, int>> sort_breakdown(Dictionary<string, int> breakdown)
		{
			List<Pair<string, int>> pairs = new List<Pair<string, int>>();
			foreach (string key in breakdown.Keys)
			{
				pairs.Add(new Pair<string, int>(key, breakdown[key]));
			}
			pairs.Sort((Pair<string, int> x, Pair<string, int> y) => x.Second.CompareTo(y.Second) * -1);
			return pairs;
		}

		private void update_table()
		{
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead("Power Statistics", "", DisplaySize.Small));
			strs.Add("<BODY>");
			strs.Add("<P class=table>");
			strs.Add("<TABLE>");
			strs.Add("<TR class=heading>");
			strs.Add("<TD colspan=3>");
			strs.Add("<B>Number of Powers</B>");
			strs.Add("</TD>");
			strs.Add("</TR>");
			strs.Add("<TR>");
			strs.Add("<TD colspan=2>");
			strs.Add("Number of powers");
			strs.Add("</TD>");
			strs.Add("<TD align=right>");
			strs.Add(this.fPowers.Count.ToString());
			strs.Add("</TD>");
			strs.Add("</TR>");
			if (this.fCreatures != 0)
			{
				double count = (double)this.fPowers.Count / (double)this.fCreatures;
				strs.Add("<TR>");
				strs.Add("<TD colspan=2>");
				strs.Add("Powers per creature");
				strs.Add("</TD>");
				strs.Add("<TD align=right>");
				strs.Add(count.ToString("F1"));
				strs.Add("</TD>");
				strs.Add("</TR>");
			}
			strs.Add("</TABLE>");
			strs.Add("</P>");
			if (this.fPowers.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				Dictionary<string, int> conditionBreakdown = this.get_condition_breakdown();
				if (conditionBreakdown.Count != 0)
				{
					strs.Add("<TR class=heading>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Conditions</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (Pair<string, int> pair in this.sort_breakdown(conditionBreakdown))
					{
						int second = pair.Second;
						if (second == 0)
						{
							continue;
						}
						double num = (double)second / (double)this.fPowers.Count;
						strs.Add("<TR>");
						strs.Add("<TD colspan=2>");
						strs.Add(pair.First);
						strs.Add("</TD>");
						strs.Add("<TD align=right>");
						object[] str = new object[] { second, " (", num.ToString("P0"), ")" };
						strs.Add(string.Concat(str));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				Dictionary<string, int> damageTypeBreakdown = this.get_damage_type_breakdown();
				if (damageTypeBreakdown.Count != 0)
				{
					strs.Add("<TR class=heading>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Damage Types</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (Pair<string, int> pair1 in this.sort_breakdown(damageTypeBreakdown))
					{
						int second1 = pair1.Second;
						double count1 = (double)second1 / (double)this.fPowers.Count;
						strs.Add("<TR>");
						strs.Add("<TD colspan=2>");
						strs.Add(pair1.First);
						strs.Add("</TD>");
						strs.Add("<TD align=right>");
						object[] objArray = new object[] { second1, " (", count1.ToString("P0"), ")" };
						strs.Add(string.Concat(objArray));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				Dictionary<string, int> keywordBreakdown = this.get_keyword_breakdown();
				if (keywordBreakdown.Count != 0)
				{
					strs.Add("<TR class=heading>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Keywords</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (Pair<string, int> pair2 in this.sort_breakdown(keywordBreakdown))
					{
						int num1 = pair2.Second;
						double count2 = (double)num1 / (double)this.fPowers.Count;
						strs.Add("<TR>");
						strs.Add("<TD colspan=2>");
						strs.Add(pair2.First);
						strs.Add("</TD>");
						strs.Add("<TD align=right>");
						object[] str1 = new object[] { num1, " (", count2.ToString("P0"), ")" };
						strs.Add(string.Concat(str1));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				Dictionary<string, double> categoryBreakdown = this.get_category_breakdown();
				if (categoryBreakdown.Count != 0)
				{
					strs.Add("<P class=table>");
					strs.Add("<TABLE>");
					strs.Add("<TR class=heading>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Powers Per Category</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (string key in categoryBreakdown.Keys)
					{
						double item = categoryBreakdown[key];
						strs.Add("<TR>");
						strs.Add("<TD colspan=2>");
						strs.Add(key);
						strs.Add("</TD>");
						strs.Add("<TD align=right>");
						strs.Add(item.ToString("P0"));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
					strs.Add("</TABLE>");
					strs.Add("</P>");
				}
			}
			if (this.fPowers.Count != 0)
			{
				strs.Add("<P class=table>");
				strs.Add("<TABLE>");
				Dictionary<string, int> damageExpressionBreakdown = this.get_damage_expression_breakdown();
				if (damageExpressionBreakdown.Count != 0)
				{
					strs.Add("<TR class=heading>");
					strs.Add("<TD colspan=3>");
					strs.Add("<B>Damage</B>");
					strs.Add("</TD>");
					strs.Add("</TR>");
					foreach (Pair<string, int> pair3 in this.sort_breakdown(damageExpressionBreakdown))
					{
						int second2 = pair3.Second;
						double num2 = (double)second2 / (double)this.fPowers.Count;
						DiceExpression diceExpression = DiceExpression.Parse(pair3.First);
						strs.Add("<TR>");
						strs.Add("<TD colspan=2>");
						object[] first = new object[] { pair3.First, " (avg ", diceExpression.Average, ", max ", diceExpression.Maximum, ")" };
						strs.Add(string.Concat(first));
						strs.Add("</TD>");
						strs.Add("<TD align=right>");
						object[] objArray1 = new object[] { second2, " (", num2.ToString("P0"), ")" };
						strs.Add(string.Concat(objArray1));
						strs.Add("</TD>");
						strs.Add("</TR>");
					}
				}
				strs.Add("</TABLE>");
				strs.Add("</P>");
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.Browser.DocumentText = HTML.Concatenate(strs);
		}
	}
}