using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SkillChallengeBreakdownForm : Form
	{
		private KeyAbilitiesPanel AbilitiesPanel;

		public SkillChallengeBreakdownForm(SkillChallenge sc)
		{
			this.InitializeComponent();
			this.AbilitiesPanel.Analyse(sc);
		}

		private void InitializeComponent()
		{
			this.AbilitiesPanel = new KeyAbilitiesPanel();
			base.SuspendLayout();
			this.AbilitiesPanel.Dock = DockStyle.Fill;
			this.AbilitiesPanel.Location = new Point(0, 0);
			this.AbilitiesPanel.Name = "AbilitiesPanel";
			this.AbilitiesPanel.Size = new System.Drawing.Size(752, 290);
			this.AbilitiesPanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(752, 290);
			base.Controls.Add(this.AbilitiesPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeBreakdownForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill Challenge Breakdown";
			base.ResumeLayout(false);
		}
	}
}