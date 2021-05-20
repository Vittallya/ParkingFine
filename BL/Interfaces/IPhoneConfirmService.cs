using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IPhoneConfirmService
    {
        int Time { get; }

        Task<bool> SendCodeAsync();

        void SetupProfile(ProfileDto profileDto);
        string ErrorMessage { get; }

        bool CheckCode(string code);
    }
}
