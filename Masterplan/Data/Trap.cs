using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Trap : IComparable<Trap>
	{
		private Guid fID = Guid.NewGuid();

		private TrapType fType;

		private string fName = "";

		private int fLevel = 1;

		private IRole fRole = new ComplexRole(RoleType.Blaster);

		private string fReadAloud = "";

		private string fDescription = "";

		private string fDetails = "";

		private List<TrapSkillData> fSkills = new List<TrapSkillData>();

		private int fInitiative = -2147483648;

		private string fTrigger = "";

		private TrapAttack fAttack = new TrapAttack();

		private List<TrapAttack> fAttacks = new List<TrapAttack>();

		private List<string> fCountermeasures = new List<string>();

		private string fURL = "";

		public TrapAttack Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public List<TrapAttack> Attacks
		{
			get
			{
				return this.fAttacks;
			}
			set
			{
				this.fAttacks = value;
			}
		}

		public List<string> Countermeasures
		{
			get
			{
				return this.fCountermeasures;
			}
			set
			{
				this.fCountermeasures = value;
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
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

		public string Info
		{
			get
			{
				object[] lower = new object[] { "Level ", this.fLevel, " ", this.fRole, " ", this.fType.ToString().ToLower() };
				return string.Concat(lower);
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
			}
		}

		public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
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

		public List<TrapSkillData> Skills
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

		public string Trigger
		{
			get
			{
				return this.fTrigger;
			}
			set
			{
				this.fTrigger = value;
			}
		}

		public TrapType Type
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

		public int XP
		{
			get
			{
				int creatureXP = 0;
				if (!(this.fRole is Minion))
				{
					ComplexRole complexRole = this.fRole as ComplexRole;
					creatureXP = Experience.GetCreatureXP(this.fLevel);
					switch (complexRole.Flag)
					{
						case RoleFlag.Elite:
						{
							creatureXP *= 2;
							break;
						}
						case RoleFlag.Solo:
						{
							creatureXP *= 5;
							break;
						}
					}
				}
				else
				{
					float single = (float)Experience.GetCreatureXP(this.fLevel) / 4f;
					creatureXP = (int)Math.Round((double)single, MidpointRounding.AwayFromZero);
				}
				if (Session.Project != null)
				{
					creatureXP = (int)((double)creatureXP * Session.Project.CampaignSettings.XP);
				}
				return creatureXP;
			}
		}

		public Trap()
		{
		}

		public void AdjustLevel(int delta)
		{
			this.fLevel += delta;
			this.fLevel = Math.Max(1, this.fLevel);
			if (this.fInitiative != -2147483648)
			{
				Trap initiative = this;
				initiative.Initiative = initiative.Initiative + delta;
				this.fInitiative = Math.Max(1, this.fInitiative);
			}
			foreach (TrapAttack fAttack in this.fAttacks)
			{
				if (fAttack.Attack != null)
				{
					PowerAttack attack = fAttack.Attack;
					attack.Bonus = attack.Bonus + delta;
					fAttack.Attack.Bonus = Math.Max(1, fAttack.Attack.Bonus);
				}
				string str = AI.ExtractDamage(fAttack.OnHit);
				if (str != "")
				{
					DiceExpression diceExpression = DiceExpression.Parse(str);
					if (diceExpression != null)
					{
						DiceExpression diceExpression1 = diceExpression.Adjust(delta);
						if (diceExpression1 != null && diceExpression.ToString() != diceExpression1.ToString())
						{
							fAttack.OnHit = fAttack.OnHit.Replace(str, string.Concat(diceExpression1, " damage"));
						}
					}
				}
				string str1 = AI.ExtractDamage(fAttack.OnMiss);
				if (str1 != "")
				{
					DiceExpression diceExpression2 = DiceExpression.Parse(str1);
					if (diceExpression2 != null)
					{
						DiceExpression diceExpression3 = diceExpression2.Adjust(delta);
						if (diceExpression3 != null && diceExpression2.ToString() != diceExpression3.ToString())
						{
							fAttack.OnMiss = fAttack.OnMiss.Replace(str1, string.Concat(diceExpression3, " damage"));
						}
					}
				}
				string str2 = AI.ExtractDamage(fAttack.Effect);
				if (str2 == "")
				{
					continue;
				}
				DiceExpression diceExpression4 = DiceExpression.Parse(str2);
				if (diceExpression4 == null)
				{
					continue;
				}
				DiceExpression diceExpression5 = diceExpression4.Adjust(delta);
				if (diceExpression5 == null || !(diceExpression4.ToString() != diceExpression5.ToString()))
				{
					continue;
				}
				fAttack.Effect = fAttack.Effect.Replace(str2, string.Concat(diceExpression5, " damage"));
			}
			foreach (TrapSkillData fSkill in this.fSkills)
			{
				TrapSkillData dC = fSkill;
				dC.DC = dC.DC + delta;
			}
		}

		public int CompareTo(Trap rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public Trap Copy()
		{
			TrapAttack trapAttack;
			Trap trap = new Trap()
			{
				ID = this.fID,
				Type = this.fType,
				Name = this.fName,
				Level = this.fLevel,
				Role = this.fRole.Copy(),
				ReadAloud = this.fReadAloud,
				Description = this.fDescription,
				Details = this.fDetails
			};
			foreach (TrapSkillData fSkill in this.fSkills)
			{
				trap.Skills.Add(fSkill.Copy());
			}
			trap.Initiative = this.fInitiative;
			trap.Trigger = this.fTrigger;
			Trap trap1 = trap;
			if (this.fAttack != null)
			{
				trapAttack = this.fAttack.Copy();
			}
			else
			{
				trapAttack = null;
			}
			trap1.Attack = trapAttack;
			foreach (TrapAttack fAttack in this.fAttacks)
			{
				trap.Attacks.Add(fAttack.Copy());
			}
			foreach (string fCountermeasure in this.fCountermeasures)
			{
				trap.Countermeasures.Add(fCountermeasure);
			}
			trap.URL = this.fURL;
			return trap;
		}

		public TrapAttack FindAttack(Guid id)
		{
			TrapAttack trapAttack;
			List<TrapAttack>.Enumerator enumerator = this.fAttacks.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TrapAttack current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					trapAttack = current;
					return trapAttack;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return trapAttack;
		}

		public TrapSkillData FindSkill(string skillname)
		{
			TrapSkillData trapSkillDatum;
			List<TrapSkillData>.Enumerator enumerator = this.fSkills.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TrapSkillData current = enumerator.Current;
					if (current.SkillName != skillname)
					{
						continue;
					}
					trapSkillDatum = current;
					return trapSkillDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return trapSkillDatum;
		}

		public TrapSkillData FindSkill(Guid id)
		{
			TrapSkillData trapSkillDatum;
			List<TrapSkillData>.Enumerator enumerator = this.fSkills.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TrapSkillData current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					trapSkillDatum = current;
					return trapSkillDatum;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return trapSkillDatum;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}