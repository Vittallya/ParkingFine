using DAL;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using AutoMapper;
using Profile = DAL.Models.Profile;
using System.Data.Entity;
using System.Collections.Generic;

namespace BL
{
    public class DeclarationService : IDeclarationService
    {
        public const int BEGIN_TIME = 10;
        public const int END_TIME = 18;
        public const int SLOT_LENGTH = 2;

        bool isEdit;

        private Evacuation _evac;
        private Declaration _currentDec;
        private Profile _profile;

        private MapperConfiguration decToDto;
        private MapperConfiguration profileToDto;

        private MapperConfiguration decFromDto;
        private MapperConfiguration profileFromDto;

        public bool IsEdit => isEdit;

        public string Message { get; private set; }

        private readonly AllDbContext dbContext;

        public DeclarationService(AllDbContext dbContext)
        {
            this.dbContext = dbContext;

            decToDto = new MapperConfiguration(x => x.CreateMap<Declaration, DeclarationDto>());
            profileToDto = new MapperConfiguration(x => x.CreateMap<Profile, ProfileDto>());

            decFromDto = new MapperConfiguration(x => x.CreateMap<DeclarationDto, Declaration>());
            profileFromDto = new MapperConfiguration(x => x.CreateMap<ProfileDto, Profile>());
        }

        public bool CanEditDeclaration(Declaration dec)
        {
            throw new System.NotImplementedException();
        }


        public void Clear()
        {
            _currentDec = null;
            _profile = null;
            _evac = null;
            isEdit = false;
        }

        public DeclarationDto GetDeclaration()
        {
            if (_evac == null)
                throw new ArgumentException("Информация об эвакуцаии не была загружена");

            
            var mapper = new Mapper(decToDto);
            var dto = mapper.Map<Declaration, DeclarationDto>(_currentDec);

            return dto;
        }

        public ProfileDto GetProfile()
        {
            if (_evac == null)
                throw new ArgumentException("Информация об эвакуцаии не была загружена");


            var mapper = new Mapper(profileToDto);
            var dto = mapper.Map<Profile, ProfileDto>(_profile);
            return dto;
        }

        public void SetupFilledDeclaration(DeclarationDto dto)
        {
            var mapper = new Mapper(decFromDto);
            var dec = mapper.Map<DeclarationDto, Declaration>(dto, _currentDec);

            _currentDec = dec;
        }

        public void SetupFilledProfile(ProfileDto dto)
        {
            var mapper = new Mapper(profileFromDto);
            var profile = mapper.Map<ProfileDto, Profile>(dto, _profile);

            _profile = profile;
        }



        public async Task<bool> SetupEvac(int evacId)
        {
            if (_evac == null || _evac.Id != evacId)
            {
                //загружается совершенно другая эвакуация, история другая
                Clear();

                _evac = dbContext.Evacuations.Find(evacId);
                await dbContext.Entry(_evac).Collection("Declarations").LoadAsync();


                _currentDec = _evac.Declarations.FirstOrDefault(x => x.DecStatus == DecStatus.Active || 
                x.DecStatus == DecStatus.ActivePaid);

                isEdit = _currentDec != null;

                _currentDec = _currentDec ?? new Declaration() { ComingDate = DateTime.Now.AddDays(1) };

                if (!isEdit)
                    _currentDec.EvacuationId = evacId;

                _profile = _currentDec.Profile ?? new Profile() { };
            }
            return isEdit;
        }

        public async Task<bool> ApplyDeclaration()
        {            
            if (isEdit)
            {
                dbContext.Entry(_currentDec).State = EntityState.Modified;
            }
            else
            {
                _currentDec.Profile = _profile;
                _currentDec.CreatingDate = DateTime.Now;
                dbContext.Profiles.Add(_profile);
                dbContext.Declarations.Add(_currentDec);
            }

            try
            {
                await dbContext.SaveChangesAsync();                
                return true;
            }
            catch(Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }


        public async Task<IEnumerable<string>> GetTimesForDate(DateTime date)
        {
            await dbContext.Declarations.LoadAsync();

            var list = dbContext.Declarations.ToList();

            List<int> busy = list.
                Where(x => (x.DecStatus == DecStatus.Active || x.DecStatus == DecStatus.ActivePaid) &&
                x.ComingDate.Date == date.Date && x.Id != _currentDec.Id).
                Select(y => y.ComingDate.TimeOfDay.Hours).ToList();

            int count = (END_TIME - BEGIN_TIME) / SLOT_LENGTH;

            List<string> free = new List<string>(count + 1 - busy.Count);

            for (int i = BEGIN_TIME; i <= END_TIME; i+= SLOT_LENGTH)
            {
                if (!busy.Contains(i))
                {
                    TimeSpan time = new TimeSpan(i, 0, 0);
                    free.Add(time.ToString("hh\\:mm"));
                }
            }

            return free;
        }

        public int GetCost()
        {
            if (_evac == null)
                throw new ArgumentException("Инфо об эвакуации не было загруж.");

            int Hours = (int)Math.Round((DateTime.Now - _evac.DateTimeEvac).TotalHours);
            int ParkingCost = _evac.Parking.CostByHour * Hours;

            return (int)(ParkingCost + _evac.EvacCost + _evac.Fine.Cost);
        }

        public void DefineStatus(DecStatus decStatus)
        {
            _currentDec.DecStatus = decStatus;
        }
    }
}