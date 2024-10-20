using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.Entities;

namespace Contacts.Domain.Repositories
{
    public interface iTokenService
    {
        public  string GetToken(Usuario usuario);
    }
}
