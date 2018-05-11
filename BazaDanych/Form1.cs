using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BazaDanych
{
    public partial class Form1 : Form
    {
         
        SqlCommand sqlCommand;
        static string connectionString = "Server=KAROL;Database=TEST_CS;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);

        private void openCloseDBconnection(string value)
        {
            using (sqlCommand = new SqlCommand(value, connection))
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

        public Form1()
        {
            InitializeComponent();


        }
            // wpisywanie usera i hasla do bazy
        private void buttonDopiszDoBazy_Click(object sender, EventArgs e)
        {

            try
            {          

                    if (String.IsNullOrEmpty(textBoxNewUser.Text) || String.IsNullOrEmpty(inputPassword.Text))
                    {
                        MessageBox.Show("Wpisz imie i haslo !");
                       
                    }

                    else
                    {
                        // wspisywanie imienia i hasla do bazy danych
                        string inputText = textBoxNewUser.Text;
                        string password = inputPassword.Text;

                        string newNameAndPassword = string.Format("INSERT INTO dbo.Users(Name,MyPassword) VALUES ('" + inputText + "','" + password + "')");
                        openCloseDBconnection(newNameAndPassword);
                        MessageBox.Show("Imie i hasło zapisane do bazy !");

                    } 
            }
             
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

            // usuwanie wszystkich danych z tabeli
        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            string deleteFromDB = "DELETE FROM dbo.Users";
            openCloseDBconnection(deleteFromDB);
            string setMainKey = "DBCC CHECKIDENT('dbo.Users', RESEED, 0)";
            openCloseDBconnection(setMainKey);
            MessageBox.Show("Wszystkie dane z tabeli usunięte !");

        }
                // usuwanie usera z tabeli
        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(textUserToDelete.Text))
                {
                    string inputUserToDelete = textUserToDelete.Text;
                    string userToDelete = string.Format("DELETE FROM dbo.Users WHERE Name = '"+ inputUserToDelete +"' ");
                    openCloseDBconnection(userToDelete);

                    MessageBox.Show("User usunięty z bazy !");


                }
                else
                {
                    MessageBox.Show("Pole uzytkownika do usuniecia nie moze byc puste!");
                    return;
                }

            }

            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            // aktualizacja usera w bazie
        private void buttonUpdate_Click(object sender, EventArgs e)
        {

            try
            {

                if (String.IsNullOrEmpty(textBoxOldUserName.Text) || String.IsNullOrEmpty(textBoxNewUserName.Text))
                {
                    MessageBox.Show("Wpisz stare imie i nowe imie usera !");

                }

                else
                {
                    string oldUser = textBoxOldUserName.Text;
                    string newUser = textBoxNewUserName.Text;

                    string userToUpdate = string.Format("UPDATE dbo.Users SET Name = '" + newUser + "' WHERE Name = '" + oldUser + "' ");
                    //string userToUpdate = "UPDATE dbo.Users SET Name = 'newUser' WHERE Name = 'oldUser'";
                    openCloseDBconnection(userToUpdate);
                    MessageBox.Show("User zaktualizowany!");


                }
              

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
    }

}
