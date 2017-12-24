using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class KeyAbilitiesPanel : UserControl
	{
		private IContainer components;

		private StringFormat fCentred = new StringFormat();

		private Dictionary<string, int> fBreakdown;

		public KeyAbilitiesPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		public void Analyse(SkillChallenge sc)
		{
			this.fBreakdown = new Dictionary<string, int>();
			this.fBreakdown["Strength"] = 0;
			this.fBreakdown["Constitution"] = 0;
			this.fBreakdown["Dexterity"] = 0;
			this.fBreakdown["Intelligence"] = 0;
			this.fBreakdown["Wisdom"] = 0;
			this.fBreakdown["Charisma"] = 0;
			foreach (SkillChallengeData skill in sc.Skills)
			{
				if (skill.Type == SkillType.AutoFail)
				{
					continue;
				}
				string str = "";
				str = (!Skills.GetAbilityNames().Contains(skill.SkillName) ? Skills.GetKeyAbility(skill.SkillName) : skill.SkillName);
				if (!this.fBreakdown.ContainsKey(str))
				{
					continue;
				}
				Dictionary<string, int> item = this.fBreakdown;
				Dictionary<string, int> strs = item;
				string str1 = str;
				item[str1] = strs[str1] + 1;
			}
			base.Invalidate();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private int get_count(int column_index)
		{
			string _label = this.get_label(column_index);
			return this.fBreakdown[_label];
		}

		private string get_label(int column_index)
		{
			switch (column_index)
			{
				case 0:
				{
					return "Strength";
				}
				case 1:
				{
					return "Constitution";
				}
				case 2:
				{
					return "Dexterity";
				}
				case 3:
				{
					return "Intelligence";
				}
				case 4:
				{
					return "Wisdom";
				}
				case 5:
				{
					return "Charisma";
				}
			}
			return "";
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			if (this.fBreakdown == null)
			{
				return;
			}
			int num = 0;
			foreach (string key in this.fBreakdown.Keys)
			{
				int item = this.fBreakdown[key];
				num = Math.Max(item, num);
			}
			int num1 = 20;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = base.ClientRectangle;
			Rectangle rectangle1 = new Rectangle(num1, num1, clientRectangle.Width - 2 * num1, rectangle.Height - 3 * num1);
			float width = (float)rectangle1.Width / 6f;
			for (int i = 0; i != 6; i++)
			{
				string _label = this.get_label(i);
				if (_label != "")
				{
					float single = width * (float)i;
					RectangleF rectangleF = new RectangleF((float)rectangle1.Left + single, (float)rectangle1.Bottom, width, (float)num1);
					e.Graphics.DrawString(_label, this.Font, Brushes.Black, rectangleF, this.fCentred);
					int _count = this.get_count(i);
					if (_count != 0)
					{
						int height = (rectangle1.Height - num1) * _count / num;
						RectangleF rectangleF1 = new RectangleF((float)rectangle1.Left + single, (float)(rectangle1.Bottom - height), width, (float)height);
						using (Brush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, Color.LightGray, Color.White, LinearGradientMode.Vertical))
						{
							e.Graphics.FillRectangle(linearGradientBrush, rectangleF1);
						}
						e.Graphics.DrawRectangle(Pens.Gray, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
						RectangleF rectangleF2 = new RectangleF((float)rectangle1.Left + single, (float)rectangle1.Top, width, (float)num1);
						e.Graphics.DrawString(_count.ToString(), this.Font, Brushes.Gray, rectangleF2, this.fCentred);
					}
				}
			}
			e.Graphics.DrawLine(Pens.Black, rectangle1.Left, rectangle1.Bottom, rectangle1.Left, rectangle1.Top);
			e.Graphics.DrawLine(Pens.Black, rectangle1.Left, rectangle1.Bottom, rectangle1.Right, rectangle1.Bottom);
		}
	}
}