using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Data
{
    public class UserExternalProvider
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ProviderName { get; set; }
        [StringLength(250)]
        public string ProviderSubjectId { get; set; }
    }
}
