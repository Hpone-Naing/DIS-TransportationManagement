using TransportationManagement.Classes;
using Microsoft.AspNetCore.Mvc;

namespace TransportationManagement.Util
{
    public class Utility
    {
        public static int DEFAULT_PAGINATION_NUMBER = 5;

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
           
            AdvanceSearch advanceSearch = new AdvanceSearch();
            advanceSearch.POSInstalled = posInstalled;
            advanceSearch.CctvInstalled = cctvInstalled;
            advanceSearch.TelematicDeviceInstalled = telematicDeviceInstalled;

            return advanceSearch;
        }

    }
}
