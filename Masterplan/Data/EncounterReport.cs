using Masterplan;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	internal class EncounterReport
	{
		private List<RoundLog> fRounds = new List<RoundLog>();

		public List<Guid> Combatants
		{
			get
			{
				List<Guid> guids = new List<Guid>();
				foreach (RoundLog fRound in this.fRounds)
				{
					foreach (TurnLog turn in fRound.Turns)
					{
						if (guids.Contains(turn.ID))
						{
							continue;
						}
						guids.Add(turn.ID);
					}
				}
				return guids;
			}
		}

		public List<RoundLog> Rounds
		{
			get
			{
				return this.fRounds;
			}
		}

		public EncounterReport()
		{
		}

		private void add_table(ReportTable table, Dictionary<Guid, int> standings)
		{
			List<int> nums = new List<int>()
			{
				25,
				18,
				15,
				12,
				10,
				8,
				6,
				4,
				2,
				1
			};
			List<int> nums1 = nums;
			List<int> nums2 = new List<int>();
			foreach (ReportRow row in table.Rows)
			{
				if (nums2.Contains(row.Total))
				{
					continue;
				}
				nums2.Add(row.Total);
			}
			Dictionary<Guid, int> guids = new Dictionary<Guid, int>();
			foreach (int num in nums2)
			{
				int item = 0;
				if (guids.Count < nums1.Count)
				{
					item = nums1[guids.Count];
				}
				foreach (ReportRow reportRow in table.Rows)
				{
					if (reportRow.Total != num)
					{
						continue;
					}
					guids[reportRow.CombatantID] = item;
				}
			}
			foreach (Guid key in guids.Keys)
			{
				if (!standings.ContainsKey(key))
				{
					standings[key] = 0;
				}
				Dictionary<Guid, int> item1 = standings;
				Dictionary<Guid, int> guids1 = item1;
				Guid guid = key;
				Guid guid1 = guid;
				item1[guid] = guids1[guid1] + guids[key];
			}
		}

		public ReportTable CreateTable(ReportType report_type, BreakdownType breakdown_type, Encounter enc)
		{
			ReportTable reportTable = new ReportTable()
			{
				ReportType = report_type,
				BreakdownType = breakdown_type
			};
			List<Pair<string, List<Guid>>> pairs = new List<Pair<string, List<Guid>>>();
			switch (breakdown_type)
			{
				case BreakdownType.Individual:
				{
					List<Guid>.Enumerator enumerator = this.Combatants.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Guid current = enumerator.Current;
							List<Guid> guids = new List<Guid>()
							{
								current
							};
							pairs.Add(new Pair<string, List<Guid>>(enc.WhoIs(current), guids));
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				case BreakdownType.Controller:
				{
					List<Guid> guids1 = new List<Guid>();
					foreach (Guid combatant in this.Combatants)
					{
						if (Session.Project.FindHero(combatant) == null)
						{
							guids1.Add(combatant);
						}
						else
						{
							List<Guid> guids2 = new List<Guid>()
							{
								combatant
							};
							pairs.Add(new Pair<string, List<Guid>>(enc.WhoIs(combatant), guids2));
						}
					}
					pairs.Add(new Pair<string, List<Guid>>("DM", guids1));
					break;
				}
				case BreakdownType.Faction:
				{
					List<Guid> guids3 = new List<Guid>();
					List<Guid> guids4 = new List<Guid>();
					List<Guid> guids5 = new List<Guid>();
					List<Guid> guids6 = new List<Guid>();
					foreach (Guid guid in this.Combatants)
					{
						if (Session.Project.FindHero(guid) == null)
						{
							switch (enc.FindSlot(enc.FindCombatData(guid)).Type)
							{
								case EncounterSlotType.Opponent:
								{
									guids6.Add(guid);
									continue;
								}
								case EncounterSlotType.Ally:
								{
									guids4.Add(guid);
									continue;
								}
								case EncounterSlotType.Neutral:
								{
									guids5.Add(guid);
									continue;
								}
								default:
								{
									continue;
								}
							}
						}
						else
						{
							guids3.Add(guid);
						}
					}
					pairs.Add(new Pair<string, List<Guid>>("PCs", guids3));
					pairs.Add(new Pair<string, List<Guid>>("Allies", guids4));
					pairs.Add(new Pair<string, List<Guid>>("Neutral", guids5));
					pairs.Add(new Pair<string, List<Guid>>("Enemies", guids6));
					break;
				}
			}
			foreach (Pair<string, List<Guid>> pair in pairs)
			{
				if (pair.Second.Count == 0)
				{
					continue;
				}
				ReportRow reportRow = new ReportRow()
				{
					Heading = pair.First
				};
				if (pair.Second.Count == 1)
				{
					reportRow.CombatantID = pair.Second[0];
				}
				for (int i = 1; i <= this.fRounds.Count; i++)
				{
					switch (report_type)
					{
						case ReportType.Time:
						{
							int num = 0;
							foreach (Guid second in pair.Second)
							{
								num += this.Time(second, i);
							}
							reportRow.Values.Add(num);
							break;
						}
						case ReportType.DamageToEnemies:
						{
							int num1 = 0;
							foreach (Guid second1 in pair.Second)
							{
								num1 += this.Damage(second1, i, false, enc);
							}
							reportRow.Values.Add(num1);
							break;
						}
						case ReportType.DamageToAllies:
						{
							int num2 = 0;
							foreach (Guid guid1 in pair.Second)
							{
								num2 += this.Damage(guid1, i, true, enc);
							}
							reportRow.Values.Add(num2);
							break;
						}
						case ReportType.Movement:
						{
							int num3 = 0;
							foreach (Guid second2 in pair.Second)
							{
								num3 += this.Movement(second2, i);
							}
							reportRow.Values.Add(num3);
							break;
						}
					}
				}
				reportTable.Rows.Add(reportRow);
			}
			reportTable.Rows.Sort();
			switch (reportTable.ReportType)
			{
				case ReportType.Time:
				case ReportType.DamageToAllies:
				{
					reportTable.Rows.Reverse();
					return reportTable;
				}
				case ReportType.DamageToEnemies:
				{
					return reportTable;
				}
				default:
				{
					return reportTable;
				}
			}
		}

		public int Damage(Guid id, int round, bool allies, Encounter enc)
		{
			int num = 0;
			foreach (RoundLog fRound in this.fRounds)
			{
				if (fRound.Round != round && round != 0)
				{
					continue;
				}
				foreach (TurnLog turn in fRound.Turns)
				{
					if (!(turn.ID == id) && !(id == Guid.Empty))
					{
						continue;
					}
					List<Guid> _allies = EncounterReport.get_allies(turn.ID, enc);
					List<Guid> guids = new List<Guid>();
					if (!allies)
					{
						foreach (Guid combatant in this.Combatants)
						{
							if (_allies.Contains(combatant))
							{
								continue;
							}
							guids.Add(combatant);
						}
					}
					else
					{
						guids.AddRange(_allies);
					}
					num += turn.Damage(guids);
				}
			}
			return num;
		}

		private static List<Guid> get_allies(Guid id, Encounter enc)
		{
			List<Guid> guids = new List<Guid>();
			if (Session.Project.FindHero(id) == null)
			{
				CombatData combatDatum = enc.FindCombatData(id);
				if (combatDatum != null)
				{
					EncounterSlot encounterSlot = enc.FindSlot(combatDatum);
					if (encounterSlot != null)
					{
						foreach (EncounterSlot slot in enc.Slots)
						{
							if (slot.Type != encounterSlot.Type)
							{
								continue;
							}
							foreach (CombatData combatDatum1 in slot.CombatData)
							{
								guids.Add(combatDatum1.ID);
							}
						}
						if (encounterSlot.Type == EncounterSlotType.Ally)
						{
							foreach (Hero hero in Session.Project.Heroes)
							{
								guids.Add(hero.ID);
							}
						}
					}
				}
			}
			else
			{
				foreach (Hero hero1 in Session.Project.Heroes)
				{
					guids.Add(hero1.ID);
				}
				foreach (EncounterSlot slot1 in enc.Slots)
				{
					if (slot1.Type != EncounterSlotType.Ally)
					{
						continue;
					}
					foreach (CombatData combatDatum2 in slot1.CombatData)
					{
						guids.Add(combatDatum2.ID);
					}
				}
			}
			return guids;
		}

		private List<TurnLog> get_turns(Guid id)
		{
			List<TurnLog> turnLogs = new List<TurnLog>();
			foreach (RoundLog fRound in this.fRounds)
			{
				foreach (TurnLog turn in fRound.Turns)
				{
					if (turn.ID != id)
					{
						continue;
					}
					turnLogs.Add(turn);
				}
			}
			return turnLogs;
		}

		public RoundLog GetRound(int round)
		{
			RoundLog roundLog;
			List<RoundLog>.Enumerator enumerator = this.fRounds.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					RoundLog current = enumerator.Current;
					if (current.Round != round)
					{
						continue;
					}
					roundLog = current;
					return roundLog;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public int Movement(Guid id, int round)
		{
			int num = 0;
			foreach (RoundLog fRound in this.fRounds)
			{
				if (fRound.Round != round && round != 0)
				{
					continue;
				}
				foreach (TurnLog turn in fRound.Turns)
				{
					if (!(turn.ID == id) && !(id == Guid.Empty))
					{
						continue;
					}
					num += turn.Movement();
				}
			}
			return num;
		}

		public List<Guid> MVPs(Encounter enc)
		{
			Dictionary<Guid, int> guids = new Dictionary<Guid, int>();
			ReportTable reportTable = this.CreateTable(ReportType.Time, BreakdownType.Controller, enc);
			reportTable.ReduceToPCs();
			this.add_table(reportTable, guids);
			ReportTable reportTable1 = this.CreateTable(ReportType.DamageToEnemies, BreakdownType.Controller, enc);
			reportTable1.ReduceToPCs();
			this.add_table(reportTable1, guids);
			ReportTable reportTable2 = this.CreateTable(ReportType.DamageToAllies, BreakdownType.Controller, enc);
			reportTable2.ReduceToPCs();
			this.add_table(reportTable2, guids);
			List<Guid> guids1 = new List<Guid>();
			int num = -2147483648;
			foreach (Guid key in guids.Keys)
			{
				int item = guids[key];
				if (item > num)
				{
					num = item;
					guids1.Clear();
				}
				if (item != num)
				{
					continue;
				}
				guids1.Add(key);
			}
			return guids1;
		}

		public int Time(Guid id, int round)
		{
			TimeSpan timeSpan = new TimeSpan();
			foreach (RoundLog fRound in this.fRounds)
			{
				if (fRound.Round != round && round != 0)
				{
					continue;
				}
				foreach (TurnLog turn in fRound.Turns)
				{
					if (!(turn.ID == id) && !(id == Guid.Empty))
					{
						continue;
					}
					timeSpan += turn.Time();
				}
			}
			return (int)timeSpan.TotalSeconds;
		}
	}
}