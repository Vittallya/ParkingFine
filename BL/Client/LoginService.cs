using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class LoginService
    {
        private readonly AllDbContext dbContext;
        private readonly MapperService mapper;

        public LoginService(AllDbContext dbContext, MapperService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        /// <summary>
        /// Returns a user, whoose phone and pass are equial to input params, otherwise - null
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public async Task<ProfileDto> LoginAsync(string phone, string pass)
        {
            await dbContext.Profiles.LoadAsync();

            var profile = await dbContext.
                Profiles.
                FirstOrDefaultAsync(x => string.Compare(x.PhoneNumber, phone) == 0 && string.Compare(x.Password, pass) == 0);

            if(profile != null)
            {
                return mapper.MapTo<Profile, ProfileDto>(profile);
            }

            return null;
        }

    }
}
