using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADFS
{
    public interface IAdfsAuthProcessor
    {
        Task TicketCreated(ADFSCreatingTiketContext context);
    }
}
