using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Appointments
{
    public class createModel : PageModel
    {
        public AppointmentsInfo AppointmentsInfo = new AppointmentsInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            AppointmentsInfo.patient_name = Request.Form["patient_name"];
            AppointmentsInfo.m_service = Request.Form["m_service"];
            AppointmentsInfo.doctor_in_charge = Request.Form["doctor_in_charge"];
            AppointmentsInfo.appointment_date = Request.Form["appointment_date"];
            AppointmentsInfo.appointment_time = Request.Form["appointment_time"];
            AppointmentsInfo.appointment_notes = Request.Form["appointment_notes"];


            if (AppointmentsInfo.patient_name.Length == 0 || AppointmentsInfo.m_service.Length == 0 ||
               AppointmentsInfo.doctor_in_charge.Length == 0 || AppointmentsInfo.appointment_date.Length == 0 || AppointmentsInfo.appointment_time.Length == 0)
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
                    String sql = "INSERT INTO Appointments " +
                        "(patient_name, m_service, doctor_in_charge, appointment_date, appointment_time, appointment_notes) VALUES " +
                        "(@patient_name, @m_service, @doctor_in_charge, @appointment_date, @appointment_time, @appointment_notes);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@patient_name", AppointmentsInfo.patient_name);
                        command.Parameters.AddWithValue("@m_service", AppointmentsInfo.m_service);
                        command.Parameters.AddWithValue("@doctor_in_charge", AppointmentsInfo.doctor_in_charge);
                        command.Parameters.AddWithValue("@appointment_date", AppointmentsInfo.appointment_date);
                        command.Parameters.AddWithValue("@appointment_time", AppointmentsInfo.appointment_time);
                        command.Parameters.AddWithValue("@appointment_notes", AppointmentsInfo.appointment_notes);

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
            AppointmentsInfo.patient_name = "";
            AppointmentsInfo.m_service = "";
            AppointmentsInfo.doctor_in_charge = "";
            AppointmentsInfo.appointment_date = "";
            AppointmentsInfo.appointment_time = "";
            AppointmentsInfo.appointment_notes = "";

            successMessage = "New Appointment Added Successfuly";

            Response.Redirect("/Appointments/Index");
        }
    }
}
