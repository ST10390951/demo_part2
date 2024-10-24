using System.Collections;
using System.Data.SqlClient;

namespace demo_part2.Models
{
    public class get_claims
    {
        public ArrayList email { get; set; } = new ArrayList();
        public ArrayList module { get; set; } = new ArrayList();    
        public ArrayList id { get; set; } = new ArrayList();
        public ArrayList hours { get; set; } = new ArrayList();
        public ArrayList rate { get; set; } = new ArrayList();
        public ArrayList note { get; set; } = new ArrayList();
        public ArrayList total { get; set; } = new ArrayList();
        public ArrayList status { get; set; } = new ArrayList();

        public ArrayList filename { get; set; } = new ArrayList();

        connection connect = new connection();
        //constructors
        public get_claims()
        {
            string emails = gets_email();

            try
            {

                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("select * from claiming where email='"+emails+"';", connects))
                    {
                        using (SqlDataReader getEMAIL = prepare.ExecuteReader())
                        {
                            if (getEMAIL.HasRows)
                            {
                                //check all but return one 
                                //hold_id = getID["id"].ToString();
                                while (getEMAIL.Read())
                                {
                                    //hold_email = getEMAIL["email"].ToString();
                                    email.Add(getEMAIL["email"].ToString());
                                    module.Add(getEMAIL["module"].ToString());
                                    id.Add(getEMAIL["id"].ToString());
                                    hours.Add(getEMAIL["hours"].ToString());
                                    rate.Add(getEMAIL["rate"].ToString());
                                    note.Add(getEMAIL["note"].ToString());
                                    total.Add(getEMAIL["total"].ToString());
                                    status.Add(getEMAIL["status"].ToString());

                                    filename.Add(getEMAIL["files"].ToString());
                                }
                            }
                            getEMAIL.Close();
                        }

                    }
                    connects.Close();
                }
            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                //hold_email = error.Message;
            }

        }
        public string gets_email()
        {
            string hold_email = "";
            try
            {

                using (SqlConnection connects = new SqlConnection(connect.connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("select * from active", connects))
                    {
                        using (SqlDataReader getEMAIL = prepare.ExecuteReader())
                        {
                            if (getEMAIL.HasRows)
                            {
                                //check all but return one 
                                //hold_id = getID["id"].ToString();
                                while (getEMAIL.Read())
                                {
                                    hold_email = getEMAIL["email"].ToString();
                                }
                            }
                            getEMAIL.Close();
                        }

                    }
                    connects.Close();
                }
            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                hold_email = error.Message;
            }
            return hold_email;
        }
    }
}
