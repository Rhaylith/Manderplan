using Masterplan.Controls;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DieRollerForm : Form
	{
		public Masterplan.Controls.DicePanel DicePanel;

		public DiceExpression Expression
		{
			get
			{
				return this.DicePanel.Expression;
			}
			set
			{
				this.DicePanel.Expression = value;
			}
		}

		public DieRollerForm()
		{
            this.InitializeComponent();

            // TODO:  Add this back to InitializeComponent()
            //this.DicePanel = new Masterplan.Controls.DicePanel();
            //this.DicePanel.Dock = DockStyle.Fill;
            //this.DicePanel.Expression = null;
            //this.DicePanel.Location = new Point(0, 0);
            //this.DicePanel.Name = "DicePanel";
            //this.DicePanel.Size = new System.Drawing.Size(247, 372);
            //this.DicePanel.TabIndex = 0;
            //base.Controls.Add(this.DicePanel);

            //this.DicePanel.UpdateView();
		}

		private void InitializeComponent()
		{

            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(247, 372);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DieRollerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Die Roller";
			base.ResumeLayout(false);
		}
	}
}