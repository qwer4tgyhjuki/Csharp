using Microsoft.CodeAnalysis.CSharp.Scripting;
public class Program
{
    /*
     * Method reads file from current folder.
     * User must enter integer number and then method gets the text up 
     * to the charcater indexed  by entered number SPACE
     * then make the substring and output the cut text
     */
    public void LoremIpsum()
    {

        string text = File.ReadAllText("./lorem_ipsum.txt");
        Console.Write("Enter count words: ");
        string countWords = Console.ReadLine();

        if(!int.TryParse(countWords, out int count))
        {
            Console.WriteLine("Invalid number");
            return;
        }
        if (count <= 0)
        {
            Console.WriteLine("Invalid number");
            return;
        }
        int indexSpace = -1;
        for(int i = 0; i < count; i++)
        {
            indexSpace = text.IndexOf(' ', indexSpace + 1);
            if(indexSpace == -1)
            {
                return;
            }
        }
        string selectedText = text.Substring(0, indexSpace);
        Console.WriteLine(selectedText);
    }
    public void Calculator()
    {
        Console.WriteLine("Enter expression:");
        /*
         * Value type is string. Enter expression using numbers and operators and use
         * method Parse for converting to expression
         */
        string expression = Console.ReadLine();
        try
        {            
            double result = Parse(expression);
            Console.WriteLine("Result: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    double Parse(string expression)
    {
        try
        {
            return CSharpScript.EvaluateAsync<double>(expression).Result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error evaluating the expression.", ex);
        }
    }
    public static void Main()
    {
        Program pr = new Program();
        while (true)
        {
            Console.WriteLine("\nSelect action (0 - Exit, 1 - Lorem Ipsum 2 - Calculator): ");
            char select = Console.ReadLine()[0];
            switch (select)
            {
                case '0':
                    Console.WriteLine("Leaving");
                    Environment.Exit(0);
                    break;
                case '1':
                    pr.LoremIpsum();
                    break;
                case '2':
                    pr.Calculator();
                    break;
                default:
                    break;
            }
        }
    }
}