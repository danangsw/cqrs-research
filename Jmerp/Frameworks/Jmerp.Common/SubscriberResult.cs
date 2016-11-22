using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Commons
{
    public class SubscriberResult
    {
        public bool IsError { get; private set; } = false;
        private IEnumerable<string> errors;

        public IEnumerable<string> Errors
        {
            get { return errors ?? new List<string>(); }
            set { errors = value; }
        }

        public SubscriberResult(bool isError, IEnumerable<string> errorMessages)
        {
            errors = errorMessages;
            IsError = isError;
        }

    }
}
