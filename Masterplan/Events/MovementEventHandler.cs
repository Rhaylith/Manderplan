using System;
using System.Drawing;
using Masterplan.Data;
using Utils;

namespace Masterplan.Events
{
	public delegate void MovementEventHandler(IToken token, Point start, Point end);
}