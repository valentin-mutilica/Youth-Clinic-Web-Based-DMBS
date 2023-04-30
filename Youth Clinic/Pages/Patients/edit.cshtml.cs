using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient; 

namespace Youth_Clinic.Pages.Patients
{
    public class editModel : PageModel
    {
        public PatientsInfo PatientsInfo = new PatientsInfo();
        public String errorMessage = "";
        public String successMessage = "";




        //to get the data of the specific Patients
        public void OnGet()
        {
            string id = Request.Query["id"];
            

            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM Patients WHERE patientid=@patientid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@patientid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                PatientsInfo.patientid = "" + reader.GetInt32(0);
                                PatientsInfo.patient_name = reader.GetString(1);
                                PatientsInfo.gender = reader.GetString(2);
                                PatientsInfo.date_of_birth = reader.GetString(3);
                                PatientsInfo.phone_number = reader.GetString(4);
                                PatientsInfo.email = reader.GetString(5);
                                PatientsInfo.address = reader.GetString(6);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        
        //to edit the parameters of the selected Patients
        public void OnPost()
        {
            PatientsInfo.patientid = Request.Form["id"];
            PatientsInfo.patient_name = Request.Form["patient_name"];
            PatientsInfo.gender = Request.Form["gender"];
            PatientsInfo.date_of_birth = Request.Form["date_of_birth"];
            PatientsInfo.phone_number = Request.Form["phone_number"];
            PatientsInfo.email = Request.Form["email"];
            PatientsInfo.address = Request.Form["address"];

            if (PatientsInfo.patientid.Length == 0 || PatientsInfo.patient_name.Length == 0 || PatientsInfo.date_of_birth.Length == 0 ||
               PatientsInfo.gender.Length == 0 || PatientsInfo.email.Length == 0 || PatientsInfo.phone_number.Length == 0 || PatientsInfo.address.Length == 0)
            {
                errorMessage = "All fields are required!! Please make sure to fill in all the information.";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Patients " +
                                 "SET patient_name=@patient_name, gender=@gender, date_of_birth=@date_of_birth, phone_number=@phone_number, email=@email, address=@address " +
                                 "WHERE patientid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@patient_name", PatientsInfo.patient_name);
                        command.Parameters.AddWithValue("@gender", PatientsInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", PatientsInfo.date_of_birth);
                        command.Parameters.AddWithValue("@phone_number", PatientsInfo.phone_number);
                        command.Parameters.AddWithValue("@email", PatientsInfo.email);
                        command.Parameters.AddWithValue("@address", PatientsInfo.address);
                        command.Parameters.AddWithValue("@id", PatientsInfo.patientid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Patients/Index");
        }
    }
}