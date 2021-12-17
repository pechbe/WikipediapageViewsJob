using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Collections;


namespace IntegrationCode
{
    public class clsFilesProcessing
    {
        const string sConstPathWikipedia = "https://dumps.wikimedia.org/other/pageviews/";
        clsFolder oFolder = new clsFolder();

        public string sPATHDOWNLOAD { get; set; }

        public string sPATHUNZIPGZFILES { get; set; }

        
        public clsFilesProcessing()
        {

        }


        public void DownloadGZfiles(string sPathgzfiles)
        {
            try
            {

                clsGetDateHour oGETDATE = new clsGetDateHour();
                oGETDATE.GetDateHour();

                sPATHDOWNLOAD = sPathgzfiles + "\\GZFILES_" + oGETDATE.sYEAR + oGETDATE.sHOUR + oGETDATE.sDAY + oGETDATE.sMINUTE;

                
                oFolder.CreateFolder(sPATHDOWNLOAD);

                WebClient mywebClient = new WebClient();
                int iHourLes = 0;
                int iLastHours = 0;
                do
                {
                    //Contrunction name path URL
                    string sHourBefore = (int.Parse(oGETDATE.sHOUR) - iHourLes).ToString().PadLeft(2, '0');
                    if ((int.Parse(oGETDATE.sHOUR) - iHourLes) == -1)
                    {
                        DateTime DateDiffrent = DateTime.Parse(oGETDATE.sDAY + "/" + oGETDATE.sMONTH + "/" + oGETDATE.sYEAR).AddDays(-1);
                        oGETDATE.sYEAR = DateDiffrent.Year.ToString();
                        oGETDATE.sMONTH = DateDiffrent.Month.ToString().PadLeft(2, '0');
                        oGETDATE.sDAY = DateDiffrent.Day.ToString().PadLeft(2, '0');

                        oGETDATE.sHOUR = "23"; sHourBefore = "23"; iHourLes = 1;
                    }
                    else
                        iHourLes++;

                    //Download gz file from http
                    try
                    {
                        string sPathUrlFile = sConstPathWikipedia + oGETDATE.sYEAR + "/" + oGETDATE.sYEAR + "-" + oGETDATE.sMONTH + "/pageviews-" + oGETDATE.sYEAR + oGETDATE.sMONTH + oGETDATE.sDAY + "-" + sHourBefore + "0000.gz";
                        string sFileDownload = sPATHDOWNLOAD + "\\pageviews-" + oGETDATE.sYEAR + oGETDATE.sMONTH + oGETDATE.sDAY + "-" + sHourBefore + "0000.gz";
                        mywebClient.DownloadFile(sPathUrlFile, sFileDownload);
                        Console.WriteLine("File downloaded: " + sFileDownload);
                        iLastHours++;
                    }
                    catch (Exception ex)
                    {
                        //throw ex; //If not exist file
                    }

                } while (iLastHours < 5);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnzipGZfiles()
        {
            try
            {
                Console.WriteLine(" ");

                sPATHUNZIPGZFILES = sPATHDOWNLOAD + "\\UNZIPFILES";
                oFolder.CreateFolder(sPATHUNZIPGZFILES);

                DirectoryInfo directorySelected = new DirectoryInfo(sPATHDOWNLOAD);
                foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
                {
                    using (FileStream originalFileStream = fileToDecompress.OpenRead())
                    {
                        string currentFileName = fileToDecompress.FullName;
                        string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                        using (FileStream decompressedFileStream = File.Create(newFileName))
                        {
                            using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                            {
                                decompressionStream.CopyTo(decompressedFileStream);
                                
                                Console.WriteLine("File descompressed: {0}", fileToDecompress.Name);
                            }  
                        }

                        string sFileDest = sPATHUNZIPGZFILES + "\\" + newFileName.ToString().Substring(newFileName.ToString().LastIndexOf("pageviews"), 25);
                        System.IO.File.Copy(newFileName, sFileDest, true);
                        System.IO.File.Delete(newFileName);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OpenGZfiles(ref List<PageViwes> objPagesViews)
        {
            string line;
            try
            {
                Console.WriteLine(" ");
         
                string[] files = Directory.GetFiles(sPATHUNZIPGZFILES);
                foreach (var file in files)
                {
                    Console.WriteLine("File read: " + file);

                    StreamReader sr = new StreamReader(file);
                    //Read the first line of text
                    line = sr.ReadLine();

                    while (line != null)
                    {
                        string[] words = line.Split(' ');
                        var arInfoViews = new ArrayList();

                        foreach (var word in words)//Read every column from file
                        {
                            arInfoViews.Add(word);

                        }
                        objPagesViews.Add(new PageViwes() { DOMAIN_CODE = arInfoViews[0].ToString(), PAGE_TITTLE = arInfoViews[1].ToString(), CNTVIEWS = int.Parse(arInfoViews[2].ToString()) });

                        line = sr.ReadLine();
                    }
                    sr.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
