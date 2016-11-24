using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Tests.Mocks
{
    public static class CustomerLists
    {
        public static readonly Customer Customer_CS00001 = new Customer(CustomerIds.CS00001, CustomerGeneralInfos.GeneralInfo_CS00001);
        public static readonly Customer Customer_CS00002 = new Customer(CustomerIds.CS00002, CustomerGeneralInfos.GeneralInfo_CS00002);
        public static readonly Customer Customer_CS00003 = new Customer(CustomerIds.CS00003, CustomerGeneralInfos.GeneralInfo_CS00003);

        public static IEnumerable<Customer> GetCustomers()
        {
            var fieldInfos = typeof(CustomerLists).GetFields(BindingFlags.Public | BindingFlags.Static);
            return fieldInfos.Select(fi => (Customer)fi.GetValue(null));
        }
    }

    public static class CustomerIds
    {
        public static readonly CustomerId CS00001 = new CustomerId("CS00001");
        public static readonly CustomerId CS00002 = new CustomerId("CS00002");
        public static readonly CustomerId CS00003 = new CustomerId("CS00003");
    }

    public static class CustomerGeneralInfos
    {
        public static readonly GeneralInfo GeneralInfo_CS00001 = new GeneralInfo(
                                                                "PT. ABC",
                                                                "Mr. John",
                                                                "0123456789",
                                                                "",
                                                                "john@abc.com",
                                                                "http://www.abc.co.id"
                                                                );
        public static readonly GeneralInfo GeneralInfo_CS00002 = new GeneralInfo(
                                                                "PT. DEF",
                                                                "Mr. Smith",
                                                                "(012)8888888",
                                                                "012-888-8888",
                                                                "smith@mail.def.co.id",
                                                                "http://www.def.co.id"
                                                                );
        public static readonly GeneralInfo GeneralInfo_CS00003 = new GeneralInfo(
                                                                "PT. BLABLAH INDONESIA",
                                                                "Mrs. Ginny",
                                                                "012-345-6789",
                                                                "012 345 6789",
                                                                "john@infoblabla.wordpress.org",
                                                                "http://infoblabla.wordpress.org"
                                                                );
    }
}
