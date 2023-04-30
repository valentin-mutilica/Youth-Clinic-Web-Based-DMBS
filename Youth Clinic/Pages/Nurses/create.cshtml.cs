using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Nurses
{
    public class createModel : PageModel
    {
        public NursesInfo NursesInfo = new NursesInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            NursesInfo.nurse_name = Request.Form["nurse_name"];
            NursesInfo.gender = Request.Form["gender"];
            NursesInfo.date_of_birth = Request.Form["date_of_birth"];
            NursesInfo.nurse_department = Request.Form["nurse_department"];
            NursesInfo.phone_number = Request.Form["phone_number"];
            NursesInfo.email = Request.Form["email"];


            if (NursesInfo.nurse_name.Length == 0 || NursesInfo.date_of_birth.Length == 0 ||
               NursesInfo.gender.Length == 0 || NursesInfo.nurse_department.Length == 0 || NursesInfo.phone_number.Length == 0 || NursesInfo.email.Length == 0)
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
                    String sql = "INSERT INTO Nurses " +
                        "(nurse_name, gender, date_of_birth, nurse_department, phone_number, email) VALUES " +
                        "(@nurse_name, @gender, @date_of_birth, @nurse_department, @phone_number, @email);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nurse_name", NursesInfo.nurse_name);
                        command.Parameters.AddWithValue("@gender", NursesInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", NursesInfo.date_of_birth);
                        command.Parameters.AddWithValue("@nurse_department", NursesInfo.nurse_department);
                        command.Parameters.AddWithValue("@phone_number", NursesInfo.phone_number);
                        command.Parameters.AddWithValue("@email", NursesInfo.email);


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
            NursesInfo.nurse_name = "";
            NursesInfo.gender = "";
            NursesInfo.date_of_birth = "";
            NursesInfo.nurse_department = "";
            NursesInfo.phone_number = "";
            NursesInfo.email = "";


            successMessage = "New nurse Added Successfuly";

            Response.Redirect("/Nurses/Index");
        }
    }
}
