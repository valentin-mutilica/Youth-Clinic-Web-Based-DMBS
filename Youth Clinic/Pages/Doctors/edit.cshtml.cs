using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Youth_Clinic.Pages.Doctors
{
    public class editModel : PageModel
    {
        public DoctorsInfo DoctorsInfo = new DoctorsInfo();
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

                    String sql = "SELECT * FROM Doctors WHERE doctorid=@doctorid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@doctorid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DoctorsInfo.doctorid = "" + reader.GetInt32(0);
                                DoctorsInfo.doctor_name = reader.GetString(1);
                                DoctorsInfo.gender = reader.GetString(2);
                                DoctorsInfo.date_of_birth = reader.GetString(3);
                                DoctorsInfo.department = reader.GetString(4);
                                DoctorsInfo.phone_number = reader.GetString(5);
                                DoctorsInfo.email = reader.GetString(6);
                                

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
            DoctorsInfo.doctorid = Request.Form["id"];
            DoctorsInfo.doctor_name = Request.Form["doctor_name"];
            DoctorsInfo.gender = Request.Form["gender"];
            DoctorsInfo.date_of_birth = Request.Form["date_of_birth"];
            DoctorsInfo.department = Request.Form["department"];
            DoctorsInfo.phone_number = Request.Form["phone_number"];
            DoctorsInfo.email = Request.Form["email"];
            

            if (DoctorsInfo.doctorid.Length == 0 || DoctorsInfo.doctor_name.Length == 0 || DoctorsInfo.date_of_birth.Length == 0 ||
               DoctorsInfo.gender.Length == 0 || DoctorsInfo.email.Length == 0 || DoctorsInfo.phone_number.Length == 0 || DoctorsInfo.department.Length == 0)
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
                    String sql = "UPDATE Doctors " +
                                 "SET doctor_name=@doctor_name, gender=@gender, date_of_birth=@date_of_birth, department=@department, phone_number=@phone_number, email=@email " +
                                 "WHERE doctorid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@doctor_name", DoctorsInfo.doctor_name);
                        command.Parameters.AddWithValue("@gender", DoctorsInfo.gender);
                        command.Parameters.AddWithValue("@date_of_birth", DoctorsInfo.date_of_birth);
                        command.Parameters.AddWithValue("@department", DoctorsInfo.department);
                        command.Parameters.AddWithValue("@phone_number", DoctorsInfo.phone_number);
                        command.Parameters.AddWithValue("@email", DoctorsInfo.email);
                        command.Parameters.AddWithValue("@id", DoctorsInfo.doctorid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Doctors/Index");
        }
    }
}
