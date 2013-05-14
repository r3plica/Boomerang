using System.Collections.ObjectModel;
using System.Web;

namespace Boomerang.Web.Providers
{
    public static class SessionsProvider
    {
        //public void AddToSession<T>(this T Target, string SessionName)
        //{
        //    if (HttpContext.Current.Session[SessionName] != null)
        //    {
        //        Collection<T> SessionClients = (Collection<T>)HttpContext.Current.Session[SessionName];
        //        SessionClients.Add(Target);
        //        HttpContext.Current.Session.Add(SessionName, SessionClients);
        //    }
        //}

        public static void AddToSession<T>(this T Target, string SessionName)
        {
            if (HttpContext.Current.Session[SessionName] != null)
            {
                Collection<T> SessionCollection = (Collection<T>)HttpContext.Current.Session[SessionName];
                SessionCollection.Add(Target);
                HttpContext.Current.Session.Add(SessionName, SessionCollection);
            }
        }

        //public void RemoveFromSession<T>(this T Target, string SessionName)
        //{
        //    if (HttpContext.Current.Session[SessionName] != null)
        //    {
        //        Collection<T> UpdatedClients = new Collection<T>();
        //        Collection<T> SessionClients = (Collection<T>)HttpContext.Current.Session[SessionName];
        //        foreach (T Client in SessionClients)
        //        {
        //            //if (Client.Id != this.Id)
        //            //    UpdatedClients.Add(Client);
        //        }
        //        HttpContext.Current.Session.Add(SessionName, UpdatedClients);
        //    }
        //}
    }
}
