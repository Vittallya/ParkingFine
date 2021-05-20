using DAL;
using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Events
{
    public class ClientRegistered: IEvent
    {
        public ClientRegistered(ProfileDto user)
        {
            User = user;
        }

        public ProfileDto User { get; set; }

    }
}
