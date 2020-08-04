using System;

namespace SweepstakesProject
{
    /// <summary>
    /// Static Class that calls all User console interactions.
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Requests input from User. Does not validate Input.
        /// </summary>
        /// <param name="prompt">The message prompt to display to user.</param>
        /// <returns>User's input</returns>
        public static string GetInputFor(string prompt)
        {
            Console.WriteLine(prompt);

            return Console.ReadLine();

        }
        /// <summary>
        /// Requests a yes/no, true/false response from user.
        /// </summary>
        /// <param name="prompt">The message prompt to display to user.</param>
        /// <returns>true for yes, false for no.</returns>
        public static bool GetInputYesNo(string prompt)
        {
            Console.WriteLine(prompt);
            string input = UI.GetInputFor("(y/n)").ToLower();
            if (input == "y" || input == "yes")
            {
                return true;
            }
            else if (input == "n" || input == "no")
            {
                return false;
            }
            else
            {
                return UI.GetInputYesNo(prompt);
            }
        }
        /// <summary>
        /// Static Call to display text. May be modified as UI design changes.
        /// </summary>
        /// <param name="text">Text to display to the user.</param>
        public static void DisplayText(string text)
        {
            Console.WriteLine(text);
        }

    }
}
