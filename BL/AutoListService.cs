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
            await dbContext.Autos.LoadAsync();            
            await dbContext.Evacuations.LoadAsync();
            

            var active = dbContext.Evacuations.
                Where(x => x.CarStatus == CarStatus.AtParking);

            if(input == null || input.Length == 0)
            {
                return active;
            }

            return active.Where(x => x.Auto.GovNumber.Contains(input) ||
               x.Auto.Mark.Contains(input) ||
               x.Auto.Model.Contains(input));
        }

        public void SetCar(Evacuation auto)
        {
            SelectedEvac = auto;
        }
    }
}
