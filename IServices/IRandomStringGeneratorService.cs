using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.IServices
{
    public interface IRandomStringGeneratorService
    {
        Task<string> GetRandomProjectCode();
    }
}
