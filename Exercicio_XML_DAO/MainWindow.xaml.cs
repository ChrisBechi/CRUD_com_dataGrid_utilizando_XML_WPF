using Exercicio_XML_DAO.InterfaceDAO;
using Exercicio_XML_DAO.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercicio_XML_DAO
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        UserDAO usuarioAtual;
        InterfaceDAOXML interfaceDAO;

        public MainWindow()
        {
            InitializeComponent();
            interfaceDAO = new InterfaceDAOXML();
            atualizarGrid();
            Limpar();
        }

        public void Limpar()
        {
            txt1.Text = String.Empty;
            txt1.BorderBrush = Brushes.Red;
            txt2.Text = String.Empty;
            txt2.BorderBrush = Brushes.Red;
            dgUsuarios.SelectedIndex = -1;
            usuarioAtual = null;
        }

        public void atualizarGrid()
        {
            dgUsuarios.ItemsSource = null;
            dgUsuarios.ItemsSource = interfaceDAO.listaUsuarios;
        }

        public void UsuarioNaoEncontrado()
        {
            if (txt1.Text != String.Empty)
                MessageBox.Show($"Para efetuar essa operação é necessario primeiro selecionar/pesquisar um usuario!", "Usuario não foi selecionado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                MessageBox.Show($"Informe o usuario para efetuar a operação!");
                txt1.Focus();
            }
        }

        public Boolean ValidacaoDeCampos()
        {
            bool erro = true;

            if (txt2.Text.Equals(String.Empty))
            {
                txt2.BorderBrush = Brushes.Orange;
                txt2.Focus();
                erro = false;
            }
            else
            {
                txt2.BorderBrush = Brushes.Green;
            }

            if (txt1.Text.Equals(String.Empty))
            {
                txt1.BorderBrush = Brushes.Orange;
                txt1.Focus();
                erro = false;
            }
            else
            {
                txt1.BorderBrush = Brushes.Green;
            }
            return erro;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usuarioAtual = interfaceDAO.BuscarUsuario(txt1.Text);
            if (usuarioAtual == null)
            {
                if (ValidacaoDeCampos())
                {
                    UserDAO usuario = new UserDAO()
                    {
                        Login = txt1.Text,
                        Nome = txt2.Text
                    };
                    interfaceDAO.Inserir(usuario);
                    Limpar();
                    atualizarGrid();
                    MessageBox.Show("Usuario cadastrado com sucesso!", "Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }else
                    MessageBox.Show("Preencha todos os campos para cadastrar um novo usuario!", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
                MessageBox.Show($"O usuario {txt1.Text} já está cadastrado no sistema!", "Já existe", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Limpar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            usuarioAtual = interfaceDAO.BuscarUsuario(txt1.Text);
            if (usuarioAtual != null)
            {
                txt1.Text = usuarioAtual.Login;
                txt2.Text = usuarioAtual.Nome;
                atualizarGrid();
            }
            else if (txt1.Text.Equals(String.Empty))
            {
                MessageBox.Show($"Informe o usuario para efetuar a operação!");
                txt1.Focus();
            }
            else
                MessageBox.Show($"O usuario {txt1.Text} não está cadastrado no sistema!", "Não encontrado", MessageBoxButton.OK, MessageBoxImage.Exclamation); ;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (usuarioAtual != null)
            {
                usuarioAtual.Nome = txt2.Text;
                interfaceDAO.Alterar(usuarioAtual);
                MessageBox.Show("Usuario Atualizado com sucesso!", "Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpar();
                atualizarGrid();
            }
            else
                UsuarioNaoEncontrado();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(usuarioAtual != null)
            {
                interfaceDAO.Remover(usuarioAtual);
                Limpar();
                atualizarGrid();
            }
            else
                UsuarioNaoEncontrado();
        }

        private void dgUsuarios_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
             UserDAO use = dgUsuarios.SelectedItem as UserDAO;
            if(use != null)
            {
                usuarioAtual = use;
                txt1.Text = usuarioAtual.Login;
                txt2.Text = usuarioAtual.Nome;
            }
        }
    }
}
