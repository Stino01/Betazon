using Microsoft.Data.SqlClient;
using Betazon.BLogic.Encryption;
using Betazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public bool getAdmin(string name, string password)
        {
            bool result = false;
            string pass = string.Empty;
            Encryption.Encryption encryption = new Encryption.Encryption();
            try
            {
                CheckDbOpening();
                //Select per estrarre i dati
                string CommandText = $"SELECT * FROM Admin A INNER JOIN EncryptionData E ON A.EncryptionDataId = E.Id";
                
                //Estrapolo i risultati
                using (SqlCommand cmdInsert = sqlCmd)
                {
                    cmdInsert.CommandText = CommandText;
                    cmdInsert.Connection = sqlCnn;
                    using (SqlDataReader reader = cmdInsert.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pass = encryption.DecryptStringFromBytes_Aes(Convert.FromBase64String(reader["EncryptedValue"].ToString().Trim()), Convert.FromBase64String(reader["AesKey"].ToString().Trim()), Convert.FromBase64String(reader["AesIV"].ToString().Trim()));
                                if (pass == password && name == reader["FirstName"].ToString())
                                    { result = true; }
                            }
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
