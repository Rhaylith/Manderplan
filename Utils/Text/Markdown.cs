using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utils.Text
{
	public class Markdown
	{
		private const string _version = "1.13";

		private const int _nestDepth = 6;

		private const int _tabWidth = 4;

		private const string _markerUL = "[*+-]";

		private const string _markerOL = "\\d+[.]";

		private string _emptyElementSuffix = " />";

		private bool _linkEmails = true;

		private bool _strictBoldItalic;

		private bool _autoNewlines;

		private bool _autoHyperlink;

		private bool _encodeProblemUrlCharacters;

		private readonly static Dictionary<string, string> _escapeTable;

		private readonly static Dictionary<string, string> _invertedEscapeTable;

		private readonly static Dictionary<string, string> _backslashEscapeTable;

		private readonly Dictionary<string, string> _urls = new Dictionary<string, string>();

		private readonly Dictionary<string, string> _titles = new Dictionary<string, string>();

		private readonly Dictionary<string, string> _htmlBlocks = new Dictionary<string, string>();

		private int _listLevel;

		private static Regex _newlinesLeadingTrailing;

		private static Regex _newlinesMultiple;

		private static Regex _leadingWhitespace;

		private static string _nestedBracketsPattern;

		private static string _nestedParensPattern;

		private static Regex _linkDef;

		private static Regex _blocksHtml;

		private static Regex _htmlTokens;

		private static Regex _anchorRef;

		private static Regex _anchorInline;

		private static Regex _anchorRefShortcut;

		private static Regex _imagesRef;

		private static Regex _imagesInline;

		private static Regex _headerSetext;

		private static Regex _headerAtx;

		private static Regex _horizontalRules;

		private static string _wholeList;

		private static Regex _listNested;

		private static Regex _listTopLevel;

		private static Regex _codeBlock;

		private static Regex _codeSpan;

		private static Regex _bold;

		private static Regex _strictBold;

		private static Regex _italic;

		private static Regex _strictItalic;

		private static Regex _blockquote;

		private static Regex _autolinkBare;

		private static Regex _outDent;

		private static Regex _codeEncoder;

		private static Regex _amps;

		private static Regex _angles;

		private static Regex _backslashEscapes;

		private static Regex _unescapes;

		private static char[] _problemUrlChars;

		public bool AutoHyperlink
		{
			get
			{
				return this._autoHyperlink;
			}
			set
			{
				this._autoHyperlink = value;
			}
		}

		public bool AutoNewLines
		{
			get
			{
				return this._autoNewlines;
			}
			set
			{
				this._autoNewlines = value;
			}
		}

		public string EmptyElementSuffix
		{
			get
			{
				return this._emptyElementSuffix;
			}
			set
			{
				this._emptyElementSuffix = value;
			}
		}

		public bool EncodeProblemUrlCharacters
		{
			get
			{
				return this._encodeProblemUrlCharacters;
			}
			set
			{
				this._encodeProblemUrlCharacters = value;
			}
		}

		public bool LinkEmails
		{
			get
			{
				return this._linkEmails;
			}
			set
			{
				this._linkEmails = value;
			}
		}

		public bool StrictBoldItalic
		{
			get
			{
				return this._strictBoldItalic;
			}
			set
			{
				this._strictBoldItalic = value;
			}
		}

		public string Version
		{
			get
			{
				return "1.13";
			}
		}

		static Markdown()
		{
			Markdown._newlinesLeadingTrailing = new Regex("^\\n+|\\n+\\z", RegexOptions.Compiled);
			Markdown._newlinesMultiple = new Regex("\\n{2,}", RegexOptions.Compiled);
			Markdown._leadingWhitespace = new Regex("^[ ]*", RegexOptions.Compiled);
			Markdown._linkDef = new Regex(string.Format("\r\n                        ^[ ]{{0,{0}}}\\[(.+)\\]:  # id = $1\r\n                          [ ]*\r\n                          \\n?                   # maybe *one* newline\r\n                          [ ]*\r\n                        <?(\\S+?)>?              # url = $2\r\n                          [ ]*\r\n                          \\n?                   # maybe one newline\r\n                          [ ]*\r\n                        (?:\r\n                            (?<=\\s)             # lookbehind for whitespace\r\n                            [\"(]\r\n                            (.+?)               # title = $3\r\n                            [\")]\r\n                            [ ]*\r\n                        )?                      # title is optional\r\n                        (?:\\n+|\\Z)", 3), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._blocksHtml = new Regex(Markdown.GetBlockPattern(), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			Markdown._htmlTokens = new Regex(string.Concat("\r\n            (<!(?:--.*?--\\s*)+>)|        # match <!-- foo -->\r\n            (<\\?.*?\\?>)|                 # match <?foo?> ", Markdown.RepeatString(" \r\n            (<[A-Za-z\\/!$](?:[^<>]|", 6), Markdown.RepeatString(")*>)", 6), " # match <tag> and </tag>"), RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorRef = new Regex(string.Format("\r\n            (                               # wrap whole match in $1\r\n                \\[\r\n                    ({0})                   # link text = $2\r\n                \\]\r\n\r\n                [ ]?                        # one optional space\r\n                (?:\\n[ ]*)?                 # one optional newline followed by spaces\r\n\r\n                \\[\r\n                    (.*?)                   # id = $3\r\n                \\]\r\n            )", Markdown.GetNestedBracketsPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorInline = new Regex(string.Format("\r\n                (                           # wrap whole match in $1\r\n                    \\[\r\n                        ({0})               # link text = $2\r\n                    \\]\r\n                    \\(                      # literal paren\r\n                        [ ]*\r\n                        ({1})               # href = $3\r\n                        [ ]*\r\n                        (                   # $4\r\n                        (['\"])           # quote char = $5\r\n                        (.*?)               # title = $6\r\n                        \\5                  # matching quote\r\n                        [ ]*                # ignore any spaces between closing quote and )\r\n                        )?                  # title is optional\r\n                    \\)\r\n                )", Markdown.GetNestedBracketsPattern(), Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._anchorRefShortcut = new Regex("\r\n            (                               # wrap whole match in $1\r\n              \\[\r\n                 ([^\\[\\]]+)                 # link text = $2; can't contain [ or ]\r\n              \\]\r\n            )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._imagesRef = new Regex("\r\n                    (               # wrap whole match in $1\r\n                    !\\[\r\n                        (.*?)       # alt text = $2\r\n                    \\]\r\n\r\n                    [ ]?            # one optional space\r\n                    (?:\\n[ ]*)?     # one optional newline followed by spaces\r\n\r\n                    \\[\r\n                        (.*?)       # id = $3\r\n                    \\]\r\n\r\n                    )", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._imagesInline = new Regex(string.Format("\r\n              (                     # wrap whole match in $1\r\n                !\\[\r\n                    (.*?)           # alt text = $2\r\n                \\]\r\n                \\s?                 # one optional whitespace character\r\n                \\(                  # literal paren\r\n                    [ ]*\r\n                    ({0})           # href = $3\r\n                    [ ]*\r\n                    (               # $4\r\n                    (['\"])       # quote char = $5\r\n                    (.*?)           # title = $6\r\n                    \\5              # matching quote\r\n                    [ ]*\r\n                    )?              # title is optional\r\n                \\)\r\n              )", Markdown.GetNestedParensPattern()), RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._headerSetext = new Regex("\r\n                ^(.+?)\r\n                [ ]*\r\n                \\n\r\n                (=+|-+)     # $1 = string of ='s or -'s\r\n                [ ]*\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._headerAtx = new Regex("\r\n                ^(\\#{1,6})  # $1 = string of #'s\r\n                [ ]*\r\n                (.+?)       # $2 = Header text\r\n                [ ]*\r\n                \\#*         # optional closing #'s (not counted)\r\n                \\n+", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._horizontalRules = new Regex("\r\n            ^[ ]{0,3}         # Leading space\r\n                ([-*_])       # $1: First marker\r\n                (?>           # Repeated marker group\r\n                    [ ]{0,2}  # Zero, one, or two spaces.\r\n                    \\1        # Marker character\r\n                ){2,}         # Group repeated at least twice\r\n                [ ]*          # Trailing spaces\r\n                $             # End of line.\r\n            ", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._wholeList = string.Format("\r\n            (                               # $1 = whole list\r\n              (                             # $2\r\n                [ ]{{0,{1}}}\r\n                ({0})                       # $3 = first list item marker\r\n                [ ]+\r\n              )\r\n              (?s:.+?)\r\n              (                             # $4\r\n                  \\z\r\n                |\r\n                  \\n{{2,}}\r\n                  (?=\\S)\r\n                  (?!                       # Negative lookahead for another list item marker\r\n                    [ ]*\r\n                    {0}[ ]+\r\n                  )\r\n              )\r\n            )", string.Format("(?:{0}|{1})", "[*+-]", "\\d+[.]"), 3);
			Markdown._listNested = new Regex(string.Concat("^", Markdown._wholeList), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._listTopLevel = new Regex(string.Concat("(?:(?<=\\n\\n)|\\A\\n?)", Markdown._wholeList), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._codeBlock = new Regex(string.Format("\r\n                    (?:\\n\\n|\\A\\n?)\r\n                    (                        # $1 = the code block -- one or more lines, starting with a space\r\n                    (?:\r\n                        (?:[ ]{{{0}}})       # Lines must start with a tab-width of spaces\r\n                        .*\\n+\r\n                    )+\r\n                    )\r\n                    ((?=^[ ]{{0,{0}}}\\S)|\\Z) # Lookahead for non-space at line-start, or end of doc", 4), RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._codeSpan = new Regex("\r\n                    (?<!\\\\)   # Character before opening ` can't be a backslash\r\n                    (`+)      # $1 = Opening run of `\r\n                    (.+?)     # $2 = The code block\r\n                    (?<!`)\r\n                    \\1\r\n                    (?!`)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._bold = new Regex("(\\*\\*|__) (?=\\S) (.+?[*_]*) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._strictBold = new Regex("([\\W_]|^) (\\*\\*|__) (?=\\S) ([^\\r]*?\\S[\\*_]*) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._italic = new Regex("(\\*|_) (?=\\S) (.+?) (?<=\\S) \\1", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._strictItalic = new Regex("([\\W_]|^) (\\*|_) (?=\\S) ([^\\r\\*_]*?\\S) \\2 ([\\W_]|$)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Markdown._blockquote = new Regex("\r\n            (                           # Wrap whole match in $1\r\n                (\r\n                ^[ ]*>[ ]?              # '>' at the start of a line\r\n                    .+\\n                # rest of the first line\r\n                (.+\\n)*                 # subsequent consecutive lines\r\n                \\n*                     # blanks\r\n                )+\r\n            )", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			Markdown._autolinkBare = new Regex("(^|\\s)(https?|ftp)(://[-A-Z0-9+&@#/%?=~_|\\[\\]\\(\\)!:,\\.;]*[-A-Z0-9+&@#/%=~_|\\[\\]])($|\\W)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Markdown._outDent = new Regex(string.Concat("^[ ]{1,", 4, "}"), RegexOptions.Multiline | RegexOptions.Compiled);
			Markdown._codeEncoder = new Regex("&|<|>|\\\\|\\*|_|\\{|\\}|\\[|\\]", RegexOptions.Compiled);
			Markdown._amps = new Regex("&(?!(#[0-9]+)|(#[xX][a-fA-F0-9])|([a-zA-Z][a-zA-Z0-9]*);)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			Markdown._angles = new Regex("<(?![A-Za-z/?\\$!])", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			Markdown._unescapes = new Regex("\u001a\\d+\u001a", RegexOptions.Compiled);
			Markdown._problemUrlChars = "\"'*()[]$:".ToCharArray();
			Markdown._escapeTable = new Dictionary<string, string>();
			Markdown._invertedEscapeTable = new Dictionary<string, string>();
			Markdown._backslashEscapeTable = new Dictionary<string, string>();
			string str = "";
			string str1 = "\\`*_{}[]()>#+-.!";
			for (int i = 0; i < str1.Length; i++)
			{
				string str2 = str1[i].ToString();
				string hashKey = Markdown.GetHashKey(str2);
				Markdown._escapeTable.Add(str2, hashKey);
				Markdown._invertedEscapeTable.Add(hashKey, str2);
				Markdown._backslashEscapeTable.Add(string.Concat("\\", str2), hashKey);
				str = string.Concat(str, Regex.Escape(string.Concat("\\", str2)), "|");
			}
			Markdown._backslashEscapes = new Regex(str.Substring(0, str.Length - 1), RegexOptions.Compiled);
		}

		public Markdown()
		{
		}

		public Markdown(MarkdownOptions options)
		{
			this._autoHyperlink = options.AutoHyperlink;
			this._autoNewlines = options.AutoNewlines;
			this._emptyElementSuffix = options.EmptyElementSuffix;
			this._encodeProblemUrlCharacters = options.EncodeProblemUrlCharacters;
			this._linkEmails = options.LinkEmails;
			this._strictBoldItalic = options.StrictBoldItalic;
		}

		private string AnchorInlineEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			string str = match.Groups[3].Value;
			string value1 = match.Groups[6].Value;
			str = this.EncodeProblemUrlChars(str);
			str = this.EscapeBoldItalic(str);
			if (str.StartsWith("<") && str.EndsWith(">"))
			{
				str = str.Substring(1, str.Length - 2);
			}
			string str1 = string.Format("<a href=\"{0}\"", str);
			if (!string.IsNullOrEmpty(value1))
			{
				value1 = value1.Replace("\"", "&quot;");
				value1 = this.EscapeBoldItalic(value1);
				str1 = string.Concat(str1, string.Format(" title=\"{0}\"", value1));
			}
			str1 = string.Concat(str1, string.Format(">{0}</a>", value));
			return str1;
		}

		private string AnchorRefEvaluator(Match match)
		{
			string str;
			string value = match.Groups[1].Value;
			string value1 = match.Groups[2].Value;
			string lowerInvariant = match.Groups[3].Value.ToLowerInvariant();
			if (lowerInvariant == "")
			{
				lowerInvariant = value1.ToLowerInvariant();
			}
			if (!this._urls.ContainsKey(lowerInvariant))
			{
				str = value;
			}
			else
			{
				string item = this._urls[lowerInvariant];
				item = this.EscapeBoldItalic(this.EncodeProblemUrlChars(item));
				str = string.Concat("<a href=\"", item, "\"");
				if (this._titles.ContainsKey(lowerInvariant))
				{
					string str1 = this.EscapeBoldItalic(this._titles[lowerInvariant]);
					str = string.Concat(str, " title=\"", str1, "\"");
				}
				str = string.Concat(str, ">", value1, "</a>");
			}
			return str;
		}

		private string AnchorRefShortcutEvaluator(Match match)
		{
			string str;
			string value = match.Groups[1].Value;
			string value1 = match.Groups[2].Value;
			string str1 = Regex.Replace(value1.ToLowerInvariant(), "[ ]*\\n[ ]*", " ");
			if (!this._urls.ContainsKey(str1))
			{
				str = value;
			}
			else
			{
				string item = this._urls[str1];
				item = this.EscapeBoldItalic(this.EncodeProblemUrlChars(item));
				str = string.Concat("<a href=\"", item, "\"");
				if (this._titles.ContainsKey(str1))
				{
					string str2 = this.EscapeBoldItalic(this._titles[str1]);
					str = string.Concat(str, " title=\"", str2, "\"");
				}
				str = string.Concat(str, ">", value1, "</a>");
			}
			return str;
		}

		private string AtxHeaderEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			int length = match.Groups[1].Value.Length;
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), length);
		}

		private string BlockQuoteEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			value = Regex.Replace(value, "^[ ]*>[ ]?", "", RegexOptions.Multiline);
			value = Regex.Replace(value, "^[ ]+$", "", RegexOptions.Multiline);
			value = this.RunBlockGamut(value);
			value = Regex.Replace(value, "^", "  ", RegexOptions.Multiline);
			value = Regex.Replace(value, "(\\s*<pre>.+?</pre>)", new MatchEvaluator(this.BlockQuoteEvaluator2), RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			return string.Format("<blockquote>\n{0}\n</blockquote>\n\n", value);
		}

		private string BlockQuoteEvaluator2(Match match)
		{
			return Regex.Replace(match.Groups[1].Value, "^  ", "", RegexOptions.Multiline);
		}

		private void Cleanup()
		{
			this.Setup();
		}

		private string CodeBlockEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			value = this.EncodeCode(this.Outdent(value));
			value = Markdown._newlinesLeadingTrailing.Replace(value, "");
			return string.Concat("\n\n<pre><code>", value, "\n</code></pre>\n\n");
		}

		private string CodeSpanEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			value = Regex.Replace(value, "^[ ]*", "");
			value = Regex.Replace(value, "[ ]*$", "");
			return string.Concat("<code>", this.EncodeCode(value), "</code>");
		}

		private string DoAnchors(string text)
		{
			text = Markdown._anchorRef.Replace(text, new MatchEvaluator(this.AnchorRefEvaluator));
			text = Markdown._anchorInline.Replace(text, new MatchEvaluator(this.AnchorInlineEvaluator));
			text = Markdown._anchorRefShortcut.Replace(text, new MatchEvaluator(this.AnchorRefShortcutEvaluator));
			return text;
		}

		private string DoAutoLinks(string text)
		{
			if (this._autoHyperlink)
			{
				text = Markdown._autolinkBare.Replace(text, "$1<$2$3>$4");
			}
			text = Regex.Replace(text, "<((https?|ftp):[^'\">\\s]+)>", new MatchEvaluator(this.HyperlinkEvaluator));
			if (this._linkEmails)
			{
				string str = "<\r\n                      (?:mailto:)?\r\n                      (\r\n                        [-.\\w]+\r\n                        \\@\r\n                        [-a-z0-9]+(\\.[-a-z0-9]+)*\\.[a-z]+\r\n                      )\r\n                      >";
				text = Regex.Replace(text, str, new MatchEvaluator(this.EmailEvaluator), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			}
			return text;
		}

		private string DoBlockQuotes(string text)
		{
			return Markdown._blockquote.Replace(text, new MatchEvaluator(this.BlockQuoteEvaluator));
		}

		private string DoCodeBlocks(string text)
		{
			text = Markdown._codeBlock.Replace(text, new MatchEvaluator(this.CodeBlockEvaluator));
			return text;
		}

		private string DoCodeSpans(string text)
		{
			return Markdown._codeSpan.Replace(text, new MatchEvaluator(this.CodeSpanEvaluator));
		}

		private string DoHardBreaks(string text)
		{
			if (!this._autoNewlines)
			{
				text = Regex.Replace(text, " {2,}\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			else
			{
				text = Regex.Replace(text, "\\n", string.Format("<br{0}\n", this._emptyElementSuffix));
			}
			return text;
		}

		private string DoHeaders(string text)
		{
			text = Markdown._headerSetext.Replace(text, new MatchEvaluator(this.SetextHeaderEvaluator));
			text = Markdown._headerAtx.Replace(text, new MatchEvaluator(this.AtxHeaderEvaluator));
			return text;
		}

		private string DoHorizontalRules(string text)
		{
			return Markdown._horizontalRules.Replace(text, string.Concat("<hr", this._emptyElementSuffix, "\n"));
		}

		private string DoImages(string text)
		{
			text = Markdown._imagesRef.Replace(text, new MatchEvaluator(this.ImageReferenceEvaluator));
			text = Markdown._imagesInline.Replace(text, new MatchEvaluator(this.ImageInlineEvaluator));
			return text;
		}

		private string DoItalicsAndBold(string text)
		{
			if (!this._strictBoldItalic)
			{
				text = Markdown._bold.Replace(text, "<strong>$2</strong>");
				text = Markdown._italic.Replace(text, "<em>$2</em>");
			}
			else
			{
				text = Markdown._strictBold.Replace(text, "$1<strong>$3</strong>$4");
				text = Markdown._strictItalic.Replace(text, "$1<em>$3</em>$4");
			}
			return text;
		}

		private string DoLists(string text)
		{
			if (this._listLevel <= 0)
			{
				text = Markdown._listTopLevel.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			else
			{
				text = Markdown._listNested.Replace(text, new MatchEvaluator(this.ListEvaluator));
			}
			return text;
		}

		private string EmailEvaluator(Match match)
		{
			string str = this.Unescape(match.Groups[1].Value);
			str = string.Concat("mailto:", str);
			str = string.Format("<a href=\"{0}\">{0}</a>", this.EncodeEmailAddress(str));
			return Regex.Replace(str, "\">.+?:", "\">");
		}

		private string EncodeAmpsAndAngles(string s)
		{
			s = Markdown._amps.Replace(s, "&amp;");
			s = Markdown._angles.Replace(s, "&lt;");
			return s;
		}

		private string EncodeCode(string code)
		{
			return Markdown._codeEncoder.Replace(code, new MatchEvaluator(this.EncodeCodeEvaluator));
		}

		private string EncodeCodeEvaluator(Match match)
		{
			string value = match.Value;
			string str = value;
			if (value != null)
			{
				if (str == "&")
				{
					return "&amp;";
				}
				if (str == "<")
				{
					return "&lt;";
				}
				if (str == ">")
				{
					return "&gt;";
				}
			}
			return Markdown._escapeTable[match.Value];
		}

		private string EncodeEmailAddress(string addr)
		{
			StringBuilder stringBuilder = new StringBuilder(addr.Length * 5);
			Random random = new Random();
			string str = addr;
			for (int i = 0; i < str.Length; i++)
			{
				char chr = str[i];
				int num = random.Next(1, 100);
				if ((num > 90 || chr == ':') && chr != '@')
				{
					stringBuilder.Append(chr);
				}
				else if (num >= 45)
				{
					stringBuilder.AppendFormat("&#{0};", (int)chr);
				}
				else
				{
					stringBuilder.AppendFormat("&#x{0:x};", (int)chr);
				}
			}
			return stringBuilder.ToString();
		}

		private string EncodeProblemUrlChars(string url)
		{
			bool flag;
			if (!this._encodeProblemUrlCharacters)
			{
				return url;
			}
			StringBuilder stringBuilder = new StringBuilder(url.Length);
			for (int i = 0; i < url.Length; i++)
			{
				char chr = url[i];
				bool flag1 = Array.IndexOf<char>(Markdown._problemUrlChars, chr) != -1;
				if (flag1 && chr == ':' && i < url.Length - 1)
				{
					if (url[i + 1] == '/')
					{
						flag = false;
					}
					else
					{
						flag = (url[i + 1] < '0' ? true : url[i + 1] > '9');
					}
					flag1 = flag;
				}
				if (!flag1)
				{
					stringBuilder.Append(chr);
				}
				else
				{
					stringBuilder.Append(string.Concat("%", string.Format("{0:x}", (byte)chr)));
				}
			}
			return stringBuilder.ToString();
		}

		private string EscapeBackslashes(string s)
		{
			return Markdown._backslashEscapes.Replace(s, new MatchEvaluator(this.EscapeBackslashesEvaluator));
		}

		private string EscapeBackslashesEvaluator(Match match)
		{
			return Markdown._backslashEscapeTable[match.Value];
		}

		private string EscapeBoldItalic(string s)
		{
			s = s.Replace("*", Markdown._escapeTable["*"]);
			s = s.Replace("_", Markdown._escapeTable["_"]);
			return s;
		}

		private string EscapeSpecialCharsWithinTagAttributes(string text)
		{
			List<Markdown.Token> tokens = this.TokenizeHTML(text);
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (Markdown.Token token in tokens)
			{
				string value = token.Value;
				if (token.Type == Markdown.TokenType.Tag)
				{
					value = value.Replace("\\", Markdown._escapeTable["\\"]);
					value = Regex.Replace(value, "(?<=.)</?code>(?=.)", Markdown._escapeTable["`"]);
					value = this.EscapeBoldItalic(value);
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		private string FormParagraphs(string text)
		{
			string[] item = Markdown._newlinesMultiple.Split(Markdown._newlinesLeadingTrailing.Replace(text, ""));
			for (int i = 0; i < (int)item.Length; i++)
			{
				if (!item[i].StartsWith("\u001a"))
				{
					item[i] = string.Concat(Markdown._leadingWhitespace.Replace(this.RunSpanGamut(item[i]), "<p>"), "</p>");
				}
				else
				{
					item[i] = this._htmlBlocks[item[i]];
				}
			}
			return string.Join("\n\n", item);
		}

		private static string GetBlockPattern()
		{
			string str = "ins|del";
			string str1 = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|address|script|noscript|form|fieldset|iframe|math";
			string str2 = "\r\n            (?>\t\t\t\t            # optional tag attributes\r\n              \\s\t\t\t            # starts with whitespace\r\n              (?>\r\n                [^>\"/]+\t            # text outside quotes\r\n              |\r\n                /+(?!>)\t\t            # slash not followed by >\r\n              |\r\n                \"[^\"]*\"\t\t        # text inside double quotes (tolerate >)\r\n              |\r\n                '[^']*'\t                # text inside single quotes (tolerate >)\r\n              )*\r\n            )?\t\r\n            ";
			string str3 = string.Concat(Markdown.RepeatString(string.Concat("\r\n                (?>\r\n                  [^<]+\t\t\t        # content without tag\r\n                |\r\n                  <\\2\t\t\t        # nested opening tag\r\n                    ", str2, "       # attributes\r\n                  (?>\r\n                      />\r\n                  |\r\n                      >"), 6), ".*?", Markdown.RepeatString("\r\n                      </\\2\\s*>\t        # closing nested tag\r\n                  )\r\n                  |\t\t\t\t\r\n                  <(?!/\\2\\s*>           # other tags with a different name\r\n                  )\r\n                )*", 6));
			string str4 = str3.Replace("\\2", "\\3");
			string str5 = "\r\n            (?>\r\n                  (?>\r\n                    (?<=\\n)     # Starting after a blank line\r\n                    |           # or\r\n                    \\A\\n?       # the beginning of the doc\r\n                  )\r\n                  (             # save in $1\r\n\r\n                    # Match from `\\n<tag>` to `</tag>\\n`, handling nested tags \r\n                    # in between.\r\n                      \r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_b_re)   # start tag = $2\r\n                        $attr>                # attributes followed by > and \\n\r\n                        $content              # content, support nesting\r\n                        </\\2>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n\r\n                  | # Special version for tags of group a.\r\n\r\n                        [ ]{0,$less_than_tab}\r\n                        <($block_tags_a_re)   # start tag = $3\r\n                        $attr>[ ]*\\n          # attributes followed by >\r\n                        $content2             # content, support nesting\r\n                        </\\3>                 # the matching end tag\r\n                        [ ]*                  # trailing spaces\r\n                        (?=\\n+|\\Z)            # followed by a newline or end of document\r\n                      \r\n                  | # Special case just for <hr />. It was easier to make a special \r\n                    # case than to make the other regex more complicated.\r\n                  \r\n                        [ ]{0,$less_than_tab}\r\n                        <(hr)                 # start tag = $2\r\n                        $attr                 # attributes\r\n                        /?>                   # the matching end tag\r\n                        [ ]*\r\n                        (?=\\n{2,}|\\Z)         # followed by a blank line or end of document\r\n                  \r\n                  | # Special case for standalone HTML comments:\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <!-- .*? -->\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                  \r\n                  | # PHP and ASP-style processor instructions (<? and <%)\r\n                  \r\n                      [ ]{0,$less_than_tab}\r\n                      (?s:\r\n                        <([?%])                # $2\r\n                        .*?\r\n                        \\2>\r\n                      )\r\n                      [ ]*\r\n                      (?=\\n{2,}|\\Z)            # followed by a blank line or end of document\r\n                      \r\n                  )\r\n            )";
			str5 = str5.Replace("$less_than_tab", 3.ToString());
			str5 = str5.Replace("$block_tags_b_re", str1);
			str5 = str5.Replace("$block_tags_a_re", str);
			str5 = str5.Replace("$attr", str2);
			return str5.Replace("$content2", str4).Replace("$content", str3);
		}

		private static string GetHashKey(string s)
		{
			int num = Math.Abs(s.GetHashCode());
			return string.Concat("\u001a", num.ToString(), "\u001a");
		}

		private static string GetNestedBracketsPattern()
		{
			if (Markdown._nestedBracketsPattern == null)
			{
				Markdown._nestedBracketsPattern = string.Concat(Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^\\[\\]]+      # Anything other than brackets\r\n                     |\r\n                       \\[\r\n                           ", 6), Markdown.RepeatString(" \\]\r\n                    )*", 6));
			}
			return Markdown._nestedBracketsPattern;
		}

		private static string GetNestedParensPattern()
		{
			if (Markdown._nestedParensPattern == null)
			{
				Markdown._nestedParensPattern = string.Concat(Markdown.RepeatString("\r\n                    (?>              # Atomic matching\r\n                       [^()\\s]+      # Anything other than parens or whitespace\r\n                     |\r\n                       \\(\r\n                           ", 6), Markdown.RepeatString(" \\)\r\n                    )*", 6));
			}
			return Markdown._nestedParensPattern;
		}

		private string HashHTMLBlocks(string text)
		{
			return Markdown._blocksHtml.Replace(text, new MatchEvaluator(this.HtmlEvaluator));
		}

		private string HtmlEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string hashKey = Markdown.GetHashKey(value);
			this._htmlBlocks[hashKey] = value;
			return string.Concat("\n\n", hashKey, "\n\n");
		}

		private string HyperlinkEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			return string.Format("<a href=\"{0}\">{0}</a>", value);
		}

		private string ImageInlineEvaluator(Match match)
		{
			string value = match.Groups[2].Value;
			string str = match.Groups[3].Value;
			string value1 = match.Groups[6].Value;
			value = value.Replace("\"", "&quot;");
			value1 = value1.Replace("\"", "&quot;");
			if (str.StartsWith("<") && str.EndsWith(">"))
			{
				str = str.Substring(1, str.Length - 2);
			}
			str = this.EncodeProblemUrlChars(str);
			str = this.EscapeBoldItalic(str);
			string str1 = string.Format("<img src=\"{0}\" alt=\"{1}\"", str, value);
			if (!string.IsNullOrEmpty(value1))
			{
				value1 = this.EscapeBoldItalic(value1);
				str1 = string.Concat(str1, string.Format(" title=\"{0}\"", value1));
			}
			str1 = string.Concat(str1, this._emptyElementSuffix);
			return str1;
		}

		private string ImageReferenceEvaluator(Match match)
		{
			string str;
			string value = match.Groups[1].Value;
			string value1 = match.Groups[2].Value;
			string lowerInvariant = match.Groups[3].Value.ToLowerInvariant();
			if (lowerInvariant == "")
			{
				lowerInvariant = value1.ToLowerInvariant();
			}
			value1 = value1.Replace("\"", "&quot;");
			if (!this._urls.ContainsKey(lowerInvariant))
			{
				str = value;
			}
			else
			{
				string item = this._urls[lowerInvariant];
				item = this.EscapeBoldItalic(this.EncodeProblemUrlChars(item));
				str = string.Format("<img src=\"{0}\" alt=\"{1}\"", item, value1);
				if (this._titles.ContainsKey(lowerInvariant))
				{
					string str1 = this.EscapeBoldItalic(this._titles[lowerInvariant]);
					str = string.Concat(str, string.Format(" title=\"{0}\"", str1));
				}
				str = string.Concat(str, this._emptyElementSuffix);
			}
			return str;
		}

		private string LinkEvaluator(Match match)
		{
			string lowerInvariant = match.Groups[1].Value.ToLowerInvariant();
			this._urls[lowerInvariant] = this.EncodeAmpsAndAngles(match.Groups[2].Value);
			if (match.Groups[3] != null && match.Groups[3].Length > 0)
			{
				this._titles[lowerInvariant] = match.Groups[3].Value.Replace("\"", "&quot;");
			}
			return "";
		}

		private string ListEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			string str = (Regex.IsMatch(match.Groups[3].Value, "[*+-]") ? "ul" : "ol");
			value = Regex.Replace(value, "\\n{2,}", "\n\n\n");
			string str1 = this.ProcessListItems(value, (str == "ul" ? "[*+-]" : "\\d+[.]"));
			return string.Format("<{0}>\n{1}</{0}>\n", str, str1);
		}

		private string ListItemEvaluator(Match match)
		{
			string value = match.Groups[4].Value;
			if (!string.IsNullOrEmpty(match.Groups[1].Value) || Regex.IsMatch(value, "\\n{2,}"))
			{
				value = this.RunBlockGamut(string.Concat(this.Outdent(value), "\n"));
			}
			else
			{
				value = this.DoLists(this.Outdent(value));
				value = value.TrimEnd(new char[] { '\n' });
				value = this.RunSpanGamut(value);
			}
			return string.Format("<li>{0}</li>\n", value);
		}

		private string Normalize(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			StringBuilder stringBuilder1 = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < text.Length; i++)
			{
				char chr = text[i];
				switch (chr)
				{
					case '\t':
					{
						int length = 4 - stringBuilder1.Length % 4;
						for (int j = 0; j < length; j++)
						{
							stringBuilder1.Append(' ');
						}
						break;
					}
					case '\n':
					{
						if (flag)
						{
							stringBuilder.Append(stringBuilder1);
						}
						stringBuilder.Append('\n');
						stringBuilder1.Length = 0;
						flag = false;
						break;
					}
					case '\v':
					case '\f':
					{
						if (!flag && text[i] != ' ')
						{
							flag = true;
						}
						stringBuilder1.Append(text[i]);
						break;
					}
					case '\r':
					{
						if (i >= text.Length - 1 || text[i + 1] == '\n')
						{
							break;
						}
						if (flag)
						{
							stringBuilder.Append(stringBuilder1);
						}
						stringBuilder.Append('\n');
						stringBuilder1.Length = 0;
						flag = false;
						break;
					}
					default:
					{
						if (chr == '\u001A')
						{
							break;
						}
						goto case '\f';
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append(stringBuilder1);
			}
			stringBuilder.Append('\n');
			return stringBuilder.Append("\n\n").ToString();
		}

		private string Outdent(string block)
		{
			return Markdown._outDent.Replace(block, "");
		}

		private string ProcessListItems(string list, string marker)
		{
			this._listLevel++;
			list = Regex.Replace(list, "\\n{2,}\\z", "\n");
			string str = string.Format("(\\n)?                      # leading line = $1\r\n                (^[ ]*)                    # leading whitespace = $2\r\n                ({0}) [ ]+                 # list marker = $3\r\n                ((?s:.+?)                  # list item text = $4\r\n                (\\n{{1,2}}))      \r\n                (?= \\n* (\\z | \\2 ({0}) [ ]+))", marker);
			list = Regex.Replace(list, str, new MatchEvaluator(this.ListItemEvaluator), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
			this._listLevel--;
			return list;
		}

		private static string RepeatString(string text, int count)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length * count);
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		private string RunBlockGamut(string text)
		{
			text = this.DoHeaders(text);
			text = this.DoHorizontalRules(text);
			text = this.DoLists(text);
			text = this.DoCodeBlocks(text);
			text = this.DoBlockQuotes(text);
			text = this.HashHTMLBlocks(text);
			text = this.FormParagraphs(text);
			return text;
		}

		private string RunSpanGamut(string text)
		{
			text = this.DoCodeSpans(text);
			text = this.EscapeSpecialCharsWithinTagAttributes(text);
			text = this.EscapeBackslashes(text);
			text = this.DoImages(text);
			text = this.DoAnchors(text);
			text = this.DoAutoLinks(text);
			text = this.EncodeAmpsAndAngles(text);
			text = this.DoItalicsAndBold(text);
			text = this.DoHardBreaks(text);
			return text;
		}

		private string SetextHeaderEvaluator(Match match)
		{
			string value = match.Groups[1].Value;
			int num = (match.Groups[2].Value.StartsWith("=") ? 1 : 2);
			return string.Format("<h{1}>{0}</h{1}>\n\n", this.RunSpanGamut(value), num);
		}

		private void Setup()
		{
			this._urls.Clear();
			this._titles.Clear();
			this._htmlBlocks.Clear();
			this._listLevel = 0;
		}

		private string StripLinkDefinitions(string text)
		{
			return Markdown._linkDef.Replace(text, new MatchEvaluator(this.LinkEvaluator));
		}

		private List<Markdown.Token> TokenizeHTML(string text)
		{
			int length = 0;
			int index = 0;
			List<Markdown.Token> tokens = new List<Markdown.Token>();
			foreach (Match match in Markdown._htmlTokens.Matches(text))
			{
				index = match.Index;
				if (length < index)
				{
					tokens.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(length, index - length)));
				}
				tokens.Add(new Markdown.Token(Markdown.TokenType.Tag, match.Value));
				length = index + match.Length;
			}
			if (length < text.Length)
			{
				tokens.Add(new Markdown.Token(Markdown.TokenType.Text, text.Substring(length, text.Length - length)));
			}
			return tokens;
		}

		public string Transform(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			this.Setup();
			text = this.Normalize(text);
			text = this.HashHTMLBlocks(text);
			text = this.StripLinkDefinitions(text);
			text = this.RunBlockGamut(text);
			text = this.Unescape(text);
			this.Cleanup();
			return string.Concat(text, "\n");
		}

		private string Unescape(string s)
		{
			return Markdown._unescapes.Replace(s, new MatchEvaluator(this.UnescapeEvaluator));
		}

		private string UnescapeEvaluator(Match match)
		{
			return Markdown._invertedEscapeTable[match.Value];
		}

		private struct Token
		{
			public Markdown.TokenType Type;

			public string Value;

			public Token(Markdown.TokenType type, string value)
			{
				this.Type = type;
				this.Value = value;
			}
		}

		private enum TokenType
		{
			Text,
			Tag
		}
	}
}