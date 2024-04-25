using MDE.CampusDetector.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.CampusDetector.Domain.Services
{
    public interface ICampusService
    {
        Task<IEnumerable<Campus>> GetAllCampuses();
    }
}
