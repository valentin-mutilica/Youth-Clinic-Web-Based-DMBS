using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        public List<AppointmentsInfo> ListAppointments = new List<AppointmentsInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Appointments";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AppointmentsInfo AppointmentsInfo = new AppointmentsInfo();

                                AppointmentsInfo.appointmentid = "" + reader.GetInt32(0);
                                AppointmentsInfo.patient_name = reader.GetString(1);
                                AppointmentsInfo.m_service = reader.GetString(2);
                                AppointmentsInfo.doctor_in_charge = reader.GetString(3);
                                AppointmentsInfo.appointment_date = reader.GetString(4);
                                AppointmentsInfo.appointment_time = reader.GetString(5);
                                AppointmentsInfo.appointment_notes = reader.GetString(6);
                                AppointmentsInfo.created_at = reader.GetDateTime(7).ToString();

                                ListAppointments.Add(AppointmentsInfo);
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

    public class AppointmentsInfo
    {
        public String appointmentid;
        public String patient_name;
        public String m_service;
        public String doctor_in_charge;
        public string appointment_date;
        public string appointment_time;
        public String appointment_notes;
        public String created_at;
    }
}
