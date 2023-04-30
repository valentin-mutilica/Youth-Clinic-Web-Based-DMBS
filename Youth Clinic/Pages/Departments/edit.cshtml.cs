using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Youth_Clinic.Pages.Departments
{
    public class editModel : PageModel
    {
        public DepartmentsInfo DepartmentsInfo = new DepartmentsInfo();
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

                    String sql = "SELECT * FROM Departments WHERE departmentid=@departmentid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@departmentid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DepartmentsInfo.departmentid = "" + reader.GetInt32(0);
                                DepartmentsInfo.department_name = reader.GetString(1);
                                DepartmentsInfo.description = reader.GetString(2);


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
            DepartmentsInfo.departmentid = Request.Form["id"];
            DepartmentsInfo.department_name = Request.Form["department_name"];
            DepartmentsInfo.description = Request.Form["description"];



            if (DepartmentsInfo.departmentid.Length == 0 || DepartmentsInfo.department_name.Length == 0 || DepartmentsInfo.description.Length == 0)
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
                    String sql = "UPDATE Departments " +
                                 "SET department_name=@department_name, description=@description " +
                                 "WHERE departmentid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@department_name", DepartmentsInfo.department_name);
                        command.Parameters.AddWithValue("@description", DepartmentsInfo.description);
                        command.Parameters.AddWithValue("@id", DepartmentsInfo.departmentid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Departments/Index");
        }
    }
}
