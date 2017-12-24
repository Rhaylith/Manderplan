using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureTemplate
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private CreatureTemplateType fType;

		private RoleType fRole;

		private bool fLeader;

		private string fSenses = "";

		private string fMovement = "";

		private int fHP;

		private int fInitiative;

		private int fAC;

		private int fFortitude;

		private int fReflex;

		private int fWill;

		private Masterplan.Data.Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifierTemplate> fDamageModifierTemplates = new List<DamageModifierTemplate>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

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

		public List<DamageModifierTemplate> DamageModifierTemplates
		{
			get
			{
				return this.fDamageModifierTemplates;
			}
			set
			{
				this.fDamageModifierTemplates = value;
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
				string str = (this.fType == CreatureTemplateType.Functional ? "Elite " : "");
				return string.Concat(str, this.fRole, (this.fLeader ? " (L)" : ""));
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

		public bool Leader
		{
			get
			{
				return this.fLeader;
			}
			set
			{
				this.fLeader = value;
			}
		}

		public string Movement
		{
			get
			{
				return this.fMovement;
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

		public RoleType Role
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

		public CreatureTemplateType Type
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
				return this.fWill;
			}
			set
			{
				this.fWill = value;
			}
		}

		public CreatureTemplate()
		{
		}

		public CreatureTemplate Copy()
		{
			Masterplan.Data.Regeneration regeneration;
			CreatureTemplate creatureTemplate = new CreatureTemplate()
			{
				Name = this.fName,
				ID = this.fID,
				Type = this.fType,
				Role = this.fRole,
				Leader = this.fLeader,
				Senses = this.fSenses,
				Movement = this.fMovement,
				HP = this.fHP,
				Initiative = this.fInitiative,
				AC = this.fAC,
				Fortitude = this.fFortitude,
				Reflex = this.fReflex,
				Will = this.fWill
			};
			CreatureTemplate creatureTemplate1 = creatureTemplate;
			if (this.fRegeneration != null)
			{
				regeneration = this.fRegeneration.Copy();
			}
			else
			{
				regeneration = null;
			}
			creatureTemplate1.Regeneration = regeneration;
			foreach (Aura fAura in this.fAuras)
			{
				creatureTemplate.Auras.Add(fAura.Copy());
			}
			foreach (CreaturePower fCreaturePower in this.fCreaturePowers)
			{
				creatureTemplate.CreaturePowers.Add(fCreaturePower.Copy());
			}
			foreach (DamageModifierTemplate fDamageModifierTemplate in this.fDamageModifierTemplates)
			{
				creatureTemplate.DamageModifierTemplates.Add(fDamageModifierTemplate.Copy());
			}
			creatureTemplate.Resist = this.fResist;
			creatureTemplate.Vulnerable = this.fVulnerable;
			creatureTemplate.Immune = this.fImmune;
			creatureTemplate.Tactics = this.fTactics;
			return creatureTemplate;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}