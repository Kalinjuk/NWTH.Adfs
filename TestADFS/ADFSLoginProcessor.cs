using ADFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestADFS
{
    public class ADFSLoginProcessor : IAdfsAuthProcessor
    {
        public Task TicketCreated(ADFSCreatingTiketContext context)
        {
            throw new NotImplementedException();
        }
    }
}
