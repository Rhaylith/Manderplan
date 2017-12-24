using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Project
	{
		private string fName = "";

		private string fAuthor = "";

		private Masterplan.Data.Party fParty = new Masterplan.Data.Party();

		private List<Hero> fHeroes = new List<Hero>();

		private List<Hero> fInactiveHeroes = new List<Hero>();

		private Masterplan.Data.Plot fPlot = new Masterplan.Data.Plot();

		private Masterplan.Data.Encyclopedia fEncyclopedia = new Masterplan.Data.Encyclopedia();

		private List<Note> fNotes = new List<Note>();

		private List<Map> fMaps = new List<Map>();

		private List<RegionalMap> fRegionalMaps = new List<RegionalMap>();

		private List<EncounterDeck> fDecks = new List<EncounterDeck>();

		private List<NPC> fNPCs = new List<NPC>();

		private List<CustomCreature> fCustomCreatures = new List<CustomCreature>();

		private List<Calendar> fCalendars = new List<Calendar>();

		private List<Attachment> fAttachments = new List<Attachment>();

		private List<Background> fBackgrounds = new List<Background>();

		private List<Parcel> fTreasureParcels = new List<Parcel>();

		private List<IPlayerOption> fPlayerOptions = new List<IPlayerOption>();

		private List<CombatState> fSavedCombats = new List<CombatState>();

		private Dictionary<string, string> fAddInData = new Dictionary<string, string>();

		private Masterplan.Data.CampaignSettings fCampaignSettings = new Masterplan.Data.CampaignSettings();

		private string fPassword = "";

		private string fPasswordHint = "";

		private Masterplan.Data.Library fLibrary = new Masterplan.Data.Library();

		public Dictionary<string, string> AddInData
		{
			get
			{
				return this.fAddInData;
			}
			set
			{
				this.fAddInData = value;
			}
		}

		public List<PlotPoint> AllPlotPoints
		{
			get
			{
				List<PlotPoint> plotPoints = new List<PlotPoint>();
				foreach (PlotPoint point in this.fPlot.Points)
				{
					plotPoints.AddRange(point.Subtree);
				}
				return plotPoints;
			}
		}

		public List<Parcel> AllTreasureParcels
		{
			get
			{
				List<Parcel> parcels = new List<Parcel>();
				parcels.AddRange(this.fTreasureParcels);
				foreach (PlotPoint allPlotPoint in this.AllPlotPoints)
				{
					parcels.AddRange(allPlotPoint.Parcels);
				}
				return parcels;
			}
		}

		public List<Attachment> Attachments
		{
			get
			{
				return this.fAttachments;
			}
			set
			{
				this.fAttachments = value;
			}
		}

		public string Author
		{
			get
			{
				return this.fAuthor;
			}
			set
			{
				this.fAuthor = value;
			}
		}

		public List<Background> Backgrounds
		{
			get
			{
				return this.fBackgrounds;
			}
			set
			{
				this.fBackgrounds = value;
			}
		}

		public List<Calendar> Calendars
		{
			get
			{
				return this.fCalendars;
			}
			set
			{
				this.fCalendars = value;
			}
		}

		public Masterplan.Data.CampaignSettings CampaignSettings
		{
			get
			{
				return this.fCampaignSettings;
			}
			set
			{
				this.fCampaignSettings = value;
			}
		}

		public List<CustomCreature> CustomCreatures
		{
			get
			{
				return this.fCustomCreatures;
			}
			set
			{
				this.fCustomCreatures = value;
			}
		}

		public List<EncounterDeck> Decks
		{
			get
			{
				return this.fDecks;
			}
			set
			{
				this.fDecks = value;
			}
		}

		public Masterplan.Data.Encyclopedia Encyclopedia
		{
			get
			{
				return this.fEncyclopedia;
			}
			set
			{
				this.fEncyclopedia = value;
			}
		}

		public List<Hero> Heroes
		{
			get
			{
				return this.fHeroes;
			}
			set
			{
				this.fHeroes = value;
			}
		}

		public List<Hero> InactiveHeroes
		{
			get
			{
				return this.fInactiveHeroes;
			}
			set
			{
				this.fInactiveHeroes = value;
			}
		}

		public Masterplan.Data.Library Library
		{
			get
			{
				return this.fLibrary;
			}
			set
			{
				this.fLibrary = value;
			}
		}

		public List<Map> Maps
		{
			get
			{
				return this.fMaps;
			}
			set
			{
				this.fMaps = value;
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

		public List<Note> Notes
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

		public List<NPC> NPCs
		{
			get
			{
				return this.fNPCs;
			}
			set
			{
				this.fNPCs = value;
			}
		}

		public Masterplan.Data.Party Party
		{
			get
			{
				return this.fParty;
			}
			set
			{
				this.fParty = value;
			}
		}

		public string Password
		{
			get
			{
				return this.fPassword;
			}
			set
			{
				this.fPassword = value;
			}
		}

		public string PasswordHint
		{
			get
			{
				return this.fPasswordHint;
			}
			set
			{
				this.fPasswordHint = value;
			}
		}

		public List<IPlayerOption> PlayerOptions
		{
			get
			{
				return this.fPlayerOptions;
			}
			set
			{
				this.fPlayerOptions = value;
			}
		}

		public Masterplan.Data.Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				this.fPlot = value;
			}
		}

		public List<RegionalMap> RegionalMaps
		{
			get
			{
				return this.fRegionalMaps;
			}
			set
			{
				this.fRegionalMaps = value;
			}
		}

		public List<CombatState> SavedCombats
		{
			get
			{
				return this.fSavedCombats;
			}
			set
			{
				this.fSavedCombats = value;
			}
		}

		public List<Parcel> TreasureParcels
		{
			get
			{
				return this.fTreasureParcels;
			}
			set
			{
				this.fTreasureParcels = value;
			}
		}

		public Project()
		{
		}

		private void add_data(PlotPoint pp, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids, List<Guid> trap_ids, List<Guid> challenge_ids, List<Guid> magic_item_ids)
		{
			if (pp.Element is Encounter)
			{
				Encounter element = pp.Element as Encounter;
				foreach (EncounterSlot slot in element.Slots)
				{
					this.add_data(slot.Card, creature_ids, template_ids, theme_ids);
				}
				foreach (Trap trap in element.Traps)
				{
					if (trap_ids.Contains(trap.ID))
					{
						continue;
					}
					trap_ids.Add(trap.ID);
				}
				foreach (SkillChallenge skillChallenge in element.SkillChallenges)
				{
					if (challenge_ids.Contains(skillChallenge.ID))
					{
						continue;
					}
					challenge_ids.Add(skillChallenge.ID);
				}
			}
			if (pp.Element is SkillChallenge)
			{
				SkillChallenge element1 = pp.Element as SkillChallenge;
				if (!challenge_ids.Contains(element1.ID))
				{
					challenge_ids.Add(element1.ID);
				}
			}
			if (pp.Element is Trap)
			{
				Trap trap1 = pp.Element as Trap;
				if (!trap_ids.Contains(trap1.ID))
				{
					trap_ids.Add(trap1.ID);
				}
			}
			foreach (Parcel parcel in pp.Parcels)
			{
				if (!(parcel.MagicItemID != Guid.Empty) || magic_item_ids.Contains(parcel.MagicItemID))
				{
					continue;
				}
				magic_item_ids.Add(parcel.MagicItemID);
			}
			foreach (PlotPoint point in pp.Subplot.Points)
			{
				this.add_data(point, creature_ids, template_ids, theme_ids, trap_ids, challenge_ids, magic_item_ids);
			}
		}

		private void add_data(EncounterDeck deck, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids)
		{
			foreach (EncounterCard card in deck.Cards)
			{
				this.add_data(card, creature_ids, template_ids, theme_ids);
			}
		}

		private void add_data(EncounterCard card, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids)
		{
			if (!creature_ids.Contains(card.CreatureID))
			{
				creature_ids.Add(card.CreatureID);
			}
			foreach (Guid templateID in card.TemplateIDs)
			{
				if (template_ids.Contains(templateID))
				{
					continue;
				}
				template_ids.Add(templateID);
			}
			if (card.ThemeID != Guid.Empty && !theme_ids.Contains(card.ThemeID))
			{
				theme_ids.Add(card.ThemeID);
			}
		}

		private void add_data(NPC npc, List<Guid> template_ids)
		{
			if (!template_ids.Contains(npc.TemplateID))
			{
				template_ids.Add(npc.TemplateID);
			}
		}

		public Attachment FindAttachment(string name)
		{
			Attachment attachment;
			List<Attachment>.Enumerator enumerator = this.fAttachments.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Attachment current = enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					attachment = current;
					return attachment;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return attachment;
		}

		public Background FindBackground(string title)
		{
			Background background;
			List<Background>.Enumerator enumerator = this.fBackgrounds.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Background current = enumerator.Current;
					if (current.Title != title)
					{
						continue;
					}
					background = current;
					return background;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return background;
		}

		public Calendar FindCalendar(Guid calendar_id)
		{
			Calendar calendar;
			List<Calendar>.Enumerator enumerator = this.fCalendars.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Calendar current = enumerator.Current;
					if (current.ID != calendar_id)
					{
						continue;
					}
					calendar = current;
					return calendar;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return calendar;
		}

		public CustomCreature FindCustomCreature(Guid creature_id)
		{
			CustomCreature customCreature;
			List<CustomCreature>.Enumerator enumerator = this.fCustomCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CustomCreature current = enumerator.Current;
					if (current.ID != creature_id)
					{
						continue;
					}
					customCreature = current;
					return customCreature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return customCreature;
		}

		public CustomCreature FindCustomCreature(string creature_name)
		{
			CustomCreature customCreature;
			List<CustomCreature>.Enumerator enumerator = this.fCustomCreatures.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CustomCreature current = enumerator.Current;
					if (current.Name != creature_name)
					{
						continue;
					}
					customCreature = current;
					return customCreature;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return customCreature;
		}

		public EncounterDeck FindDeck(Guid deck_id)
		{
			EncounterDeck encounterDeck;
			List<EncounterDeck>.Enumerator enumerator = this.fDecks.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncounterDeck current = enumerator.Current;
					if (current.ID != deck_id)
					{
						continue;
					}
					encounterDeck = current;
					return encounterDeck;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return encounterDeck;
		}

		public Hero FindHero(Guid hero_id)
		{
			Hero hero;
			foreach (Hero fHero in this.fHeroes)
			{
				if (fHero.ID != hero_id)
				{
					continue;
				}
				hero = fHero;
				return hero;
			}
			List<Hero>.Enumerator enumerator = this.fInactiveHeroes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Hero current = enumerator.Current;
					if (current.ID != hero_id)
					{
						continue;
					}
					hero = current;
					return hero;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return hero;
		}

		public Hero FindHero(string hero_name)
		{
			Hero hero;
			foreach (Hero fHero in this.fHeroes)
			{
				if (fHero.Name != hero_name)
				{
					continue;
				}
				hero = fHero;
				return hero;
			}
			List<Hero>.Enumerator enumerator = this.fInactiveHeroes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Hero current = enumerator.Current;
					if (current.Name != hero_name)
					{
						continue;
					}
					hero = current;
					return hero;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return hero;
		}

		public Note FindNote(Guid note_id)
		{
			Note note;
			List<Note>.Enumerator enumerator = this.fNotes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Note current = enumerator.Current;
					if (current.ID != note_id)
					{
						continue;
					}
					note = current;
					return note;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return note;
		}

		public NPC FindNPC(Guid npc_id)
		{
			NPC nPC;
			List<NPC>.Enumerator enumerator = this.fNPCs.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					NPC current = enumerator.Current;
					if (current.ID != npc_id)
					{
						continue;
					}
					nPC = current;
					return nPC;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return nPC;
		}

		public PlotPoint FindParent(Masterplan.Data.Plot p)
		{
			PlotPoint plotPoint;
			List<PlotPoint> plotPoints = new List<PlotPoint>();
			this.get_all_points(Session.Project.Plot, plotPoints);
			List<PlotPoint>.Enumerator enumerator = plotPoints.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PlotPoint current = enumerator.Current;
					if (current.Subplot != p)
					{
						continue;
					}
					plotPoint = current;
					return plotPoint;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return plotPoint;
		}

		public Masterplan.Data.Plot FindParent(PlotPoint pp)
		{
			Masterplan.Data.Plot plot;
			List<Masterplan.Data.Plot> plots = new List<Masterplan.Data.Plot>();
			this.get_all_plots(Session.Project.Plot, plots);
			List<Masterplan.Data.Plot>.Enumerator enumerator = plots.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Masterplan.Data.Plot current = enumerator.Current;
					List<PlotPoint>.Enumerator enumerator1 = current.Points.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							if (enumerator1.Current.ID != pp.ID)
							{
								continue;
							}
							plot = current;
							return plot;
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
			return plot;
		}

		public IPlayerOption FindPlayerOption(Guid option_id)
		{
			IPlayerOption playerOption;
			List<IPlayerOption>.Enumerator enumerator = this.fPlayerOptions.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IPlayerOption current = enumerator.Current;
					if (current.ID != option_id)
					{
						continue;
					}
					playerOption = current;
					return playerOption;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return playerOption;
		}

		public RegionalMap FindRegionalMap(Guid map_id)
		{
			RegionalMap regionalMap;
			List<RegionalMap>.Enumerator enumerator = this.fRegionalMaps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					RegionalMap current = enumerator.Current;
					if (current.ID != map_id)
					{
						continue;
					}
					regionalMap = current;
					return regionalMap;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return regionalMap;
		}

		public Map FindTacticalMap(Guid map_id)
		{
			Map map;
			List<Map>.Enumerator enumerator = this.fMaps.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Map current = enumerator.Current;
					if (current.ID != map_id)
					{
						continue;
					}
					map = current;
					return map;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return map;
		}

		private void get_all_plots(Masterplan.Data.Plot p, List<Masterplan.Data.Plot> plots)
		{
			plots.Add(p);
			foreach (PlotPoint plotPoint in (p != null ? p.Points : Session.Project.Plot.Points))
			{
				this.get_all_plots(plotPoint.Subplot, plots);
			}
		}

		private void get_all_points(Masterplan.Data.Plot p, List<PlotPoint> points)
		{
			foreach (PlotPoint plotPoint in (p != null ? p.Points : Session.Project.Plot.Points))
			{
				points.Add(plotPoint);
				this.get_all_points(plotPoint.Subplot, points);
			}
		}

		public void Import(Project p)
		{
			p.Update();
			PlotPoint plotPoint = new PlotPoint(p.Name)
			{
				Subplot = p.Plot
			};
			this.fPlot.Points.Add(plotPoint);
			this.fEncyclopedia.Import(p.Encyclopedia);
			this.fNotes.AddRange(p.Notes);
			this.fMaps.AddRange(p.Maps);
			this.fRegionalMaps.AddRange(p.RegionalMaps);
			this.fDecks.AddRange(p.Decks);
			this.fNPCs.AddRange(p.NPCs);
			this.fCustomCreatures.AddRange(p.CustomCreatures);
			this.fCalendars.AddRange(p.Calendars);
			this.fAttachments.AddRange(p.Attachments);
			this.fPlayerOptions.AddRange(p.PlayerOptions);
			foreach (Background background in p.Backgrounds)
			{
				if (background.Details == "")
				{
					continue;
				}
				Background background1 = this.FindBackground(background.Title);
				if (background1 != null)
				{
					if (background1.Details != "")
					{
						Background background2 = background1;
						background2.Details = string.Concat(background2.Details, Environment.NewLine);
					}
					Background background3 = background1;
					background3.Details = string.Concat(background3.Details, background.Details);
				}
				else
				{
					this.fBackgrounds.AddRange(p.Backgrounds);
				}
			}
			this.PopulateProjectLibrary();
			this.fLibrary.Import(p.Library);
			this.SimplifyProjectLibrary();
		}

		private void populate_challenges(List<Guid> challenge_ids)
		{
			List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
			foreach (SkillChallenge skillChallenge in this.fLibrary.SkillChallenges)
			{
				if (skillChallenge != null && challenge_ids.Contains(skillChallenge.ID))
				{
					continue;
				}
				skillChallenges.Add(skillChallenge);
			}
			foreach (SkillChallenge skillChallenge1 in skillChallenges)
			{
				this.fLibrary.SkillChallenges.Remove(skillChallenge1);
			}
			foreach (Guid challengeId in challenge_ids)
			{
				if (this.fLibrary.FindSkillChallenge(challengeId) != null)
				{
					continue;
				}
				SkillChallenge skillChallenge2 = Session.FindSkillChallenge(challengeId, SearchType.Global);
				if (skillChallenge2 == null)
				{
					continue;
				}
				this.fLibrary.SkillChallenges.Add(skillChallenge2);
			}
		}

		private void populate_creatures(List<Guid> creature_ids)
		{
			List<Creature> creatures = new List<Creature>();
			foreach (Creature creature in this.fLibrary.Creatures)
			{
				if (creature != null && creature_ids.Contains(creature.ID))
				{
					continue;
				}
				creatures.Add(creature);
			}
			foreach (Creature creature1 in creatures)
			{
				this.fLibrary.Creatures.Remove(creature1);
			}
			foreach (Guid creatureId in creature_ids)
			{
				if (this.fLibrary.FindCreature(creatureId) != null)
				{
					continue;
				}
				ICreature creature2 = Session.FindCreature(creatureId, SearchType.Global);
				if (creature2 == null)
				{
					continue;
				}
				this.fLibrary.Creatures.Add(creature2 as Creature);
			}
		}

		private void populate_magic_items(List<Guid> magic_item_ids)
		{
			List<MagicItem> magicItems = new List<MagicItem>();
			foreach (MagicItem magicItem in this.fLibrary.MagicItems)
			{
				if (magicItem != null && magic_item_ids.Contains(magicItem.ID))
				{
					continue;
				}
				magicItems.Add(magicItem);
			}
			foreach (MagicItem magicItem1 in magicItems)
			{
				this.fLibrary.MagicItems.Remove(magicItem1);
			}
			foreach (Guid magicItemId in magic_item_ids)
			{
				if (this.fLibrary.FindMagicItem(magicItemId) != null)
				{
					continue;
				}
				MagicItem magicItem2 = Session.FindMagicItem(magicItemId, SearchType.Global);
				if (magicItem2 == null)
				{
					continue;
				}
				this.fLibrary.MagicItems.Add(magicItem2);
			}
		}

		private void populate_templates(List<Guid> template_ids)
		{
			List<CreatureTemplate> creatureTemplates = new List<CreatureTemplate>();
			foreach (CreatureTemplate template in this.fLibrary.Templates)
			{
				if (template != null && template_ids.Contains(template.ID))
				{
					continue;
				}
				creatureTemplates.Add(template);
			}
			foreach (CreatureTemplate creatureTemplate in creatureTemplates)
			{
				this.fLibrary.Templates.Remove(creatureTemplate);
			}
			foreach (Guid templateId in template_ids)
			{
				if (this.fLibrary.FindTemplate(templateId) != null)
				{
					continue;
				}
				CreatureTemplate creatureTemplate1 = Session.FindTemplate(templateId, SearchType.Global);
				if (creatureTemplate1 == null)
				{
					continue;
				}
				this.fLibrary.Templates.Add(creatureTemplate1);
			}
		}

		private void populate_themes(List<Guid> theme_ids)
		{
			List<MonsterTheme> monsterThemes = new List<MonsterTheme>();
			foreach (MonsterTheme theme in this.fLibrary.Themes)
			{
				if (theme != null && theme_ids.Contains(theme.ID))
				{
					continue;
				}
				monsterThemes.Add(theme);
			}
			foreach (MonsterTheme monsterTheme in monsterThemes)
			{
				this.fLibrary.Themes.Remove(monsterTheme);
			}
			foreach (Guid themeId in theme_ids)
			{
				if (this.fLibrary.FindTheme(themeId) != null)
				{
					continue;
				}
				MonsterTheme monsterTheme1 = Session.FindTheme(themeId, SearchType.Global);
				if (monsterTheme1 == null)
				{
					continue;
				}
				this.fLibrary.Themes.Add(monsterTheme1);
			}
		}

		private void populate_tiles()
		{
			List<Guid> guids = new List<Guid>();
			foreach (Map fMap in this.fMaps)
			{
				foreach (TileData tile in fMap.Tiles)
				{
					if (guids.Contains(tile.TileID))
					{
						continue;
					}
					guids.Add(tile.TileID);
				}
			}
			List<Tile> tiles = new List<Tile>();
			foreach (Tile tile1 in this.fLibrary.Tiles)
			{
				if (tile1 != null && guids.Contains(tile1.ID))
				{
					continue;
				}
				tiles.Add(tile1);
			}
			foreach (Tile tile2 in tiles)
			{
				this.fLibrary.Tiles.Remove(tile2);
			}
			foreach (Guid guid in guids)
			{
				if (Session.FindTile(guid, SearchType.Project) != null)
				{
					continue;
				}
				Tile tile3 = Session.FindTile(guid, SearchType.External);
				if (tile3 == null)
				{
					continue;
				}
				this.fLibrary.Tiles.Add(tile3);
			}
		}

		private void populate_traps(List<Guid> trap_ids)
		{
			List<Trap> traps = new List<Trap>();
			foreach (Trap trap in this.fLibrary.Traps)
			{
				if (trap != null && trap_ids.Contains(trap.ID))
				{
					continue;
				}
				traps.Add(trap);
			}
			foreach (Trap trap1 in traps)
			{
				this.fLibrary.Traps.Remove(trap1);
			}
			foreach (Guid trapId in trap_ids)
			{
				if (this.fLibrary.FindTrap(trapId) != null)
				{
					continue;
				}
				Trap trap2 = Session.FindTrap(trapId, SearchType.Global);
				if (trap2 == null)
				{
					continue;
				}
				this.fLibrary.Traps.Add(trap2);
			}
		}

		public void PopulateProjectLibrary()
		{
			List<Guid> guids = new List<Guid>();
			List<Guid> guids1 = new List<Guid>();
			List<Guid> guids2 = new List<Guid>();
			List<Guid> guids3 = new List<Guid>();
			List<Guid> guids4 = new List<Guid>();
			List<Guid> guids5 = new List<Guid>();
			foreach (PlotPoint point in this.fPlot.Points)
			{
				this.add_data(point, guids, guids1, guids2, guids3, guids4, guids5);
			}
			foreach (EncounterDeck fDeck in this.fDecks)
			{
				this.add_data(fDeck, guids, guids1, guids2);
			}
			foreach (NPC fNPC in this.fNPCs)
			{
				this.add_data(fNPC, guids1);
			}
			this.populate_creatures(guids);
			this.populate_templates(guids1);
			this.populate_themes(guids2);
			this.populate_traps(guids3);
			this.populate_challenges(guids4);
			this.populate_magic_items(guids5);
			this.populate_tiles();
		}

		public void SetStandardBackgroundItems()
		{
			this.fBackgrounds.Add(new Background("Introduction"));
			this.fBackgrounds.Add(new Background("Adventure Synopsis"));
			this.fBackgrounds.Add(new Background("Adventure Background"));
			this.fBackgrounds.Add(new Background("DM Information"));
			this.fBackgrounds.Add(new Background("Player Introduction"));
			this.fBackgrounds.Add(new Background("Character Hooks"));
			this.fBackgrounds.Add(new Background("Treasure Preparation"));
			this.fBackgrounds.Add(new Background("Continuing the Story"));
		}

		public void SimplifyProjectLibrary()
		{
			List<Creature> creatures = new List<Creature>();
			foreach (Creature creature in this.fLibrary.Creatures)
			{
				if (creature == null)
				{
					continue;
				}
				ICreature creature1 = Session.FindCreature(creature.ID, SearchType.External);
				if (creature1 == null || !(creature1 is Creature))
				{
					continue;
				}
				creatures.Add(creature1 as Creature);
			}
			foreach (Creature creature2 in creatures)
			{
				this.fLibrary.Creatures.Remove(creature2);
			}
			List<CreatureTemplate> creatureTemplates = new List<CreatureTemplate>();
			foreach (CreatureTemplate template in this.fLibrary.Templates)
			{
				if (template == null || Session.FindTemplate(template.ID, SearchType.External) == null)
				{
					continue;
				}
				creatureTemplates.Add(template);
			}
			foreach (CreatureTemplate creatureTemplate in creatureTemplates)
			{
				this.fLibrary.Templates.Remove(creatureTemplate);
			}
			List<MonsterTheme> monsterThemes = new List<MonsterTheme>();
			foreach (MonsterTheme theme in this.fLibrary.Themes)
			{
				if (theme == null || Session.FindTheme(theme.ID, SearchType.External) == null)
				{
					continue;
				}
				monsterThemes.Add(theme);
			}
			foreach (MonsterTheme monsterTheme in monsterThemes)
			{
				this.fLibrary.Themes.Remove(monsterTheme);
			}
			List<Trap> traps = new List<Trap>();
			foreach (Trap trap in this.fLibrary.Traps)
			{
				if (trap == null || Session.FindTrap(trap.ID, SearchType.External) == null)
				{
					continue;
				}
				traps.Add(trap);
			}
			foreach (Trap trap1 in traps)
			{
				this.fLibrary.Traps.Remove(trap1);
			}
			List<SkillChallenge> skillChallenges = new List<SkillChallenge>();
			foreach (SkillChallenge skillChallenge in this.fLibrary.SkillChallenges)
			{
				if (skillChallenge == null || Session.FindSkillChallenge(skillChallenge.ID, SearchType.External) == null)
				{
					continue;
				}
				skillChallenges.Add(skillChallenge);
			}
			foreach (SkillChallenge skillChallenge1 in skillChallenges)
			{
				this.fLibrary.SkillChallenges.Remove(skillChallenge1);
			}
			List<MagicItem> magicItems = new List<MagicItem>();
			foreach (MagicItem magicItem in this.fLibrary.MagicItems)
			{
				if (magicItem == null || Session.FindMagicItem(magicItem.ID, SearchType.External) == null)
				{
					continue;
				}
				magicItems.Add(magicItem);
			}
			foreach (MagicItem magicItem1 in magicItems)
			{
				this.fLibrary.MagicItems.Remove(magicItem1);
			}
			List<Tile> tiles = new List<Tile>();
			foreach (Tile tile in this.fLibrary.Tiles)
			{
				if (tile == null || Session.FindTile(tile.ID, SearchType.External) == null)
				{
					continue;
				}
				tiles.Add(tile);
			}
			foreach (Tile tile1 in tiles)
			{
				this.fLibrary.Tiles.Remove(tile1);
			}
		}

		public void Update()
		{
			this.fLibrary.Update();
			if (this.fPassword == null)
			{
				this.fPassword = "";
			}
			if (this.fPasswordHint == null)
			{
				this.fPasswordHint = "";
			}
			if (this.fParty.XP == 0)
			{
				this.fParty.XP = Experience.GetHeroXP(this.fParty.Level);
			}
			if (this.fAuthor == null)
			{
				this.fAuthor = "";
			}
			if (this.fRegionalMaps == null)
			{
				this.fRegionalMaps = new List<RegionalMap>();
			}
			foreach (RegionalMap fRegionalMap in this.fRegionalMaps)
			{
				foreach (MapLocation location in fRegionalMap.Locations)
				{
					if (location.Category != null)
					{
						continue;
					}
					location.Category = "";
				}
				Program.SetResolution(fRegionalMap.Image);
			}
			foreach (Hero fHero in this.fHeroes)
			{
				if (fHero.Key == null)
				{
					fHero.Key = "";
				}
				if (fHero.Level == 0)
				{
					fHero.Level = this.fParty.Level;
				}
				if (fHero.Effects == null)
				{
					fHero.Effects = new List<OngoingCondition>();
				}
				foreach (OngoingCondition effect in fHero.Effects)
				{
					if (effect.Defences == null)
					{
						effect.Defences = new List<DefenceType>();
					}
					if (effect.DamageModifier == null)
					{
						effect.DamageModifier = new DamageModifier();
					}
					if (effect.Regeneration == null)
					{
						effect.Regeneration = new Regeneration();
					}
					if (effect.Aura != null)
					{
						continue;
					}
					effect.Aura = new Aura();
				}
				if (fHero.Tokens == null)
				{
					fHero.Tokens = new List<CustomToken>();
				}
				foreach (CustomToken token in fHero.Tokens)
				{
					if (token.TerrainPower == null || !(token.TerrainPower.ID == Guid.Empty))
					{
						continue;
					}
					token.ID = Guid.NewGuid();
				}
				if (fHero.Portrait != null)
				{
					Program.SetResolution(fHero.Portrait);
				}
				if (fHero.CombatData != null)
				{
					continue;
				}
				fHero.CombatData = new CombatData();
			}
			if (this.fInactiveHeroes == null)
			{
				this.fInactiveHeroes = new List<Hero>();
			}
			foreach (Hero fInactiveHero in this.fInactiveHeroes)
			{
				if (fInactiveHero.Effects == null)
				{
					fInactiveHero.Effects = new List<OngoingCondition>();
				}
				foreach (OngoingCondition defenceTypes in fInactiveHero.Effects)
				{
					if (defenceTypes.Defences == null)
					{
						defenceTypes.Defences = new List<DefenceType>();
					}
					if (defenceTypes.DamageModifier == null)
					{
						defenceTypes.DamageModifier = new DamageModifier();
					}
					if (defenceTypes.Regeneration != null)
					{
						continue;
					}
					defenceTypes.Regeneration = new Regeneration();
				}
				if (fInactiveHero.Tokens == null)
				{
					fInactiveHero.Tokens = new List<CustomToken>();
				}
				foreach (CustomToken customToken in fInactiveHero.Tokens)
				{
					if (customToken.TerrainPower == null || !(customToken.TerrainPower.ID == Guid.Empty))
					{
						continue;
					}
					customToken.ID = Guid.NewGuid();
				}
				if (fInactiveHero.Portrait != null)
				{
					Program.SetResolution(fInactiveHero.Portrait);
				}
				if (fInactiveHero.CombatData != null)
				{
					continue;
				}
				fInactiveHero.CombatData = new CombatData();
			}
			if (this.fNPCs == null)
			{
				this.fNPCs = new List<NPC>();
			}
			while (this.fNPCs.Contains(null))
			{
				this.fNPCs.Remove(null);
			}
			foreach (NPC fNPC in this.fNPCs)
			{
				if (fNPC == null)
				{
					continue;
				}
				if (fNPC.Auras == null)
				{
					fNPC.Auras = new List<Aura>();
				}
				foreach (Aura aura in fNPC.Auras)
				{
					if (aura.Keywords != null)
					{
						continue;
					}
					aura.Keywords = "";
				}
				if (fNPC.CreaturePowers == null)
				{
					fNPC.CreaturePowers = new List<CreaturePower>();
				}
				CreatureHelper.UpdateRegen(fNPC);
				foreach (CreaturePower creaturePower in fNPC.CreaturePowers)
				{
					CreatureHelper.UpdatePowerRange(fNPC, creaturePower);
				}
				if (fNPC.Tactics == null)
				{
					fNPC.Tactics = "";
				}
				if (fNPC.Image == null)
				{
					continue;
				}
				Program.SetResolution(fNPC.Image);
			}
			while (this.fCustomCreatures.Contains(null))
			{
				this.fCustomCreatures.Remove(null);
			}
			foreach (CustomCreature fCustomCreature in this.fCustomCreatures)
			{
				if (fCustomCreature == null)
				{
					continue;
				}
				if (fCustomCreature.Auras == null)
				{
					fCustomCreature.Auras = new List<Aura>();
				}
				foreach (Aura aura1 in fCustomCreature.Auras)
				{
					if (aura1.Keywords != null)
					{
						continue;
					}
					aura1.Keywords = "";
				}
				if (fCustomCreature.CreaturePowers == null)
				{
					fCustomCreature.CreaturePowers = new List<CreaturePower>();
				}
				if (fCustomCreature.DamageModifiers == null)
				{
					fCustomCreature.DamageModifiers = new List<DamageModifier>();
				}
				CreatureHelper.UpdateRegen(fCustomCreature);
				foreach (CreaturePower creaturePower1 in fCustomCreature.CreaturePowers)
				{
					CreatureHelper.UpdatePowerRange(fCustomCreature, creaturePower1);
				}
				if (fCustomCreature.Tactics == null)
				{
					fCustomCreature.Tactics = "";
				}
				if (fCustomCreature.Image == null)
				{
					continue;
				}
				Program.SetResolution(fCustomCreature.Image);
			}
			if (this.fCalendars == null)
			{
				this.fCalendars = new List<Calendar>();
			}
			if (this.fEncyclopedia == null)
			{
				this.fEncyclopedia = new Masterplan.Data.Encyclopedia();
			}
			while (this.fEncyclopedia.Entries.Contains(null))
			{
				this.fEncyclopedia.Entries.Remove(null);
			}
			foreach (EncyclopediaEntry entry in this.fEncyclopedia.Entries)
			{
				if (entry.Category == null)
				{
					entry.Category = "";
				}
				if (entry.DMInfo == null)
				{
					entry.DMInfo = "";
				}
				if (entry.Images == null)
				{
					entry.Images = new List<EncyclopediaImage>();
				}
				foreach (EncyclopediaImage image in entry.Images)
				{
					Program.SetResolution(image.Image);
				}
			}
			if (this.fEncyclopedia.Groups == null)
			{
				this.fEncyclopedia.Groups = new List<EncyclopediaGroup>();
			}
			if (this.fNotes == null)
			{
				this.fNotes = new List<Note>();
			}
			foreach (Note fNote in this.fNotes)
			{
				if (fNote.Category != null)
				{
					continue;
				}
				fNote.Category = "";
			}
			if (this.fAttachments == null)
			{
				this.fAttachments = new List<Attachment>();
			}
			if (this.fBackgrounds == null)
			{
				this.fBackgrounds = new List<Background>();
				this.SetStandardBackgroundItems();
			}
			if (this.fTreasureParcels == null)
			{
				this.fTreasureParcels = new List<Parcel>();
			}
			if (this.fPlayerOptions == null)
			{
				this.fPlayerOptions = new List<IPlayerOption>();
			}
			if (this.fSavedCombats == null)
			{
				this.fSavedCombats = new List<CombatState>();
			}
			foreach (CombatState fSavedCombat in this.fSavedCombats)
			{
				if (fSavedCombat.Encounter.Waves == null)
				{
					fSavedCombat.Encounter.Waves = new List<EncounterWave>();
				}
				if (fSavedCombat.Sketches == null)
				{
					fSavedCombat.Sketches = new List<MapSketch>();
				}
				if (fSavedCombat.Log == null)
				{
					fSavedCombat.Log = new EncounterLog();
				}
				foreach (OngoingCondition quickEffect in fSavedCombat.QuickEffects)
				{
					if (quickEffect.Defences == null)
					{
						quickEffect.Defences = new List<DefenceType>();
					}
					if (quickEffect.DamageModifier == null)
					{
						quickEffect.DamageModifier = new DamageModifier();
					}
					if (quickEffect.Regeneration == null)
					{
						quickEffect.Regeneration = new Regeneration();
					}
					if (quickEffect.Aura != null)
					{
						continue;
					}
					quickEffect.Aura = new Aura();
				}
			}
			if (this.fAddInData == null)
			{
				this.fAddInData = new Dictionary<string, string>();
			}
			if (this.fCampaignSettings == null)
			{
				this.fCampaignSettings = new Masterplan.Data.CampaignSettings();
			}
			if (this.fCampaignSettings.XP == 0)
			{
				this.fCampaignSettings.XP = 1;
			}
			this.update_plot(this.fPlot);
		}

		private void update_plot(Masterplan.Data.Plot p)
		{
			if (p.Goals == null)
			{
				p.Goals = new PartyGoals();
			}
			if (p.FiveByFive == null)
			{
				p.FiveByFive = new FiveByFiveData();
			}
			foreach (PlotPoint point in p.Points)
			{
				if (point.ReadAloud == null)
				{
					point.ReadAloud = "";
				}
				if (point.Parcels == null)
				{
					point.Parcels = new List<Parcel>();
				}
				if (point.EncyclopediaEntryIDs == null)
				{
					point.EncyclopediaEntryIDs = new List<Guid>();
				}
				if (point.Element is Encounter)
				{
					Encounter element = point.Element as Encounter;
					if (element.Traps == null)
					{
						element.Traps = new List<Trap>();
					}
					foreach (Trap trap in element.Traps)
					{
						if (trap.Description == null)
						{
							trap.Description = "";
						}
						if (trap.Attacks == null)
						{
							trap.Attacks = new List<TrapAttack>();
						}
						if (trap.Attack != null)
						{
							trap.Attacks.Add(trap.Attack);
							trap.Initiative = (trap.Attack.HasInitiative ? trap.Attack.Initiative : -2147483648);
							trap.Trigger = trap.Attack.Trigger;
							trap.Attack = null;
						}
						foreach (TrapAttack attack in trap.Attacks)
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
						if (trap.Trigger == null)
						{
							trap.Trigger = "";
						}
						foreach (TrapSkillData skill in trap.Skills)
						{
							if (skill.ID != Guid.Empty)
							{
								continue;
							}
							skill.ID = Guid.NewGuid();
						}
					}
					if (element.SkillChallenges == null)
					{
						element.SkillChallenges = new List<SkillChallenge>();
					}
					foreach (SkillChallenge skillChallenge in element.SkillChallenges)
					{
						if (skillChallenge.Notes == null)
						{
							skillChallenge.Notes = "";
						}
						foreach (SkillChallengeData skillChallengeResult in skillChallenge.Skills)
						{
							if (skillChallengeResult.Results != null)
							{
								continue;
							}
							skillChallengeResult.Results = new SkillChallengeResult();
						}
					}
					if (element.CustomTokens == null)
					{
						element.CustomTokens = new List<CustomToken>();
					}
					foreach (CustomToken customToken in element.CustomTokens)
					{
						if (customToken.TerrainPower == null || !(customToken.TerrainPower.ID == Guid.Empty))
						{
							continue;
						}
						customToken.TerrainPower.ID = Guid.NewGuid();
					}
					if (element.Notes == null)
					{
						element.Notes = new List<EncounterNote>();
						element.SetStandardEncounterNotes();
					}
					if (element.Waves == null)
					{
						element.Waves = new List<EncounterWave>();
					}
					foreach (EncounterSlot allSlot in element.AllSlots)
					{
						allSlot.SetDefaultDisplayNames();
						foreach (CombatData combatDatum in allSlot.CombatData)
						{
							combatDatum.Initiative = -2147483648;
							if (combatDatum.ID == Guid.Empty)
							{
								combatDatum.ID = Guid.NewGuid();
							}
							if (combatDatum.UsedPowers != null)
							{
								continue;
							}
							combatDatum.UsedPowers = new List<Guid>();
						}
					}
				}
				if (point.Element is SkillChallenge)
				{
					SkillChallenge level = point.Element as SkillChallenge;
					if (level.ID == Guid.Empty)
					{
						level.ID = Guid.NewGuid();
					}
					if (level.Name == null)
					{
						level.Name = "Skill Challenge";
					}
					if (level.Level <= 0)
					{
						level.Level = this.fParty.Level;
					}
					if (level.Notes == null)
					{
						level.Notes = "";
					}
					foreach (SkillChallengeData skillChallengeDatum in level.Skills)
					{
						if (skillChallengeDatum.Difficulty == Difficulty.Random)
						{
							skillChallengeDatum.Difficulty = Difficulty.Moderate;
						}
						if (skillChallengeDatum.Results != null)
						{
							continue;
						}
						skillChallengeDatum.Results = new SkillChallengeResult();
					}
				}
				if (point.Element is TrapElement)
				{
					TrapElement trapAttacks = point.Element as TrapElement;
					if (trapAttacks.Trap.Description == null)
					{
						trapAttacks.Trap.Description = "";
					}
					if (trapAttacks.Trap.Attacks == null)
					{
						trapAttacks.Trap.Attacks = new List<TrapAttack>();
					}
					if (trapAttacks.Trap.Attack != null)
					{
						trapAttacks.Trap.Attacks.Add(trapAttacks.Trap.Attack);
						trapAttacks.Trap.Initiative = (trapAttacks.Trap.Attack.HasInitiative ? trapAttacks.Trap.Attack.Initiative : -2147483648);
						trapAttacks.Trap.Trigger = trapAttacks.Trap.Attack.Trigger;
						trapAttacks.Trap.Attack = null;
					}
					foreach (TrapAttack trapAttack in trapAttacks.Trap.Attacks)
					{
						if (trapAttack.ID == Guid.Empty)
						{
							trapAttack.ID = Guid.NewGuid();
						}
						if (trapAttack.Name == null)
						{
							trapAttack.Name = "Attack";
						}
						if (trapAttack.Keywords == null)
						{
							trapAttack.Keywords = "";
						}
						if (trapAttack.Notes != null)
						{
							continue;
						}
						trapAttack.Notes = "";
					}
					if (trapAttacks.Trap.Trigger == null)
					{
						trapAttacks.Trap.Trigger = "";
					}
					foreach (TrapSkillData trapSkillDatum in trapAttacks.Trap.Skills)
					{
						if (trapSkillDatum.ID != Guid.Empty)
						{
							continue;
						}
						trapSkillDatum.ID = Guid.NewGuid();
					}
				}
				if (point.Element is Quest)
				{
					Quest first = point.Element as Quest;
					if (first.Type == QuestType.Minor && first.XP == 0)
					{
						first.XP = Experience.GetMinorQuestXP(first.Level).First;
					}
				}
				this.update_plot(point.Subplot);
			}
		}
	}
}