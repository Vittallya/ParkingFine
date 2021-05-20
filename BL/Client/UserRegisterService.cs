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
    public class UserRegisterService
    {
        private readonly AllDbContext db;
        private readonly UserService userService;
        private readonly MapperService mapper;
        public int DelayIntervalSec { get; } = 3;
        public string ErrorMessage { get; private set; }
        public bool IsBeginned { get; private set; }
        public UserRegisterService( AllDbContext allDbContext,
            UserService userService,
            MapperService mapper)
        {
            this.db = allDbContext;
            this.userService = userService;
            this.mapper = mapper;
        }

        ProfileDto _curProfile;
        public bool IsProfileSettedUp => _curProfile != null;
        public void SetupProfileData(ProfileDto dto)
        {
            IsBeginned = true;
            _curProfile = dto;
        }
        public ProfileDto GetProfile()
        {
            if (_curProfile == null)
                throw new ArgumentException("Профиль не был загружен");

            return _curProfile;
        }

        string _passw;

        /// <summary>
        /// УСТАНОВКА КАК ТЕКЩУЕГО ПОЛЬЗОВАТЕЛЯ НЕ ПРОИЗВОДИТСЯ
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public void SetupPass(string pass)
        {
            _passw = pass;
        }

        /// <summary>
        /// УСТАНОВКА КАК ТЕКЩУЕГО ПОЛЬЗОВАТЕЛЯ НЕ ПРОИЗВОДИТСЯ
        /// </summary>
        /// <returns></returns>

        public async Task<int> RegisterAsync(bool setupUser = true)
        {
            if (_curProfile == null)
                throw new ArgumentException("Профиль не был загружен");


            await db.Profiles.LoadAsync();

            if (await db.Profiles.FirstOrDefaultAsync(x => x.Password == _passw) != null)
            {
                ErrorMessage = "Такой пароль уже есть";
                return -1;
            }

            Profile profile = mapper.MapTo<ProfileDto, Profile>(_curProfile);
            profile.Password = _passw;
            db.Profiles.Add(profile);

            try
            {
                await db.SaveChangesAsync();

                if (setupUser)
                {
                    _curProfile.Id = profile.Id;
                    userService.SetupUser(_curProfile);
                }

                return profile.Id;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return -1;
            }
        }
        public void Clear()
        {
            _curProfile = null;
            IsBeginned = false;
        }
    }
}
