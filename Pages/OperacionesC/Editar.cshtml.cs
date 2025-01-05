using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Motos.Pages.OperacionesC
{
    public class EditarModel : PageModel
    {
        [BindProperty]
        public int IdOrden { get; set; }

        [BindProperty, Required(ErrorMessage = "Se requiere el nombre del dueño")]
        public string Dueño { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere el tipo de servicio")]
        public string Servicio { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere el ID del encargado")]
        public string EncargadoId { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere un número de teléfono")]
        public string Telefono { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere el monto del pago")]
        public decimal Pago { get; set; }

        [BindProperty]
        public string? Notas { get; set; }
        
        public string ErrorMessage { get; set; } = "";

        public void OnGet(int idOrden)
        {
            try
            {
                string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Ordenes WHERE Id_orden = @IdOrden";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdOrden", idOrden);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IdOrden = reader.GetInt32(0);
                                Dueño = reader.GetString(1);
                                Servicio = reader.GetString(2);
                                EncargadoId = reader.GetString(3);
                                Pago = reader.GetDecimal(4);
                                Telefono = reader.GetString(5);
                                Notas = reader.IsDBNull(6) ? null : reader.GetString(6);
                                
                            }
                            else
                            {
                                Response.Redirect("/OperacionesC/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al obtener los datos: {ex.Message}";
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Por favor, corrige los errores en el formulario.";

                // Depurar y mostrar todos los errores de validación
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Error en {error.Key}: {subError.ErrorMessage}");
                    }
                }

                return Page();
            }

            try
            {
                string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
            UPDATE Ordenes
            SET 
                dueño = @Dueño,
                servicio = @Servicio,
                encargado_id = @EncargadoId,
                pago = @Pago,
                telefono = @Telefono,
                notas = @Notas
            WHERE Id_orden = @IdOrden";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdOrden", IdOrden);
                        command.Parameters.AddWithValue("@Dueño", Dueño);
                        command.Parameters.AddWithValue("@Servicio", Servicio);
                        command.Parameters.AddWithValue("@EncargadoId", EncargadoId);
                        command.Parameters.AddWithValue("@Pago", Pago);
                        command.Parameters.AddWithValue("@Telefono", Telefono);
                        command.Parameters.AddWithValue("@Notas", Notas ?? (object)DBNull.Value);
                        

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("/OperacionesC/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ocurrió un error al actualizar los datos: {ex.Message}";
                return Page();
            }
        }
    }
}
