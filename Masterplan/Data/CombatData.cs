using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CombatData : IComparable<CombatData>
	{
		public static Point NoPoint;

		private Guid fID = Guid.NewGuid();

		private string fDisplayName = "";

		private Point fLocation = CombatData.NoPoint;

		private bool fVisible = true;

		private int fInitiative = -2147483648;

		private bool fDelaying;

		private int fDamage;

		private int fTempHP;

		private int fAltitude;

		private List<Guid> fUsedPowers = new List<Guid>();

		private List<OngoingCondition> fConditions = new List<OngoingCondition>();

		public int Altitude
		{
			get
			{
				return this.fAltitude;
			}
			set
			{
				this.fAltitude = value;
			}
		}

		public List<OngoingCondition> Conditions
		{
			get
			{
				return this.fConditions;
			}
			set
			{
				this.fConditions = value;
			}
		}

		public int Damage
		{
			get
			{
				return this.fDamage;
			}
			set
			{
				this.fDamage = value;
			}
		}

		public bool Delaying
		{
			get
			{
				return this.fDelaying;
			}
			set
			{
				this.fDelaying = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return this.fDisplayName;
			}
			set
			{
				this.fDisplayName = value;
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

		public Point Location
		{
			get
			{
				return this.fLocation;
			}
			set
			{
				this.fLocation = value;
			}
		}

		public int TempHP
		{
			get
			{
				return this.fTempHP;
			}
			set
			{
				this.fTempHP = value;
			}
		}

		public List<Guid> UsedPowers
		{
			get
			{
				return this.fUsedPowers;
			}
			set
			{
				this.fUsedPowers = value;
			}
		}

		public bool Visible
		{
			get
			{
				return this.fVisible;
			}
			set
			{
				this.fVisible = value;
			}
		}

		static CombatData()
		{
			CombatData.NoPoint = new Point(-2147483648, -2147483648);
		}

		public CombatData()
		{
		}

		public int CompareTo(CombatData rhs)
		{
			return this.fDisplayName.CompareTo(rhs.DisplayName);
		}

		public CombatData Copy()
		{
			CombatData combatDatum = new CombatData()
			{
				ID = this.fID,
				DisplayName = this.fDisplayName,
				Location = new Point(this.fLocation.X, this.fLocation.Y),
				Visible = this.fVisible,
				Initiative = this.fInitiative,
				Delaying = this.fDelaying,
				Damage = this.fDamage,
				TempHP = this.fTempHP,
				Altitude = this.fAltitude
			};
			foreach (Guid fUsedPower in this.fUsedPowers)
			{
				combatDatum.UsedPowers.Add(fUsedPower);
			}
			foreach (OngoingCondition fCondition in this.fConditions)
			{
				combatDatum.Conditions.Add(fCondition.Copy());
			}
			return combatDatum;
		}

		public void Reset(bool reset_damage)
		{
			this.fLocation = CombatData.NoPoint;
			this.fVisible = true;
			this.fInitiative = -2147483648;
			this.fDelaying = false;
			this.fTempHP = 0;
			this.fAltitude = 0;
			this.fUsedPowers.Clear();
			this.fConditions.Clear();
			if (reset_damage)
			{
				this.fDamage = 0;
			}
		}

		public override string ToString()
		{
			return this.fDisplayName;
		}
	}
}