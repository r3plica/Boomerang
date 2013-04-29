using Core;
using Core.Data;
using System.Collections.ObjectModel;

namespace Core.Components
{
    public static class GenericManager
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
