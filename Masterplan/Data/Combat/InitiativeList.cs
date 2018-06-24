using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace Masterplan.Data.Combat
{
    [Serializable]
    public class InitiativeList
    {
        // Had to convert this back to a list from a linkedlist so that it's serializable
        List<CombatData> playerList = new List<CombatData>();

        private int CurrentPlayerNode;
        public Dictionary<string, List<CombatData>> ManualInitiativeDictionary = new Dictionary<string, List<CombatData>>();

        public CombatData CurrentActor
        {
            get { return this.playerList[CurrentPlayerNode]; }
        }

        public void AdvanceToPrevTurn()
        {
            var prevTurn = this.CurrentPlayerNode;
            do
            {
                this.CurrentPlayerNode = this.CurrentPlayerNode == 0 ? this.playerList.Count-1: this.CurrentPlayerNode-1;
            }
            while (this.CurrentPlayerNode != prevTurn && this.playerList[CurrentPlayerNode].SkipInitiative == true);
        }

        public void AdvanceNextTurn()
        {
            var prevTurn = this.CurrentPlayerNode;
            do
            {
                this.CurrentPlayerNode = this.CurrentPlayerNode == this.playerList.Count-1 ? 0 : this.CurrentPlayerNode+1;
            }
            while (this.CurrentPlayerNode != prevTurn && this.playerList[CurrentPlayerNode].SkipInitiative == true);
        }

        public CombatData PeekNextActor()
        {
            var prevTurn = this.CurrentPlayerNode;
            var nextTurn = this.CurrentPlayerNode;
            do
            {
                nextTurn = nextTurn == this.playerList.Count-1 ? 0 : nextTurn+1;
            }
            while (nextTurn != prevTurn && this.playerList[nextTurn].SkipInitiative == true);

            return this.playerList[nextTurn];
        }

        public void Remove(CombatData data)
        {
            this.playerList.Remove(data);
        }

        public void AddBefore(CombatData placement, CombatData newData)
        {
            this.playerList.Insert(this.playerList.FindIndex(x => x == placement), newData);
        }

        public void AddAfter(CombatData placement, CombatData newData)
        {
            this.playerList.Insert(this.playerList.FindIndex(x => x == placement)+1, newData);
        }


        public void ToggleDelay(CombatData entity)
        {
            entity.Delaying = !entity.Delaying;
            if (!entity.Delaying)
            {
                //Undelay
                entity.Initiative = this.CurrentActor.Initiative;
                this.playerList.Remove(entity);
                this.playerList.Insert(this.CurrentPlayerNode, entity);
            }
        }

        public List<int> GetAsList()
        {
            return playerList.Select(player => player.Initiative).ToList();
        }

        public void Reset(Encounter encounter)
        {
            List<int> nums = new List<int>();
            foreach (EncounterSlot allSlot in encounter.AllSlots)
            {
                foreach (CombatData combatDatum in allSlot.CombatData)
                {
                    if (allSlot.GetState(combatDatum) == CreatureState.Defeated)
                    {
                        continue;
                    }
                    int initiative = combatDatum.Initiative;
                    if (initiative == Int32.MinValue || nums.Contains(initiative))
                    {
                        continue;
                    }
                    playerList.Add(combatDatum);
                }
            }
            foreach (Hero hero in Session.Project.Heroes)
            {
                int num = hero.CombatData.Initiative;
                if (num == Int32.MinValue || nums.Contains(num))
                {
                    continue;
                }
                playerList.Add(hero.CombatData);
            }
            foreach (Trap trap in encounter.Traps)
            {
                CombatData value = trap.CombatData;
                if (value.Delaying)
                {
                    continue;
                }
                int initiative1 = value.Initiative;
                if (initiative1 == Int32.MinValue || nums.Contains(initiative1))
                {
                    continue;
                }
                playerList.Add(value);
            }

            this.SortList();
            this.CurrentPlayerNode = 0;
        }

        public void RollInitiative(Encounter encounter)
        {
            List<Pair<List<CombatData>, int>> pairs = new List<Pair<List<CombatData>, int>>();

            foreach (Hero hero in Session.Project.Heroes)
            {
                if (hero.CombatData.Initiative != Int32.MinValue)
                {
                    continue;
                }
                switch (Session.Preferences.HeroInitiativeMode)
                {
                    case InitiativeMode.AutoIndividual:
                        {
                            List<CombatData> combatDatas = new List<CombatData>()
                        {
                            hero.CombatData
                        };
                            pairs.Add(new Pair<List<CombatData>, int>(combatDatas, hero.InitBonus));
                            continue;
                        }
                    case InitiativeMode.ManualIndividual:
                        {
                            ManualInitiativeDictionary[hero.Name] = new List<CombatData>()
                        {
                            hero.CombatData
                        };
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            }
            foreach (EncounterSlot slot in encounter.Slots)
            {
                switch (Session.Preferences.InitiativeMode)
                {
                    case InitiativeMode.AutoGroup:
                        {
                            List<CombatData> combatDatas1 = new List<CombatData>();
                            foreach (CombatData combatDatum in slot.CombatData)
                            {
                                if (combatDatum.Initiative != -2147483648)
                                {
                                    continue;
                                }
                                combatDatas1.Add(combatDatum);
                            }
                            if (combatDatas1.Count == 0)
                            {
                                continue;
                            }
                            pairs.Add(new Pair<List<CombatData>, int>(combatDatas1, slot.Card.Initiative));
                            continue;
                        }
                    case InitiativeMode.AutoIndividual:
                        {
                            List<CombatData>.Enumerator enumerator = slot.CombatData.GetEnumerator();
                            try
                            {
                                while (enumerator.MoveNext())
                                {
                                    CombatData current = enumerator.Current;
                                    if (current.Initiative != -2147483648)
                                    {
                                        continue;
                                    }
                                    List<CombatData> combatDatas2 = new List<CombatData>()
                                {
                                    current
                                };
                                    pairs.Add(new Pair<List<CombatData>, int>(combatDatas2, slot.Card.Initiative));
                                }
                                continue;
                            }
                            finally
                            {
                                ((IDisposable)enumerator).Dispose();
                            }
                        }
                    case InitiativeMode.ManualIndividual:
                        {
                            List<CombatData>.Enumerator enumerator1 = slot.CombatData.GetEnumerator();
                            try
                            {
                                while (enumerator1.MoveNext())
                                {
                                    CombatData current1 = enumerator1.Current;
                                    if (current1.Initiative != Int32.MinValue)
                                    {
                                        continue;
                                    }
                                    ManualInitiativeDictionary[current1.DisplayName] = new List<CombatData>()
                                {
                                    current1
                                };
                                }
                                continue;
                            }
                            finally
                            {
                                ((IDisposable)enumerator1).Dispose();
                            }
                        }
                    case InitiativeMode.ManualGroup:
                        {
                            List<CombatData> combatDatas3 = new List<CombatData>();
                            foreach (CombatData combatDatum1 in slot.CombatData)
                            {
                                if (combatDatum1.Initiative != Int32.MinValue)
                                {
                                    continue;
                                }
                                combatDatas3.Add(combatDatum1);
                            }
                            if (combatDatas3.Count == 0)
                            {
                                continue;
                            }
                            ManualInitiativeDictionary[slot.Card.Title] = combatDatas3;
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            }
            foreach (Trap trap in encounter.Traps)
            {
                if (trap.Initiative == Int32.MinValue)
                {
                    continue;
                }
                CombatData item = trap.CombatData;
                if (item.Initiative != Int32.MinValue)
                {
                    continue;
                }
                switch (Session.Preferences.TrapInitiativeMode)
                {
                    case InitiativeMode.AutoIndividual:
                        {
                            List<CombatData> combatDatas4 = new List<CombatData>()
                        {
                            item
                        };
                            pairs.Add(new Pair<List<CombatData>, int>(combatDatas4, trap.Initiative));
                            continue;
                        }
                    case InitiativeMode.ManualIndividual:
                        {
                            ManualInitiativeDictionary[trap.Name] = new List<CombatData>()
                        {
                            item
                        };
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            }
            foreach (Pair<List<CombatData>, int> pair in pairs)
            {
                int num = Session.Dice(1, 20) + pair.Second;
                foreach (CombatData first in pair.First)
                {
                    first.Initiative = num;
                }
            }

            this.Reset(encounter);
        }

        public void StartEncounter()
        {
            this.CurrentPlayerNode = 0;
        }

        public void UpdateInitiative(CombatData entity, int newInitiative)
        {
            if (playerList.Contains(entity))
            {
                this.playerList.Remove(entity);
            }

            entity.Initiative = newInitiative;

            // Todo:  this should handle matching modifiers for same initiatives!
            this.playerList.Insert(this.playerList.FindIndex(x => x.Initiative < entity.Initiative), entity);
        }

        private void SortList()
        {
            // Sort player list
            LinkedList<CombatData> tempPlayerList = new LinkedList<CombatData>(this.playerList);
            this.playerList.Clear();
            IEnumerable<CombatData> orderedPlayerEnumerable = tempPlayerList.OrderByDescending(player => player.Initiative).AsEnumerable();
            orderedPlayerEnumerable.ToList().ForEach(value => this.playerList.Add(value));
        }
    }
}
