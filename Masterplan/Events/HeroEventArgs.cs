using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class HeroEventArgs : EventArgs
	{
		private Masterplan.Data.Hero fHero;

		public Masterplan.Data.Hero Hero
		{
			get
			{
				return this.fHero;
			}
		}

		public HeroEventArgs(Masterplan.Data.Hero hero)
		{
			this.fHero = hero;
		}
	}
}