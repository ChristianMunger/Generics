using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.WithoutGenerics
{
    public class OriginalTextFileProcessor
    {

        // what if we wanted to loadlogs in a similar way as we loadpersons.
        // we could just copy and paste LoadPeople... this would take a lot of type changes and copying and pasting
        public static List<Person> LoadPeople(string filePath)
        {
            List<Person> output = new List<Person>();
            Person p;
            var lines = File.ReadAllLines(filePath).ToList();

            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(",");
                p = new Person();
                p.FirstName = vals[0];
                p.isAlive = bool.Parse(vals[1]);
                p.LastName = vals[2];

                output.Add(p);
            }

            return output;
        }


        public static void SavePeople(List<Person> people, string filePath)
        {
            List<string> lines = new List<string>();

            lines.Add("FirstName,IsAlive,LastName");

            foreach (var person in people)
            {
                lines.Add($"{ person.FirstName},{person.isAlive},{person.LastName}");

            }

           File.WriteAllLines(filePath, lines);
        }
    }
}
