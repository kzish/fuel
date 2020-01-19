using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fuelApi.Models
{
    public class Globals
    {
        private static string log_file = @"c:\\rubiem\\fuel.txt";


        public static void log_data_to_file(string source, object data)
        {
            try
            {
                dynamic obj = new JObject();
                obj.source = source;
                obj.msg = data.ToString();
                var logdata = JsonConvert.SerializeObject(obj);
                System.IO.File.AppendAllText(log_file, logdata + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(log_file, ex.Message + Environment.NewLine);
            }
        }


        public enum eFuelType
        {
            petrol,
            diesel,
            gas
        }

        public enum eState
        {
            pending,
            closed
        }

        public static string GetFuelType(int type)
        {
            if (type == 0) return "Petrol";
            if (type == 1) return "Diesel";
            if (type == 2) return "Gas";
            return "";
        }



    }
}
