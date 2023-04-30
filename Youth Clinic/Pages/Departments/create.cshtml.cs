using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Departments
{
    public class createModel : PageModel
    {
        public DepartmentsInfo DepartmentsInfo = new DepartmentsInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            DepartmentsInfo.department_name = Request.Form["department_name"];
            DepartmentsInfo.description = Request.Form["description"];



            if (DepartmentsInfo.department_name.Length < 2 || DepartmentsInfo.description.Length < 2)
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
                    String sql = "INSERT INTO Departments " +
                        "(department_name, description) VALUES " +
                        "(@department_name, @description);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@department_name", DepartmentsInfo.department_name);
                        command.Parameters.AddWithValue("@description", DepartmentsInfo.description);


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
            DepartmentsInfo.department_name = "";
            DepartmentsInfo.description = "";

            successMessage = "New Department Added Successfuly";

            Response.Redirect("/Departments/Index");
        }
    }
}
