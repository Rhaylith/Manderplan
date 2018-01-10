using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DamageTypesForm : Form
	{
		private Button OKBtn;

		private Button CancelBtn;

		private ListView TypeList;

		private ColumnHeader TypeHdr;

		private List<DamageType> fTypes;

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
			}
		}

		public DamageTypesForm(List<DamageType> types)
		{
			this.InitializeComponent();
			this.fTypes = types;
			foreach (DamageType value in Enum.GetValues(typeof(DamageType)))
			{
				if (value == DamageType.Untyped)
				{
					continue;
				}
				ListViewItem listViewItem = this.TypeList.Items.Add(value.ToString());
				listViewItem.Checked = this.fTypes.Contains(value);
				listViewItem.Tag = value;
			}
		}

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TypeList = new ListView();
			this.TypeHdr = new ColumnHeader();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(94, 308);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(175, 308);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TypeList.CheckBoxes = true;
			this.TypeList.Columns.AddRange(new ColumnHeader[] { this.TypeHdr });
			this.TypeList.FullRowSelect = true;
			this.TypeList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TypeList.HideSelection = false;
			this.TypeList.Location = new Point(12, 12);
			this.TypeList.MultiSelect = false;
			this.TypeList.Name = "TypeList";
			this.TypeList.Size = new System.Drawing.Size(238, 290);
			this.TypeList.TabIndex = 0;
			this.TypeList.UseCompatibleStateImageBehavior = false;
			this.TypeList.View = View.Details;
			this.TypeHdr.Text = "Type";
			this.TypeHdr.Width = 200;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(262, 343);
			base.Controls.Add(this.TypeList);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageTypesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage Type";
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			List<DamageType> damageTypes = new List<DamageType>();
			foreach (ListViewItem checkedItem in this.TypeList.CheckedItems)
			{
				damageTypes.Add((DamageType)checkedItem.Tag);
			}
			this.fTypes = damageTypes;
		}
	}
}