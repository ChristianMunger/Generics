using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.WithGenerics
{
    public static class GenericTextFileProcessor
    {
        // method returns a List<T> ... T can be any type so we use the delimiter at the end
        // where T : class, new()  limits T to a class(not int, etc.) with an empty constructor(new())
        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            var lines = File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();
            // Reflection => allows to look at an object at runtime and get properties... kinda expensive so use sparingly
            T entry = new T(); // create a new instance of type T ... here's why we need the new() requirement of an empty constructor in the class
            var cols = entry.GetType().GetProperties(); // "put" all properties in cols

            // check to be sure we have at least one header row and one data row
            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was either empty or missing");
            }

            // split header into one column header per entry
            var headers = lines[0].Split(',');

            // remove header from lines so we don't have to worry about skipping the first row
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();
                // splits row into columns. now the index of this row matches the index of the header
                // so the FirstName column header lines up with the FirstName value in this row
                var vals = row.Split(',');

                // loops through each header entry so we can compare that against the list of columns
                // once we get the matching column we can do the SetValue method to set the column value for our entry variable
                // to the vals item at the same index as this particular header
                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                        {
                            col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                        }
                    }
                }
                output.Add(entry);
            }
            return output;
        }

        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "You must populate the data param with at least one object");
            }
            var cols = data[0].GetType().GetProperties();

            // loops through each column and gets the name so it can comma separate into header row
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            // Adds the column header entries to the first line 
            // removing the last comma from the end first 
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();
                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
