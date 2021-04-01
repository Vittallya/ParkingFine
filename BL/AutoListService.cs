using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BL
{
    public class AutoListService : IAutoListService
    {
        private readonly AllDbContext dbContext;

        private IQueryable<Evacuation> _evacs;

        public AutoListService(AllDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Evacuation SelectedEvac { get; private set; }

        public async Task Reload()
        {
            await dbContext.Evacuations.LoadAsync();
            _evacs = dbContext.Evacuations.Where(x => x.CarStatus == CarStatus.AtParking).AsNoTracking();
        }

        public async Task<IEnumerable<Evacuation>> GetEvacuations(string input = null)
        {
            if(_evacs == null)
            {
                await Reload();
            }
            

            if(input == null || input.Length == 0)
            {
                return _evacs;
            }

            return _evacs.Where(x => x.Auto.GovNumber.Contains(input) ||
               x.Auto.Mark.Contains(input) ||
               x.Auto.Model.Contains(input));
        }

        public void SetCar(Evacuation auto)
        {
            SelectedEvac = auto;
        }
    }
}
