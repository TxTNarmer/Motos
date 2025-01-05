using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Motos.Pages.OperacionesC
{
    public class Crear : PageModel
    {
        // Propiedades que coinciden con los campos del formulario
        [BindProperty, Required(ErrorMessage = "Se requiere un ID de la orden")]
        public int IdOrden { get; set; }

        [BindProperty, Required(ErrorMessage = "Se requiere el nombre del dueño")]
        public string Dueño { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere el servicio")]
        public string Servicio { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere el ID del encargado")]
        public int EncargadoId { get; set; }

        [BindProperty, Required(ErrorMessage = "Se requiere el pago")]
        public decimal Pago { get; set; }

        [BindProperty, Required(ErrorMessage = "Se requiere el teléfono")]
        public string? Telefono { get; set; } = "";

        [BindProperty]
        public string? Notas { get; set; }

        // Variable para mensajes de error
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
        }

        // Método que maneja el POST cuando se envía el formulario
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // Asignar valores predeterminados si son nulos
            Telefono ??= "";
            Notas ??= "";

            // Crear nueva orden en la base de datos
            try
            {
                string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL para insertar nueva orden
                    string sql = "INSERT INTO ordenes (dueño, servicio, encargado_id, pago, telefono, notas) " +
                                 "VALUES (@Dueño, @Servicio, @EncargadoId, @Pago, @Telefono, @Notas)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                    
                        command.Parameters.AddWithValue("@Dueño", Dueño);
                        command.Parameters.AddWithValue("@Servicio", Servicio);
                        command.Parameters.AddWithValue("@EncargadoId", EncargadoId);
                        command.Parameters.AddWithValue("@Pago", Pago);
                        command.Parameters.AddWithValue("@Telefono", Telefono);
                        command.Parameters.AddWithValue("@Notas", Notas);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ErrorMessage = ex.Message;
                return;
            }

            // Redirigir a la página de índice
            Response.Redirect("/OperacionesC/Index");
        }
    }
}
