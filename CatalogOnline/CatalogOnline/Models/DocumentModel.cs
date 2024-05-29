namespace CatalogOnline.Models
{
    public class DocumentModel
    {

        public int document_id { get; set; }
        public int user_id { get; set; }

        public string type { get; set; }


        public DateTime date { get; set; }

        public string title { get; set; }


        public string description { get; set; }

        public string location { get; set; }
        

 
    }
}
