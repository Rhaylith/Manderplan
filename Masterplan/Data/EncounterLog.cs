using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterLog
	{
		private List<IEncounterLogEntry> fEntries = new List<IEncounterLogEntry>();

		private bool fActive = true;

		public bool Active
		{
			get
			{
				return this.fActive;
			}
			set
			{
				this.fActive = value;
			}
		}

		public List<IEncounterLogEntry> Entries
		{
			get
			{
				return this.fEntries;
			}
			set
			{
				this.fEntries = value;
			}
		}

		public EncounterLog()
		{
		}

		public void AddDamageEntry(Guid id, int damage, List<DamageType> types)
		{
			if (!this.fActive)
			{
				return;
			}
			DamageLogEntry damageLogEntry = new DamageLogEntry()
			{
				CombatantID = id,
				Amount = damage,
				Types = types
			};
			this.fEntries.Add(damageLogEntry);
		}

		public void AddEffectEntry(Guid id, string text, bool added)
		{
			if (!this.fActive)
			{
				return;
			}
			EffectLogEntry effectLogEntry = new EffectLogEntry()
			{
				CombatantID = id,
				EffectText = text,
				Added = added
			};
			this.fEntries.Add(effectLogEntry);
		}

		public void AddMoveEntry(Guid id, int distance, string text)
		{
			if (!this.fActive)
			{
				return;
			}
			MoveLogEntry moveLogEntry = new MoveLogEntry()
			{
				CombatantID = id,
				Distance = distance,
				Details = text
			};
			this.fEntries.Add(moveLogEntry);
		}

		public void AddPauseEntry()
		{
			if (!this.fActive)
			{
				return;
			}
			PauseLogEntry pauseLogEntry = new PauseLogEntry();
			this.fEntries.Add(pauseLogEntry);
		}

		public void AddPowerEntry(Guid id, string text, bool added)
		{
			if (!this.fActive)
			{
				return;
			}
			PowerLogEntry powerLogEntry = new PowerLogEntry()
			{
				CombatantID = id,
				PowerName = text,
				Added = added
			};
			this.fEntries.Add(powerLogEntry);
		}

		public void AddResumeEntry()
		{
			if (!this.fActive)
			{
				return;
			}
			ResumeLogEntry resumeLogEntry = new ResumeLogEntry();
			this.fEntries.Add(resumeLogEntry);
		}

		public void AddSkillChallengeEntry(Guid id, bool success)
		{
			if (!this.fActive)
			{
				return;
			}
			SkillChallengeLogEntry skillChallengeLogEntry = new SkillChallengeLogEntry()
			{
				CombatantID = id,
				Success = success
			};
			this.fEntries.Add(skillChallengeLogEntry);
		}

		public void AddSkillEntry(Guid id, string text)
		{
			if (!this.fActive)
			{
				return;
			}
			SkillLogEntry skillLogEntry = new SkillLogEntry()
			{
				CombatantID = id,
				SkillName = text
			};
			this.fEntries.Add(skillLogEntry);
		}

		public void AddStartRoundEntry(int round)
		{
			if (!this.fActive)
			{
				return;
			}
			StartRoundLogEntry startRoundLogEntry = new StartRoundLogEntry()
			{
				Round = round
			};
			this.fEntries.Add(startRoundLogEntry);
		}

		public void AddStartTurnEntry(Guid id)
		{
			if (!this.fActive)
			{
				return;
			}
			StartTurnLogEntry startTurnLogEntry = new StartTurnLogEntry()
			{
				CombatantID = id
			};
			this.fEntries.Add(startTurnLogEntry);
		}

		public void AddStateEntry(Guid id, CreatureState state)
		{
			if (!this.fActive)
			{
				return;
			}
			StateLogEntry stateLogEntry = new StateLogEntry()
			{
				CombatantID = id,
				State = state
			};
			this.fEntries.Add(stateLogEntry);
		}

		internal EncounterReport CreateReport(Encounter enc, bool all_entries)
		{
			EncounterReport encounterReport = new EncounterReport();
			RoundLog roundLog = null;
			TurnLog timestamp = null;
			foreach (IEncounterLogEntry fEntry in this.fEntries)
			{
				StartRoundLogEntry startRoundLogEntry = fEntry as StartRoundLogEntry;
				StartTurnLogEntry startTurnLogEntry = fEntry as StartTurnLogEntry;
				if (startRoundLogEntry != null)
				{
					if (roundLog != null)
					{
						encounterReport.Rounds.Add(roundLog);
					}
					roundLog = new RoundLog(startRoundLogEntry.Round);
				}
				else if (startTurnLogEntry == null)
				{
					if (!all_entries && !fEntry.Important)
					{
						continue;
					}
					timestamp.Entries.Add(fEntry);
				}
				else
				{
					if (timestamp != null)
					{
						timestamp.End = startTurnLogEntry.Timestamp;
						roundLog.Turns.Add(timestamp);
					}
					timestamp = new TurnLog(startTurnLogEntry.CombatantID)
					{
						Start = startTurnLogEntry.Timestamp
					};
				}
			}
			if (roundLog != null)
			{
				if (timestamp != null)
				{
					if (timestamp.Entries.Count != 0)
					{
						timestamp.End = timestamp.Entries[timestamp.Entries.Count - 1].Timestamp;
					}
					roundLog.Turns.Add(timestamp);
				}
				encounterReport.Rounds.Add(roundLog);
			}
			return encounterReport;
		}

		internal static string GetName(Guid id, Encounter enc, bool detailed)
		{
			CombatData combatDatum = enc.FindCombatData(id);
			if (combatDatum != null)
			{
				if (detailed)
				{
					return combatDatum.DisplayName;
				}
				EncounterSlot encounterSlot = enc.FindSlot(combatDatum);
				if (encounterSlot != null)
				{
					ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
					if (creature != null && creature.Category != "")
					{
						return creature.Category;
					}
				}
			}
			Hero hero = Session.Project.FindHero(id);
			if (hero != null)
			{
				return hero.Name;
			}
			Trap trap = enc.FindTrap(id);
			if (trap == null)
			{
				return "Creature";
			}
			return trap.Name;
		}
	}
}