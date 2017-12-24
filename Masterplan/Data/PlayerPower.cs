using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class PlayerPower : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlayerPowerType fType = PlayerPowerType.Encounter;

		private string fReadAloud = "";

		private string fKeywords = "";

		private ActionType fAction = ActionType.Standard;

		private string fRange = "Melee weapon";

		private List<PlayerPowerSection> fSections = new List<PlayerPowerSection>();

		public ActionType Action
		{
			get
			{
				return this.fAction;
			}
			set
			{
				this.fAction = value;
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

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
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

		public string Range
		{
			get
			{
				return this.fRange;
			}
			set
			{
				this.fRange = value;
			}
		}

		public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
			}
		}

		public List<PlayerPowerSection> Sections
		{
			get
			{
				return this.fSections;
			}
			set
			{
				this.fSections = value;
			}
		}

		public PlayerPowerType Type
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

		public PlayerPower()
		{
		}

		public PlayerPower Copy()
		{
			PlayerPower playerPower = new PlayerPower()
			{
				ID = this.fID,
				Name = this.fName,
				Type = this.fType,
				ReadAloud = this.fReadAloud,
				Keywords = this.fKeywords,
				Action = this.fAction,
				Range = this.fRange
			};
			foreach (PlayerPowerSection fSection in this.fSections)
			{
				playerPower.Sections.Add(fSection.Copy());
			}
			return playerPower;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}