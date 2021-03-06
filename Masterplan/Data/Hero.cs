using Masterplan;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Hero : IToken, IComparable<Hero>
	{
		private Guid fID = Guid.NewGuid();

		private string fKey = "";

		private string fName = "";

		private string fPlayer = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private string fRace = "";

		private int fLevel = Session.Project.Party.Level;

		private string fClass = "";

		private string fParagonPath = "";

		private string fEpicDestiny = "";

		private string fPowerSource = "";

		private HeroRoleType fRole;

		private Masterplan.Data.CombatData fCombatData = new Masterplan.Data.CombatData();

		private int fHP;

		private int fAC = 10;

		private int fFortitude = 10;

		private int fReflex = 10;

		private int fWill = 10;

		private int fInitBonus;

		private int fPassivePerception = 10;

		private int fPassiveInsight = 10;

		private string fLanguages = "";

		private List<OngoingCondition> fEffects = new List<OngoingCondition>();

		private List<CustomToken> fTokens = new List<CustomToken>();

		private Image fPortrait;

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

		public string Class
		{
			get
			{
				return this.fClass;
			}
			set
			{
				this.fClass = value;
			}
		}

		public Masterplan.Data.CombatData CombatData
		{
			get
			{
				return this.fCombatData;
			}
			set
			{
				this.fCombatData = value;
				this.fCombatData.ID = this.fID;
				this.fCombatData.DisplayName = this.fName;
			}
		}

		public List<OngoingCondition> Effects
		{
			get
			{
				return this.fEffects;
			}
			set
			{
				this.fEffects = value;
			}
		}

		public string EpicDestiny
		{
			get
			{
				return this.fEpicDestiny;
			}
			set
			{
				this.fEpicDestiny = value;
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
				if (this.fCombatData != null)
				{
					this.fCombatData.ID = value;
				}
			}
		}

		public string Info
		{
			get
			{
				string str = string.Concat("Level ", this.fLevel);
				if (this.fRace != "")
				{
					if (str != "")
					{
						str = string.Concat(str, " ");
					}
					str = string.Concat(str, this.fRace);
				}
				if (this.fClass != "")
				{
					if (str != "")
					{
						str = string.Concat(str, " ");
					}
					str = string.Concat(str, this.fClass);
				}
				if (this.fParagonPath != "")
				{
					if (str != "")
					{
						str = string.Concat(str, " / ");
					}
					str = string.Concat(str, this.fParagonPath);
				}
				if (this.fEpicDestiny != "")
				{
					if (str != "")
					{
						str = string.Concat(str, " / ");
					}
					str = string.Concat(str, this.fEpicDestiny);
				}
				return str;
			}
		}

		public int InitBonus
		{
			get
			{
				return this.fInitBonus;
			}
			set
			{
				this.fInitBonus = value;
			}
		}

		public string Key
		{
			get
			{
				return this.fKey;
			}
			set
			{
				this.fKey = value;
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

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
				if (this.fCombatData != null)
				{
					this.fCombatData.DisplayName = value;
				}
			}
		}

		public string ParagonPath
		{
			get
			{
				return this.fParagonPath;
			}
			set
			{
				this.fParagonPath = value;
			}
		}

		public int PassiveInsight
		{
			get
			{
				return this.fPassiveInsight;
			}
			set
			{
				this.fPassiveInsight = value;
			}
		}

		public int PassivePerception
		{
			get
			{
				return this.fPassivePerception;
			}
			set
			{
				this.fPassivePerception = value;
			}
		}

		public string Player
		{
			get
			{
				return this.fPlayer;
			}
			set
			{
				this.fPlayer = value;
			}
		}

		public Image Portrait
		{
			get
			{
				return this.fPortrait;
			}
			set
			{
				this.fPortrait = value;
			}
		}

		public string PowerSource
		{
			get
			{
				return this.fPowerSource;
			}
			set
			{
				this.fPowerSource = value;
			}
		}

		public string Race
		{
			get
			{
				return this.fRace;
			}
			set
			{
				this.fRace = value;
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

		public HeroRoleType Role
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

		public List<CustomToken> Tokens
		{
			get
			{
				return this.fTokens;
			}
			set
			{
				this.fTokens = value;
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

		public Hero()
		{
		}

		public int CompareTo(Hero rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public Hero Copy()
		{
			Hero hero = new Hero()
			{
				ID = this.fID,
				Key = this.fKey,
				Name = this.fName,
				Player = this.fPlayer,
				Size = this.fSize,
				Race = this.fRace,
				Level = this.fLevel,
				Class = this.fClass,
				ParagonPath = this.fParagonPath,
				EpicDestiny = this.fEpicDestiny,
				PowerSource = this.fPowerSource,
				Role = this.fRole,
				CombatData = this.fCombatData.Copy(),
				HP = this.fHP,
				AC = this.fAC,
				Fortitude = this.fFortitude,
				Reflex = this.fReflex,
				Will = this.fWill,
				InitBonus = this.fInitBonus,
				PassivePerception = this.fPassivePerception,
				PassiveInsight = this.fPassiveInsight,
				Languages = this.fLanguages,
				Portrait = this.fPortrait
			};
			foreach (OngoingCondition fEffect in this.fEffects)
			{
				hero.Effects.Add(fEffect.Copy());
			}
			foreach (CustomToken fToken in this.fTokens)
			{
				hero.Tokens.Add(fToken.Copy());
			}
			return hero;
		}

		public CreatureState GetState(int damage)
		{
			if (this.fHP != 0)
			{
				int num = this.fHP - damage;
				int num1 = this.fHP / 2;
				if (num <= 0)
				{
					return CreatureState.Defeated;
				}
				if (num <= num1)
				{
					return CreatureState.Bloodied;
				}
			}
			return CreatureState.Active;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}