using Generics.WithGenerics;
using Generics.WithoutGenerics;

namespace Generics
{
    class Progam
    {
        static void Main(string[] args)
        {

            //// generics are good for preventing runtime errors
            //List<int> ages = new List<int>(); // so, int is a generics and only int items can be added to ages
            //ages.Add(23);
            //// ages.Add(12.4); doesn't work

            Console.ReadLine();
            DemonstrateTextFileStorage();

            Console.WriteLine();
            Console.WriteLine("Press enter to shut down...");
            Console.ReadLine();
        }

        private static void DemonstrateTextFileStorage()
        {
            List<Person> people = new List<Person>();
            List<LogEntry> logs = new List<LogEntry>();

            string peopleFile = @"C:\tmp\people.csv";
            string logFile = @"C:\tmp\logs.csv";

            PopulateLists(people, logs);

            GenericTextFileProcessor.SaveToTextFile<Person>(people, peopleFile);
            GenericTextFileProcessor.SaveToTextFile<LogEntry>(logs, logFile);

            var newPeople = GenericTextFileProcessor.LoadFromTextFile<Person>(peopleFile);
            var newLogs = GenericTextFileProcessor.LoadFromTextFile<LogEntry>(logFile);

            foreach (var p in newPeople)
            {
                Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive} )");
            }
            foreach (var l in newLogs)
            {
                Console.WriteLine($"{l.ErrorCode}: {l.Message} {l.TimeOfEvent.ToShortTimeString}");
            }


            //OriginalTextFileProcessor.SavePeople(people, peopleFile);
            //var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);

            //foreach (var p in newPeople)
            //{
            //    Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive} )");
            //}
        }

        private static void PopulateLists(List<Person> people, List<LogEntry> logs)
        {

            people.Add(new Person { FirstName = "Christian", LastName = "Munger" });
            people.Add(new Person { FirstName = "Sue", LastName = "Storm", IsAlive=false });
            people.Add(new Person { FirstName = "Tom", LastName = "Brady" });

            logs.Add(new LogEntry { Message = "I'm tired", ErrorCode = 9999 });
            logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 1234 });
            logs.Add(new LogEntry { Message = "Hi", ErrorCode = 5234 });



        }
    }
}