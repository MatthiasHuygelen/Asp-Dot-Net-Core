using System;

namespace Dependency.Services
{
    public class SingletonService : ISingletonService
    {
        private Guid _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid ToonGuid()
        {
            return _guid;
        }
    }
}
