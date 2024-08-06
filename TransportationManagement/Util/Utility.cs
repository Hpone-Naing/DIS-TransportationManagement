using TransportationManagement.Classes;
using Microsoft.AspNetCore.Mvc;

namespace TransportationManagement.Util
{
    public class Utility
    {
        public static int DEFAULT_PAGINATION_NUMBER = 35;

        public static void AlertMessage(Controller controller, string message, string color)
        {
            controller.TempData["Message"] = message;
            controller.TempData["CssColor"] = color;
        }

        public static AdvanceSearch MakeAdvanceSearch(HttpContext context)
        {
            string posInstalled = context.Request.Query["POSInstalled"];
            string telematicDeviceInstalled = context.Request.Query["TelematicDeviceInstalled"];
            string cctvInstalled = context.Request.Query["CctvInstalled"];
            string fuelType = context.Request.Query["FuelType"];
            string ybsCompany = context.Request.Query["YBSCompany"];
            string manufacturer = context.Request.Query["Manufacturer"];
            string ybsName = context.Request.Query["YBSName"];
            AdvanceSearch advanceSearch = new AdvanceSearch();
            advanceSearch.POSInstalled = posInstalled;
            advanceSearch.CctvInstalled = cctvInstalled;
            advanceSearch.TelematicDeviceInstalled = telematicDeviceInstalled;
            advanceSearch.FuelType = fuelType;
            advanceSearch.Manufacturer = manufacturer;
            advanceSearch.YBSCompany = ybsCompany;
            advanceSearch.YBSName = ybsName;
            return advanceSearch;
        }

    }
}
