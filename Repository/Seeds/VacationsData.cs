using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class VacationsData
    {

        public static List<Vacation> DefaultOffices = new List<Vacation>()
        {
            new Vacation()
            {
                Id = 1
            },
            new Vacation()
            {
                Id = 1
            }
        };
    }
}
