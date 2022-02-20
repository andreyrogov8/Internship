using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static partial class TestData
    {
        public static List<Workplace> GetWorkplaces(List<Map> maps)
        {
            return new List<Workplace>
            {
                new Workplace
                {
                    WorkplaceNumber = 1,
                    WorkplaceType = WorkplaceType.LongTerm,
                    NextToWindow = true,
                    HasPC = true,
                    HasMonitor = true,
                    HasKeyboard = true,
                    HasMouse = true,
                    HasHeadset = true,
                    IsDeleted = false,
                    MapId = maps.Where(map => map.Office.City == "Warsaw" && map.FloorNumber == 1).First().Id,
                },

                new Workplace
                {
                    WorkplaceNumber = 2,
                    WorkplaceType = WorkplaceType.LongTerm,
                    NextToWindow = true,
                    HasPC = true,
                    HasMonitor = true,
                    HasKeyboard = true,
                    HasMouse = true,
                    HasHeadset = true,
                    IsDeleted = false,
                    MapId = maps.Where(map => map.Office.City == "Warsaw" && map.FloorNumber == 1).First().Id,
                },

                new Workplace
                {
                    WorkplaceNumber = 3,
                    WorkplaceType = WorkplaceType.LongTerm,
                    NextToWindow = true,
                    HasPC = true,
                    HasMonitor = true,
                    HasKeyboard = true,
                    HasMouse = true,
                    HasHeadset = true,
                    IsDeleted = false,
                    MapId = maps.Where(map => map.Office.City == "Warsaw" && map.FloorNumber == 1).First().Id,
                },

                new Workplace
                {
                    WorkplaceNumber = 4,
                    WorkplaceType = WorkplaceType.LongTerm,
                    NextToWindow = true,
                    HasPC = true,
                    HasMonitor = true,
                    HasKeyboard = true,
                    HasMouse = true,
                    HasHeadset = true,
                    IsDeleted = false,
                    MapId = maps.Where(map => map.Office.City == "Warsaw" && map.FloorNumber == 2).First().Id,
                },

                new Workplace
                {
                    WorkplaceNumber = 5,
                    WorkplaceType = WorkplaceType.LongTerm,
                    NextToWindow = true,
                    HasPC = true,
                    HasMonitor = true,
                    HasKeyboard = true,
                    HasMouse = true,
                    HasHeadset = true,
                    IsDeleted = false,
                    MapId = maps.Where(map => map.Office.City == "Warsaw" && map.FloorNumber == 2).First().Id,
                },
            };
        }
    }
}
