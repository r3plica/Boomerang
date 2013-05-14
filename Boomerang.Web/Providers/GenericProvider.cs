using Boomerang.Web;
using Boomerang.Web.Data;
using System.Collections.ObjectModel;

namespace Boomerang.Web.Providers
{
    public static class GenericProvider
    {
        public static Collection<GenericType> GetSalaryRates()
        {
            return SalaryRates.Get();
        }

        public static Collection<GenericType> GetTransportTypes()
        {
            return TransportTypes.Get();
        }
    }
}
