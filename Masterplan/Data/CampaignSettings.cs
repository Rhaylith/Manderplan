using System;

namespace Masterplan.Data
{
	[Serializable]
	public class CampaignSettings
	{
		private double fHP = 1;

		private double fXP = 1;

		private int fAttackBonus;

		private int fACBonus;

		private int fNADBonus;

		public int ACBonus
		{
			get
			{
				return this.fACBonus;
			}
			set
			{
				this.fACBonus = value;
			}
		}

		public int AttackBonus
		{
			get
			{
				return this.fAttackBonus;
			}
			set
			{
				this.fAttackBonus = value;
			}
		}

		public double HP
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

		public int NADBonus
		{
			get
			{
				return this.fNADBonus;
			}
			set
			{
				this.fNADBonus = value;
			}
		}

		public double XP
		{
			get
			{
				return this.fXP;
			}
			set
			{
				this.fXP = value;
			}
		}

		public CampaignSettings()
		{
		}
	}
}