using EventFlow.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using System;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel
{
    public class Customer : Entity<CustomerId>
    {
        public Customer(
            CustomerId id,
            GeneralInfo generalInfo,
            AddressDetail addressDetail = null,
            AccountingDetail accountingDetail = null
            ) 
            : base(id)
        {
            if (generalInfo == null) throw new ArgumentNullException(nameof(generalInfo));

            GeneralInfo = generalInfo;
            AddressDetail = addressDetail;
            AccountingDetail = accountingDetail;
        }

        public GeneralInfo GeneralInfo { get; private set; }
        public AddressDetail AddressDetail { get; private set; }
        public AccountingDetail AccountingDetail { get; private set; }
    }
}
