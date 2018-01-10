using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	public class DemographicsPanel : UserControl
	{
		private IContainer components;

		private Masterplan.Data.Library fLibrary;

		private DemographicsSource fSource;

		private DemographicsMode fMode;

		private StringFormat fCentred = new StringFormat();

		private Dictionary<string, int> fBreakdown;

		[Category("Data")]
		[Description("The library to display.")]
		public Masterplan.Data.Library Library
		{
			get
			{
				return this.fLibrary;
			}
			set
			{
				this.fLibrary = value;
				this.fBreakdown = null;
				base.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("The type of breakdown to show.")]
		public DemographicsMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				this.fBreakdown = null;
				base.Invalidate();
			}
		}

		[Category("Appearance")]
		[Description("The category of information to show.")]
		public DemographicsSource Source
		{
			get
			{
				return this.fSource;
			}
			set
			{
				this.fSource = value;
				this.fBreakdown = null;
				base.Invalidate();
			}
		}

		public DemographicsPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		private void @add(string label)
		{
			if (this.fBreakdown.ContainsKey(label))
			{
				Dictionary<string, int> item = this.fBreakdown;
				Dictionary<string, int> strs = item;
				string str = label;
				item[str] = strs[str] + 1;
			}
		}

		private void add_library(Masterplan.Data.Library library)
		{
			switch (this.fSource)
			{
				case DemographicsSource.Creatures:
				{
					List<Creature>.Enumerator enumerator = library.Creatures.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Creature current = enumerator.Current;
							switch (this.fMode)
							{
								case DemographicsMode.Level:
								{
									this.@add(current.Level.ToString());
									continue;
								}
								case DemographicsMode.Role:
								case DemographicsMode.Status:
								{
									this.analyse_role(current.Role);
									continue;
								}
								default:
								{
									continue;
								}
							}
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				case DemographicsSource.Traps:
				{
					List<Trap>.Enumerator enumerator1 = library.Traps.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							Trap trap = enumerator1.Current;
							switch (this.fMode)
							{
								case DemographicsMode.Level:
								{
									this.@add(trap.Level.ToString());
									continue;
								}
								case DemographicsMode.Role:
								case DemographicsMode.Status:
								{
									this.analyse_role(trap.Role);
									continue;
								}
								default:
								{
									continue;
								}
							}
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator1).Dispose();
					}
				}
				case DemographicsSource.MagicItems:
				{
					List<MagicItem>.Enumerator enumerator2 = library.MagicItems.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							MagicItem magicItem = enumerator2.Current;
							if (this.fMode != DemographicsMode.Level)
							{
								continue;
							}
							this.@add(magicItem.Level.ToString());
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				default:
				{
					return;
				}
			}
		}

		private void analyse_data()
		{
			try
			{
				List<Masterplan.Data.Library> libraries = new List<Masterplan.Data.Library>();
				if (this.fLibrary != null)
				{
					libraries.Add(this.fLibrary);
				}
				else
				{
					libraries.AddRange(Session.Libraries);
				}
				this.fBreakdown = new Dictionary<string, int>();
				this.set_labels(libraries);
				foreach (Masterplan.Data.Library library in libraries)
				{
					this.add_library(library);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void analyse_role(IRole role)
		{
			ComplexRole complexRole = role as ComplexRole;
			if (complexRole != null)
			{
				switch (this.fMode)
				{
					case DemographicsMode.Role:
					{
						this.@add(complexRole.Type.ToString());
						break;
					}
					case DemographicsMode.Status:
					{
						this.@add(complexRole.Flag.ToString());
						if (!complexRole.Leader)
						{
							break;
						}
						this.@add("Leader");
						break;
					}
				}
			}
			Minion minion = role as Minion;
			if (minion != null)
			{
				switch (this.fMode)
				{
					case DemographicsMode.Role:
					{
						if (!minion.HasRole)
						{
							break;
						}
						this.@add(minion.Type.ToString());
						return;
					}
					case DemographicsMode.Status:
					{
						this.@add("Minion");
						break;
					}
					default:
					{
						return;
					}
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private int find_max_level(DemographicsSource source, List<Masterplan.Data.Library> libraries)
		{
			int level = 0;
			foreach (Masterplan.Data.Library library in libraries)
			{
				switch (source)
				{
					case DemographicsSource.Creatures:
					{
						List<Creature>.Enumerator enumerator = library.Creatures.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								Creature current = enumerator.Current;
								if (current.Level <= level)
								{
									continue;
								}
								level = current.Level;
							}
							continue;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					case DemographicsSource.Traps:
					{
						List<Trap>.Enumerator enumerator1 = library.Traps.GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								Trap trap = enumerator1.Current;
								if (trap.Level <= level)
								{
									continue;
								}
								level = trap.Level;
							}
							continue;
						}
						finally
						{
							((IDisposable)enumerator1).Dispose();
						}
					}
					case DemographicsSource.MagicItems:
					{
						List<MagicItem>.Enumerator enumerator2 = library.MagicItems.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								MagicItem magicItem = enumerator2.Current;
								if (magicItem.Level <= level)
								{
									continue;
								}
								level = magicItem.Level;
							}
							continue;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
					}
					default:
					{
						continue;
					}
				}
			}
			return level;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.fBreakdown == null)
			{
				this.analyse_data();
			}
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			int count = this.fBreakdown.Keys.Count;
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num1 = 0;
			foreach (string key in this.fBreakdown.Keys)
			{
				int item = this.fBreakdown[key];
				num1 = Math.Max(num1, item);
				num = Math.Min(num, item);
			}
			int num2 = num1 - num;
			if (num2 == 0)
			{
				return;
			}
			int num3 = 20;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = base.ClientRectangle;
			Rectangle rectangle1 = new Rectangle(num3, num3, clientRectangle.Width - 2 * num3, rectangle.Height - 3 * num3);
			float width = (float)rectangle1.Width / (float)count;
			List<string> strs = new List<string>();
			strs.AddRange(this.fBreakdown.Keys);
			using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size * 0.8f))
			{
				for (int i = 0; i != strs.Count; i++)
				{
					string str = strs[i];
					float single = width * (float)i;
					RectangleF rectangleF = new RectangleF((float)rectangle1.Left + single, (float)rectangle1.Bottom, width, (float)num3);
					e.Graphics.DrawString(str, font, Brushes.Black, rectangleF, this.fCentred);
					int item1 = this.fBreakdown[str];
					if (item1 != 0)
					{
						Color color = (item1 >= 0 ? Color.LightGray : Color.White);
						Color color1 = (item1 >= 0 ? Color.White : Color.LightGray);
						int num4 = Math.Max(item1, 0);
						int num5 = Math.Min(item1, 0);
						int bottom = rectangle1.Bottom - (rectangle1.Height - num3) * (num4 - num) / num2;
						int height = (rectangle1.Height - num3) * (num4 - num5) / num2;
						RectangleF rectangleF1 = new RectangleF((float)rectangle1.Left + single, (float)bottom, width, (float)height);
						using (Brush linearGradientBrush = new LinearGradientBrush(rectangleF1, color, color1, LinearGradientMode.Vertical))
						{
							e.Graphics.FillRectangle(linearGradientBrush, rectangleF1);
							e.Graphics.DrawRectangle(Pens.Gray, rectangleF1.X, rectangleF1.Y, rectangleF1.Width, rectangleF1.Height);
						}
						RectangleF rectangleF2 = new RectangleF((float)rectangle1.Left + single, (float)rectangle1.Top, width, (float)num3);
						e.Graphics.DrawString(item1.ToString(), font, Brushes.Gray, rectangleF2, this.fCentred);
					}
				}
			}
			int bottom1 = rectangle1.Bottom - (rectangle1.Height - num3) * -num / num2;
			e.Graphics.DrawLine(Pens.Black, rectangle1.Left, bottom1, rectangle1.Right, bottom1);
			e.Graphics.DrawLine(Pens.Black, rectangle1.Left, rectangle1.Bottom, rectangle1.Left, rectangle1.Top);
		}

		private void set_labels(List<Masterplan.Data.Library> libraries)
		{
			switch (this.fMode)
			{
				case DemographicsMode.Level:
				{
					int num = this.find_max_level(this.fSource, libraries);
					for (int i = 1; i <= num; i++)
					{
						this.fBreakdown[i.ToString()] = 0;
					}
					return;
				}
				case DemographicsMode.Role:
				{
					switch (this.fSource)
					{
						case DemographicsSource.Creatures:
						{
							this.fBreakdown["Artillery"] = 0;
							this.fBreakdown["Brute"] = 0;
							this.fBreakdown["Controller"] = 0;
							this.fBreakdown["Lurker"] = 0;
							this.fBreakdown["Skirmisher"] = 0;
							this.fBreakdown["Soldier"] = 0;
							return;
						}
						case DemographicsSource.Traps:
						{
							this.fBreakdown["Blaster"] = 0;
							this.fBreakdown["Lurker"] = 0;
							this.fBreakdown["Obstacle"] = 0;
							this.fBreakdown["Warder"] = 0;
							return;
						}
						default:
						{
							return;
						}
					}
				}
				case DemographicsMode.Status:
				{
					this.fBreakdown["Standard"] = 0;
					this.fBreakdown["Elite"] = 0;
					this.fBreakdown["Solo"] = 0;
					this.fBreakdown["Minion"] = 0;
					this.fBreakdown["Leader"] = 0;
					return;
				}
				default:
				{
					return;
				}
			}
		}

		internal void ShowTable(ReportTable table)
		{
			this.fBreakdown = new Dictionary<string, int>();
			foreach (ReportRow row in table.Rows)
			{
				this.fBreakdown[row.Heading] = row.Total;
			}
			base.Invalidate();
		}
	}
}