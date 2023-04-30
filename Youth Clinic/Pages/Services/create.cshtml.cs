using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Youth_Clinic.Pages.Patients;

namespace Youth_Clinic.Pages.Services
{
    public class createModel : PageModel
    {
        public ServicesInfo ServicesInfo = new ServicesInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
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
            //save the service into the database
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO MedicalServices " +
                        "(medical_service, doctor_name, service_department, service_description, price) VALUES " +
                        "(@medical_service, @doctor_name, @service_department, @service_description, @price);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@medical_service", ServicesInfo.medical_service);
                        command.Parameters.AddWithValue("@doctor_name", ServicesInfo.doctor_name);
                        command.Parameters.AddWithValue("@service_department", ServicesInfo.service_department);
                        command.Parameters.AddWithValue("@service_description", ServicesInfo.service_description);
                        command.Parameters.AddWithValue("@price", ServicesInfo.price);

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
            ServicesInfo.medical_service = "";
            ServicesInfo.doctor_name = "";
            ServicesInfo.service_department = "";
            ServicesInfo.service_description = "";
            ServicesInfo.price = "";

            successMessage = "New Service Added Successfuly";

            Response.Redirect("/Services/Index");
        }
    }
}
