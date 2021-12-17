using System;

namespace IntegrationCode
{
    public class clsGetDateHour
    {
        public string sYEAR { get; set; }

        public string sMONTH { get; set; }
        public string sDAY { get; set; }
        public string sHOUR { get; set; }
        public string sMINUTE { get; set; }

        public clsGetDateHour()
        {

        }

        public void GetDateHour()
        {
            try
            {
                sYEAR = DateTime.UtcNow.Year.ToString();
                sMONTH = DateTime.UtcNow.Month.ToString().PadLeft(2, '0');
                sDAY = DateTime.UtcNow.Day.ToString().PadLeft(2, '0');
                sHOUR = DateTime.UtcNow.Hour.ToString().PadLeft(2, '0');
                sMINUTE = DateTime.UtcNow.Minute.ToString().PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
