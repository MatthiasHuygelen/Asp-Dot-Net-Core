using System;

namespace Dependency.Services
{
    public class TransientService : ITransientService
    {
        private Guid _guid;

        public TransientService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid ToonGuid()
        {
            return _guid;
        }
    }
}
