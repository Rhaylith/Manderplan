using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Creature : ICreature, IComparable<Creature>
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

		private string fMovement = "6";

		private string fAlignment = "";

		private string fLanguages = "";

		private string fSkills = "";

		private string fEquipment = "";

		private string fCategory = "";

		private Ability fStrength = new Ability();

		private Ability fConstitution = new Ability();

		private Ability fDexterity = new Ability();

		private Ability fIntelligence = new Ability();

		private Ability fWisdom = new Ability();

		private Ability fCharisma = new Ability();

		private int fHP;

		private int fInitiative;

		private int fAC = 10;

		private int fFortitude = 10;

		private int fReflex = 10;

		private int fWill = 10;

		private Masterplan.Data.Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifier> fDamageModifiers = new List<DamageModifier>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

		private System.Drawing.Image fImage;

		private string fURL = "";

		public int AC
		{
			get
			{
				return this.fAC;
			}
			set
			{
				this.fAC = value;
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
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
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
				return this.fFortitude;
			}
			set
			{
				this.fFortitude = value;
			}
		}

		public int HP
		{
			get
			{
				return this.fHP;
			}
			set
			{
				this.fHP = value;
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
				return this.fInitiative;
			}
			set
			{
				this.fInitiative = value;
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
				return this.fReflex;
			}
			set
			{
				this.fReflex = value;
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

		public string URL
		{
			get
			{
				return this.fURL;
			}
			set
			{
				this.fURL = value;
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
				return this.fWill;
			}
			set
			{
				this.fWill = value;
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

		public Creature()
		{
		}

		public Creature(ICreature c)
		{
			CreatureHelper.CopyFields(c, this);
		}

		public int CompareTo(Creature rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public Creature Copy()
		{
			Masterplan.Data.Regeneration regeneration;
			Creature creature = new Creature()
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
				Category = this.fCategory,
				Strength = this.fStrength.Copy(),
				Constitution = this.fConstitution.Copy(),
				Dexterity = this.fDexterity.Copy(),
				Intelligence = this.fIntelligence.Copy(),
				Wisdom = this.fWisdom.Copy(),
				Charisma = this.fCharisma.Copy(),
				HP = this.fHP,
				Initiative = this.fInitiative,
				AC = this.fAC,
				Fortitude = this.fFortitude,
				Reflex = this.fReflex,
				Will = this.fWill
			};
			Creature creature1 = creature;
			if (this.fRegeneration != null)
			{
				regeneration = this.fRegeneration.Copy();
			}
			else
			{
				regeneration = null;
			}
			creature1.Regeneration = regeneration;
			foreach (Aura fAura in this.fAuras)
			{
				creature.Auras.Add(fAura.Copy());
			}
			foreach (CreaturePower fCreaturePower in this.fCreaturePowers)
			{
				creature.CreaturePowers.Add(fCreaturePower.Copy());
			}
			foreach (DamageModifier fDamageModifier in this.fDamageModifiers)
			{
				creature.DamageModifiers.Add(fDamageModifier.Copy());
			}
			creature.Resist = this.fResist;
			creature.Vulnerable = this.fVulnerable;
			creature.Immune = this.fImmune;
			creature.Tactics = this.fTactics;
			creature.Image = this.fImage;
			creature.URL = this.fURL;
			return creature;
		}

		public static int GetSize(CreatureSize size)
		{
			switch (size)
			{
				case CreatureSize.Large:
				{
					return 2;
				}
				case CreatureSize.Huge:
				{
					return 3;
				}
				case CreatureSize.Gargantuan:
				{
					return 4;
				}
			}
			return 1;
		}

		public static int GetSpeed(CreatureSize size)
		{
			switch (size)
			{
				case CreatureSize.Tiny:
				case CreatureSize.Small:
				{
					return 4;
				}
				case CreatureSize.Medium:
				{
					return 6;
				}
				case CreatureSize.Large:
				{
					return 6;
				}
				case CreatureSize.Huge:
				{
					return 8;
				}
				case CreatureSize.Gargantuan:
				{
					return 10;
				}
			}
			return 6;
		}

		public override string ToString()
		{
			return string.Concat(this.fName, " (", this.Info, ")");
		}
	}
}