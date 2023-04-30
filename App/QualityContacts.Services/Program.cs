namespace QualityContacts.Services
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World, it's me!");

            List<string> numbers = new List<string>();

            numbers.Add("+49 0201 123456");
            numbers.Add("+44 0201123456");
            numbers.Add("0033 0201/123456");
            numbers.Add("0049201123456");
            numbers.Add("(0)201 1234 56");
            numbers.Add("+49 (941) 790-4780");
            numbers.Add("015115011900");
            numbers.Add("+91 09870987 899");
            numbers.Add("[+49] (0)89-800/849-50");
            numbers.Add("+49 (8024) [990-477]");


            Validator validator = new Validator();
            Parser parser = new Parser();

            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            foreach (var number in numbers)
            {
                if (!validator.Validate(number).IsValid)
                {
                    Console.WriteLine($"{number} is not a valid number");
                }
                else
                {
                    var phoneNumber = parser.Parse(number); //phoneNumberUtil.Parse(number, "DE");

                    //Console.WriteLine(phoneNumber.FormattedPhoneNumber);
                }

               
            }

            Console.WriteLine("Finished");

        }
    }
}