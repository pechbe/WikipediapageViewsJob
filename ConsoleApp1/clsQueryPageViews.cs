using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace IntegrationCode
{
    public class clsQueryPageViews
    {
        public clsQueryPageViews()
        {

        }

        public void QueryPageViews(List<PageViwes> objPagesViews)
        {
            try
            {

                var QueryPrincipalViews =
                (from x in objPagesViews
                 group x by new { x.DOMAIN_CODE, x.PAGE_TITTLE } into g
                 orderby g.Sum(x => x.CNTVIEWS) descending
                 select new { g.Key.DOMAIN_CODE, g.Key.PAGE_TITTLE, CNTVIEWS = g.Sum(x => x.CNTVIEWS) }).Take(100);

                Console.WriteLine(" ");
                Console.WriteLine("{0}\t{1}\t{2}", "DOMAIN_CODE".PadRight(50), "PAGE_TITTLE".PadRight(50), "CNTVIEWS".PadRight(50));
                Console.WriteLine("{0}\t{1}\t{2}", new String('-', 50), new String('-', 50), new String('-', 50));

                foreach (var categ in QueryPrincipalViews)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", categ.DOMAIN_CODE.PadRight(50), categ.PAGE_TITTLE.PadRight(50), categ.CNTVIEWS);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
