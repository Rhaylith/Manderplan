using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Library : IComparable<Library>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fSecurityData = Program.SecurityData;

		private List<Creature> fCreatures = new List<Creature>();

		private List<CreatureTemplate> fTemplates = new List<CreatureTemplate>();

		private List<MonsterTheme> fThemes = new List<MonsterTheme>();

		private List<Trap> fTraps = new List<Trap>();

		private List<SkillChallenge> fSkillChallenges = new List<SkillChallenge>();

		private List<MagicItem> fMagicItems = new List<MagicItem>();

		private List<Artifact> fArtifacts = new List<Artifact>();

		private List<Tile> fTiles = new List<Tile>();

		private List<TerrainPower> fTerrainPowers = new List<TerrainPower>();

		public List<Artifact> Artifacts
		{
			get
			{
				return this.fArtifacts;
			}
			set
			{
				this.fArtifacts = value;
			}
		}

		public List<Creature> Creatures
		{
			get
			{
				return this.fCreatures;
			}
			set
			{
				this.fCreatures = value;
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

		public List<MagicItem> MagicItems
		{
			get
			{
				return this.fMagicItems;
			}
			set
			{
				this.fMagicItems = value;
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

		internal string SecurityData
		{
			get
			{
				return this.fSecurityData;
			}
			set
			{
				this.fSecurityData = value;
			}
		}

		public bool ShowInAutoBuild
		{
			get
			{
				bool flag;
				List<Tile>.Enumerator enumerator = this.fTiles.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Tile current = enumerator.Current;
						if (current == null || current.Category == TileCategory.Special || current.Category == TileCategory.Map)
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

		public List<CreatureTemplate> Templates
		{
			get
			{
				return this.fTemplates;
			}
			set
			{
				this.fTemplates = value;
			}
		}

		public List<TerrainPower> TerrainPowers
		{
			get
			{
				return this.fTerrainPowers;
			}
			set
			{
				this.fTerrainPowers = value;
			}
		}

		public List<MonsterTheme> Themes
		{
			get
			{
				return this.fThemes;
			}
			set
			{
				this.fThemes = value;
			}
		}

		public List<Tile> Tiles
		{
			get
			{
				return this.fTiles;
			}
			set
			{
				this.fTiles = value;
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

		public Library()
		{
		}

		public int CompareTo(Library rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public Artifact FindArtifact(Guid item_id)
		{
			Artifact artifact;
			List<Artifact>.Enumerator enumerator = this.fArtifacts.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Artifact current = enumerator.Current;
					if (current == null || !(current.ID == item_id))
					{
						continue;
					}
					artifact = current;
					return artifact;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return artifact;
		}

		public Artifact FindArtifact(string item_name)
		{
			Artifact artifact;
			List<Artifact>.Enumerator enumerator = this.fArtifacts.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Artifact current = enumerator.Current;
					if (current == null || !(current.Name == item_name))
					{
						continue;
					}
					artifact = current;
					return artifact;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return artifact;
		}

		public Creature FindCreature(Guid creature_id)
		{
			Creature creature;
			List<Creature>.Enumerator enumerator = this.fCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Creature current = enumerator.Current;
					if (current == null || !(current.ID == creature_id))
					{
						continue;
					}
					creature = current;
					return creature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return creature;
		}

		public Creature FindCreature(string creature_name)
		{
			Creature creature;
			List<Creature>.Enumerator enumerator = this.fCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Creature current = enumerator.Current;
					if (current == null || !(current.Name == creature_name))
					{
						continue;
					}
					creature = current;
					return creature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return creature;
		}

		public Creature FindCreature(string creature_name, int level)
		{
			Creature creature;
			List<Creature>.Enumerator enumerator = this.fCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Creature current = enumerator.Current;
					if (current == null || !(current.Name == creature_name) || current.Level != level)
					{
						continue;
					}
					creature = current;
					return creature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return creature;
		}

		public MagicItem FindMagicItem(Guid item_id)
		{
			MagicItem magicItem;
			List<MagicItem>.Enumerator enumerator = this.fMagicItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MagicItem current = enumerator.Current;
					if (current == null || !(current.ID == item_id))
					{
						continue;
					}
					magicItem = current;
					return magicItem;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return magicItem;
		}

		public MagicItem FindMagicItem(string item_name)
		{
			MagicItem magicItem;
			List<MagicItem>.Enumerator enumerator = this.fMagicItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MagicItem current = enumerator.Current;
					if (current == null || !(current.Name == item_name))
					{
						continue;
					}
					magicItem = current;
					return magicItem;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return magicItem;
		}

		public MagicItem FindMagicItem(string item_name, int level)
		{
			MagicItem magicItem;
			List<MagicItem>.Enumerator enumerator = this.fMagicItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MagicItem current = enumerator.Current;
					if (current == null || !(current.Name == item_name) || current.Level != level)
					{
						continue;
					}
					magicItem = current;
					return magicItem;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return magicItem;
		}

		public SkillChallenge FindSkillChallenge(Guid sc_id)
		{
			SkillChallenge skillChallenge;
			List<SkillChallenge>.Enumerator enumerator = this.fSkillChallenges.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SkillChallenge current = enumerator.Current;
					if (current == null || !(current.ID == sc_id))
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

		public CreatureTemplate FindTemplate(Guid template_id)
		{
			CreatureTemplate creatureTemplate;
			List<CreatureTemplate>.Enumerator enumerator = this.fTemplates.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CreatureTemplate current = enumerator.Current;
					if (current == null || !(current.ID == template_id))
					{
						continue;
					}
					creatureTemplate = current;
					return creatureTemplate;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return creatureTemplate;
		}

		public TerrainPower FindTerrainPower(Guid item_id)
		{
			TerrainPower terrainPower;
			List<TerrainPower>.Enumerator enumerator = this.fTerrainPowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TerrainPower current = enumerator.Current;
					if (current == null || !(current.ID == item_id))
					{
						continue;
					}
					terrainPower = current;
					return terrainPower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return terrainPower;
		}

		public TerrainPower FindTerrainPower(string item_name)
		{
			TerrainPower terrainPower;
			List<TerrainPower>.Enumerator enumerator = this.fTerrainPowers.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TerrainPower current = enumerator.Current;
					if (current == null || !(current.Name == item_name))
					{
						continue;
					}
					terrainPower = current;
					return terrainPower;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return terrainPower;
		}

		public MonsterTheme FindTheme(Guid theme_id)
		{
			MonsterTheme monsterTheme;
			List<MonsterTheme>.Enumerator enumerator = this.fThemes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					MonsterTheme current = enumerator.Current;
					if (current == null || !(current.ID == theme_id))
					{
						continue;
					}
					monsterTheme = current;
					return monsterTheme;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return monsterTheme;
		}

		public Tile FindTile(Guid tile_id)
		{
			Tile tile;
			List<Tile>.Enumerator enumerator = this.fTiles.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Tile current = enumerator.Current;
					if (current == null || !(current.ID == tile_id))
					{
						continue;
					}
					tile = current;
					return tile;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return tile;
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
					if (current == null || !(current.ID == trap_id))
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

		public Trap FindTrap(string trap_name)
		{
			Trap trap;
			List<Trap>.Enumerator enumerator = this.fTraps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Trap current = enumerator.Current;
					if (current == null || !(current.Name == trap_name))
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

		public Trap FindTrap(string trap_name, int level, string role_string)
		{
			Trap trap;
			List<Trap>.Enumerator enumerator = this.fTraps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Trap current = enumerator.Current;
					if (current == null || !(current.Name == trap_name) || current.Level != level || !(current.Role.ToString() == role_string))
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

		public void Import(Library lib)
		{
			foreach (Creature creature in lib.Creatures)
			{
				if (creature == null || this.FindCreature(creature.ID) != null)
				{
					continue;
				}
				this.fCreatures.Add(creature);
			}
			foreach (CreatureTemplate template in lib.Templates)
			{
				if (template == null || this.FindTemplate(template.ID) != null)
				{
					continue;
				}
				this.fTemplates.Add(template);
			}
			foreach (MonsterTheme theme in lib.Themes)
			{
				if (theme == null || this.FindTheme(theme.ID) != null)
				{
					continue;
				}
				this.fThemes.Add(theme);
			}
			foreach (Trap trap in lib.Traps)
			{
				if (trap == null || this.FindTrap(trap.ID) != null)
				{
					continue;
				}
				this.fTraps.Add(trap);
			}
			foreach (SkillChallenge skillChallenge in lib.SkillChallenges)
			{
				if (skillChallenge == null || this.FindSkillChallenge(skillChallenge.ID) != null)
				{
					continue;
				}
				this.fSkillChallenges.Add(skillChallenge);
			}
			foreach (MagicItem magicItem in lib.MagicItems)
			{
				if (magicItem == null || this.FindMagicItem(magicItem.ID) != null)
				{
					continue;
				}
				this.fMagicItems.Add(magicItem);
			}
			foreach (Artifact artifact in lib.Artifacts)
			{
				if (artifact == null || this.FindArtifact(artifact.ID) != null)
				{
					continue;
				}
				this.fArtifacts.Add(artifact);
			}
			foreach (Tile tile in lib.Tiles)
			{
				if (tile == null || this.FindTile(tile.ID) != null)
				{
					continue;
				}
				this.fTiles.Add(tile);
			}
		}

		public override string ToString()
		{
			return this.fName;
		}

		public void Update()
		{
			if (this.fID == Guid.Empty)
			{
				this.fID = Guid.NewGuid();
			}
			foreach (Creature fCreature in this.fCreatures)
			{
				if (fCreature == null)
				{
					continue;
				}
				if (fCreature.Category == null)
				{
					fCreature.Category = "";
				}
				if (fCreature.Senses == null)
				{
					fCreature.Senses = "";
				}
				if (fCreature.Movement == null)
				{
					fCreature.Movement = "";
				}
				if (fCreature.Auras == null)
				{
					fCreature.Auras = new List<Aura>();
				}
				foreach (Aura aura in fCreature.Auras)
				{
					if (aura.Keywords != null)
					{
						continue;
					}
					aura.Keywords = "";
				}
				if (fCreature.CreaturePowers == null)
				{
					fCreature.CreaturePowers = new List<CreaturePower>();
				}
				if (fCreature.DamageModifiers == null)
				{
					fCreature.DamageModifiers = new List<DamageModifier>();
				}
				foreach (CreaturePower creaturePower in fCreature.CreaturePowers)
				{
					if (creaturePower.Condition == null)
					{
						creaturePower.Condition = "";
					}
					creaturePower.ExtractAttackDetails();
				}
				CreatureHelper.UpdateRegen(fCreature);
				foreach (CreaturePower creaturePower1 in fCreature.CreaturePowers)
				{
					CreatureHelper.UpdatePowerRange(fCreature, creaturePower1);
				}
				if (fCreature.Tactics == null)
				{
					fCreature.Tactics = "";
				}
				if (fCreature.URL == null)
				{
					fCreature.URL = "";
				}
				if (fCreature.Image == null)
				{
					continue;
				}
				Program.SetResolution(fCreature.Image);
			}
			foreach (CreatureTemplate fTemplate in this.fTemplates)
			{
				if (fTemplate == null)
				{
					continue;
				}
				if (fTemplate.Senses == null)
				{
					fTemplate.Senses = "";
				}
				if (fTemplate.Movement == null)
				{
					fTemplate.Movement = "";
				}
				if (fTemplate.Auras == null)
				{
					fTemplate.Auras = new List<Aura>();
				}
				foreach (Aura aura1 in fTemplate.Auras)
				{
					if (aura1.Keywords != null)
					{
						continue;
					}
					aura1.Keywords = "";
				}
				if (fTemplate.CreaturePowers == null)
				{
					fTemplate.CreaturePowers = new List<CreaturePower>();
				}
				if (fTemplate.DamageModifierTemplates == null)
				{
					fTemplate.DamageModifierTemplates = new List<DamageModifierTemplate>();
				}
				foreach (CreaturePower creaturePower2 in fTemplate.CreaturePowers)
				{
					if (creaturePower2.Condition == null)
					{
						creaturePower2.Condition = "";
					}
					creaturePower2.ExtractAttackDetails();
				}
				if (fTemplate.Tactics != null)
				{
					continue;
				}
				fTemplate.Tactics = "";
			}
			if (this.fThemes == null)
			{
				this.fThemes = new List<MonsterTheme>();
			}
			foreach (MonsterTheme fTheme in this.fThemes)
			{
				if (fTheme == null)
				{
					continue;
				}
				foreach (ThemePowerData power in fTheme.Powers)
				{
					power.Power.ExtractAttackDetails();
				}
			}
			if (this.fTraps == null)
			{
				this.fTraps = new List<Trap>();
			}
			foreach (Trap fTrap in this.fTraps)
			{
				if (fTrap.Description == null)
				{
					fTrap.Description = "";
				}
				if (fTrap.Attacks == null)
				{
					fTrap.Attacks = new List<TrapAttack>();
				}
				if (fTrap.Attack != null)
				{
					fTrap.Attacks.Add(fTrap.Attack);
					fTrap.Initiative = (fTrap.Attack.HasInitiative ? fTrap.Attack.Initiative : -2147483648);
					fTrap.Trigger = fTrap.Attack.Trigger;
					fTrap.Attack = null;
				}
				foreach (TrapAttack attack in fTrap.Attacks)
				{
					if (attack.ID == Guid.Empty)
					{
						attack.ID = Guid.NewGuid();
					}
					if (attack.Name == null)
					{
						attack.Name = "Attack";
					}
					if (attack.Keywords == null)
					{
						attack.Keywords = "";
					}
					if (attack.Notes != null)
					{
						continue;
					}
					attack.Notes = "";
				}
				if (fTrap.Trigger == null)
				{
					fTrap.Trigger = "";
				}
				foreach (TrapSkillData skill in fTrap.Skills)
				{
					if (skill.ID != Guid.Empty)
					{
						continue;
					}
					skill.ID = Guid.NewGuid();
				}
			}
			if (this.fSkillChallenges == null)
			{
				this.fSkillChallenges = new List<SkillChallenge>();
			}
			foreach (SkillChallenge fSkillChallenge in this.fSkillChallenges)
			{
				if (fSkillChallenge.Notes == null)
				{
					fSkillChallenge.Notes = "";
				}
				foreach (SkillChallengeData skillChallengeResult in fSkillChallenge.Skills)
				{
					if (skillChallengeResult.Results != null)
					{
						continue;
					}
					skillChallengeResult.Results = new SkillChallengeResult();
				}
			}
			if (this.fMagicItems == null)
			{
				this.fMagicItems = new List<MagicItem>();
			}
			if (this.fArtifacts == null)
			{
				this.fArtifacts = new List<Artifact>();
			}
			foreach (Tile fTile in this.fTiles)
			{
				Program.SetResolution(fTile.Image);
				if (fTile.Keywords != null)
				{
					continue;
				}
				fTile.Keywords = "";
			}
			if (this.fTerrainPowers == null)
			{
				this.fTerrainPowers = new List<TerrainPower>();
			}
		}
	}
}