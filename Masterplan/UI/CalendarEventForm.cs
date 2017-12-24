using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CalendarEventForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label MonthLbl;

		private ComboBox MonthBox;

		private Label DayLbl;

		private NumericUpDown DayBox;

		private CalendarEvent fEvent;

		private Calendar fCalendar;

		public CalendarEvent Event
		{
			get
			{
				return this.fEvent;
			}
		}

		public CalendarEventForm(CalendarEvent ce, Calendar calendar)
		{
			this.InitializeComponent();
			this.fEvent = ce.Copy();
			this.fCalendar = calendar;
			foreach (MonthInfo month in this.fCalendar.Months)
			{
				this.MonthBox.Items.Add(month);
			}
			this.NameBox.Text = this.fEvent.Name;
			this.DayBox.Value = this.fEvent.DayIndex + 1;
			MonthInfo monthInfo = this.fCalendar.FindMonth(this.fEvent.MonthID);
			this.MonthBox.SelectedItem = monthInfo;
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
			this.MonthLbl = new Label();
			this.MonthBox = new ComboBox();
			this.DayLbl = new Label();
			this.DayBox = new NumericUpDown();
			((ISupportInitialize)this.DayBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(98, 100);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 6;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(179, 100);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.NameBox.Location = new Point(58, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(196, 20);
			this.NameBox.TabIndex = 1;
			this.MonthLbl.AutoSize = true;
			this.MonthLbl.Location = new Point(12, 41);
			this.MonthLbl.Name = "MonthLbl";
			this.MonthLbl.Size = new System.Drawing.Size(40, 13);
			this.MonthLbl.TabIndex = 2;
			this.MonthLbl.Text = "Month:";
			this.MonthBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.MonthBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MonthBox.FormattingEnabled = true;
			this.MonthBox.Location = new Point(58, 38);
			this.MonthBox.Name = "MonthBox";
			this.MonthBox.Size = new System.Drawing.Size(196, 21);
			this.MonthBox.TabIndex = 3;
			this.MonthBox.SelectedIndexChanged += new EventHandler(this.MonthBox_SelectedIndexChanged);
			this.DayLbl.AutoSize = true;
			this.DayLbl.Location = new Point(12, 67);
			this.DayLbl.Name = "DayLbl";
			this.DayLbl.Size = new System.Drawing.Size(29, 13);
			this.DayLbl.TabIndex = 4;
			this.DayLbl.Text = "Day:";
			this.DayBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.DayBox.Location = new Point(58, 65);
			NumericUpDown dayBox = this.DayBox;
			int[] numArray = new int[] { 1, 0, 0, 0 };
			dayBox.Minimum = new decimal(numArray);
			this.DayBox.Name = "DayBox";
			this.DayBox.Size = new System.Drawing.Size(196, 20);
			this.DayBox.TabIndex = 5;
			NumericUpDown num = this.DayBox;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			num.Value = new decimal(numArray1);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(266, 135);
			base.Controls.Add(this.DayBox);
			base.Controls.Add(this.DayLbl);
			base.Controls.Add(this.MonthBox);
			base.Controls.Add(this.MonthLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CalendarEventForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Event";
			((ISupportInitialize)this.DayBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MonthBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			MonthInfo selectedItem = this.MonthBox.SelectedItem as MonthInfo;
			this.DayBox.Maximum = selectedItem.DayCount;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fEvent.Name = this.NameBox.Text;
			this.fEvent.DayIndex = (int)this.DayBox.Value - 1;
			MonthInfo selectedItem = this.MonthBox.SelectedItem as MonthInfo;
			this.fEvent.MonthID = selectedItem.ID;
		}
	}
}