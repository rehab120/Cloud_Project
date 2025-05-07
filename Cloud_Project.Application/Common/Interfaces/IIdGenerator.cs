using Cloud_Project.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Project.Application.Common.Interfaces
{
    public interface IIdGenerator
    {
        string GenerateId<T>(ModelPrefix prefix) where T : class;
    }
}
