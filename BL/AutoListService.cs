using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BL
{
    public class AutoListService : IAutoListService
    {
        private readonly AllDbContext dbContext;

        public AutoListService(AllDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Evacuation SelectedEvac { get; private set; }

        public async Task<IEnumerable<Evacuation>> GetEvacuations(string input = null)
        {
            await dbContext.Evacuations.Include(x => x.Auto).LoadAsync();

            var active = dbContext.Evacuations.
                Where(x => x.CarStatus == CarStatus.AtParking);

            if(input == null || input.Length == 0)
            {
                return active;
            }

            return active.Where(x => x.Auto.GovNumber.StartsWith(input) ||
               x.Auto.Mark.StartsWith(input) ||
               x.Auto.Model.StartsWith(input));
        }

        public void SetCar(Evacuation auto)
        {
            SelectedEvac = auto;
        }
    }
}
