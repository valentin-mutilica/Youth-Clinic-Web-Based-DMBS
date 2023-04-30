using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Youth_Clinic.Pages.Nurses
{
    public class IndexModel : PageModel
    {
        public List<NursesInfo> listNurses = new List<NursesInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Nurses ORDER BY NURSE_DEPARTMENT";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NursesInfo NursesInfo = new NursesInfo();

                                NursesInfo.nurseid = " " + reader.GetInt32(0);
                                NursesInfo.nurse_name = reader.GetString(1);
                                NursesInfo.gender = reader.GetString(2);
                                NursesInfo.date_of_birth = reader.GetString(3);
                                NursesInfo.nurse_department = reader.GetString(4);
                                NursesInfo.phone_number = reader.GetString(5);
                                NursesInfo.email = reader.GetString(6);

                                listNurses.Add(NursesInfo);
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

    public class NursesInfo
    {
        public String nurseid;
        public String nurse_name;
        public String gender;
        public String date_of_birth;
        public String nurse_department;
        public String phone_number;
        public String email;

    }
}

