using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Encounter : IElement
	{
		private List<EncounterSlot> fSlots = new List<EncounterSlot>();

		private List<Trap> fTraps = new List<Trap>();

		private List<SkillChallenge> fSkillChallenges = new List<SkillChallenge>();

		private List<CustomToken> fCustomTokens = new List<CustomToken>();

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		private List<EncounterNote> fNotes = new List<EncounterNote>();

		private List<EncounterWave> fWaves = new List<EncounterWave>();

		public List<EncounterSlot> AllSlots
		{
			get
			{
				List<EncounterSlot> encounterSlots = new List<EncounterSlot>();
				encounterSlots.AddRange(this.fSlots);
				if (this.fWaves != null)
				{
					foreach (EncounterWave fWafe in this.fWaves)
					{
						encounterSlots.AddRange(fWafe.Slots);
					}
				}
				return encounterSlots;
			}
		}

		public int Count
		{
			get
			{
				int count = 0;
				foreach (EncounterSlot allSlot in this.AllSlots)
				{
					count += allSlot.CombatData.Count;
				}
				return count;
			}
		}

		public List<CustomToken> CustomTokens
		{
			get
			{
				return this.fCustomTokens;
			}
			set
			{
				this.fCustomTokens = value;
			}
		}

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public List<EncounterNote> Notes
		{
			get
			{
				return this.fNotes;
			}
			set
			{
				this.fNotes = value;
			}
		}

		public List<SkillChallenge> SkillChallenges
		{
			get
			{
				return this.fSkillChallenges;
			}
			set
			{
				this.fSkillChallenges = value;
			}
		}

		public List<EncounterSlot> Slots
		{
			get
			{
				return this.fSlots;
			}
			set
			{
				this.fSlots = value;
			}
		}

		public List<Trap> Traps
		{
			get
			{
				return this.fTraps;
			}
			set
			{
				this.fTraps = value;
			}
		}

		public List<EncounterWave> Waves
		{
			get
			{
				return this.fWaves;
			}
			set
			{
				this.fWaves = value;
			}
		}

		public Encounter()
		{
		}

		public bool Contains(Guid combatant_id)
		{
			bool flag;
			List<EncounterSlot>.Enumerator enumerator = this.AllSlots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Card.CreatureID != combatant_id)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public IElement Copy()
		{
			Encounter encounter = new Encounter();
			foreach (EncounterSlot fSlot in this.fSlots)
			{
				encounter.Slots.Add(fSlot.Copy());
			}
			foreach (Trap fTrap in this.fTraps)
			{
				encounter.Traps.Add(fTrap.Copy());
			}
			foreach (SkillChallenge fSkillChallenge in this.fSkillChallenges)
			{
				encounter.SkillChallenges.Add(fSkillChallenge.Copy() as SkillChallenge);
			}
			foreach (CustomToken fCustomToken in this.fCustomTokens)
			{
				encounter.CustomTokens.Add(fCustomToken.Copy());
			}
			encounter.MapID = this.fMapID;
			encounter.MapAreaID = this.fMapAreaID;
			foreach (EncounterNote fNote in this.fNotes)
			{
				encounter.Notes.Add(fNote.Copy());
			}
			foreach (EncounterWave fWafe in this.fWaves)
			{
				encounter.Waves.Add(fWafe.Copy());
			}
			return encounter;
		}

		public CombatData FindCombatData(Guid id)
		{
			CombatData combatDatum;
			List<EncounterSlot>.Enumerator enumerator = this.AllSlots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					List<CombatData>.Enumerator enumerator1 = enumerator.Current.CombatData.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							CombatData current = enumerator1.Current;
							if (current.ID != id)
							{
								continue;
							}
							combatDatum = current;
							return combatDatum;
						}
					}
					finally
					{
						((IDisposable)enumerator1).Dispose();
					}
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return combatDatum;
		}

		public EncounterNote FindNote(string note_title)
		{
			EncounterNote encounterNote;
			List<EncounterNote>.Enumerator enumerator = this.fNotes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterNote current = enumerator.Current;
					if (current.Title != note_title)
					{
						continue;
					}
					encounterNote = current;
					return encounterNote;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterNote;
		}

		public SkillChallenge FindSkillChallenge(Guid challenge_id)
		{
			SkillChallenge skillChallenge;
			List<SkillChallenge>.Enumerator enumerator = this.fSkillChallenges.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SkillChallenge current = enumerator.Current;
					if (current.ID != challenge_id)
					{
						continue;
					}
					skillChallenge = current;
					return skillChallenge;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return skillChallenge;
		}

		public EncounterSlot FindSlot(Guid slot_id)
		{
			EncounterSlot encounterSlot;
			List<EncounterSlot>.Enumerator enumerator = this.AllSlots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterSlot current = enumerator.Current;
					if (current.ID != slot_id)
					{
						continue;
					}
					encounterSlot = current;
					return encounterSlot;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterSlot;
		}

		public EncounterSlot FindSlot(CombatData data)
		{
			EncounterSlot encounterSlot;
			List<EncounterSlot>.Enumerator enumerator = this.AllSlots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterSlot current = enumerator.Current;
					if (!current.CombatData.Contains(data))
					{
						continue;
					}
					encounterSlot = current;
					return encounterSlot;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterSlot;
		}

		public Trap FindTrap(Guid trap_id)
		{
			Trap trap;
			List<Trap>.Enumerator enumerator = this.fTraps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Trap current = enumerator.Current;
					if (current.ID != trap_id)
					{
						continue;
					}
					trap = current;
					return trap;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return trap;
		}

		public EncounterWave FindWave(EncounterSlot slot)
		{
			EncounterWave encounterWave;
			List<EncounterWave>.Enumerator enumerator = this.fWaves.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterWave current = enumerator.Current;
					if (!current.Slots.Contains(slot))
					{
						continue;
					}
					encounterWave = current;
					return encounterWave;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterWave;
		}

		private Difficulty get_diff(int party_level, int party_size)
		{
			if (this.GetXP() <= 0)
			{
				return Difficulty.Trivial;
			}
			int level = this.GetLevel(party_size) - party_level;
			if (level < -2)
			{
				return Difficulty.Trivial;
			}
			if (level == -2 || level == -1)
			{
				return Difficulty.Easy;
			}
			if (level == 0 || level == 1)
			{
				return Difficulty.Moderate;
			}
			if (level != 2 && level != 3 && level != 4)
			{
				return Difficulty.Extreme;
			}
			return Difficulty.Hard;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			List<Difficulty> difficulties = new List<Difficulty>();
			foreach (EncounterSlot allSlot in this.AllSlots)
			{
				if (allSlot.Type != EncounterSlotType.Opponent)
				{
					continue;
				}
				ICreature creature = Session.FindCreature(allSlot.Card.CreatureID, SearchType.Global);
				if (creature == null)
				{
					continue;
				}
				difficulties.Add(AI.GetThreatDifficulty(creature.Level + allSlot.Card.LevelAdjustment, party_level));
			}
			foreach (Trap fTrap in this.fTraps)
			{
				difficulties.Add(AI.GetThreatDifficulty(fTrap.Level, party_level));
			}
			foreach (SkillChallenge fSkillChallenge in this.fSkillChallenges)
			{
				difficulties.Add(fSkillChallenge.GetDifficulty(party_level, party_size));
			}
			difficulties.Add(this.get_diff(party_level, party_size));
			if (difficulties.Contains(Difficulty.Extreme))
			{
				return Difficulty.Extreme;
			}
			if (difficulties.Contains(Difficulty.Hard))
			{
				return Difficulty.Hard;
			}
			if (difficulties.Contains(Difficulty.Moderate))
			{
				return Difficulty.Moderate;
			}
			if (difficulties.Contains(Difficulty.Easy))
			{
				return Difficulty.Easy;
			}
			return Difficulty.Trivial;
		}

		public int GetLevel(int party_size)
		{
			if (party_size == 0)
			{
				return -1;
			}
			int xP = this.GetXP();
			if (Session.Project != null)
			{
				xP = (int)((double)xP / Session.Project.CampaignSettings.XP);
			}
			xP /= party_size;
			int num = 0;
			int num1 = 2147483647;
			for (int i = 0; i <= 40; i++)
			{
				int creatureXP = Experience.GetCreatureXP(i);
				int num2 = Math.Abs(xP - creatureXP);
				if (num2 < num1)
				{
					num = i;
					num1 = num2;
				}
			}
			return num;
		}

		public int GetXP()
		{
			int xP = 0;
			foreach (EncounterSlot allSlot in this.AllSlots)
			{
				xP += allSlot.XP;
			}
			foreach (Trap fTrap in this.fTraps)
			{
				xP += fTrap.XP;
			}
			foreach (SkillChallenge fSkillChallenge in this.fSkillChallenges)
			{
				xP += fSkillChallenge.GetXP();
			}
			xP = Math.Max(0, xP);
			return xP;
		}

		public void SetStandardEncounterNotes()
		{
			this.fNotes.Add(new EncounterNote("Illumination"));
			this.fNotes.Add(new EncounterNote("Features of the Area"));
			this.fNotes.Add(new EncounterNote("Setup"));
			this.fNotes.Add(new EncounterNote("Tactics"));
			this.fNotes.Add(new EncounterNote("Victory Conditions"));
		}

		public string WhoIs(Guid id)
		{
			string displayName;
			foreach (EncounterSlot allSlot in this.AllSlots)
			{
				foreach (CombatData combatDatum in allSlot.CombatData)
				{
					if (combatDatum.ID != id)
					{
						continue;
					}
					displayName = combatDatum.DisplayName;
					return displayName;
				}
			}
			foreach (Hero hero in Session.Project.Heroes)
			{
				if (hero.ID != id)
				{
					continue;
				}
				displayName = hero.Name;
				return displayName;
			}
			List<Trap>.Enumerator enumerator = this.fTraps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Trap current = enumerator.Current;
					if (current.ID != id)
					{
						continue;
					}
					displayName = current.Name;
					return displayName;
				}
				return "";
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return displayName;
		}
	}
}