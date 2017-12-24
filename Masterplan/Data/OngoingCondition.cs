using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class OngoingCondition : IComparable<OngoingCondition>
	{
		private OngoingType fType;

		private string fData = "";

		private Masterplan.Data.DamageType fDamageType;

		private int fValue = 2;

		private int fDefenceMod = 2;

		private List<DefenceType> fDefences = new List<DefenceType>();

		private Masterplan.Data.Regeneration fRegeneration = new Masterplan.Data.Regeneration();

		private Masterplan.Data.DamageModifier fDamageModifier = new Masterplan.Data.DamageModifier();

		private Masterplan.Data.Aura fAura = new Masterplan.Data.Aura();

		private DurationType fDuration = DurationType.SaveEnds;

		private Guid fDurationCreatureID = Guid.Empty;

		private int fDurationRound = -2147483648;

		private int fSavingThrowModifier;

		public Masterplan.Data.Aura Aura
		{
			get
			{
				return this.fAura;
			}
			set
			{
				this.fAura = value;
			}
		}

		public Masterplan.Data.DamageModifier DamageModifier
		{
			get
			{
				return this.fDamageModifier;
			}
			set
			{
				this.fDamageModifier = value;
			}
		}

		public Masterplan.Data.DamageType DamageType
		{
			get
			{
				return this.fDamageType;
			}
			set
			{
				this.fDamageType = value;
			}
		}

		public string Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value;
			}
		}

		public int DefenceMod
		{
			get
			{
				return this.fDefenceMod;
			}
			set
			{
				this.fDefenceMod = value;
			}
		}

		public List<DefenceType> Defences
		{
			get
			{
				return this.fDefences;
			}
			set
			{
				this.fDefences = value;
			}
		}

		public DurationType Duration
		{
			get
			{
				return this.fDuration;
			}
			set
			{
				this.fDuration = value;
			}
		}

		public Guid DurationCreatureID
		{
			get
			{
				return this.fDurationCreatureID;
			}
			set
			{
				this.fDurationCreatureID = value;
			}
		}

		public int DurationRound
		{
			get
			{
				return this.fDurationRound;
			}
			set
			{
				this.fDurationRound = value;
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

		public int SavingThrowModifier
		{
			get
			{
				return this.fSavingThrowModifier;
			}
			set
			{
				this.fSavingThrowModifier = value;
			}
		}

		public OngoingType Type
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

		public int Value
		{
			get
			{
				return this.fValue;
			}
			set
			{
				this.fValue = value;
			}
		}

		public OngoingCondition()
		{
		}

		public int CompareTo(OngoingCondition rhs)
		{
			return this.ToString().CompareTo(rhs.ToString());
		}

		public OngoingCondition Copy()
		{
			Masterplan.Data.Regeneration regeneration;
			Masterplan.Data.DamageModifier damageModifier;
			Masterplan.Data.Aura aura;
			OngoingCondition ongoingCondition = new OngoingCondition()
			{
				Type = this.fType,
				Data = this.fData,
				DamageType = this.fDamageType,
				Value = this.fValue,
				DefenceMod = this.fDefenceMod,
				Defences = new List<DefenceType>()
			};
			foreach (DefenceType fDefence in this.fDefences)
			{
				ongoingCondition.fDefences.Add(fDefence);
			}
			OngoingCondition ongoingCondition1 = ongoingCondition;
			if (this.fRegeneration != null)
			{
				regeneration = this.fRegeneration.Copy();
			}
			else
			{
				regeneration = null;
			}
			ongoingCondition1.Regeneration = regeneration;
			OngoingCondition ongoingCondition2 = ongoingCondition;
			if (this.fDamageModifier != null)
			{
				damageModifier = this.fDamageModifier.Copy();
			}
			else
			{
				damageModifier = null;
			}
			ongoingCondition2.DamageModifier = damageModifier;
			OngoingCondition ongoingCondition3 = ongoingCondition;
			if (this.fAura != null)
			{
				aura = this.fAura.Copy();
			}
			else
			{
				aura = null;
			}
			ongoingCondition3.Aura = aura;
			ongoingCondition.Duration = this.fDuration;
			ongoingCondition.DurationCreatureID = this.fDurationCreatureID;
			ongoingCondition.DurationRound = this.fDurationRound;
			ongoingCondition.SavingThrowModifier = this.fSavingThrowModifier;
			return ongoingCondition;
		}

		public string GetDuration(Encounter enc)
		{
			string str = "";
			switch (this.fDuration)
			{
				case DurationType.Encounter:
				{
					return str;
				}
				case DurationType.SaveEnds:
				{
					str = "save ends";
					if (this.SavingThrowModifier == 0)
					{
						return str;
					}
					string str1 = (this.SavingThrowModifier >= 0 ? "+" : "");
					object obj = str;
					object[] savingThrowModifier = new object[] { obj, " with ", str1, this.SavingThrowModifier, " mod" };
					str = string.Concat(savingThrowModifier);
					return str;
				}
				case DurationType.BeginningOfTurn:
				{
					string str2 = "";
					if (this.fDurationCreatureID != Guid.Empty)
					{
						str2 = (enc == null ? "my" : string.Concat(enc.WhoIs(this.fDurationCreatureID), "'s"));
					}
					else
					{
						str2 = "someone else's";
					}
					str = string.Concat(str, "until the start of ", str2, " next turn");
					return str;
				}
				case DurationType.EndOfTurn:
				{
					string str3 = "";
					if (this.fDurationCreatureID != Guid.Empty)
					{
						str3 = (enc == null ? "my" : string.Concat(enc.WhoIs(this.fDurationCreatureID), "'s"));
					}
					else
					{
						str3 = "someone else's";
					}
					str = string.Concat(str, "until the end of ", str3, " next turn");
					return str;
				}
				default:
				{
					return str;
				}
			}
		}

		public string ToString(Encounter enc, bool html)
		{
			string str = this.ToString();
			if (html)
			{
				str = string.Concat("<B>", str, "</B>");
			}
			string duration = this.GetDuration(enc);
			if (duration != "")
			{
				str = string.Concat(str, " (", duration, ")");
			}
			return str;
		}

		public override string ToString()
		{
			string str = "";
			switch (this.fType)
			{
				case OngoingType.Condition:
				{
					str = this.fData;
					break;
				}
				case OngoingType.Damage:
				{
					if (this.fDamageType != Masterplan.Data.DamageType.Untyped)
					{
						string lower = this.fDamageType.ToString().ToLower();
						object[] objArray = new object[] { this.fValue, " ongoing ", lower, " damage" };
						str = string.Concat(objArray);
						break;
					}
					else
					{
						str = string.Concat(this.fValue, " ongoing damage");
						break;
					}
				}
				case OngoingType.DefenceModifier:
				{
					str = this.fDefenceMod.ToString();
					if (this.fDefenceMod >= 0)
					{
						str = string.Concat("+", str);
					}
					string str1 = "";
					if (this.fDefences.Count != 4)
					{
						foreach (DefenceType fDefence in this.fDefences)
						{
							if (str1 != "")
							{
								str1 = string.Concat(str1, ", ");
							}
							str1 = string.Concat(str1, fDefence.ToString());
						}
					}
					else
					{
						str1 = "defences";
					}
					str = string.Concat(str, " to ", str1);
					break;
				}
				case OngoingType.DamageModifier:
				{
					str = this.fDamageModifier.ToString();
					break;
				}
				case OngoingType.Regeneration:
				{
					str = string.Concat("Regeneration ", this.fRegeneration.Value);
					break;
				}
				case OngoingType.Aura:
				{
					object[] radius = new object[] { "Aura ", this.fAura.Radius, ": ", this.fAura.Description };
					str = string.Concat(radius);
					break;
				}
			}
			return str;
		}
	}
}