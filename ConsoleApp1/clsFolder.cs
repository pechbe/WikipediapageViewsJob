using System;
using System.IO;

namespace IntegrationCode
{
    public class clsFolder
    {
        public clsFolder()
        {

        }
        public  void CreateFolder(string sNameCarpet)
        {
            try
            {
                if (!Directory.Exists(sNameCarpet))
                {
                    Directory.CreateDirectory(sNameCarpet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
