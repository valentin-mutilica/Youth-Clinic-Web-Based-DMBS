using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Services
{
    public class IndexModel : PageModel
    {
        public List<ServicesInfo> listServices = new List<ServicesInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MedicalServices ORDER BY SERVICE_DEPARTMENT";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ServicesInfo ServicesInfo = new ServicesInfo();

                                ServicesInfo.serviceid = "" + reader.GetInt32(0);
                                ServicesInfo.medical_service = reader.GetString(1);
                                ServicesInfo.doctor_name = reader.GetString(2);
                                ServicesInfo.service_department = reader.GetString(3);
                                ServicesInfo.service_description = reader.GetString(4);
                                ServicesInfo.price = "" + reader.GetInt32(5);


                                listServices.Add(ServicesInfo);
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

    public class ServicesInfo
    {
        public String serviceid;
        public String medical_service;
        public String doctor_name;
        public String service_department;
        public String service_description;
        public String price;
    }
}

