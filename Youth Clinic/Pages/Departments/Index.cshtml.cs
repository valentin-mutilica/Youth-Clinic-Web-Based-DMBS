using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Departments
{
    public class IndexModel : PageModel
    {
        public List<DepartmentsInfo> listDepartments = new List<DepartmentsInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Departments";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartmentsInfo DepartmentsInfo = new DepartmentsInfo();

                                DepartmentsInfo.departmentid = "" + reader.GetInt32(0);
                                DepartmentsInfo.department_name = reader.GetString(1);
                                DepartmentsInfo.description = reader.GetString(2);

                                listDepartments.Add(DepartmentsInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }

    public class DepartmentsInfo
    {
        public String departmentid;
        public String department_name;
        public String description;

    }
}


