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
    public class ParkingsService
    {
        private readonly AllDbContext dbContext;
        private readonly MapperService mapper;

        public ParkingsService(AllDbContext dbContext, MapperService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IEnumerable<Parking> parkings;

        public async Task ReloadAsync()
        {
            await dbContext.Parkings.LoadAsync();

            parkings = await dbContext.Parkings.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Parking> GetParkings()
        {
            if (parkings == null)
                throw new ArgumentException("Call 'ReloadAsync' method first");

            return parkings;
        }
    }
}
