using System;

namespace Dependency.Services
{
    public class ScopedService : IScopedService
    {
        private Guid _guid;

        public ScopedService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid ToonGuid()
        {
            return _guid;
        }
    }
}
