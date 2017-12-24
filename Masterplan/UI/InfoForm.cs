using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class InfoForm : Form
	{
		private IContainer components;

		private Masterplan.Controls.InfoPanel InfoPanel;

		public int Level
		{
			get
			{
				return this.InfoPanel.Level;
			}
			set
			{
				this.InfoPanel.Level = value;
			}
		}

		public InfoForm()
		{
			this.InitializeComponent();
			this.InfoPanel.Level = (Session.Project != null ? Session.Project.Party.Level : 1);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.InfoPanel = new Masterplan.Controls.InfoPanel();
			base.SuspendLayout();
			this.InfoPanel.Dock = DockStyle.Fill;
			this.InfoPanel.Level = 1;
			this.InfoPanel.Location = new Point(0, 0);
			this.InfoPanel.Name = "InfoPanel";
			this.InfoPanel.Size = new System.Drawing.Size(246, 433);
			this.InfoPanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(246, 433);
			base.Controls.Add(this.InfoPanel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InfoForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Information";
			base.ResumeLayout(false);
		}
	}
}