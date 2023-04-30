using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Patients
{
    public class createModel : PageModel
    {
        public PatientsInfo PatientsInfo = new PatientsInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            PatientsInfo.patient_name = Request.Form["patient_name"];
            PatientsInfo.gender = Request.Form["gender"];
            PatientsInfo.date_of_birth = Request.Form["date_of_birth"];
            PatientsInfo.phone_number = Request.Form["phone_number"];
            PatientsInfo.email = Request.Form["email"];
            PatientsInfo.address = Request.Form["address"];


            if (PatientsInfo.patient_name.Length == 0 || PatientsInfo.date_of_birth.Length == 0 ||
               PatientsInfo.gender.Length == 0 || PatientsInfo.email.Length == 0 || PatientsInfo.phone_number.Length == 0 || PatientsInfo.address.Length == 0)
            {
                errorMessage = "All fields are required!! Please make sure to fill in all the information.";
                return;
            }
            //save the customer into the database
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Patients " +
                        "(patient_name, gender, date_of_birth, phone_number, email, address) VALUES " +
                        "(@patient_name, @gender, @date_of_birth, @phone_number, @email, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@patient_name", PatientsInfo.patient_name);
                        command.Parameters.AddWithValue("@gender", PatientsInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", PatientsInfo.date_of_birth);
                        command.Parameters.AddWithValue("@phone_number", PatientsInfo.phone_number);
                        command.Parameters.AddWithValue("@email", PatientsInfo.email);
                        command.Parameters.AddWithValue("@address", PatientsInfo.address);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            //Initialization of the parameters
            PatientsInfo.patient_name = "";
            PatientsInfo.gender = "";
            PatientsInfo.date_of_birth = "";
            PatientsInfo.phone_number = "";
            PatientsInfo.email = "";
            PatientsInfo.address = "";

            successMessage = "New Patient Added Successfuly";

            Response.Redirect("/Patients/Index");
        }
    }
}
