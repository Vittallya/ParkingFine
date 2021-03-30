using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    public class DbContextLoader
    {

        private bool _loaded;

        public AllDbContext DbContext { get; private set; }

        public async Task LoadAsync()
        {
            Thread thread = new Thread(() => { DbContext = new AllDbContext(); _loaded = true; });
            thread.Start();

            await Task.Run(() =>
            {
                while (!_loaded)
                {
                    Task.Delay(100);
                }
            });
        }

        
    }
}
