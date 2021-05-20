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

    
    public class DeclarationService
    {
        public const int BEGIN_TIME = 10;
        public const int END_TIME = 18;
        public const int SLOT_LENGTH = 2;

        bool isEdit;

        private Evacuation _evac;
        private Declaration _currentDec;
        public bool IsEdit => isEdit;

        public string Message { get; private set; }

        private readonly AllDbContext dbContext;
        private readonly MapperService mapper;

        public DeclarationService(AllDbContext dbContext, MapperService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public string Status { get; private set; }


        public void Clear()
        {
            _currentDec = null;
            _evac = null;
            isEdit = false;
            IsPadied = false;
        }

        public DeclarationDto GetDeclaration()
        {
            if (_evac == null)
                throw new ArgumentException("Информация об эвакуцаии не была загружена");

            return mapper.MapTo<Declaration, DeclarationDto>(_currentDec);
        }
        public void SetupFilledDeclaration(DeclarationDto dto)
        {
            _currentDec = mapper.MapTo<DeclarationDto, Declaration>(dto, _currentDec);
        }

        int _profileId = -1;

        public void SetupProfile(int profileId)
        {
            _profileId = profileId;
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
            }
            return isEdit;
        }
        public async Task<bool> ApplyDeclaration()
        {
            if (_profileId == -1)
                throw new ArgumentException("Профиль не был установлен");


            if (isEdit)
            {
                dbContext.Entry(_currentDec).State = EntityState.Modified;
            }
            else
            {
                _currentDec.ProfileId = _profileId;
                _currentDec.CreatingDate = DateTime.Now;
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
        public bool IsPadied { get; private set; }
        public void DefineStatus(DecStatus decStatus)
        {
            _currentDec.DecStatus = decStatus;

            if(decStatus == DecStatus.ActivePaid)
            {
                IsPadied = true;
            }
        }
    }
}