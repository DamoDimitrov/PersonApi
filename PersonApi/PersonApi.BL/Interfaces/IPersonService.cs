using PersonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.BL.Interfaces
{
    internal interface IPersonService
    {
        Task RegisterPersonASync(Person person);
    }
}
