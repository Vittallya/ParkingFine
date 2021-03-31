using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class DbContextLoader
    {

        private bool _loaded;
        private AllDbContext _dbContext;

        public AllDbContext DbContext { get; private set; }

        public async Task LoadAsync()
        {
            Thread thread = new Thread(() => { DbContext = new AllDbContext(); DbContext.Autos.Load(); _loaded = true; });
            thread.Start();

            await Task.Run(() =>
            {
                while (!_loaded)
                {
                    Task.Delay(100);
                }
            });
        }

        public DbContextLoader(AllDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DisposeConnection()
        {
            _dbContext.Database.Connection.Close();
            _dbContext.Database.Connection.Dispose();  
        }
    }
}
