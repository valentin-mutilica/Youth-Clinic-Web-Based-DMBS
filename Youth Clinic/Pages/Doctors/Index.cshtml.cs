using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Youth_Clinic.Pages.Doctors
{
    public class IndexModel : PageModel
    {
        public List<DoctorsInfo> listDoctors = new List<DoctorsInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Doctors ORDER BY DEPARTMENT";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DoctorsInfo DoctorsInfo = new DoctorsInfo();

                                DoctorsInfo.doctorid = "" + reader.GetInt32(0);
                                DoctorsInfo.doctor_name = reader.GetString(1);
                                DoctorsInfo.gender = reader.GetString(2);
                                DoctorsInfo.date_of_birth = reader.GetString(3);
                                DoctorsInfo.department = reader.GetString(4);
                                DoctorsInfo.phone_number = reader.GetString(5);
                                DoctorsInfo.email = reader.GetString(6);

                                listDoctors.Add(DoctorsInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }

    public class DoctorsInfo
    {
        public String doctorid;
        public String doctor_name;
        public String gender;
        public String date_of_birth;
        public String department;
        public String phone_number;
        public String email;

    }
}

