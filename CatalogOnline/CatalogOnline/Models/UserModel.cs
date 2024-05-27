using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class UserModel
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string legal_name { get; set; }
        public string email { get; set; }
    }
}
