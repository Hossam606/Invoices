using Task_Entities.Entities;

namespace Task_Client.Models
{
    public class UserDisplayModel
    {
            public IEnumerable<User> Users { get; set; }
            public string STerm { get; set; } = "";
        
    }
}
