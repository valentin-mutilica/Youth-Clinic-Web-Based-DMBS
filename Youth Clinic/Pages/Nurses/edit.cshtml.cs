using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Nurses
{
    public class editModel : PageModel
    {
        public NursesInfo NursesInfo = new NursesInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

            string id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM Nurses WHERE nurseid=@nurseid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nurseid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NursesInfo.nurseid = "" + reader.GetInt32(0);
                                NursesInfo.nurse_name = reader.GetString(1);
                                NursesInfo.gender = reader.GetString(2);
                                NursesInfo.date_of_birth = reader.GetString(3);
                                NursesInfo.nurse_department = reader.GetString(4);
                                NursesInfo.phone_number = reader.GetString(5);
                                NursesInfo.email = reader.GetString(6);


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
            NursesInfo.nurseid = Request.Form["id"];
            NursesInfo.nurse_name = Request.Form["nurse_name"];
            NursesInfo.gender = Request.Form["gender"];
            NursesInfo.date_of_birth = Request.Form["date_of_birth"];
            NursesInfo.nurse_department = Request.Form["nurse_department"];
            NursesInfo.phone_number = Request.Form["phone_number"];
            NursesInfo.email = Request.Form["email"];


            if (NursesInfo.nurseid.Length == 0 || NursesInfo.nurse_name.Length == 0 || NursesInfo.date_of_birth.Length == 0 ||
               NursesInfo.gender.Length == 0 || NursesInfo.email.Length == 0 || NursesInfo.phone_number.Length == 0 || NursesInfo.nurse_department.Length == 0)
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
                    String sql = "UPDATE Nurses " +
                                 "SET nurse_name=@nurse_name, gender=@gender, date_of_birth=@date_of_birth, nurse_department=@nurse_department, phone_number=@phone_number, email=@email " +
                                 "WHERE nurseid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nurse_name", NursesInfo.nurse_name);
                        command.Parameters.AddWithValue("@gender", NursesInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", NursesInfo.date_of_birth);
                        command.Parameters.AddWithValue("@nurse_department", NursesInfo.nurse_department);
                        command.Parameters.AddWithValue("@phone_number", NursesInfo.phone_number);
                        command.Parameters.AddWithValue("@email", NursesInfo.email);
                        command.Parameters.AddWithValue("@id", NursesInfo.nurseid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Nurses/Index");
        }
    }
}
