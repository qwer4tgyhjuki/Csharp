using System.Text.RegularExpressions;

class RegularExpressions
{
    static void Main()
    {
        string inputString = "Hello, my email is joe.barbaro@gmail.com";
        string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

        bool isEmailValid = IsEmailValid(inputString, pattern);

        if (isEmailValid)
        {
            string email = ExtractEmail(inputString, pattern);
            Console.WriteLine($"Valid email found: {email}");
        }
        else
        {
            Console.WriteLine("No valid email found in the input string.");
        }
    }
    static bool IsEmailValid(string input, string pattern)
    {
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }
    static string ExtractEmail(string input, string pattern)
    {
        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);

        if (match.Success)
        {
            return match.Value;
        }
        else
        {
            return null;
        }
    }
}