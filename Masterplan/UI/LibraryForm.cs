using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class LibraryForm : Form
	{
		private Masterplan.Data.Library fLibrary;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label InfoLbl;

		public Masterplan.Data.Library Library
		{
			get
			{
				return this.fLibrary;
			}
		}

		public LibraryForm(Masterplan.Data.Library lib)
		{
			this.InitializeComponent();
			this.fLibrary = lib;
			string userName = SystemInformation.UserName;
			string computerName = SystemInformation.ComputerName;
			Label infoLbl = this.InfoLbl;
			string[] strArrays = new string[] { "Note that when you create a library it will be usable only by this user (", userName, ") on this computer (", computerName, ")." };
			infoLbl.Text = string.Concat(strArrays);
			this.NameBox.Text = this.fLibrary.Name;
			this.NameBox_TextChanged(null, null);
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.InfoLbl = new Label();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(226, 69);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(307, 69);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(326, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.InfoLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.InfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.InfoLbl.Location = new Point(12, 35);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(370, 31);
			this.InfoLbl.TabIndex = 4;
			this.InfoLbl.Text = "Note that when you create a library it will be usable only by this user (xxx) on this computer (xxx).";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(394, 104);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LibraryForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Library";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			if (this.NameBox.Text == "")
			{
				this.OKBtn.Enabled = false;
				return;
			}
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			DirectoryInfo directoryInfo = new DirectoryInfo(FileName.Directory(entryAssembly.FullName));
			string str = string.Concat(directoryInfo, this.NameBox.Text, ".library");
			this.OKBtn.Enabled = !File.Exists(str);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fLibrary.Name = this.NameBox.Text;
		}
	}
}