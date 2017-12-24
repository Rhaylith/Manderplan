using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CustomCreature : ICreature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private CreatureOrigin fOrigin = CreatureOrigin.Natural;

		private CreatureType fType = CreatureType.MagicalBeast;

		private string fKeywords = "";

		private int fLevel = 1;

		private IRole fRole = new ComplexRole();

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

		public int AC
		{
			get
			{
				int num = Statistics.AC(this.fLevel, this.fRole);
				return num + this.fACModifier;
			}
			set
			{
				int num = Statistics.AC(this.fLevel, this.fRole);
				this.fACModifier = value - num;
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
				return "";
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
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fStrength.Score, this.fConstitution.Score);
				return num + Ability.GetModifier(num1) + this.fFortitudeModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fStrength.Score, this.fConstitution.Score);
				int modifier = Ability.GetModifier(num1);
				this.fFortitudeModifier = value - num - modifier;
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
				if (this.fRole is Minion)
				{
					return 1;
				}
				int num = Statistics.HP(this.fLevel, this.fRole as ComplexRole, this.fConstitution.Score);
				return num + this.fHPModifier;
			}
			set
			{
				if (this.fRole is Minion)
				{
					return;
				}
				int num = Statistics.HP(this.fLevel, this.fRole as ComplexRole, this.fConstitution.Score);
				this.fHPModifier = value - num;
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
				object[] objArray = new object[] { "Level ", this.fLevel, " ", this.fRole };
				return string.Concat(objArray);
			}
		}

		public int Initiative
		{
			get
			{
				int num = Statistics.Initiative(this.fLevel, this.fRole);
				return num + this.fDexterity.Modifier + this.fInitiativeModifier;
			}
			set
			{
				int num = Statistics.Initiative(this.fLevel, this.fRole);
				this.fInitiativeModifier = value - num - this.fDexterity.Modifier;
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
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fDexterity.Score, this.fIntelligence.Score);
				return num + Ability.GetModifier(num1) + this.fReflexModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fDexterity.Score, this.fIntelligence.Score);
				int modifier = Ability.GetModifier(num1);
				this.fReflexModifier = value - num - modifier;
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
				return this.fRole;
			}
			set
			{
				this.fRole = value;
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
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fWisdom.Score, this.fCharisma.Score);
				return num + Ability.GetModifier(num1) + this.fWillModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int num1 = Math.Max(this.fWisdom.Score, this.fCharisma.Score);
				int modifier = Ability.GetModifier(num1);
				this.fWillModifier = value - num - modifier;
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

		public CustomCreature()
		{
		}

		public CustomCreature(ICreature c)
		{
			CreatureHelper.CopyFields(c, this);
		}

		public CustomCreature Copy()
		{
			Masterplan.Data.Regeneration regeneration;
			CustomCreature customCreature = new CustomCreature()
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails,
				Size = this.fSize,
				Origin = this.fOrigin,
				Type = this.fType,
				Keywords = this.fKeywords,
				Level = this.fLevel,
				Role = this.fRole.Copy(),
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
			CustomCreature customCreature1 = customCreature;
			if (this.fRegeneration != null)
			{
				regeneration = this.fRegeneration.Copy();
			}
			else
			{
				regeneration = null;
			}
			customCreature1.Regeneration = regeneration;
			foreach (Aura fAura in this.fAuras)
			{
				customCreature.Auras.Add(fAura.Copy());
			}
			foreach (CreaturePower fCreaturePower in this.fCreaturePowers)
			{
				customCreature.CreaturePowers.Add(fCreaturePower.Copy());
			}
			foreach (DamageModifier fDamageModifier in this.fDamageModifiers)
			{
				customCreature.DamageModifiers.Add(fDamageModifier.Copy());
			}
			customCreature.Resist = this.fResist;
			customCreature.Vulnerable = this.fVulnerable;
			customCreature.Immune = this.fImmune;
			customCreature.Tactics = this.fTactics;
			customCreature.Image = this.fImage;
			return customCreature;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}