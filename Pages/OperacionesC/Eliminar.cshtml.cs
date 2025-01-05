using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Motos.Pages.OperacionesC
{
    public class Eliminar : PageModel
    {
        // Variable pública para pasar datos al front-end
        public int OrdenId { get; set; }

        public void OnGet(int id)
        {
            // Asigna el ID de la orden para mostrarlo en la pantalla
            OrdenId = id;
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                eliminarOrden(id);
                return RedirectToPage("/OperacionesC/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la orden: {ex.Message}");               
                return RedirectToPage("/Error");
            }
        }

        private void eliminarOrden(int id)
        {
            try
            {
                // Cadena de conexión a la base de datos
                string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sentencia SQL para eliminar el registro
                    string sql = "DELETE FROM Ordenes WHERE Id_orden = @IdOrden";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Parámetro para evitar inyecciones SQL
                        command.Parameters.AddWithValue("@IdOrden", id);

                        // Ejecutar la consulta y verificar si se afectaron filas
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No se encontró la orden especificada para eliminar.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error al interactuar con la base de datos: " + sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo eliminar la orden: " + ex.Message);
                throw;
            }
        }
    }
}
