using Masterplan.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OddsForm : Form
	{
		private DiceGraphPanel DiceGraph;

		public OddsForm()
		{
			this.InitializeComponent();
			this.DiceGraph.Dice = new List<int>(new int[] { 4, 6, 8, 10 });
		}

		public OddsForm(List<int> dice, int constant, string title)
		{
			this.InitializeComponent();
			this.DiceGraph.Dice = dice;
			this.DiceGraph.Constant = constant;
			this.DiceGraph.Title = title;
		}

		private void InitializeComponent()
		{
			this.DiceGraph = new DiceGraphPanel();
			base.SuspendLayout();
			this.DiceGraph.Constant = 0;
			this.DiceGraph.Dice = null;
			this.DiceGraph.Dock = DockStyle.Fill;
			this.DiceGraph.Location = new Point(0, 0);
			this.DiceGraph.Name = "DiceGraph";
			this.DiceGraph.Size = new System.Drawing.Size(449, 262);
			this.DiceGraph.TabIndex = 0;
			this.DiceGraph.Title = "";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(449, 262);
			base.Controls.Add(this.DiceGraph);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OddsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Odds";
			base.ResumeLayout(false);
		}
	}
}