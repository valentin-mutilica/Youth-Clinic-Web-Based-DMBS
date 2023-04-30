using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Services
{
    public class editModel : PageModel
    {
        public ServicesInfo ServicesInfo = new ServicesInfo();
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

                    String sql = "SELECT * FROM MedicalServices WHERE serviceid=@serviceid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@serviceid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ServicesInfo.serviceid = "" + reader.GetInt32(0);
                                ServicesInfo.medical_service = reader.GetString(1);
                                ServicesInfo.doctor_name = reader.GetString(2);
                                ServicesInfo.service_department = reader.GetString(3);
                                ServicesInfo.service_description = reader.GetString(4);
                                ServicesInfo.price = "" + reader.GetInt32(5);

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
            ServicesInfo.serviceid = Request.Form["id"];
            ServicesInfo.medical_service = Request.Form["medical_service"];
            ServicesInfo.doctor_name = Request.Form["doctor_name"];
            ServicesInfo.service_department = Request.Form["service_department"];
            ServicesInfo.service_description = Request.Form["service_description"];
            ServicesInfo.price = Request.Form["price"];

            if (ServicesInfo.medical_service.Length == 0 || ServicesInfo.doctor_name.Length == 0 ||
               ServicesInfo.service_department.Length == 0 || ServicesInfo.service_description.Length == 0 || ServicesInfo.price.Length == 0)
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
                    String sql = "UPDATE MedicalServices " +
                                 "SET medical_service=@medical_service, doctor_name=@doctor_name, service_department=@service_department, service_description=@service_description, price=@price " +
                                 "WHERE serviceid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@medical_service", ServicesInfo.medical_service);
                        command.Parameters.AddWithValue("@doctor_name", ServicesInfo.doctor_name);
                        command.Parameters.AddWithValue("@service_department", ServicesInfo.service_department);
                        command.Parameters.AddWithValue("@service_description", ServicesInfo.service_description);
                        command.Parameters.AddWithValue("@price", ServicesInfo.price);
                        command.Parameters.AddWithValue("@id", ServicesInfo.serviceid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Services/Index");
        }
    }
}
