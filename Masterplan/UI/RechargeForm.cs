using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RechargeForm : Form
	{
		private Button CancelBtn;

		private Button OKBtn;

		private Panel ListPanel;

		private ListView EffectList;

		private ColumnHeader PowerHdr;

		private ColumnHeader SaveHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RollBtn;

		private Label InfoLbl;

		private ColumnHeader ResultHdr;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton SavedBtn;

		private ToolStripButton NotSavedBtn;

		private ColumnHeader RechargeHdr;

		private CombatData fData;

		private EncounterCard fCard;

		private Dictionary<Guid, int> fRolls = new Dictionary<Guid, int>();

		public Guid SelectedPowerID
		{
			get
			{
				if (this.EffectList.SelectedItems.Count == 0)
				{
					return Guid.Empty;
				}
				return (Guid)this.EffectList.SelectedItems[0].Tag;
			}
		}

		public RechargeForm(CombatData data, EncounterCard card)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fData = data;
			this.fCard = card;
			this.Text = string.Concat("Power Recharging: ", this.fData.DisplayName);
			foreach (Guid usedPower in this.fData.UsedPowers)
			{
				CreaturePower _power = this.get_power(usedPower);
				if (_power == null || _power.Action == null || _power.Action.Recharge == "")
				{
					continue;
				}
				this.fRolls[usedPower] = Session.Dice(1, 6);
			}
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RollBtn.Enabled = this.SelectedPowerID != Guid.Empty;
			if (this.SelectedPowerID == Guid.Empty)
			{
				this.SavedBtn.Enabled = false;
				this.NotSavedBtn.Enabled = false;
				return;
			}
			int item = this.fRolls[this.SelectedPowerID];
			this.SavedBtn.Enabled = item != 2147483647;
			this.NotSavedBtn.Enabled = item != -2147483648;
		}

		private int get_minimum(string recharge_str)
		{
			int num = 2147483647;
			if (recharge_str.Contains("6"))
			{
				num = 6;
			}
			if (recharge_str.Contains("5"))
			{
				num = 5;
			}
			if (recharge_str.Contains("4"))
			{
				num = 4;
			}
			if (recharge_str.Contains("3"))
			{
				num = 3;
			}
			if (recharge_str.Contains("2"))
			{
				num = 2;
			}
			return num;
		}

		private CreaturePower get_power(Guid power_id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.fCard.CreaturePowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != power_id)
					{
						continue;
					}
					creaturePower = current;
					return creaturePower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RechargeForm));
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ListPanel = new Panel();
			this.EffectList = new ListView();
			this.PowerHdr = new ColumnHeader();
			this.RechargeHdr = new ColumnHeader();
			this.SaveHdr = new ColumnHeader();
			this.ResultHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.RollBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SavedBtn = new ToolStripButton();
			this.NotSavedBtn = new ToolStripButton();
			this.InfoLbl = new Label();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(508, 277);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(427, 277);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.ListPanel.Controls.Add(this.EffectList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 33);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new System.Drawing.Size(571, 238);
			this.ListPanel.TabIndex = 1;
			ListView.ColumnHeaderCollection columns = this.EffectList.Columns;
			ColumnHeader[] powerHdr = new ColumnHeader[] { this.PowerHdr, this.RechargeHdr, this.SaveHdr, this.ResultHdr };
			columns.AddRange(powerHdr);
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new System.Drawing.Size(571, 213);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.PowerHdr.Text = "Power";
			this.PowerHdr.Width = 200;
			this.RechargeHdr.Text = "Recharge Condition";
			this.RechargeHdr.Width = 150;
			this.SaveHdr.Text = "Roll";
			this.SaveHdr.TextAlign = HorizontalAlignment.Center;
			this.SaveHdr.Width = 76;
			this.ResultHdr.Text = "Result";
			this.ResultHdr.Width = 111;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] rollBtn = new ToolStripItem[] { this.RollBtn, this.toolStripSeparator2, this.SavedBtn, this.NotSavedBtn };
			items.AddRange(rollBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(571, 25);
			this.Toolbar.TabIndex = 2;
			this.Toolbar.Text = "toolStrip1";
			this.RollBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RollBtn.Image = (Image)componentResourceManager.GetObject("RollBtn.Image");
			this.RollBtn.ImageTransparentColor = Color.Magenta;
			this.RollBtn.Name = "RollBtn";
			this.RollBtn.Size = new System.Drawing.Size(41, 22);
			this.RollBtn.Text = "Reroll";
			this.RollBtn.Click += new EventHandler(this.RollBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.SavedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SavedBtn.Image = (Image)componentResourceManager.GetObject("SavedBtn.Image");
			this.SavedBtn.ImageTransparentColor = Color.Magenta;
			this.SavedBtn.Name = "SavedBtn";
			this.SavedBtn.Size = new System.Drawing.Size(111, 22);
			this.SavedBtn.Text = "Mark as Recharged";
			this.SavedBtn.Click += new EventHandler(this.SavedBtn_Click);
			this.NotSavedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NotSavedBtn.Image = (Image)componentResourceManager.GetObject("NotSavedBtn.Image");
			this.NotSavedBtn.ImageTransparentColor = Color.Magenta;
			this.NotSavedBtn.Name = "NotSavedBtn";
			this.NotSavedBtn.Size = new System.Drawing.Size(134, 22);
			this.NotSavedBtn.Text = "Mark as Not Recharged";
			this.NotSavedBtn.Click += new EventHandler(this.NotSavedBtn_Click);
			this.InfoLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(571, 21);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "The following expended powers have recharge conditions.";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(595, 312);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RechargeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Recharging";
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void NotSavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = -2147483648;
				this.update_list();
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			List<Guid> guids = new List<Guid>();
			foreach (Guid key in this.fRolls.Keys)
			{
				if (!this.fRolls.ContainsKey(key))
				{
					continue;
				}
				int item = this.fRolls[key];
				CreaturePower _power = this.get_power(key);
				if (_power == null || _power.Action == null || _power.Action.Recharge == "")
				{
					continue;
				}
				int _minimum = this.get_minimum(_power.Action.Recharge);
				if (_minimum == 0 || item < _minimum)
				{
					continue;
				}
				guids.Add(key);
			}
			foreach (Guid guid in guids)
			{
				this.fData.UsedPowers.Remove(guid);
			}
		}

		private void RollBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = Session.Dice(1, 6);
				this.update_list();
			}
		}

		private void SavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = 2147483647;
				this.update_list();
			}
		}

		private void update_list()
		{
			Guid selectedPowerID = this.SelectedPowerID;
			this.EffectList.BeginUpdate();
			this.EffectList.Items.Clear();
			foreach (Guid usedPower in this.fData.UsedPowers)
			{
				if (!this.fRolls.ContainsKey(usedPower))
				{
					continue;
				}
				CreaturePower _power = this.get_power(usedPower);
				if (_power == null)
				{
					continue;
				}
				int item = this.fRolls[usedPower];
				ListViewItem d = this.EffectList.Items.Add(_power.Name);
				d.SubItems.Add(_power.Action.Recharge);
				d.Tag = _power.ID;
				if (usedPower == selectedPowerID)
				{
					d.Selected = true;
				}
				if (item == -2147483648)
				{
					d.SubItems.Add("-");
					d.SubItems.Add("Not recharged");
				}
				else if (item != 2147483647)
				{
					int _minimum = this.get_minimum(_power.Action.Recharge);
					if (_minimum != 2147483647)
					{
						d.SubItems.Add(item.ToString());
						if (item < _minimum)
						{
							d.SubItems.Add("Not recharged");
						}
						else
						{
							d.SubItems.Add("Recharged");
							d.ForeColor = SystemColors.GrayText;
						}
					}
					else
					{
						d.SubItems.Add("Not rolled");
						d.SubItems.Add("Not rolled");
					}
				}
				else
				{
					d.SubItems.Add("-");
					d.SubItems.Add("Recharged");
					d.ForeColor = SystemColors.GrayText;
				}
			}
			if (this.EffectList.Items.Count == 0)
			{
				ListViewItem grayText = this.EffectList.Items.Add("(no rechargable powers)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.EffectList.EndUpdate();
		}
	}
}