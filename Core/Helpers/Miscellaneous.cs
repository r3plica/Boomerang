using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public static class Miscellaneous
    {
        public static bool IsInDateRange(this string Date, DateTime Now, DateTime Then)
        {
            if (Date != null)
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

        public static bool ParseText(this string Text, string Query)
        {
            bool bFound = false;

            if (Text != null && Query != null)
            {
                if (Text.ToLower().Contains(Query.ToLower()))
                    bFound = true;

                if (Query.IndexOf(",") > 0)
                {
                    if (Text.ToLower().Contains(Query.Substring(0, Query.IndexOf(","))))
                        bFound = true;
                    else
                        bFound = ParseText(Text, Query.Substring(Query.IndexOf(",") + 1));
                }

            }

            return bFound;
        }

        public static void Append(this Collection<Client> Collection, IEnumerable<Client> Items)
        {
            foreach (Client c in Items)
            {
                Collection.Add(c);
            }
        }

        public static void Append(this Collection<Candidate> Collection, IEnumerable<Candidate> Items)
        {
            foreach (Candidate c in Items)
            {
                Collection.Add(c);
            }
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
