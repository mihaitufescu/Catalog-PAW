using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class UserModel
    {
        public string username { get; set; }
        public string legal_name { get; set; }
        public string email { get; set; }
    }
}
