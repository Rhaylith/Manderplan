using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.UI
{
	internal class CreatureBuilderForm : Form
	{
		private const int SAMPLE_POWERS = 5;

		private ICreature fCreature;

		private List<CreaturePower> fSamplePowers = new List<CreaturePower>();

		private CreatureBuilderForm.SidebarType fSidebar;

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private TabControl Pages;

		private TabPage StatBlockPage;

		private WebBrowser StatBlockBrowser;

		private TabPage PicturePage;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsImport;

		private ToolStripMenuItem OptionsVariant;

		private PictureBox PortraitBox;

		private ToolStrip PictureToolbar;

		private ToolStripButton PictureBrowseBtn;

		private ToolStripButton PicturePasteBtn;

		private ToolStripButton PictureClearBtn;

		private TabPage EntryPage;

		private WebBrowser EntryBrowser;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem OptionsEntry;

		private ToolStripButton LevelDownBtn;

		private ToolStripButton LevelUpBtn;

		private ToolStripLabel LevelLbl;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem OptionsPowerBrowser;

		private ToolStripMenuItem OptionsRandom;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton AdviceBtn;

		private ToolStripButton PowersBtn;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		public ICreature Creature
		{
			get
			{
				return this.fCreature;
			}
		}

		public CreatureBuilderForm(ICreature creature)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			if (creature is Masterplan.Data.Creature)
			{
				this.fCreature = (creature as Masterplan.Data.Creature).Copy();
			}
			if (creature is CustomCreature)
			{
				this.fCreature = (creature as CustomCreature).Copy();
			}
			if (creature is NPC)
			{
				this.fCreature = (creature as NPC).Copy();
				this.OptionsImport.Enabled = false;
				this.OptionsVariant.Enabled = false;
			}
			if (Session.Project != null)
			{
				this.update_entry();
			}
			else
			{
				this.Pages.TabPages.Remove(this.EntryPage);
				this.OptionsEntry.Enabled = false;
			}
			this.find_sample_powers();
			this.update_view();
		}

		private void add_power(CreaturePower power)
		{
			this.fCreature.CreaturePowers.Add(power);
			this.update_statblock();
		}

		private void AdviceBtn_Click(object sender, EventArgs e)
		{
			if (this.fSidebar != CreatureBuilderForm.SidebarType.Advice)
			{
				this.fSidebar = CreatureBuilderForm.SidebarType.Advice;
				this.update_statblock();
			}
		}

		private CreaturePower alter_power_level(CreaturePower power, int original_level, int new_level)
		{
			CreaturePower creaturePower = power.Copy();
			CreatureHelper.AdjustPowerLevel(creaturePower, new_level - original_level);
			if (original_level <= 15 && new_level > 15)
			{
				creaturePower.Details = creaturePower.Details.Replace("Dazed", "Stunned");
				creaturePower.Details = creaturePower.Details.Replace("dazed", "stunned");
				creaturePower.Details = creaturePower.Details.Replace("Slowed", "Immobilised");
				creaturePower.Details = creaturePower.Details.Replace("slowed", "immobilised");
			}
			if (original_level > 15 && new_level <= 15)
			{
				creaturePower.Details = creaturePower.Details.Replace("Stunned", "Dazed");
				creaturePower.Details = creaturePower.Details.Replace("Stunned", "Dazed");
				creaturePower.Details = creaturePower.Details.Replace("Immobilised", "Immobilized");
				creaturePower.Details = creaturePower.Details.Replace("immobilised", "immobilized");
				creaturePower.Details = creaturePower.Details.Replace("Immobilized", "Slowed");
				creaturePower.Details = creaturePower.Details.Replace("immobilized", "slowed");
			}
			return creaturePower;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PicturePasteBtn.Enabled = Clipboard.ContainsImage();
			this.PictureClearBtn.Enabled = this.fCreature.Image != null;
			this.AdviceBtn.Checked = this.fSidebar == CreatureBuilderForm.SidebarType.Advice;
			this.PowersBtn.Checked = this.fSidebar == CreatureBuilderForm.SidebarType.Powers;
			this.LevelDownBtn.Enabled = this.fCreature.Level > 1;
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			Regeneration regeneration;
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					int level = this.fCreature.Level;
					string str = this.fCreature.Role.ToString();
					if ((new CreatureProfileForm(this.fCreature)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						if ((this.fCreature.Level != level || this.fCreature.Role.ToString() != str) && MessageBox.Show(string.Concat(string.Concat("You've changed this creature's level and/or role.", Environment.NewLine), "Do you want to update its combat statistics to match?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							if (!(this.fCreature.Role is ComplexRole))
							{
								this.fCreature.HP = 1;
							}
							else
							{
								this.fCreature.HP = Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score);
							}
							this.fCreature.Initiative = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role);
							this.fCreature.AC = Statistics.AC(this.fCreature.Level, this.fCreature.Role);
							this.fCreature.Fortitude = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
							this.fCreature.Reflex = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
							this.fCreature.Will = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
						}
						this.find_sample_powers();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "combat")
				{
					e.Cancel = true;
					if ((new CreatureStatsForm(this.fCreature)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "ability")
				{
					e.Cancel = true;
					if ((new CreatureAbilityForm(this.fCreature)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "damage")
				{
					e.Cancel = true;
					if ((new DamageModListForm(this.fCreature)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "senses")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fCreature, DetailsField.Senses, "Note: Do not add Perception here; it should be entered as a skill.");
					if (detailsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Senses = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "movement")
				{
					e.Cancel = true;
					DetailsForm detailsForm1 = new DetailsForm(this.fCreature, DetailsField.Movement, null);
					if (detailsForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Movement = detailsForm1.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "alignment")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fCreature, DetailsField.Alignment, null);
					if (detailsForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Alignment = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "languages")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fCreature, DetailsField.Languages, null);
					if (detailsForm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Languages = detailsForm3.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "skills")
				{
					e.Cancel = true;
					if ((new CreatureSkillsForm(this.fCreature)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "equipment")
				{
					e.Cancel = true;
					DetailsForm detailsForm4 = new DetailsForm(this.fCreature, DetailsField.Equipment, null);
					if (detailsForm4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Equipment = detailsForm4.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "tactics")
				{
					e.Cancel = true;
					DetailsForm detailsForm5 = new DetailsForm(this.fCreature, DetailsField.Tactics, null);
					if (detailsForm5.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Tactics = detailsForm5.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "import")
				{
					e.Cancel = true;
					this.import_creature();
				}
				if (e.Url.LocalPath == "variant")
				{
					e.Cancel = true;
					this.create_variant();
				}
				if (e.Url.LocalPath == "random")
				{
					e.Cancel = true;
					this.create_random();
				}
				if (e.Url.LocalPath == "hybrid")
				{
					e.Cancel = true;
					this.create_hybrid();
				}
				if (e.Url.LocalPath == "name")
				{
					e.Cancel = true;
					string name = this.fCreature.Name;
					this.fCreature.Name = this.generate_name();
					for (int i = 0; i != this.fCreature.CreaturePowers.Count; i++)
					{
						CreaturePower item = this.fCreature.CreaturePowers[i];
						item = this.replace_name(item, name, "", this.fCreature.Name);
						this.fCreature.CreaturePowers[i] = item;
					}
					for (int j = 0; j != this.fSamplePowers.Count; j++)
					{
						CreaturePower creaturePower = this.fSamplePowers[j];
						creaturePower = this.replace_name(creaturePower, name, "", this.fCreature.Name);
						this.fSamplePowers[j] = creaturePower;
					}
					this.update_statblock();
				}
				if (e.Url.LocalPath == "template")
				{
					e.Cancel = true;
					CreatureTemplateSelectForm creatureTemplateSelectForm = new CreatureTemplateSelectForm();
					if (creatureTemplateSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK && creatureTemplateSelectForm.Template != null)
					{
						EncounterCard encounterCard = new EncounterCard(this.fCreature);
						encounterCard.TemplateIDs.Add(creatureTemplateSelectForm.Template.ID);
						ICreature creature = null;
						if (this.fCreature is Masterplan.Data.Creature)
						{
							creature = new Masterplan.Data.Creature();
						}
						if (this.fCreature is CustomCreature)
						{
							creature = new CustomCreature();
						}
						if (this.fCreature is NPC)
						{
							creature = new NPC();
						}
						creature.Name = encounterCard.Title;
						creature.Level = encounterCard.Level;
						creature.Senses = encounterCard.Senses;
						creature.Movement = encounterCard.Movement;
						creature.Resist = encounterCard.Resist;
						creature.Vulnerable = encounterCard.Vulnerable;
						creature.Immune = encounterCard.Immune;
						ComplexRole complexRole = new ComplexRole()
						{
							Leader = encounterCard.Leader,
							Flag = encounterCard.Flag,
							Type = encounterCard.Roles[0]
						};
						creature.Role = complexRole;
						creature.Initiative = encounterCard.Initiative;
						creature.HP = encounterCard.HP;
						creature.AC = encounterCard.AC;
						creature.Fortitude = encounterCard.Fortitude;
						creature.Reflex = encounterCard.Reflex;
						creature.Will = encounterCard.Will;
						ICreature creature1 = creature;
						if (encounterCard.Regeneration != null)
						{
							regeneration = encounterCard.Regeneration;
						}
						else
						{
							regeneration = null;
						}
						creature1.Regeneration = regeneration;
						foreach (Aura aura in encounterCard.Auras)
						{
							creature.Auras.Add(aura.Copy());
						}
						foreach (CreaturePower creaturePower1 in encounterCard.CreaturePowers)
						{
							creature.CreaturePowers.Add(creaturePower1.Copy());
						}
						foreach (DamageModifier damageModifier in encounterCard.DamageModifiers)
						{
							creature.DamageModifiers.Add(damageModifier.Copy());
						}
						Guid d = this.fCreature.ID;
						CreatureHelper.CopyFields(creature, this.fCreature);
						this.fCreature.ID = d;
						this.find_sample_powers();
						this.update_view();
					}
				}
			}
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "addpower")
				{
					e.Cancel = true;
					CreaturePower creaturePower2 = new CreaturePower()
					{
						Name = "New Power",
						Action = new PowerAction()
					};
					PowerBuilderForm powerBuilderForm = new PowerBuilderForm(creaturePower2, this.fCreature, false);
					if (powerBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.CreaturePowers.Add(powerBuilderForm.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addtrait")
				{
					e.Cancel = true;
					CreaturePower creaturePower3 = new CreaturePower()
					{
						Name = "New Trait",
						Action = null
					};
					PowerBuilderForm powerBuilderForm1 = new PowerBuilderForm(creaturePower3, this.fCreature, false);
					if (powerBuilderForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.CreaturePowers.Add(powerBuilderForm1.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addaura")
				{
					e.Cancel = true;
					Aura aura1 = new Aura()
					{
						Name = "New Aura",
						Details = "1"
					};
					AuraForm auraForm = new AuraForm(aura1);
					if (auraForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Auras.Add(auraForm.Aura);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "browse")
				{
					e.Cancel = true;
					this.OptionsPowerBrowser_Click(null, null);
				}
				if (e.Url.LocalPath == "statistics")
				{
					e.Cancel = true;
					List<ICreature> creatures = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, true);
					List<CreaturePower> creaturePowers = new List<CreaturePower>();
					foreach (ICreature creature2 in creatures)
					{
						creaturePowers.AddRange(creature2.CreaturePowers);
					}
					(new PowerStatisticsForm(creaturePowers, creatures.Count)).ShowDialog();
				}
				if (e.Url.LocalPath == "regenedit")
				{
					e.Cancel = true;
					Regeneration regeneration1 = this.fCreature.Regeneration ?? new Regeneration(5, "");
					RegenerationForm regenerationForm = new RegenerationForm(regeneration1);
					if (regenerationForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Regeneration = regenerationForm.Regeneration;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenremove")
				{
					e.Cancel = true;
					this.fCreature.Regeneration = null;
					this.update_statblock();
				}
				if (e.Url.LocalPath == "refresh")
				{
					e.Cancel = true;
					this.find_sample_powers();
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "poweredit")
			{
				CreaturePower creaturePower4 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower4 != null)
				{
					e.Cancel = true;
					int power = this.fCreature.CreaturePowers.IndexOf(creaturePower4);
					PowerBuilderForm powerBuilderForm2 = new PowerBuilderForm(creaturePower4, this.fCreature, false);
					if (powerBuilderForm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.CreaturePowers[power] = powerBuilderForm2.Power;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "powerremove")
			{
				CreaturePower creaturePower5 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower5 != null)
				{
					e.Cancel = true;
					this.fCreature.CreaturePowers.Remove(creaturePower5);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "powerduplicate")
			{
				CreaturePower creaturePower6 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower6 != null)
				{
					e.Cancel = true;
					int num = this.fCreature.CreaturePowers.IndexOf(creaturePower6);
					creaturePower6 = creaturePower6.Copy();
					creaturePower6.ID = Guid.NewGuid();
					this.fCreature.CreaturePowers.Insert(num, creaturePower6);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "auraedit")
			{
				Aura aura2 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura2 != null)
				{
					e.Cancel = true;
					int num1 = this.fCreature.Auras.IndexOf(aura2);
					AuraForm auraForm1 = new AuraForm(aura2);
					if (auraForm1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fCreature.Auras[num1] = auraForm1.Aura;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "auraremove")
			{
				Aura aura3 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura3 != null)
				{
					e.Cancel = true;
					this.fCreature.Auras.Remove(aura3);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "samplepower")
			{
				CreaturePower creaturePower7 = this.find_sample_power(new Guid(e.Url.LocalPath));
				if (creaturePower7 != null)
				{
					e.Cancel = true;
					this.fCreature.CreaturePowers.Add(creaturePower7);
					this.fSamplePowers.Remove(creaturePower7);
					if (this.fSamplePowers.Count == 0)
					{
						this.find_sample_powers();
					}
					this.update_statblock();
				}
			}
		}

		private void create_hybrid()
		{
			CreatureMultipleSelectForm creatureMultipleSelectForm = new CreatureMultipleSelectForm();
			if (creatureMultipleSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.splice(creatureMultipleSelectForm.SelectedCreatures);
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void create_random()
		{
			RandomCreatureForm randomCreatureForm = new RandomCreatureForm(this.fCreature.Level, this.fCreature.Role);
			if (randomCreatureForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				List<ICreature> creatures = this.find_matching_creatures(randomCreatureForm.Role, randomCreatureForm.Level, true);
				if (creatures.Count == 0)
				{
					return;
				}
				this.splice(creatures);
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void create_variant()
		{
			Regeneration regeneration;
			VariantWizard variantWizard = new VariantWizard();
			if (variantWizard.Show() == System.Windows.Forms.DialogResult.OK)
			{
				VariantData data = variantWizard.Data as VariantData;
				EncounterCard encounterCard = new EncounterCard()
				{
					CreatureID = data.BaseCreature.ID
				};
				foreach (CreatureTemplate template in data.Templates)
				{
					encounterCard.TemplateIDs.Add(template.ID);
				}
				ICreature creature = null;
				if (this.fCreature is Masterplan.Data.Creature)
				{
					creature = new Masterplan.Data.Creature();
				}
				if (this.fCreature is CustomCreature)
				{
					creature = new CustomCreature();
				}
				if (this.fCreature is NPC)
				{
					creature = new NPC();
				}
				creature.Name = string.Concat("Variant ", encounterCard.Title);
				creature.Details = data.BaseCreature.Details;
				creature.Size = data.BaseCreature.Size;
				creature.Level = encounterCard.Level;
				if (data.BaseCreature.Image != null)
				{
					creature.Image = new Bitmap(data.BaseCreature.Image);
				}
				creature.Senses = encounterCard.Senses;
				creature.Movement = encounterCard.Movement;
				creature.Resist = encounterCard.Resist;
				creature.Vulnerable = encounterCard.Vulnerable;
				creature.Immune = encounterCard.Immune;
				if (!(data.BaseCreature.Role is Minion))
				{
					ComplexRole complexRole = new ComplexRole()
					{
						Type = data.Roles[data.SelectedRoleIndex],
						Flag = encounterCard.Flag,
						Leader = encounterCard.Leader
					};
					creature.Role = complexRole;
				}
				else
				{
					creature.Role = new Minion();
				}
				creature.Strength.Score = data.BaseCreature.Strength.Score;
				creature.Constitution.Score = data.BaseCreature.Constitution.Score;
				creature.Dexterity.Score = data.BaseCreature.Dexterity.Score;
				creature.Intelligence.Score = data.BaseCreature.Intelligence.Score;
				creature.Wisdom.Score = data.BaseCreature.Wisdom.Score;
				creature.Charisma.Score = data.BaseCreature.Charisma.Score;
				creature.Initiative = data.BaseCreature.Initiative;
				creature.HP = encounterCard.HP;
				creature.AC = encounterCard.AC;
				creature.Fortitude = encounterCard.Fortitude;
				creature.Reflex = encounterCard.Reflex;
				creature.Will = encounterCard.Will;
				ICreature creature1 = creature;
				if (encounterCard.Regeneration != null)
				{
					regeneration = encounterCard.Regeneration;
				}
				else
				{
					regeneration = null;
				}
				creature1.Regeneration = regeneration;
				foreach (Aura aura in encounterCard.Auras)
				{
					creature.Auras.Add(aura.Copy());
				}
				foreach (CreaturePower creaturePower in encounterCard.CreaturePowers)
				{
					creature.CreaturePowers.Add(creaturePower.Copy());
				}
				foreach (DamageModifier damageModifier in encounterCard.DamageModifiers)
				{
					creature.DamageModifiers.Add(damageModifier.Copy());
				}
				Guid d = this.fCreature.ID;
				CreatureHelper.CopyFields(creature, this.fCreature);
				this.fCreature.ID = d;
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Export Creature",
				FileName = this.fCreature.Name,
				Filter = Program.CreatureFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Masterplan.Data.Creature creature = new Masterplan.Data.Creature(this.fCreature);
				if (!Serialisation<Masterplan.Data.Creature>.Save(saveFileDialog.FileName, creature, SerialisationMode.Binary))
				{
					MessageBox.Show("The creature could not be exported.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private Aura find_aura(Guid id)
		{
			Aura aura;
			List<Aura>.Enumerator enumerator = this.fCreature.Auras.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Aura current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					aura = current;
					return aura;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private string find_common_name(List<ICreature> creatures)
		{
			Dictionary<string, int> strs = new Dictionary<string, int>();
			for (int i = 0; i != creatures.Count; i++)
			{
				string name = creatures[i].Name;
				for (int j = i + 1; j != creatures.Count; j++)
				{
					string str = creatures[j].Name;
					string str1 = StringHelper.LongestCommonToken(name, str);
					if (str1.Length >= 3)
					{
						if (!strs.ContainsKey(str1))
						{
							strs[str1] = 0;
						}
						Dictionary<string, int> item = strs;
						Dictionary<string, int> strs1 = item;
						string str2 = str1;
						item[str2] = strs1[str2] + 1;
					}
				}
			}
			string str3 = "";
			if (strs.Keys.Count != 0)
			{
				foreach (string key in strs.Keys)
				{
					if (strs[key] <= (strs.ContainsKey(str3) ? strs[str3] : 0))
					{
						continue;
					}
					str3 = key;
				}
			}
			return str3;
		}

		private string find_description(List<ICreature> creatures)
		{
			List<string> strs = new List<string>()
			{
				"er",
				"ist"
			};
			List<string> strs1 = new List<string>();
			foreach (ICreature creature in creatures)
			{
				string[] strArrays = creature.Name.Split(null);
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					bool flag = false;
					foreach (string str1 in strs)
					{
						if (!str.EndsWith(str1))
						{
							continue;
						}
						flag = true;
						break;
					}
					if (flag)
					{
						strs1.Add(str);
					}
				}
			}
			if (strs1.Count == 0)
			{
				return "";
			}
			return strs1[Session.Random.Next(strs1.Count)];
		}

		private List<ICreature> find_matching_creatures(IRole role, int level, bool exact_match)
		{
			List<ICreature> creatures = new List<ICreature>();
			int num = (exact_match ? 0 : 1);
			List<ICreature> creatures1 = new List<ICreature>();
			foreach (ICreature creature in Session.Creatures)
			{
				creatures1.Add(creature);
			}
			if (Session.Project != null)
			{
				foreach (ICreature customCreature in Session.Project.CustomCreatures)
				{
					creatures1.Add(customCreature);
				}
				foreach (ICreature nPC in Session.Project.NPCs)
				{
					creatures1.Add(nPC);
				}
			}
			foreach (ICreature creature1 in creatures1)
			{
				bool flag = Math.Abs(creature1.Level - level) <= num;
				bool str = creature1.Role.ToString() == role.ToString();
				if (!flag || !str)
				{
					continue;
				}
				creatures.Add(creature1);
			}
			return creatures;
		}

		private CreaturePower find_power(Guid id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.fCreature.CreaturePowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != id)
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

		private CreaturePower find_sample_power(Guid id)
		{
			CreaturePower creaturePower;
			List<CreaturePower>.Enumerator enumerator = this.fSamplePowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreaturePower current = enumerator.Current;
					if (current.ID != id)
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

		private void find_sample_powers()
		{
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			List<CreaturePower> creaturePowers = new List<CreaturePower>();
			foreach (CreaturePower creaturePower in this.fCreature.CreaturePowers)
			{
				if (creaturePower == null)
				{
					continue;
				}
				binarySearchTree.Add(creaturePower.Name.ToLower());
			}
			List<ICreature> creatures = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, false);
			foreach (ICreature creature in creatures)
			{
				foreach (CreaturePower creaturePower1 in creature.CreaturePowers)
				{
					if (creaturePower1 == null || binarySearchTree.Contains(creaturePower1.Name.ToLower()))
					{
						continue;
					}
					CreaturePower creaturePower2 = this.replace_name(creaturePower1, creature.Name, creature.Category, this.fCreature.Name);
					creaturePower2 = this.alter_power_level(creaturePower2, creature.Level, this.fCreature.Level);
					if (creaturePower2 == null)
					{
						continue;
					}
					creaturePowers.Add(creaturePower2.Copy());
					binarySearchTree.Add(creaturePower2.Name);
				}
			}
			int num = Math.Min(creaturePowers.Count, 5);
			this.fSamplePowers.Clear();
			while (this.fSamplePowers.Count != num)
			{
				int num1 = Session.Random.Next(creaturePowers.Count);
				CreaturePower item = creaturePowers[num1];
				if (item == null)
				{
					continue;
				}
				this.fSamplePowers.Add(item);
				creaturePowers.Remove(item);
			}
		}

		private string generate_name()
		{
			string str = ExoticName.SingleName();
			List<ICreature> creatures = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, false);
			string str1 = this.find_description(creatures);
			if (str1 != "")
			{
				str = string.Concat(str, " ", str1);
			}
			return str;
		}

		private void import_creature()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.MonsterFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string str = File.ReadAllText(openFileDialog.FileName);
				Masterplan.Data.Creature creature = AppImport.ImportCreature(str);
				if (creature != null)
				{
					Guid d = this.fCreature.ID;
					CreatureHelper.CopyFields(creature, this.fCreature);
					this.fCreature.ID = d;
					this.find_sample_powers();
					this.update_view();
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CreatureBuilderForm));
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsImport = new ToolStripMenuItem();
			this.OptionsVariant = new ToolStripMenuItem();
			this.OptionsRandom = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.OptionsEntry = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.OptionsPowerBrowser = new ToolStripMenuItem();
			this.LevelDownBtn = new ToolStripButton();
			this.LevelUpBtn = new ToolStripButton();
			this.LevelLbl = new ToolStripLabel();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.AdviceBtn = new ToolStripButton();
			this.PowersBtn = new ToolStripButton();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.Pages = new TabControl();
			this.StatBlockPage = new TabPage();
			this.StatBlockBrowser = new WebBrowser();
			this.PicturePage = new TabPage();
			this.PictureToolbar = new ToolStrip();
			this.PictureBrowseBtn = new ToolStripButton();
			this.PicturePasteBtn = new ToolStripButton();
			this.PictureClearBtn = new ToolStripButton();
			this.PortraitBox = new PictureBox();
			this.EntryPage = new TabPage();
			this.EntryBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			this.Pages.SuspendLayout();
			this.StatBlockPage.SuspendLayout();
			this.PicturePage.SuspendLayout();
			this.PictureToolbar.SuspendLayout();
			((ISupportInitialize)this.PortraitBox).BeginInit();
			this.EntryPage.SuspendLayout();
			base.SuspendLayout();
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] fileMenu = new ToolStripItem[] { this.FileMenu, this.OptionsMenu, this.LevelDownBtn, this.LevelUpBtn, this.LevelLbl, this.toolStripSeparator3, this.AdviceBtn, this.PowersBtn };
			items.AddRange(fileMenu);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(684, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.FileExport });
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new System.Drawing.Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.OptionsMenu.DropDownItems;
			ToolStripItem[] optionsImport = new ToolStripItem[] { this.OptionsImport, this.OptionsVariant, this.OptionsRandom, this.toolStripSeparator1, this.OptionsEntry, this.toolStripSeparator2, this.OptionsPowerBrowser };
			dropDownItems.AddRange(optionsImport);
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new System.Drawing.Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsImport.Name = "OptionsImport";
			this.OptionsImport.Size = new System.Drawing.Size(242, 22);
			this.OptionsImport.Text = "Import from Adventure Tools...";
			this.OptionsImport.Click += new EventHandler(this.OptionsImport_Click);
			this.OptionsVariant.Name = "OptionsVariant";
			this.OptionsVariant.Size = new System.Drawing.Size(242, 22);
			this.OptionsVariant.Text = "Copy an Existing Creature...";
			this.OptionsVariant.Click += new EventHandler(this.OptionsVariant_Click);
			this.OptionsRandom.Name = "OptionsRandom";
			this.OptionsRandom.Size = new System.Drawing.Size(242, 22);
			this.OptionsRandom.Text = "Generate a Random Creature...";
			this.OptionsRandom.Click += new EventHandler(this.OptionsRandom_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
			this.OptionsEntry.Name = "OptionsEntry";
			this.OptionsEntry.Size = new System.Drawing.Size(242, 22);
			this.OptionsEntry.Text = "Create / Edit Encyclopedia Entry";
			this.OptionsEntry.Click += new EventHandler(this.OptionsEntry_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
			this.OptionsPowerBrowser.Name = "OptionsPowerBrowser";
			this.OptionsPowerBrowser.Size = new System.Drawing.Size(242, 22);
			this.OptionsPowerBrowser.Text = "Power Browser";
			this.OptionsPowerBrowser.Click += new EventHandler(this.OptionsPowerBrowser_Click);
			this.LevelDownBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelDownBtn.Image = (Image)componentResourceManager.GetObject("LevelDownBtn.Image");
			this.LevelDownBtn.ImageTransparentColor = Color.Magenta;
			this.LevelDownBtn.Name = "LevelDownBtn";
			this.LevelDownBtn.Size = new System.Drawing.Size(23, 22);
			this.LevelDownBtn.Text = "-";
			this.LevelDownBtn.ToolTipText = "Level Down";
			this.LevelDownBtn.Click += new EventHandler(this.LevelDownBtn_Click);
			this.LevelUpBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelUpBtn.Image = (Image)componentResourceManager.GetObject("LevelUpBtn.Image");
			this.LevelUpBtn.ImageTransparentColor = Color.Magenta;
			this.LevelUpBtn.Name = "LevelUpBtn";
			this.LevelUpBtn.Size = new System.Drawing.Size(23, 22);
			this.LevelUpBtn.Text = "+";
			this.LevelUpBtn.ToolTipText = "Level Up";
			this.LevelUpBtn.Click += new EventHandler(this.LevelUpBtn_Click);
			this.LevelLbl.Alignment = ToolStripItemAlignment.Right;
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new System.Drawing.Size(37, 22);
			this.LevelLbl.Text = "Level:";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.AdviceBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AdviceBtn.Image = (Image)componentResourceManager.GetObject("AdviceBtn.Image");
			this.AdviceBtn.ImageTransparentColor = Color.Magenta;
			this.AdviceBtn.Name = "AdviceBtn";
			this.AdviceBtn.Size = new System.Drawing.Size(47, 22);
			this.AdviceBtn.Text = "Advice";
			this.AdviceBtn.Click += new EventHandler(this.AdviceBtn_Click);
			this.PowersBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowersBtn.Image = (Image)componentResourceManager.GetObject("PowersBtn.Image");
			this.PowersBtn.ImageTransparentColor = Color.Magenta;
			this.PowersBtn.Name = "PowersBtn";
			this.PowersBtn.Size = new System.Drawing.Size(49, 22);
			this.PowersBtn.Text = "Powers";
			this.PowersBtn.Click += new EventHandler(this.PowersBtn_Click);
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new System.Drawing.Size(684, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(597, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(516, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.Pages.Controls.Add(this.StatBlockPage);
			this.Pages.Controls.Add(this.PicturePage);
			this.Pages.Controls.Add(this.EntryPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 25);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(684, 418);
			this.Pages.TabIndex = 3;
			this.StatBlockPage.Controls.Add(this.StatBlockBrowser);
			this.StatBlockPage.Location = new Point(4, 22);
			this.StatBlockPage.Name = "StatBlockPage";
			this.StatBlockPage.Padding = new System.Windows.Forms.Padding(3);
			this.StatBlockPage.Size = new System.Drawing.Size(676, 392);
			this.StatBlockPage.TabIndex = 0;
			this.StatBlockPage.Text = "Stat Block";
			this.StatBlockPage.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(3, 3);
			this.StatBlockBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new System.Drawing.Size(670, 386);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.PicturePage.Controls.Add(this.PictureToolbar);
			this.PicturePage.Controls.Add(this.PortraitBox);
			this.PicturePage.Location = new Point(4, 22);
			this.PicturePage.Name = "PicturePage";
			this.PicturePage.Padding = new System.Windows.Forms.Padding(3);
			this.PicturePage.Size = new System.Drawing.Size(676, 392);
			this.PicturePage.TabIndex = 1;
			this.PicturePage.Text = "Picture";
			this.PicturePage.UseVisualStyleBackColor = true;
			ToolStripItemCollection toolStripItemCollections = this.PictureToolbar.Items;
			ToolStripItem[] pictureBrowseBtn = new ToolStripItem[] { this.PictureBrowseBtn, this.PicturePasteBtn, this.PictureClearBtn };
			toolStripItemCollections.AddRange(pictureBrowseBtn);
			this.PictureToolbar.Location = new Point(3, 3);
			this.PictureToolbar.Name = "PictureToolbar";
			this.PictureToolbar.Size = new System.Drawing.Size(670, 25);
			this.PictureToolbar.TabIndex = 6;
			this.PictureToolbar.Text = "toolStrip1";
			this.PictureBrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PictureBrowseBtn.Image = (Image)componentResourceManager.GetObject("PictureBrowseBtn.Image");
			this.PictureBrowseBtn.ImageTransparentColor = Color.Magenta;
			this.PictureBrowseBtn.Name = "PictureBrowseBtn";
			this.PictureBrowseBtn.Size = new System.Drawing.Size(49, 22);
			this.PictureBrowseBtn.Text = "Browse";
			this.PictureBrowseBtn.Click += new EventHandler(this.PictureBrowseBtn_Click);
			this.PicturePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PicturePasteBtn.Image = (Image)componentResourceManager.GetObject("PicturePasteBtn.Image");
			this.PicturePasteBtn.ImageTransparentColor = Color.Magenta;
			this.PicturePasteBtn.Name = "PicturePasteBtn";
			this.PicturePasteBtn.Size = new System.Drawing.Size(39, 22);
			this.PicturePasteBtn.Text = "Paste";
			this.PicturePasteBtn.Click += new EventHandler(this.PicturePasteBtn_Click);
			this.PictureClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PictureClearBtn.Image = (Image)componentResourceManager.GetObject("PictureClearBtn.Image");
			this.PictureClearBtn.ImageTransparentColor = Color.Magenta;
			this.PictureClearBtn.Name = "PictureClearBtn";
			this.PictureClearBtn.Size = new System.Drawing.Size(38, 22);
			this.PictureClearBtn.Text = "Clear";
			this.PictureClearBtn.Click += new EventHandler(this.PictureClearBtn_Click);
			this.PortraitBox.BackColor = SystemColors.ControlLight;
			this.PortraitBox.BorderStyle = BorderStyle.FixedSingle;
			this.PortraitBox.Dock = DockStyle.Fill;
			this.PortraitBox.Location = new Point(3, 3);
			this.PortraitBox.Name = "PortraitBox";
			this.PortraitBox.Size = new System.Drawing.Size(670, 386);
			this.PortraitBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PortraitBox.TabIndex = 4;
			this.PortraitBox.TabStop = false;
			this.PortraitBox.DoubleClick += new EventHandler(this.PictureBrowseBtn_Click);
			this.EntryPage.Controls.Add(this.EntryBrowser);
			this.EntryPage.Location = new Point(4, 22);
			this.EntryPage.Name = "EntryPage";
			this.EntryPage.Padding = new System.Windows.Forms.Padding(3);
			this.EntryPage.Size = new System.Drawing.Size(676, 392);
			this.EntryPage.TabIndex = 2;
			this.EntryPage.Text = "Encyclopedia Entry";
			this.EntryPage.UseVisualStyleBackColor = true;
			this.EntryBrowser.AllowWebBrowserDrop = false;
			this.EntryBrowser.Dock = DockStyle.Fill;
			this.EntryBrowser.IsWebBrowserContextMenuEnabled = false;
			this.EntryBrowser.Location = new Point(3, 3);
			this.EntryBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.EntryBrowser.Name = "EntryBrowser";
			this.EntryBrowser.ScriptErrorsSuppressed = true;
			this.EntryBrowser.Size = new System.Drawing.Size(670, 386);
			this.EntryBrowser.TabIndex = 0;
			this.EntryBrowser.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(684, 478);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.StatBlockPage.ResumeLayout(false);
			this.PicturePage.ResumeLayout(false);
			this.PicturePage.PerformLayout();
			this.PictureToolbar.ResumeLayout(false);
			this.PictureToolbar.PerformLayout();
			((ISupportInitialize)this.PortraitBox).EndInit();
			this.EntryPage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LevelDownBtn_Click(object sender, EventArgs e)
		{
			CreatureHelper.AdjustCreatureLevel(this.fCreature, -1);
			this.find_sample_powers();
			this.update_statblock();
		}

		private void LevelUpBtn_Click(object sender, EventArgs e)
		{
			CreatureHelper.AdjustCreatureLevel(this.fCreature, 1);
			this.find_sample_powers();
			this.update_statblock();
		}

		private void OptionsEntry_Click(object sender, EventArgs e)
		{
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.fCreature.ID);
			if (encyclopediaEntry == null)
			{
				if (MessageBox.Show(string.Concat(string.Concat("There is no encyclopedia entry associated with this creature.", Environment.NewLine), "Would you like to create one now?"), "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry()
				{
					Name = this.fCreature.Name,
					AttachmentID = this.fCreature.ID,
					Category = "Creatures"
				};
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int entry = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[entry] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
				this.update_entry();
			}
		}

		private void OptionsImport_Click(object sender, EventArgs e)
		{
			this.import_creature();
		}

		private void OptionsPowerBrowser_Click(object sender, EventArgs e)
		{
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(this.fCreature.Name, this.fCreature.Level, this.fCreature.Role, new PowerCallback(this.add_power));
			powerBrowserForm.ShowDialog();
		}

		private void OptionsRandom_Click(object sender, EventArgs e)
		{
			this.create_random();
		}

		private void OptionsVariant_Click(object sender, EventArgs e)
		{
			this.create_variant();
		}

		private void PictureBrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ImageFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fCreature.Image = Image.FromFile(openFileDialog.FileName);
				Program.SetResolution(this.fCreature.Image);
				this.update_picture();
			}
		}

		private void PictureClearBtn_Click(object sender, EventArgs e)
		{
			this.fCreature.Image = null;
			this.update_picture();
		}

		private void PicturePasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fCreature.Image = Clipboard.GetImage();
				Program.SetResolution(this.fCreature.Image);
				this.update_picture();
			}
		}

		private void PowersBtn_Click(object sender, EventArgs e)
		{
			if (this.fSidebar != CreatureBuilderForm.SidebarType.Powers)
			{
				this.fSidebar = CreatureBuilderForm.SidebarType.Powers;
				this.update_statblock();
			}
		}

		private CreaturePower replace_name(CreaturePower power, string original_name, string original_category, string replacement)
		{
			CreaturePower creaturePower = power.Copy();
			if (!string.IsNullOrEmpty(original_name) && !replacement.Contains(original_name))
			{
				creaturePower.Details = this.replace_text(creaturePower.Details, original_name, replacement);
				creaturePower.Description = this.replace_text(creaturePower.Description, original_name, replacement);
				creaturePower.Condition = this.replace_text(creaturePower.Condition, original_name, replacement);
				creaturePower.Range = this.replace_text(creaturePower.Range, original_name, replacement);
			}
			if (!string.IsNullOrEmpty(original_category) && !replacement.Contains(original_category))
			{
				creaturePower.Details = this.replace_text(creaturePower.Details, original_category, replacement);
				creaturePower.Description = this.replace_text(creaturePower.Description, original_category, replacement);
				creaturePower.Condition = this.replace_text(creaturePower.Condition, original_category, replacement);
				creaturePower.Range = this.replace_text(creaturePower.Range, original_category, replacement);
			}
			return creaturePower;
		}

		private string replace_text(string source, string original, string replacement)
		{
			if (source == null || original == null || replacement == null)
			{
				return source;
			}
			if (replacement.Contains(original))
			{
				return source;
			}
			string str = source;
			while (true)
			{
				int num = str.ToLower().IndexOf(original.ToLower());
				if (num == -1)
				{
					break;
				}
				string str1 = str.Substring(0, num);
				string str2 = str.Substring(num + original.Length);
				str = string.Concat(str1, replacement.ToLower(), str2);
			}
			return str;
		}

		private void splice(List<ICreature> genepool)
		{
			if (this.fCreature is Masterplan.Data.Creature)
			{
				this.fCreature = new Masterplan.Data.Creature();
			}
			if (this.fCreature is CustomCreature)
			{
				this.fCreature = new CustomCreature();
			}
			if (this.fCreature is NPC)
			{
				this.fCreature = new NPC();
			}
			int num = 2147483647;
			int num1 = -2147483648;
			foreach (Masterplan.Data.Creature creature in genepool)
			{
				num = Math.Min(num, creature.Level);
				num1 = Math.Max(num1, creature.Level);
			}
			this.fCreature.Level = Session.Random.Next(num, num1 + 1);
			int num2 = Session.Random.Next(genepool.Count);
			this.fCreature.Role = genepool[num2].Role.Copy();
			int num3 = Session.Random.Next(genepool.Count);
			this.fCreature.Size = genepool[num3].Size;
			int num4 = Session.Random.Next(genepool.Count);
			this.fCreature.Origin = genepool[num4].Origin;
			int num5 = Session.Random.Next(genepool.Count);
			this.fCreature.Type = genepool[num5].Type;
			string str = this.find_common_name(genepool);
			if (str != "")
			{
				this.fCreature.Name = str;
				string str1 = this.find_description(genepool);
				if (str1 == "")
				{
					this.fCreature.Name = string.Concat("New ", this.fCreature.Name);
				}
				else
				{
					ICreature creature1 = this.fCreature;
					creature1.Name = string.Concat(creature1.Name, " ", str1);
				}
			}
			else
			{
				this.fCreature.Name = this.generate_name();
			}
			this.fCreature.Category = "";
			int num6 = Session.Random.Next(genepool.Count);
			this.fCreature.Keywords = genepool[num6].Keywords;
			int num7 = Session.Random.Next(genepool.Count);
			this.fCreature.Strength.Score = genepool[num7].Strength.Score;
			this.fCreature.Constitution.Score = genepool[num7].Constitution.Score;
			this.fCreature.Dexterity.Score = genepool[num7].Dexterity.Score;
			this.fCreature.Intelligence.Score = genepool[num7].Intelligence.Score;
			this.fCreature.Wisdom.Score = genepool[num7].Wisdom.Score;
			this.fCreature.Charisma.Score = genepool[num7].Charisma.Score;
			int num8 = Session.Random.Next(genepool.Count);
			int num9 = Statistics.AC(genepool[num8].Level, genepool[num8].Role);
			int num10 = Statistics.NAD(genepool[num8].Level, genepool[num8].Role);
			int aC = genepool[num8].AC;
			int fortitude = genepool[num8].Fortitude;
			int reflex = genepool[num8].Reflex;
			int will = genepool[num8].Will;
			int num11 = aC - num9;
			int num12 = fortitude - num10;
			int num13 = reflex - num10;
			int num14 = will - num10;
			this.fCreature.AC = Statistics.AC(this.fCreature.Level, this.fCreature.Role) + num11;
			this.fCreature.Fortitude = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num12;
			this.fCreature.Reflex = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num13;
			this.fCreature.Will = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num14;
			if (this.fCreature.Role is ComplexRole)
			{
				List<ICreature> creatures = new List<ICreature>();
				foreach (ICreature creature2 in genepool)
				{
					if (!(creature2.Role is ComplexRole))
					{
						continue;
					}
					creatures.Add(creature2);
				}
				int num15 = Session.Random.Next(creatures.Count);
				int num16 = Statistics.HP(creatures[num15].Level, creatures[num15].Role as ComplexRole, creatures[num15].Constitution.Score);
				int hP = creatures[num15].HP - num16;
				this.fCreature.HP = Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score) + hP;
			}
			int num17 = Session.Random.Next(genepool.Count);
			int num18 = Statistics.Initiative(genepool[num17].Level, genepool[num17].Role);
			int initiative = genepool[num17].Initiative - num18;
			this.fCreature.Initiative = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role) + initiative;
			int num19 = Session.Random.Next(genepool.Count);
			this.fCreature.Languages = genepool[num19].Languages;
			int num20 = Session.Random.Next(genepool.Count);
			this.fCreature.Movement = genepool[num20].Movement;
			int num21 = Session.Random.Next(genepool.Count);
			this.fCreature.Senses = genepool[num21].Senses;
			int num22 = Session.Random.Next(genepool.Count);
			this.fCreature.DamageModifiers.Clear();
			foreach (DamageModifier damageModifier in genepool[num22].DamageModifiers)
			{
				this.fCreature.DamageModifiers.Add(damageModifier.Copy());
			}
			this.fCreature.Resist = genepool[num22].Resist;
			this.fCreature.Vulnerable = genepool[num22].Vulnerable;
			this.fCreature.Immune = genepool[num22].Immune;
			int num23 = Session.Random.Next(genepool.Count);
			this.fCreature.Auras.Clear();
			foreach (Aura aura in genepool[num23].Auras)
			{
				this.fCreature.Auras.Add(aura.Copy());
			}
			int num24 = Session.Random.Next(genepool.Count);
			this.fCreature.Regeneration = genepool[num24].Regeneration;
			Dictionary<CreaturePowerCategory, List<CreaturePower>> creaturePowerCategories = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
			Dictionary<Guid, string> guids = new Dictionary<Guid, string>();
			Dictionary<Guid, string> name = new Dictionary<Guid, string>();
			Array values = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory value in values)
			{
				creaturePowerCategories[value] = new List<CreaturePower>();
			}
			foreach (ICreature creature3 in genepool)
			{
				foreach (CreaturePower creaturePower in creature3.CreaturePowers)
				{
					creaturePowerCategories[creaturePower.Category].Add(creaturePower);
					guids[creaturePower.ID] = creature3.Name;
					name[creaturePower.ID] = creature3.Name;
				}
			}
			this.fCreature.CreaturePowers.Clear();
			foreach (CreaturePowerCategory creaturePowerCategory in values)
			{
				if (creaturePowerCategories[creaturePowerCategory].Count == 0)
				{
					continue;
				}
				int count = creaturePowerCategories[creaturePowerCategory].Count / genepool.Count;
				if (Session.Random.Next(6) == 0)
				{
					count++;
				}
				for (int i = 0; i != count; i++)
				{
					int num25 = Session.Random.Next(creaturePowerCategories[creaturePowerCategory].Count);
					CreaturePower item = creaturePowerCategories[creaturePowerCategory][num25];
					string item1 = guids[item.ID];
					string item2 = name[item.ID];
					item = this.replace_name(item, item1, item2, this.fCreature.Name);
					this.fCreature.CreaturePowers.Add(item);
				}
			}
			int num26 = Session.Random.Next(genepool.Count);
			this.fCreature.Skills = genepool[num26].Skills;
			int num27 = Session.Random.Next(genepool.Count);
			this.fCreature.Alignment = genepool[num27].Alignment;
			this.fCreature.Tactics = "";
			this.fCreature.Equipment = "";
			this.fCreature.Details = "";
			this.fCreature.Image = null;
		}

		private void update_entry()
		{
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.fCreature.ID);
			this.EntryBrowser.DocumentText = HTML.EncyclopediaEntry(encyclopediaEntry, Session.Project.Encyclopedia, DisplaySize.Small, true, false, false, false);
		}

		private void update_picture()
		{
			this.PortraitBox.Image = this.fCreature.Image;
		}

		private void update_statblock()
		{
			EncounterCard encounterCard = new EncounterCard(this.fCreature);
			List<string> head = HTML.GetHead("Creature", "", DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.AddRange(encounterCard.AsText(null, CardMode.Build, true));
			head.Add("</P>");
			head.Add("</TD>");
			switch (this.fSidebar)
			{
				case CreatureBuilderForm.SidebarType.Advice:
				{
					head.Add("<TD class=clear>");
					head.Add("<P class=table>");
					bool count = Session.Creatures.Count >= 2;
					head.Add("<P class=table>");
					head.Add("<TABLE>");
					head.Add("<TR class=heading>");
					head.Add("<TD><B>Create A New Creature</B></TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Import a <A href=build:import>creature file</A> from Adventure Tools");
					head.Add("</TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Create a <A href=build:variant>variant</A> of an existing creature");
					head.Add("</TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Generate a <A href=build:random>random creature</A>");
					head.Add("</TD>");
					head.Add("</TR>");
					if (count)
					{
						head.Add("<TR>");
						head.Add("<TD>");
						head.Add("Generate a <A href=build:hybrid>hybrid creature</A>");
						head.Add("</TD>");
						head.Add("</TR>");
					}
					head.Add("</TABLE>");
					head.Add("</P>");
					bool flag = false;
					ComplexRole role = this.fCreature.Role as ComplexRole;
					if (role != null)
					{
						flag = role.Flag != RoleFlag.Solo;
					}
					head.Add("<P class=table>");
					head.Add("<TABLE>");
					head.Add("<TR class=heading>");
					head.Add("<TD><B>Modify This Creature</B></TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Generate a <A href=build:name>new name</A> for this creature");
					head.Add("</TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Browse for <A href=power:browse>existing powers</A> for this creature");
					head.Add("</TD>");
					head.Add("</TR>");
					if (flag)
					{
						head.Add("<TR>");
						head.Add("<TD>");
						head.Add("Apply a <A href=build:template>template</A> to this creature");
						head.Add("</TD>");
					}
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("See <A href=power:statistics>power statistics</A> for other creatures of this type");
					head.Add("</TD>");
					head.Add("</TR>");
					head.Add("</TABLE>");
					head.Add("</P>");
					head.Add("<P class=table>");
					head.Add("<TABLE>");
					head.Add("<TR class=heading>");
					head.Add("<TD colspan=2><B>Creature Advice</B></TD>");
					head.Add("</TR>");
					int num = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role);
					int num1 = Statistics.AC(this.fCreature.Level, this.fCreature.Role);
					int num2 = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
					bool flag1 = (this.fCreature.Role == null ? false : this.fCreature.Role is Minion);
					if (!flag1)
					{
						if (!flag1)
						{
							Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score);
						}
						head.Add("<TR>");
						head.Add("<TD>Hit Points</TD>");
						head.Add(string.Concat("<TD align=center>+", Statistics.AttackBonus(DefenceType.AC, this.fCreature.Level, this.fCreature.Role), "</TD>"));
						head.Add("</TR>");
					}
					head.Add("<TR>");
					head.Add("<TD>Initiative Bonus</TD>");
					head.Add(string.Concat("<TD align=center>+", num, "</TD>"));
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>Armour Class</TD>");
					head.Add(string.Concat("<TD align=center>+", num1, "</TD>"));
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>Other Defences</TD>");
					head.Add(string.Concat("<TD align=center>+", num2, "</TD>"));
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>Number of Powers</TD>");
					head.Add("<TD align=center>4 - 6</TD>");
					head.Add("</TR>");
					head.Add("</TABLE>");
					head.Add("</P>");
					head.Add("</TD>");
					break;
				}
				case CreatureBuilderForm.SidebarType.Powers:
				{
					head.Add("<TD class=clear>");
					head.Add("<P class=table>");
					if (this.fSamplePowers.Count != 0)
					{
						head.Add("<P text-align=left>");
						head.Add("The following powers might be suitable for this creature.");
						head.Add("Click <A href=power:refresh>here</A> to resample this list, or <A href=power:browse>here</A> to look for other powers.");
						head.Add("</P>");
						head.Add("<P class=table>");
						head.Add("<TABLE>");
						Dictionary<CreaturePowerCategory, List<CreaturePower>> creaturePowerCategories = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
						Array values = Enum.GetValues(typeof(CreaturePowerCategory));
						foreach (CreaturePowerCategory value in values)
						{
							creaturePowerCategories[value] = new List<CreaturePower>();
						}
						foreach (CreaturePower fSamplePower in this.fSamplePowers)
						{
							creaturePowerCategories[fSamplePower.Category].Add(fSamplePower);
						}
						foreach (CreaturePowerCategory creaturePowerCategory in values)
						{
							if (creaturePowerCategories[creaturePowerCategory].Count == 0)
							{
								continue;
							}
							string str = "";
							switch (creaturePowerCategory)
							{
								case CreaturePowerCategory.Trait:
								{
									str = "Traits";
									break;
								}
								case CreaturePowerCategory.Standard:
								case CreaturePowerCategory.Move:
								case CreaturePowerCategory.Minor:
								case CreaturePowerCategory.Free:
								{
									str = string.Concat(creaturePowerCategory, " Actions");
									break;
								}
								case CreaturePowerCategory.Triggered:
								{
									str = "Triggered Actions";
									break;
								}
								case CreaturePowerCategory.Other:
								{
									str = "Other Actions";
									break;
								}
							}
							head.Add("<TR class=creature>");
							head.Add("<TD colspan=3>");
							head.Add(string.Concat("<B>", str, "</B>"));
							head.Add("</TD>");
							head.Add("</TR>");
							foreach (CreaturePower item in creaturePowerCategories[creaturePowerCategory])
							{
								head.AddRange(item.AsHTML(null, CardMode.View, false));
								head.Add("<TR>");
								head.Add("<TD colspan=3 align=center>");
								object[] d = new object[] { "<A href=samplepower:", item.ID, ">copy this power into ", this.fCreature.Name, "</A>" };
								head.Add(string.Concat(d));
								head.Add("</TD>");
								head.Add("</TR>");
							}
						}
						head.Add("</TABLE>");
						head.Add("</P>");
					}
					head.Add("</TD>");
					break;
				}
			}
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void update_view()
		{
			this.update_statblock();
			this.update_picture();
		}

		private enum SidebarType
		{
			Advice,
			Powers
		}
	}
}