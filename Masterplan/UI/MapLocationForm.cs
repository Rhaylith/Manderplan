using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MapLocationForm : Form
	{
		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Label CatLbl;

		private ComboBox CatBox;

		private Masterplan.Data.MapLocation fLocation;

		public Masterplan.Data.MapLocation MapLocation
		{
			get
			{
				return this.fLocation;
			}
		}

		public MapLocationForm(Masterplan.Data.MapLocation loc)
		{
			this.InitializeComponent();
			this.CatBox.Items.Add("City");
			this.CatBox.Items.Add("Town");
			this.CatBox.Items.Add("Lake");
			this.CatBox.Items.Add("Port");
			this.CatBox.Items.Add("Mountain");
			this.CatBox.Items.Add("Volcano");
			this.CatBox.Items.Add("Chasm");
			this.CatBox.Items.Add("Sinkhole");
			this.CatBox.Items.Add("Cavern");
			this.CatBox.Items.Add("Marsh");
			this.CatBox.Items.Add("Swamp");
			this.CatBox.Items.Add("Fen");
			this.CatBox.Items.Add("Desert");
			this.CatBox.Items.Add("River");
			this.CatBox.Items.Add("Waterfall");
			this.CatBox.Items.Add("Ruin");
			this.CatBox.Items.Add("Outpost");
			this.CatBox.Items.Add("Inn");
			this.CatBox.Items.Add("Tower");
			this.CatBox.Items.Add("Barracks");
			this.CatBox.Items.Add("Hall");
			this.CatBox.Items.Add("Shop");
			this.CatBox.Items.Add("Market");
			this.CatBox.Items.Add("Gate");
			this.CatBox.Items.Add("Stables");
			this.CatBox.Items.Add("Warehouse");
			this.CatBox.Items.Add("Temple");
			this.fLocation = loc;
			this.NameBox.Text = this.fLocation.Name;
			this.CatBox.Text = this.fLocation.Category;
		}

		private void InitializeComponent()
		{
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.CatLbl = new Label();
			this.CatBox = new ComboBox();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(70, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(213, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(127, 73);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(208, 73);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 41);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new System.Drawing.Size(52, 13);
			this.CatLbl.TabIndex = 2;
			this.CatLbl.Text = "Category:";
			this.CatBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.CatBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.CatBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(70, 38);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new System.Drawing.Size(213, 21);
			this.CatBox.Sorted = true;
			this.CatBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(295, 108);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapLocationForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Location";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fLocation.Name = this.NameBox.Text;
			this.fLocation.Category = this.CatBox.Text;
		}
	}
}