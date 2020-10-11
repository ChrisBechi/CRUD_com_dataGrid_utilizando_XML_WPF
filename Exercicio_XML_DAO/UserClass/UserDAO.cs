using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio_XML_DAO.UserClass
{
    [Serializable()]
    public class UserDAO
    {
        public string Login { get; set; }
        public string Nome { get; set; }
    }
}
