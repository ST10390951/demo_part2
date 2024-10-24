using System.Data.SqlClient;

namespace demo_part2.Models
{
    public class check_login
    {
        public string email { get; set; }
        public string role { get; set; }

        public string password { get; set; }
        //connection string

        connection connect = new connection();

        //method to check the user
        public string login_user(string email,string role,string password)
        {
            //temporal message
            string message = "";
            Console.WriteLine(email+" and "+password);
            try
            {
                //connect and open 
                using(SqlConnection connects = new SqlConnection(connect.connecting())) 
                {
                    //open it
                connects.Open();
                    //query
                    string query = "select * from users where email='"+email+"' and password='"+password+"';";
                

                    //executing
                    using (SqlCommand prepare = new SqlCommand(query,connects)) 
                    { 
                        //read the data
                        using (SqlDataReader find_user = prepare.ExecuteReader())
                        {
                            //checking if the the user found
                            if(find_user.HasRows) {

                                //then assign message
                                message = "found";
                                Console.WriteLine(message);
                            
                            }
                            else
                            {
                                message = "user not found";
                            }
                        }

                    }
                    connects.Close();
                    if(message == "found")
                    {
                        update_active(email);
                    }
                }
            }
            catch (IOException erro_db)
            //return error
            {
                message = erro_db.Message;
            }
            return message;
        }
        //update active user method
        public void update_active(string email)
        {
            try
            {
                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();
                    string query = "update active set email='" + email + "'";

                    using(SqlCommand done = new SqlCommand(query, connects))
                    {
                        done.ExecuteNonQuery();
                    }
                    connects.Close();
                } 
            }
            catch(IOException error) 
            {
                Console.WriteLine("error"+error.Message);
            }
        }
    }
}
