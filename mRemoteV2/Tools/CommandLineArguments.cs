using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tools
{
    // Adapted from http://qntm.org/cmd
    public class CommandLineArguments
    {
        #region "Protected Fields"
        #endregion
        protected List<Argument> Arguments = new List<Argument>();

        #region "Public Properties"
        public bool EscapeForShell { get; set; }
        #endregion

        #region "Public Methods"
        public void Add(string argument, bool forceQuotes = false)
        {
            Arguments.Add(new Argument(argument, false, forceQuotes));
        }

        public void Add(params string[] argumentArray)
        {
            foreach (string argument in argumentArray)
            {
                Add(argument);
            }
        }

        public void AddFileName(string fileName, bool forceQuotes = false)
        {
            Arguments.Add(new Argument(fileName, true, forceQuotes));
        }

        public override string ToString()
        {
            List<string> processedArguments = new List<string>();

            foreach (Argument argument in Arguments)
            {
                processedArguments.Add(ProcessArgument(argument, EscapeForShell));
            }

            return string.Join(" ", processedArguments.ToArray());
        }

        public static string PrefixFileName(string argument)
        {
            if (argument.StartsWith("-"))
                argument = ".\\" + argument;

            return argument;
        }

        public static string EscapeBackslashes(string argument)
        {
            // Sequence of backslashes followed by a double quote:
            //     double up all the backslashes and escape the double quote
            return Regex.Replace(argument, "(\\\\*)\"", "$1$1\\\"");
        }

        public static string QuoteArgument(string argument, bool forceQuotes = false)
        {
            if (!forceQuotes & !string.IsNullOrEmpty(argument) & !argument.Contains(" "))
                return argument;

            // Sequence of backslashes followed by the end of the string
            // (which will become a double quote):
            //     double up all the backslashes
            argument = Regex.Replace(argument, "(\\\\*)$", "$1$1");

            return "\"" + argument + "\"";
        }

        public static string EscapeShellMetacharacters(string argument)
        {
            return Regex.Replace(argument, "([()%!^\"<>&|])", "^$1");
        }
        #endregion

        #region "Protected Methods"
        protected static string ProcessArgument(Argument argument, bool escapeForShell = false)
        {
            string text = argument.Text;

            if (argument.IsFileName)
                text = PrefixFileName(text);
            text = EscapeBackslashes(text);
            text = QuoteArgument(text, argument.ForceQuotes);
            if (escapeForShell)
                text = EscapeShellMetacharacters(text);

            return text;
        }
        #endregion

        #region "Protected Classes"
        protected class Argument
        {
            public Argument(string text, bool isFileName = false, bool forceQuotes = false)
            {
                this.Text = text;
                this.IsFileName = isFileName;
                this.ForceQuotes = forceQuotes;
            }

            public string Text { get; set; }
            public bool IsFileName { get; set; }
            public bool ForceQuotes { get; set; }
        }
        #endregion
    }
}
