using System;

namespace Masterplan.Data
{
	public interface IElement
	{
		IElement Copy();

		Difficulty GetDifficulty(int party_level, int party_size);

		int GetXP();
	}
}