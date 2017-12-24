using Masterplan;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class NPC : ICreature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private CreatureOrigin fOrigin = CreatureOrigin.Natural;

		private CreatureType fType = CreatureType.MagicalBeast;

		private string fKeywords = "";

		private int fLevel = 1;

		private Guid fTemplateID = Guid.Empty;

		private string fSenses = "";

		private string fMovement = "";

		private string fAlignment = "";

		private string fLanguages = "";

		private string fSkills = "";

		private string fEquipment = "";

		private Ability fStrength = new Ability();

		private Ability fConstitution = new Ability();

		private Ability fDexterity = new Ability();

		private Ability fIntelligence = new Ability();

		private Ability fWisdom = new Ability();

		private Ability fCharisma = new Ability();

		private int fInitiativeModifier;

		private int fHPModifier;

		private int fACModifier;

		private int fFortitudeModifier;

		private int fReflexModifier;

		private int fWillModifier;

		private Masterplan.Data.Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifier> fDamageModifiers = new List<DamageModifier>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

		private System.Drawing.Image fImage;

		public int AbilityCost
		{
			get
			{
				int cost = 0;
				int num = 0;
				int score = 10;
				cost += this.fStrength.Cost;
				if (this.fStrength.Score < 10)
				{
					num++;
				}
				if (this.fStrength.Score < score)
				{
					score = this.fStrength.Score;
				}
				cost += this.fConstitution.Cost;
				if (this.fConstitution.Score < 10)
				{
					num++;
				}
				if (this.fConstitution.Score < score)
				{
					score = this.fConstitution.Score;
				}
				cost += this.fDexterity.Cost;
				if (this.fDexterity.Score < 10)
				{
					num++;
				}
				if (this.fDexterity.Score < score)
				{
					score = this.fDexterity.Score;
				}
				cost += this.fIntelligence.Cost;
				if (this.fIntelligence.Score < 10)
				{
					num++;
				}
				if (this.fIntelligence.Score < score)
				{
					score = this.fIntelligence.Score;
				}
				cost += this.fWisdom.Cost;
				if (this.fWisdom.Score < 10)
				{
					num++;
				}
				if (this.fWisdom.Score < score)
				{
					score = this.fWisdom.Score;
				}
				cost += this.fCharisma.Cost;
				if (this.fCharisma.Score < 10)
				{
					num++;
				}
				if (this.fCharisma.Score < score)
				{
					score = this.fCharisma.Score;
				}
				if (num > 1)
				{
					return -1;
				}
				if (score < 8)
				{
					return -1;
				}
				if (score == 9)
				{
					cost++;
				}
				if (score > 9)
				{
					cost += 2;
				}
				return cost;
			}
		}

		public int AC
		{
			get
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.AC;
				}
				return modifier + this.fACModifier;
			}
			set
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.AC;
				}
				this.fACModifier = value - modifier;
			}
		}

		public int ACModifier
		{
			get
			{
				return this.fACModifier;
			}
			set
			{
				this.fACModifier = value;
			}
		}

		public string Alignment
		{
			get
			{
				return this.fAlignment;
			}
			set
			{
				this.fAlignment = value;
			}
		}

		public List<Aura> Auras
		{
			get
			{
				return this.fAuras;
			}
			set
			{
				this.fAuras = value;
			}
		}

		public string Category
		{
			get
			{
				return "NPCs";
			}
			set
			{
			}
		}

		public Ability Charisma
		{
			get
			{
				return this.fCharisma;
			}
			set
			{
				this.fCharisma = value;
			}
		}

		public Ability Constitution
		{
			get
			{
				return this.fConstitution;
			}
			set
			{
				this.fConstitution = value;
			}
		}

		public List<CreaturePower> CreaturePowers
		{
			get
			{
				return this.fCreaturePowers;
			}
			set
			{
				this.fCreaturePowers = value;
			}
		}

		public List<DamageModifier> DamageModifiers
		{
			get
			{
				return this.fDamageModifiers;
			}
			set
			{
				this.fDamageModifiers = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public Ability Dexterity
		{
			get
			{
				return this.fDexterity;
			}
			set
			{
				this.fDexterity = value;
			}
		}

		public string Equipment
		{
			get
			{
				return this.fEquipment;
			}
			set
			{
				this.fEquipment = value;
			}
		}

		public int Fortitude
		{
			get
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fStrength.Score, this.fConstitution.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Fortitude;
				}
				return modifier + this.fFortitudeModifier;
			}
			set
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fStrength.Score, this.fConstitution.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Fortitude;
				}
				this.fFortitudeModifier = value - modifier;
			}
		}

		public int FortitudeModifier
		{
			get
			{
				return this.fFortitudeModifier;
			}
			set
			{
				this.fFortitudeModifier = value;
			}
		}

		public int HP
		{
			get
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate == null)
				{
					return 0;
				}
				int score = this.fConstitution.Score;
				score = score + (this.fLevel + 1) * creatureTemplate.HP;
				return score + this.fHPModifier;
			}
			set
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					int score = this.fConstitution.Score;
					score = score + (this.fLevel + 1) * creatureTemplate.HP;
					this.fHPModifier = value - score;
				}
			}
		}

		public int HPModifier
		{
			get
			{
				return this.fHPModifier;
			}
			set
			{
				this.fHPModifier = value;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public System.Drawing.Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public string Immune
		{
			get
			{
				return this.fImmune;
			}
			set
			{
				this.fImmune = value;
			}
		}

		public string Info
		{
			get
			{
				object[] role = new object[] { "Level ", this.fLevel, " ", this.Role };
				return string.Concat(role);
			}
		}

		public int Initiative
		{
			get
			{
				int modifier = this.fLevel / 2 + this.fDexterity.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Initiative;
				}
				return modifier + this.fInitiativeModifier;
			}
			set
			{
				int modifier = this.fLevel / 2 + this.fDexterity.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Initiative;
				}
				this.fInitiativeModifier = value - modifier;
			}
		}

		public int InitiativeModifier
		{
			get
			{
				return this.fInitiativeModifier;
			}
			set
			{
				this.fInitiativeModifier = value;
			}
		}

		public Ability Intelligence
		{
			get
			{
				return this.fIntelligence;
			}
			set
			{
				this.fIntelligence = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public string Languages
		{
			get
			{
				return this.fLanguages;
			}
			set
			{
				this.fLanguages = value;
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public string Movement
		{
			get
			{
				if (this.fMovement != null && !(this.fMovement == ""))
				{
					return this.fMovement;
				}
				return string.Concat(Creature.GetSpeed(this.fSize), " squares");
			}
			set
			{
				this.fMovement = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public CreatureOrigin Origin
		{
			get
			{
				return this.fOrigin;
			}
			set
			{
				this.fOrigin = value;
			}
		}

		public string Phenotype
		{
			get
			{
				string str = string.Concat(this.fSize, " ", this.fOrigin.ToString().ToLower());
				str = (this.fType != CreatureType.MagicalBeast ? string.Concat(str, " ", this.fType.ToString().ToLower()) : string.Concat(str, " magical beast"));
				if (this.fKeywords != null && this.fKeywords != "")
				{
					str = string.Concat(str, " (", this.fKeywords.ToLower(), ")");
				}
				return str;
			}
		}

		public int Reflex
		{
			get
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Reflex;
				}
				return modifier + this.fReflexModifier;
			}
			set
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Reflex;
				}
				this.fReflexModifier = value - modifier;
			}
		}

		public int ReflexModifier
		{
			get
			{
				return this.fReflexModifier;
			}
			set
			{
				this.fReflexModifier = value;
			}
		}

		public Masterplan.Data.Regeneration Regeneration
		{
			get
			{
				return this.fRegeneration;
			}
			set
			{
				this.fRegeneration = value;
			}
		}

		public string Resist
		{
			get
			{
				return this.fResist;
			}
			set
			{
				this.fResist = value;
			}
		}

		public IRole Role
		{
			get
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate == null)
				{
					return null;
				}
				ComplexRole complexRole = new ComplexRole()
				{
					Type = creatureTemplate.Role,
					Leader = creatureTemplate.Leader
				};
				return complexRole;
			}
			set
			{
			}
		}

		public string Senses
		{
			get
			{
				return this.fSenses;
			}
			set
			{
				this.fSenses = value;
			}
		}

		public CreatureSize Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public string Skills
		{
			get
			{
				return this.fSkills;
			}
			set
			{
				this.fSkills = value;
			}
		}

		public Ability Strength
		{
			get
			{
				return this.fStrength;
			}
			set
			{
				this.fStrength = value;
			}
		}

		public string Tactics
		{
			get
			{
				return this.fTactics;
			}
			set
			{
				this.fTactics = value;
			}
		}

		public Guid TemplateID
		{
			get
			{
				return this.fTemplateID;
			}
			set
			{
				this.fTemplateID = value;
			}
		}

		public CreatureType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public string Vulnerable
		{
			get
			{
				return this.fVulnerable;
			}
			set
			{
				this.fVulnerable = value;
			}
		}

		public int Will
		{
			get
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fWisdom.Score, this.fCharisma.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Will;
				}
				return modifier + this.fWillModifier;
			}
			set
			{
				int modifier = 10 + this.fLevel / 2;
				Ability ability = new Ability()
				{
					Score = Math.Max(this.fWisdom.Score, this.fCharisma.Score)
				};
				modifier += ability.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					modifier += creatureTemplate.Will;
				}
				this.fWillModifier = value - modifier;
			}
		}

		public int WillModifier
		{
			get
			{
				return this.fWillModifier;
			}
			set
			{
				this.fWillModifier = value;
			}
		}

		public Ability Wisdom
		{
			get
			{
				return this.fWisdom;
			}
			set
			{
				this.fWisdom = value;
			}
		}

		public NPC()
		{
		}

		public NPC Copy()
		{
			Masterplan.Data.Regeneration regeneration;
			NPC nPC = new NPC()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails,
				Size = this.fSize,
				Origin = this.fOrigin,
				Type = this.fType,
				Keywords = this.fKeywords,
				Level = this.fLevel,
				TemplateID = this.fTemplateID,
				Senses = this.fSenses,
				Movement = this.fMovement,
				Alignment = this.fAlignment,
				Languages = this.fLanguages,
				Skills = this.fSkills,
				Equipment = this.fEquipment,
				Strength = this.fStrength.Copy(),
				Constitution = this.fConstitution.Copy(),
				Dexterity = this.fDexterity.Copy(),
				Intelligence = this.fIntelligence.Copy(),
				Wisdom = this.fWisdom.Copy(),
				Charisma = this.fCharisma.Copy(),
				InitiativeModifier = this.fInitiativeModifier,
				HPModifier = this.fHPModifier,
				ACModifier = this.fACModifier,
				FortitudeModifier = this.fFortitudeModifier,
				ReflexModifier = this.fReflexModifier,
				WillModifier = this.fWillModifier
			};
			NPC nPC1 = nPC;
			if (this.fRegeneration != null)
			{
				regeneration = this.fRegeneration.Copy();
			}
			else
			{
				regeneration = null;
			}
			nPC1.Regeneration = regeneration;
			foreach (Aura fAura in this.fAuras)
			{
				nPC.Auras.Add(fAura.Copy());
			}
			foreach (CreaturePower fCreaturePower in this.fCreaturePowers)
			{
				nPC.CreaturePowers.Add(fCreaturePower.Copy());
			}
			foreach (DamageModifier fDamageModifier in this.fDamageModifiers)
			{
				nPC.DamageModifiers.Add(fDamageModifier.Copy());
			}
			nPC.Resist = this.fResist;
			nPC.Vulnerable = this.fVulnerable;
			nPC.Immune = this.fImmune;
			nPC.Tactics = this.fTactics;
			nPC.Image = this.fImage;
			return nPC;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}