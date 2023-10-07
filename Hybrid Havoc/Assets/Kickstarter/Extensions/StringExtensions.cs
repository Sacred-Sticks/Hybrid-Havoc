namespace Kickstarter.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveParenthesis(string input)
        {
            return input.TrimStart('(').TrimEnd(')');
        }
    }
}
