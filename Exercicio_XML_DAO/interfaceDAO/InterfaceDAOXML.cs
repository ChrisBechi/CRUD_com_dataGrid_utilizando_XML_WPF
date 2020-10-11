using Exercicio_XML_DAO.UserClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Exercicio_XML_DAO.InterfaceDAO
{
    class InterfaceDAOXML
    {
        const string ARQUIVO_ARMAZENAMENTO = "usuarios.xml";
        public List<UserDAO> listaUsuarios;

        public void LerArquivo()
        {
            if (File.Exists(ARQUIVO_ARMAZENAMENTO))
            {
                using (FileStream fs = new FileStream(ARQUIVO_ARMAZENAMENTO, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<UserDAO>));
                    listaUsuarios = (List<UserDAO>) xml.Deserialize(fs);
                }
            } else
                listaUsuarios = new List<UserDAO>();
        }

        public void GravarArquivo()
        {
            using (FileStream fs = new FileStream(ARQUIVO_ARMAZENAMENTO, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<UserDAO>));
                xml.Serialize(fs, listaUsuarios);
            }
        }

        public InterfaceDAOXML()
        {
            LerArquivo();
        }

        public UserDAO BuscarUsuario(string login)
        {
            var usuario = listaUsuarios.Where(usuarios => usuarios.Login.Equals(login));
            return usuario.Any() ? usuario.First() : null;
        }

        public void Inserir(UserDAO usuario)
        {
            listaUsuarios.Add(usuario);
            GravarArquivo();
        }

        public void Alterar(UserDAO usuario)
        {
            UserDAO user = BuscarUsuario(usuario.Login);
            if (user != null)
            {
                user.Nome = usuario.Nome;
                GravarArquivo();
            }
            else
                throw new Exception($"O usuario {usuario.Login} não está cadastrado no sistema!");
        }

        public void Remover(UserDAO usuario)
        {
            UserDAO use = BuscarUsuario(usuario.Login);
            if (use != null)
            {
                listaUsuarios.Remove(usuario);
                GravarArquivo();
            }
            else
                throw new Exception($"O usuario {usuario.Login} não está cadastrado no sistema!");
        }
        
    }
}
