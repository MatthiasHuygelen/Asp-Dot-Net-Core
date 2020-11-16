using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
