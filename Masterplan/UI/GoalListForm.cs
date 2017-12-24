using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class GoalListForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private TreeView GoalTree;

		private ToolStrip Toolbar;

		private System.Windows.Forms.Panel BrowserPanel;

		private WebBrowser Browser;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private System.Windows.Forms.Panel Panel;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ClearBtn;

		private PartyGoals fPartyGoals;

		private bool fCreatePlot;

		public bool CreatePlot
		{
			get
			{
				return this.fCreatePlot;
			}
		}

		public PartyGoals Goals
		{
			get
			{
				return this.fPartyGoals;
			}
		}

		public Goal SelectedGoal
		{
			get
			{
				if (this.GoalTree.SelectedNode == null)
				{
					return null;
				}
				return this.GoalTree.SelectedNode.Tag as Goal;
			}
			set
			{
				this.GoalTree.SelectedNode = this.find_node(value, this.GoalTree.Nodes);
			}
		}

		public GoalListForm(PartyGoals goals)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.fPartyGoals = goals.Copy();
			this.update_tree();
			this.update_goal();
		}

		private void add_goal(Goal goal, TreeNode parent)
		{
			TreeNode treeNode = ((parent != null ? parent.Nodes : this.GoalTree.Nodes)).Add(goal.Name);
			treeNode.Tag = goal;
			foreach (Goal prerequisite in goal.Prerequisites)
			{
				this.add_goal(prerequisite, treeNode);
			}
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			GoalForm goalForm = new GoalForm(new Goal("New Goal"));
			if (goalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				List<Goal> prerequisites = (this.SelectedGoal != null ? this.SelectedGoal.Prerequisites : this.fPartyGoals.Goals);
				if (prerequisites == this.fPartyGoals.Goals && this.fPartyGoals.Goals.Count != 0)
				{
					prerequisites = this.fPartyGoals.Goals[0].Prerequisites;
				}
				prerequisites.Add(goalForm.Goal);
				this.update_tree();
				this.SelectedGoal = goalForm.Goal;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedGoal != null;
			this.EditBtn.Enabled = this.SelectedGoal != null;
			this.ClearBtn.Enabled = this.fPartyGoals.Goals.Count != 0;
			this.OKBtn.Enabled = this.fPartyGoals.Goals.Count != 0;
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.fPartyGoals.Goals.Clear();
			this.update_tree();
			this.update_goal();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedGoal != null)
			{
				List<Goal> goal = this.fPartyGoals.FindList(this.SelectedGoal);
				if (goal != null)
				{
					int num = goal.IndexOf(this.SelectedGoal);
					GoalForm goalForm = new GoalForm(this.SelectedGoal);
					if (goalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						goal[num] = goalForm.Goal;
						this.update_tree();
						this.SelectedGoal = goalForm.Goal;
					}
				}
			}
		}

		private TreeNode find_node(Goal goal, TreeNodeCollection tnc)
		{
			TreeNode treeNode;
			IEnumerator enumerator = tnc.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode current = (TreeNode)enumerator.Current;
					if (current.Tag as Goal != goal)
					{
						TreeNode treeNode1 = this.find_node(goal, current.Nodes);
						if (treeNode1 == null)
						{
							continue;
						}
						treeNode = treeNode1;
						return treeNode;
					}
					else
					{
						treeNode = current;
						return treeNode;
					}
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return treeNode;
		}

		private void GoalListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				System.Windows.Forms.DialogResult dialogResult = MessageBox.Show("Do you want to build a plotline from these goals?", "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dialogResult != System.Windows.Forms.DialogResult.Cancel)
				{
					switch (dialogResult)
					{
						case System.Windows.Forms.DialogResult.Yes:
						{
							this.fCreatePlot = true;
							return;
						}
						case System.Windows.Forms.DialogResult.No:
						{
							this.fCreatePlot = false;
							return;
						}
						default:
						{
							return;
						}
					}
				}
				e.Cancel = true;
			}
		}

		private void GoalTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.update_goal();
		}

		private void GoalTree_DragDrop(object sender, DragEventArgs e)
		{
		}

		private void GoalTree_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			Goal data = e.Data.GetData(typeof(Goal)) as Goal;
			Point client = this.GoalTree.PointToClient(System.Windows.Forms.Cursor.Position);
			TreeNode nodeAt = this.GoalTree.GetNodeAt(client);
			if (nodeAt != null)
			{
				this.GoalTree.SelectedNode = nodeAt;
				if (this.SelectedGoal.Prerequisites.Contains(data))
				{
					return;
				}
				if (data.Subtree.Contains(this.SelectedGoal))
				{
					return;
				}
				e.Effect = DragDropEffects.Move;
			}
		}

		private void GoalTree_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedGoal != null)
			{
				Goal selectedGoal = this.SelectedGoal;
				if (base.DoDragDrop(selectedGoal, DragDropEffects.Move) == DragDropEffects.Move)
				{
					this.fPartyGoals.FindList(selectedGoal).Remove(selectedGoal);
					this.SelectedGoal.Prerequisites.Add(selectedGoal);
					this.update_tree();
					this.update_goal();
				}
			}
		}

		private void GoalTree_MouseDown(object sender, MouseEventArgs e)
		{
			Point client = this.GoalTree.PointToClient(System.Windows.Forms.Cursor.Position);
			this.GoalTree.SelectedNode = this.GoalTree.GetNodeAt(client);
			this.update_goal();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GoalListForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.GoalTree = new TreeView();
			this.BrowserPanel = new System.Windows.Forms.Panel();
			this.Browser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.Panel = new System.Windows.Forms.Panel();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ClearBtn = new ToolStripButton();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			this.Panel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(492, 301);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(573, 301);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(0, 25);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.GoalTree);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new System.Drawing.Size(636, 258);
			this.Splitter.SplitterDistance = 235;
			this.Splitter.TabIndex = 2;
			this.GoalTree.AllowDrop = true;
			this.GoalTree.Dock = DockStyle.Fill;
			this.GoalTree.Location = new Point(0, 0);
			this.GoalTree.Name = "GoalTree";
			this.GoalTree.Size = new System.Drawing.Size(235, 258);
			this.GoalTree.TabIndex = 1;
			this.GoalTree.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.GoalTree.DragDrop += new DragEventHandler(this.GoalTree_DragDrop);
			this.GoalTree.AfterSelect += new TreeViewEventHandler(this.GoalTree_AfterSelect);
			this.GoalTree.MouseDown += new MouseEventHandler(this.GoalTree_MouseDown);
			this.GoalTree.ItemDrag += new ItemDragEventHandler(this.GoalTree_ItemDrag);
			this.GoalTree.DragOver += new DragEventHandler(this.GoalTree_DragOver);
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(397, 258);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.Size = new System.Drawing.Size(395, 256);
			this.Browser.TabIndex = 0;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn, this.toolStripSeparator1, this.ClearBtn };
			items.AddRange(addBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(636, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.Panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Panel.Controls.Add(this.Splitter);
			this.Panel.Controls.Add(this.Toolbar);
			this.Panel.Location = new Point(12, 12);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(636, 283);
			this.Panel.TabIndex = 3;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.ClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearBtn.Image = (Image)componentResourceManager.GetObject("ClearBtn.Image");
			this.ClearBtn.ImageTransparentColor = Color.Magenta;
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(55, 22);
			this.ClearBtn.Text = "Clear All";
			this.ClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(660, 336);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GoalListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Party Goals";
			base.FormClosing += new FormClosingEventHandler(this.GoalListForm_FormClosing);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Panel.ResumeLayout(false);
			this.Panel.PerformLayout();
			base.ResumeLayout(false);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedGoal != null)
			{
				List<Goal> goals = this.fPartyGoals.FindList(this.SelectedGoal);
				if (goals != null)
				{
					goals.Remove(this.SelectedGoal);
					this.update_tree();
					this.update_goal();
				}
			}
		}

		private void update_goal()
		{
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(HTML.Goal(this.SelectedGoal));
		}

		private void update_tree()
		{
			this.GoalTree.Nodes.Clear();
			foreach (Goal goal in this.fPartyGoals.Goals)
			{
				this.add_goal(goal, null);
			}
			if (this.fPartyGoals.Goals.Count == 0)
			{
				TreeNode grayText = this.GoalTree.Nodes.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.GoalTree.ExpandAll();
		}
	}
}