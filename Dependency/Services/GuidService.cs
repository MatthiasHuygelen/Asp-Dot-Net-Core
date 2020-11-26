using System;

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
