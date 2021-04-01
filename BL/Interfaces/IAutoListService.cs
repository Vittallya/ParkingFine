using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL
{
    public interface IAutoListService
    {
        Task<IEnumerable<Evacuation>> GetEvacuations(string input = null);
        void SetCar(Evacuation auto);

        Task Reload();

        Evacuation SelectedEvac { get; }
    }
}
