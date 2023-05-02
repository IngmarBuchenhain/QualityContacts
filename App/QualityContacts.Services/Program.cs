namespace QualityContacts.Services
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World, it's me!");

            // Add debug tests here if needed.

            var parser = new ContactParser();

            parser.ParseContactInput("AB -Cabc. ");

            Console.WriteLine("Finished");

        }
    }
}