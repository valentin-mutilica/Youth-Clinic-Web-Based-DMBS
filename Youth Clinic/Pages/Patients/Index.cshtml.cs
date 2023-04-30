using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Youth_Clinic.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientsInfo> listPatients = new List<PatientsInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Patients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientsInfo PatientsInfo = new PatientsInfo();

                                PatientsInfo.patientid = "" + reader.GetInt32(0);
                                PatientsInfo.patient_name = reader.GetString(1);
                                PatientsInfo.gender = reader.GetString(2);
                                PatientsInfo.date_of_birth = reader.GetString(3);
                                PatientsInfo.phone_number = reader.GetString(4);
                                PatientsInfo.email = reader.GetString(5);
                                PatientsInfo.address = reader.GetString(6);

                                listPatients.Add(PatientsInfo);
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

    public class PatientsInfo
    {
        public String patientid;
        public String patient_name;
        public String gender;
        public String date_of_birth;
        public String phone_number;
        public String email;
        public String address;
    }
}

