using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Doctors
{
    public class createModel : PageModel
    {
        public DoctorsInfo DoctorsInfo = new DoctorsInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            DoctorsInfo.doctor_name = Request.Form["doctor_name"];
            DoctorsInfo.gender = Request.Form["gender"];
            DoctorsInfo.date_of_birth = Request.Form["date_of_birth"];
            DoctorsInfo.department = Request.Form["department"];
            DoctorsInfo.phone_number = Request.Form["phone_number"];
            DoctorsInfo.email = Request.Form["email"];


            if (DoctorsInfo.doctor_name.Length == 0 || DoctorsInfo.date_of_birth.Length == 0 ||
               DoctorsInfo.gender.Length == 0 || DoctorsInfo.department.Length == 0 || DoctorsInfo.phone_number.Length == 0 || DoctorsInfo.email.Length == 0)
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
                    String sql = "INSERT INTO Doctors " +
                        "(doctor_name, gender, date_of_birth, department, phone_number, email) VALUES " +
                        "(@doctor_name, @gender, @date_of_birth, @department, @phone_number, @email);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@doctor_name", DoctorsInfo.doctor_name);
                        command.Parameters.AddWithValue("@gender", DoctorsInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", DoctorsInfo.date_of_birth);
                        command.Parameters.AddWithValue("@department", DoctorsInfo.department);
                        command.Parameters.AddWithValue("@phone_number", DoctorsInfo.phone_number);
                        command.Parameters.AddWithValue("@email", DoctorsInfo.email);

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
            DoctorsInfo.doctor_name = "";
            DoctorsInfo.gender = "";
            DoctorsInfo.date_of_birth = "";
            DoctorsInfo.department = "";
            DoctorsInfo.phone_number = "";
            DoctorsInfo.email = "";
            

            successMessage = "New Doctor Added Successfuly";

            Response.Redirect("/Doctors/Index");
        }
    }
}
