using Microsoft.EntityFrameworkCore;
 
namespace DotnetFlix.Models
{
    public class UserDashboard
    {
        public User User { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}