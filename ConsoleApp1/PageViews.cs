using System;


namespace IntegrationCode
{
    public class PageViwes : IEquatable<PageViwes>
    {
        public int ID { get; set; }
        public string DOMAIN_CODE { get; set; }

        public string PAGE_TITTLE { get; set; }

        public int CNTVIEWS { get; set; }

        public override string ToString()
        {
            return "DOMAIN_CODE: " + DOMAIN_CODE + "   PAGE_TITTLE: " + PAGE_TITTLE + "   CNTVIEWS: " + CNTVIEWS;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            PageViwes objAsPart = obj as PageViwes;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return ID;
        }
        public bool Equals(PageViwes other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }

    }
}
