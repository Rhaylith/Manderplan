using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class Conditions
	{
		public Conditions()
		{
		}

		public static List<string> GetConditionInfo(string condition)
		{
			List<string> strs = new List<string>(condition.ToLower().Split(null));
			List<string> strs1 = new List<string>();
			if (strs.Contains("blinded"))
			{
				strs1.Add("Grant CA; targets have total concealment; -10 to Perception; can't flank");
			}
			if (strs.Contains("dazed"))
			{
				strs1.Add("Grant CA; one std / move / minor action per turn; no immediate / opportunity actions; can't flank");
			}
			if (strs.Contains("deafened"))
			{
				strs1.Add("Can't hear; -10 to Perception");
			}
			if (strs.Contains("dominated"))
			{
				strs1.Add("Grant CA; can't flank; can't take actions; dominating creature chooses one action on your turn, can make you use at-will powers");
			}
			if (strs.Contains("dying"))
			{
				strs1.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank; make death save each round");
			}
			if (strs.Contains("grabbed"))
			{
				strs1.Add("Can't move (can teleport, can be forced to move)");
			}
			if (strs.Contains("helpless"))
			{
				strs1.Add("Grant CA; can be targeted by coup de grace");
			}
			if (strs.Contains("immobilised"))
			{
				strs1.Add("Can't move (can teleport, can be forced to move)");
			}
			if (strs.Contains("marked"))
			{
				strs1.Add("-2 to attack when your attack doesn't include the marker");
			}
			if (strs.Contains("petrified"))
			{
				strs1.Add("Can't take actions; resist 20 to all damage; unaware of surroundings; don't age");
			}
			if (strs.Contains("prone"))
			{
				strs1.Add("Grant CA to enemies making melee attacks; can't move (can teleport, crawl or be forced to move); +2 defences vs ranged attacks from non-adjacent enemies; -2 to attacks");
			}
			if (strs.Contains("removed"))
			{
				strs1.Add("Can't take actions; no line of sight or effect");
			}
			if (strs.Contains("restrained"))
			{
				strs1.Add("Grant CA; can't move (can teleport); -2 to attacks");
			}
			if (strs.Contains("slowed"))
			{
				strs1.Add("Speed is 2");
			}
			if (strs.Contains("stunned"))
			{
				strs1.Add("Grant CA; can't take actions; can't flank");
			}
			if (strs.Contains("surprised"))
			{
				strs1.Add("Grant CA; can't take actions; can't flank");
			}
			if (strs.Contains("unconscious"))
			{
				strs1.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank");
			}
			if (strs.Contains("weakened"))
			{
				strs1.Add("Attacks deal half damage");
			}
			return strs1;
		}

		public static List<string> GetConditions()
		{
			List<string> strs = new List<string>()
			{
				"Blinded",
				"Dazed",
				"Deafened",
				"Dominated",
				"Dying",
				"Grabbed",
				"Helpless",
				"Immobilised",
				"Marked",
				"Petrified",
				"Prone",
				"Removed from play",
				"Restrained",
				"Slowed",
				"Stunned",
				"Surprised",
				"Unconscious",
				"Weakened"
			};
			return strs;
		}
	}
}