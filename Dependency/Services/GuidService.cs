using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency.Services
{
    public interface IGuidService{
        Guid ToonGuid();
    }

    public class GuidService : IGuidService
    {
        public GuidService(IScopedService scopedService)
        {
            ScopedService = scopedService;
        }

        public IScopedService ScopedService;

        public Guid ToonGuid()
        {
            return ScopedService.ToonGuid();
        }
    }
}
