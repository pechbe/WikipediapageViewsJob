using System;
using System.IO;
using System.Collections.Generic;

namespace IntegrationCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Type a path to download files, and then press Enter: ");
                string sPathgzfiles = Console.ReadLine();

                if (Directory.Exists(sPathgzfiles))
                {

                    clsFilesProcessing oFileProc = new clsFilesProcessing();

                    oFileProc.DownloadGZfiles(sPathgzfiles);

                    oFileProc.UnzipGZfiles();

                    List<PageViwes> objPagesViews = new List<PageViwes>();
                    oFileProc.OpenGZfiles(ref objPagesViews);

                    clsQueryPageViews oQueryPageViews = new clsQueryPageViews();
                    oQueryPageViews.QueryPageViews(objPagesViews);
                }
                else
                {
                    Console.WriteLine("{0}  is not a valid directory.", sPathgzfiles);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
