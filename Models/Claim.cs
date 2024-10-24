using System.Data.SqlClient;
using demo_part2.Models;
namespace demo_part2.Models
{
    public class Claim // Updated to Claim
    {
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string HoursWorked { get; set; }
        public string HourRate { get; set; }
        public string Description { get; set; }

        // Connection
        private connection connect = new connection();

        // Method to insert a claim
        public string InsertClaim(string module, string hoursWorked, string rate, string note, string filename)
        {
            string message = "";
            string userId = GetId();
            string userEmail = GetEmail(); // Ensure this method is defined

            string total = (int.Parse(hoursWorked) * int.Parse(rate)).ToString();
            string query = "INSERT INTO claiming (user_email, module, user_id, hours_worked, hour_rate, note, filename, total, status) " +
                           $"VALUES ('{userEmail}', '{module}', '{userId}', '{hoursWorked}', '{rate}', '{note}', '{filename}', '{total}', 'pending');";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();
                    using (SqlCommand done = new SqlCommand(query, connects))
                    {
                        done.ExecuteNonQuery();
                        message = "done";
                    }
                }
            }
            catch (IOException error)
            {
                message = error.Message;
            }

            return message;
        }

        // Implement GetId method if not already defined
        public string GetId()
        {
            string holdId = "";
            try
            {
                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();

                    using (SqlCommand cprepare = new SqlCommand("SELECT * FROM active", connects))
                    {
                        using (SqlDataReader getId = cprepare.ExecuteReader())
                        {
                            if (getId.HasRows)
                            {
                                while (getId.Read())
                                {
                                    holdId = getId["id"].ToString(); // Ensure this matches your actual column name
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                holdId = error.Message;
            }
            return holdId;
        }

        public string GetEmail()
        {
            string holdEmail = "";
            try
            {
                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("SELECT * FROM active", connects))
                    {
                        using (SqlDataReader getEmail = prepare.ExecuteReader())
                        {
                            if (getEmail.HasRows)
                            {
                                while (getEmail.Read())
                                {
                                    holdEmail = getEmail["email"].ToString(); // Ensure this matches your actual column name
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                holdEmail = error.Message;
            }
            return holdEmail;
        }
    }
}
