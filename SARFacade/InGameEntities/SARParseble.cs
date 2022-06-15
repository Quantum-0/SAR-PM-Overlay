using System;
using System.Text.RegularExpressions;

namespace SAR_Overlay
{
    public abstract class SARParseble
    {
#pragma warning disable CS8618
        protected static Regex Parser { get; }
#pragma warning restore CS8618
        public static SARParseble Parse(string str) => throw new NotImplementedException();
    }
}
