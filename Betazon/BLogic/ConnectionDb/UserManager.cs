using Microsoft.Data.SqlClient;
using Betazon.BLogic.Encryption;

namespace Betazon.BLogic.ConnectionDb
{
    public class UserManager
    {
        public bool isDbValid;
        //Istanza connessione al db
        SqlConnection sqlCnn;

        //Comando da effettuare
        SqlCommand sqlCmd = new SqlCommand();

        public UserManager(string DbConnection)
        {
            sqlCnn = new SqlConnection(DbConnection);

            //Controllo della connessione
            using (SqlConnection sql = sqlCnn)
            {
                try
                {
                    sql.Open();
                    sqlCnn = new SqlConnection(sql.ConnectionString);
                    sqlCnn.Open();
                    isDbValid = true;
                }
                catch (Exception)
                {
                    isDbValid = false;
                }
            }
        }

        //Controllo chiusura DB
        private void CheckDbObject()
        {
            if (sqlCnn.State == System.Data.ConnectionState.Open)
            {
                sqlCnn.Close();
            }

            sqlCmd.Parameters.Clear();
        }

        //Controllo apertura DB
        private void CheckDbOpening()
        {
            if (sqlCnn.State == System.Data.ConnectionState.Closed)
            {
                sqlCnn.Open();
            }
        }

        public bool getCustomer(string name, string password)
        {
            bool result = false;
            Encryption.Encryption encryption = new Encryption.Encryption();
            try
            {
                CheckDbOpening();
                //Select per estrarre i dati
                string CommandText = $"SELECT * FROM Customers WHERE FirstName = @FirstName AND PasswordHash = @PasswordHash";

                //Estrapolo i risultati
                using (SqlCommand cmdInsert = sqlCmd)
                {
                    password = encryption.EncryptString(password);
                    cmdInsert.Parameters.AddWithValue("@FirstName", name);
                    cmdInsert.Parameters.AddWithValue("@PasswordHash", password);

                    cmdInsert.CommandText = CommandText;
                    cmdInsert.Connection = sqlCnn;
                    using (SqlDataReader reader = cmdInsert.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Chiudo la connessione
                CheckDbObject();
            }

            //Ritorno la lista popolata
            return result;
        }

    }
}
