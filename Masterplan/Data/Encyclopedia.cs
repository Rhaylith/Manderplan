using Masterplan;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Encyclopedia
	{
		private List<EncyclopediaEntry> fEntries = new List<EncyclopediaEntry>();

		private List<EncyclopediaLink> fLinks = new List<EncyclopediaLink>();

		private List<EncyclopediaGroup> fGroups = new List<EncyclopediaGroup>();

		public List<EncyclopediaEntry> Entries
		{
			get
			{
				return this.fEntries;
			}
			set
			{
				this.fEntries = value;
			}
		}

		public List<EncyclopediaGroup> Groups
		{
			get
			{
				return this.fGroups;
			}
			set
			{
				this.fGroups = value;
			}
		}

		public List<EncyclopediaLink> Links
		{
			get
			{
				return this.fLinks;
			}
			set
			{
				this.fLinks = value;
			}
		}

		public Encyclopedia()
		{
		}

		public Encyclopedia Copy()
		{
			Encyclopedia encyclopedium = new Encyclopedia();
			foreach (EncyclopediaEntry fEntry in this.fEntries)
			{
				encyclopedium.Entries.Add(fEntry.Copy());
			}
			foreach (EncyclopediaLink fLink in this.fLinks)
			{
				encyclopedium.Links.Add(fLink.Copy());
			}
			foreach (EncyclopediaGroup fGroup in this.fGroups)
			{
				encyclopedium.Groups.Add(fGroup.Copy());
			}
			return encyclopedium;
		}

		public EncyclopediaEntry FindEntry(Guid entry_id)
		{
			EncyclopediaEntry encyclopediaEntry;
			List<EncyclopediaEntry>.Enumerator enumerator = this.fEntries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaEntry current = enumerator.Current;
					if (current.ID != entry_id)
					{
						continue;
					}
					encyclopediaEntry = current;
					return encyclopediaEntry;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public EncyclopediaEntry FindEntry(string name)
		{
			EncyclopediaEntry encyclopediaEntry;
			List<EncyclopediaEntry>.Enumerator enumerator = this.fEntries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaEntry current = enumerator.Current;
					if (current.Name.ToLower() != name.ToLower())
					{
						continue;
					}
					encyclopediaEntry = current;
					return encyclopediaEntry;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public EncyclopediaEntry FindEntryForAttachment(Guid attachment_id)
		{
			EncyclopediaEntry encyclopediaEntry;
			List<EncyclopediaEntry>.Enumerator enumerator = Session.Project.Encyclopedia.Entries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaEntry current = enumerator.Current;
					if (current.AttachmentID != attachment_id)
					{
						continue;
					}
					encyclopediaEntry = current;
					return encyclopediaEntry;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public EncyclopediaGroup FindGroup(Guid entry_id)
		{
			EncyclopediaGroup encyclopediaGroup;
			List<EncyclopediaGroup>.Enumerator enumerator = this.fGroups.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaGroup current = enumerator.Current;
					if (current.ID != entry_id)
					{
						continue;
					}
					encyclopediaGroup = current;
					return encyclopediaGroup;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public EncyclopediaLink FindLink(Guid entry_id_1, Guid entry_id_2)
		{
			EncyclopediaLink encyclopediaLink;
			List<EncyclopediaLink>.Enumerator enumerator = this.fLinks.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EncyclopediaLink current = enumerator.Current;
					if (!current.EntryIDs.Contains(entry_id_1) || !current.EntryIDs.Contains(entry_id_2))
					{
						continue;
					}
					encyclopediaLink = current;
					return encyclopediaLink;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		public void Import(Encyclopedia enc)
		{
			if (enc == null)
			{
				return;
			}
			foreach (EncyclopediaEntry entry in enc.Entries)
			{
				EncyclopediaEntry encyclopediaEntry = this.FindEntry(entry.ID);
				if (encyclopediaEntry != null)
				{
					this.Entries.Remove(encyclopediaEntry);
				}
				this.Entries.Add(entry);
			}
			foreach (EncyclopediaGroup group in enc.Groups)
			{
				EncyclopediaGroup encyclopediaGroup = this.FindGroup(group.ID);
				if (encyclopediaGroup != null)
				{
					this.Groups.Remove(encyclopediaGroup);
				}
				this.Groups.Add(group);
			}
			foreach (EncyclopediaLink link in enc.Links)
			{
				EncyclopediaLink encyclopediaLink = this.FindLink(link.EntryIDs[0], link.EntryIDs[1]);
				if (encyclopediaLink != null)
				{
					this.Links.Remove(encyclopediaLink);
				}
				this.Links.Add(link);
			}
		}
	}
}