namespace System
{
    public static class StringExtensions
    {
        public static string Pluralize(this String str)
        {
            return str.ToUpper().EndsWith("L") ? str.Substring(0, str.Length - 1) + "is" : str + "s";
        }
    }
}
