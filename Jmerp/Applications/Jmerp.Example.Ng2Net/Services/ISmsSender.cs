using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jmerp.Example.Ng2Net.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
