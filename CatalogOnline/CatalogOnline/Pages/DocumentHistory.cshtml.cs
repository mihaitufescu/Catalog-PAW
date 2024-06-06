using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogOnline.DAL.DBO;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "student")]
    public class DocumentHistoryModel : PageModel
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IUserRepository userRepository;

        public DocumentHistoryModel(IDocumentRepository _documentRepository, IUserRepository _userRepository)
        {
            documentRepository = _documentRepository;
            userRepository = _userRepository;
        }

        public List<Document> Documents { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = userRepository.GetUserById(int.Parse(userId));

                if (user != null)
                {
                    Documents = documentRepository.GetDocumentsByUserId(user.user_id).ToList();
                }
            }
        }
    }
}
