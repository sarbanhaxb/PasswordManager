using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class Item
    {
        public int Id { get; set; }
        public int UserID{ get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public Item() { }

        public override string ToString()
        {
            return Title;
        }
    }
}
