using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Boomerang.Web.Helpers
{
    enum SearchType
    {
        Or,
        And
    }

    public static class Miscellaneous
    {
        public static bool IsInDateRange(this string Date, DateTime Now, DateTime Then)
        {
            if (!string.IsNullOrEmpty(Date))
            {
                if (Date.LastIndexOf("/") < 0)
                    Date.Replace("-", "/");

                string Day = Date.Substring(0, Date.IndexOf("/"));
                string Month = Date.Substring(Date.IndexOf("/") + 1, 2);
                string Year = Date.Substring(Date.LastIndexOf("/") + 1);
                
                System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-gb");
                DateTime CurrentDate = DateTime.Parse(Day + "/" + Month + "/" + Year, cultureinfo);

                if (CurrentDate > Now.AddDays(-1) && CurrentDate < Then)
                    return true;
            }

            return false;
        }

        public static bool ParseText(this string text, string query)
        {
            if (text == null) 
                return false; // If our source text is null, return false

            if (!text.Contains(" ") && !text.Contains(",") && !query.Contains(" ") && !query.Contains(",")) // If we are using single phrases
                return StandardSearch(text.ToLower(), query.ToLower()); // Perform a standard search
            else
            {
                var textArray = GetTextArray(text);

                if (textArray.Count() > 0)
                    return AdvancedSearch(textArray, query); // if our array has items, do an advanced search
                else
                    return AdvancedSearch(text, query); // If our array is empty then do an advanced search (our query must have either spaces or commas)

            }
        }

        private static string[] GetTextArray(string text)
        {
            var textArray = new string[] { }; // Create an empty array

            if (text.Contains(" ") && text.Contains(","))
            {
                var temporaryText = text.Replace(", ", ","); // strip trailing spaces
                temporaryText = temporaryText.Replace(" ,", ","); // strip leading spaces

                if (temporaryText.Contains(" ")) // If we still have spaces
                    temporaryText = string.Join(",", temporaryText.Split(' ')); // replace our temporary text with our new string using Join

                textArray = temporaryText.Split(','); // store our array
            }
            else 
            {
                if (text.Contains(" "))
                    textArray = text.ToLower().Split(' '); // If our text has spaces, then create an array

                if (text.Contains(","))
                    textArray = text.ToLower().Split(','); // if our text has commas, then create an array
            }

            return textArray;
        }

        /// <summary>
        /// This advanced search parses our query against a field and returns a boolean true if we find a match
        /// 
        /// This will need looking at in the future. At the moment it assumes that you will either search for matches using spaces OR commas. If you use both 
        /// then it will not match your items
        /// </summary>
        /// <param name="text">Array of strings to match</param>
        /// <param name="query">Our query</param>
        /// <returns>A boolean</returns>
        private static bool AdvancedSearch(string[] text, string query)
        {
            // Perform AND search (must be done first)
            if (query.Contains(","))
            {
                foreach (string queryPart in query.Split(','))
                    foreach (string textPart in text)
                        if (StandardSearch(textPart.Trim(), queryPart.Trim()))
                            return true;

                return false;
            }

            // Perform OR search
            if (query.Contains(" "))
            {
                foreach (string queryPart in query.Split(' '))
                {
                    var foundMatch = false;

                    foreach (string textPart in text)
                    {
                        if (StandardSearch(textPart, queryPart))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                        return false;
                }

                return true;
            }
            
            foreach (string textPart in text) // If we get this far, then the query is just a single phrase
                if (StandardSearch(textPart, query)) // If we find our text
                    return true; // We have a match

            return false; // Fail safe
        }

        /// <summary>
        /// This advanced search parses our query against a field and returns a boolean true if we find a match
        /// 
        /// This will need looking at in the future. At the moment it assumes that you will either search for matches using spaces OR commas. If you use both 
        /// then it will not match your items
        /// </summary>
        /// <param name="text">our string to match</param>
        /// <param name="query">Our query</param>
        /// <returns>A boolean</returns>
        private static bool AdvancedSearch(string text, string query)
        {
            // Perform OR search
            if (query.Contains(","))
            {
                foreach (string queryPart in query.Split(','))
                    if (StandardSearch(text.Trim(), queryPart.Trim()))
                        return true;
            }

            // Perform AND search
            if (query.Contains(" "))
                return false; // If our query has spaces, then return false because our text does not (spaces are AND searches)

            return false;
        }

        private static bool StandardSearch(string text, string query)
        {
            if (query.Contains('*'))
                return WildcardSearch(text, query);
            else
                if (text.ToLower() == query.ToLower())
                    return true;

            return false;
        }

        private static bool WildcardSearch(string text, string query)
        {
            var queryLength = query.Length - 1;
            var wildcard = query.IndexOf('*');

            if (wildcard == 0)
                return SearchEndOfText(text, query);

            if (wildcard == queryLength)
                return SearchStartOfText(text, query);

            return SearchMiddleOfText(text, query);
        }

        private static bool SearchStartOfText(string text, string query)
        {
            if (text.StartsWith(query.Substring(0, query.IndexOf('*'))))
                return true;

            return false;
        }

        private static bool SearchEndOfText(string text, string query)
        {
            if (text.EndsWith(query.Substring(1)))
                return true;

            return false;
        }

        private static bool SearchMiddleOfText(string text, string query)
        {
            var wildcard = query.IndexOf('*');
            var startText = query.Substring(0, wildcard);
            var endText = query.Substring(wildcard + 1);

            if (text.StartsWith(startText) && text.EndsWith(endText))
                return true;

            return false;
        }

        public static void Append(this Collection<Client> Collection, IEnumerable<Client> Items)
        {
            if (Items != null)
                foreach (Client c in Items)
                    Collection.Add(c);
        }

        public static void Append(this Collection<Candidate> Collection, IEnumerable<Candidate> Items)
        {
            if (Items != null)
                foreach (Candidate c in Items)
                    Collection.Add(c);
        }

        public static void Compare(this Collection<Client> Collection, IEnumerable<Client> Items)
        {
            if (Items.Count() > 0)
            {
                Collection<Client> Temporary = new Collection<Client>();
                foreach (Client c in Collection)
                {
                    Temporary.Add(c);
                }

                Collection.Clear(); // Remove our items
                bool bHasItems = (Temporary.Count() > 0) ? true : false;

                foreach (Client c in Items) // For each item in our enumeration
                {
                    if (bHasItems) // If our current collection has any items
                    {
                        foreach (Client e in Temporary) // For each item in our temporary collection
                        {
                            if (c.Id == e.Id) // If the Id is the same as the id in our items collection
                                Collection.Add(c); // Add to the current collection
                        }
                    }
                    else // If we have no items in our collection
                        Collection.Add(c); // Add all the items to our collection
                }
            }
        }

        public static void Compare(this Collection<Candidate> Collection, IEnumerable<Candidate> Items)
        {
            if (Items.Count() > 0)
            {
                Collection<Candidate> Temporary = new Collection<Candidate>();
                foreach (Candidate c in Collection)
                {
                    Temporary.Add(c);
                }

                Collection.Clear(); // Remove our items
                bool bHasItems = (Temporary.Count() > 0) ? true : false;

                foreach (Candidate c in Items) // For each item in our enumeration
                {
                    if (bHasItems) // If our current collection has any items
                    {
                        foreach (Candidate e in Temporary) // For each item in our temporary collection
                        {
                            if (c.Id == e.Id) // If the Id is the same as the id in our items collection
                                Collection.Add(c); // Add to the current collection
                        }
                    }
                    else // If we have no items in our collection
                        Collection.Add(c); // Add all the items to our collection
                }
            }
        }

        public static Collection<Client> RemoveDuplicates(this Collection<Client> Collection)
        {
            Collection<Client> Stripped = new Collection<Client>();

            foreach (Client c in Collection)
            {
                bool Found = false;
                foreach (Client s in Stripped)
                {
                    if (c.Id == s.Id)
                        Found = true;
                }

                if (!Found)
                    Stripped.Add(c);
            }

            return Stripped;
        }

        public static Collection<Candidate> RemoveDuplicates(this Collection<Candidate> Collection)
        {
            Collection<Candidate> Stripped = new Collection<Candidate>();

            foreach (Candidate c in Collection)
            {
                bool Found = false;
                foreach (Candidate s in Stripped)
                {
                    if (c.Id == s.Id)
                        Found = true;
                }

                if (!Found)
                    Stripped.Add(c);
            }

            return Stripped;
        }

        public static string CutToSize(this string Target, int Size)
        {
            if (Target != null)
                if (Target.Length > Size)
                    return Target.Substring(0, Size) + "...";

            return "";
        }
    }
}
