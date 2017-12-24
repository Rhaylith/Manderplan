using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	public interface ICreature
	{
		int AC
		{
			get;
			set;
		}

		string Alignment
		{
			get;
			set;
		}

		List<Aura> Auras
		{
			get;
			set;
		}

		string Category
		{
			get;
			set;
		}

		Ability Charisma
		{
			get;
			set;
		}

		Ability Constitution
		{
			get;
			set;
		}

		List<CreaturePower> CreaturePowers
		{
			get;
			set;
		}

		List<DamageModifier> DamageModifiers
		{
			get;
			set;
		}

		string Details
		{
			get;
			set;
		}

		Ability Dexterity
		{
			get;
			set;
		}

		string Equipment
		{
			get;
			set;
		}

		int Fortitude
		{
			get;
			set;
		}

		int HP
		{
			get;
			set;
		}

		Guid ID
		{
			get;
			set;
		}

		System.Drawing.Image Image
		{
			get;
			set;
		}

		string Immune
		{
			get;
			set;
		}

		string Info
		{
			get;
		}

		int Initiative
		{
			get;
			set;
		}

		Ability Intelligence
		{
			get;
			set;
		}

		string Keywords
		{
			get;
			set;
		}

		string Languages
		{
			get;
			set;
		}

		int Level
		{
			get;
			set;
		}

		string Movement
		{
			get;
			set;
		}

		string Name
		{
			get;
			set;
		}

		CreatureOrigin Origin
		{
			get;
			set;
		}

		string Phenotype
		{
			get;
		}

		int Reflex
		{
			get;
			set;
		}

		Masterplan.Data.Regeneration Regeneration
		{
			get;
			set;
		}

		string Resist
		{
			get;
			set;
		}

		IRole Role
		{
			get;
			set;
		}

		string Senses
		{
			get;
			set;
		}

		CreatureSize Size
		{
			get;
			set;
		}

		string Skills
		{
			get;
			set;
		}

		Ability Strength
		{
			get;
			set;
		}

		string Tactics
		{
			get;
			set;
		}

		CreatureType Type
		{
			get;
			set;
		}

		string Vulnerable
		{
			get;
			set;
		}

		int Will
		{
			get;
			set;
		}

		Ability Wisdom
		{
			get;
			set;
		}
	}
}