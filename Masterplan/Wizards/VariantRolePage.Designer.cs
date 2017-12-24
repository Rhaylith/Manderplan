using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantRolePage : UserControl, IWizardPage
	{
		private IContainer components;

		private Label InfoLbl;

		private ComboBox RoleBox;

		private VariantData fData;

		public bool AllowBack
		{
			get
			{
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return false;
			}
		}

		public bool AllowNext
		{
			get
			{
				return true;
			}
		}

		public VariantRolePage()
		{
			this.InitializeComponent();
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
			this.InfoLbl = new Label();
			this.RoleBox = new ComboBox();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(268, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select the role that your new creature will fill.";
			this.RoleBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.RoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(3, 43);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new System.Drawing.Size(262, 21);
			this.RoleBox.TabIndex = 2;
			this.RoleBox.SelectedIndexChanged += new EventHandler(this.RoleBox_SelectedIndexChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.RoleBox);
			base.Controls.Add(this.InfoLbl);
			base.Name = "VariantRolePage";
			base.Size = new System.Drawing.Size(268, 93);
			base.ResumeLayout(false);
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnFinish()
		{
			return false;
		}

		public bool OnNext()
		{
			return true;
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = data as VariantData;
			}
			this.RoleBox.Items.Clear();
			foreach (RoleType role in this.fData.Roles)
			{
				this.RoleBox.Items.Add(role);
			}
			this.RoleBox.SelectedIndex = this.fData.SelectedRoleIndex;
		}

		private void RoleBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.fData.SelectedRoleIndex = this.RoleBox.SelectedIndex;
		}
	}
}