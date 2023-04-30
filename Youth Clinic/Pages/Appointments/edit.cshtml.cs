using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Youth_Clinic.Pages.Appointments
{
    public class editModel : PageModel
    {
        public AppointmentsInfo AppointmentsInfo = new AppointmentsInfo();
        public String errorMessage = "";
        public String successMessage = "";




        //to get the data of the specific Appointments
        public void OnGet()
        {
            string id = Request.Query["id"];


            try
            {
                String connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM Appointments WHERE appointmentid=@appointmentid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@appointmentid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AppointmentsInfo.appointmentid = "" + reader.GetInt32(0);
                                AppointmentsInfo.patient_name = reader.GetString(1);
                                AppointmentsInfo.m_service = reader.GetString(2);
                                AppointmentsInfo.doctor_in_charge = reader.GetString(3);
                                AppointmentsInfo.appointment_date = reader.GetString(4);
                                AppointmentsInfo.appointment_time = reader.GetString(5);
                                AppointmentsInfo.appointment_notes = reader.GetString(6);

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


        //to edit the parameters of the selected Appointments
        public void OnPost()
        {
            AppointmentsInfo.appointmentid = Request.Form["id"];
            AppointmentsInfo.patient_name = Request.Form["patient_name"];
            AppointmentsInfo.m_service = Request.Form["m_service"];
            AppointmentsInfo.doctor_in_charge = Request.Form["doctor_in_charge"];
            AppointmentsInfo.appointment_date = Request.Form["appointment_date"];
            AppointmentsInfo.appointment_time = Request.Form["appointment_time"];
            AppointmentsInfo.appointment_notes = Request.Form["appointment_notes"];

            if (AppointmentsInfo.appointmentid.Length == 0 || AppointmentsInfo.patient_name.Length == 0 || AppointmentsInfo.doctor_in_charge.Length == 0 ||
               AppointmentsInfo.m_service.Length == 0 || AppointmentsInfo.appointment_time.Length == 0 || AppointmentsInfo.appointment_date.Length == 0 || AppointmentsInfo.appointment_notes.Length == 0)
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
                    String sql = "UPDATE Appointments " +
                                 "SET patient_name=@patient_name, m_service=@m_service, doctor_in_charge=@doctor_in_charge, appointment_date=@appointment_date, appointment_time=@appointment_time, appointment_notes=@appointment_notes " +
                                 "WHERE appointmentid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@patient_name", AppointmentsInfo.patient_name);
                        command.Parameters.AddWithValue("@m_service", AppointmentsInfo.m_service);
                        command.Parameters.AddWithValue("@doctor_in_charge", AppointmentsInfo.doctor_in_charge);
                        command.Parameters.AddWithValue("@appointment_date", AppointmentsInfo.appointment_date);
                        command.Parameters.AddWithValue("@appointment_time", AppointmentsInfo.appointment_time);
                        command.Parameters.AddWithValue("@appointment_notes", AppointmentsInfo.appointment_notes);
                        command.Parameters.AddWithValue("@id", AppointmentsInfo.appointmentid);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Appointments/Index");
        }
    }
}
