using EventFlow.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel
{
    public class Customer : Entity<CustomerId>
    {
        public Customer(
            CustomerId id,
            GeneralInfo generalInfo
            ) 
            : base(id)
        {
            GeneralInfo = generalInfo;
        }

        public GeneralInfo GeneralInfo { get; private set; }
    }
}
