using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Motos.Pages.OperacionesC
{
    public class Index : PageModel
    {
        // Lista para almacenar las órdenes recuperadas de la base de datos
        public List<OrdenInfo> OrdenList { get; set; } = new List<OrdenInfo>();

        // Método que se ejecuta al cargar la página
        public void OnGet()
        {
            try
            {
                // Cadena de conexión a la base de datos
                string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre la conexión a la base de datos
                    connection.Open();

                    // Consulta SQL para recuperar los datos de la tabla 'ordenes'
                    string sql = "SELECT * FROM Ordenes";

                    // Comando para ejecutar la consulta SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Ejecuta la consulta y obtiene los resultados en un SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Recorre los resultados fila por fila
                            while (reader.Read())
                            {
                                // Verifica si la fila tiene al menos 7 columnas esperadas
                                if (reader.FieldCount >= 7)
                                {
                                    // Crea una nueva instancia de OrdenInfo para almacenar los datos de la fila actual
                                    OrdenInfo ordenInfo = new OrdenInfo
                                    {
                                        IdOrden = reader.GetInt32(0), // Obtiene el valor de la columna 'dueño' o una cadena vacía si es NULL
                                        Dueño = reader.IsDBNull(1) ? string.Empty : reader.GetString(1), // Obtiene el valor de la columna 'dueño' o una cadena vacía si es NULL
                                        Servicio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2), // Obtiene el valor de la columna 'servicio' o una cadena vacía si es NULL
                                        EncargadoId = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // Obtiene el valor de la columna 'encargado_id' o una cadena vacía si es NULL
                                        Pago = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4), // Obtiene el valor de la columna 'pago' o 0 si es NULL
                                        Telefono = reader.IsDBNull(5) ? string.Empty : reader.GetString(5), // Obtiene el valor de la columna 'telefono' o una cadena vacía si es NULL
                                        Notas = reader.IsDBNull(6) ? string.Empty : reader.GetString(6), // Obtiene el valor de la columna 'notas' o una cadena vacía si es NULL
                                        FechaHora = reader.IsDBNull(7) ? string.Empty : reader.GetDateTime(7).ToString("MM/dd/yyyy HH:mm") // Obtiene la fecha y hora formateada o una cadena vacía si es NULL
                                    };

                                    // Agrega la orden a la lista
                                    OrdenList.Add(ordenInfo);
                                }
                                else
                                {
                                    // Imprime un mensaje si el número de columnas no coincide con lo esperado
                                    Console.WriteLine("Número de columnas no esperado.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura e imprime cualquier error ocurrido durante la conexión o ejecución de la consulta
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // Clase que representa una orden
    public class OrdenInfo
    {
        public int IdOrden {get; set;}
        public string Dueño { get; set; } = ""; // Propiedad para el nombre del dueño
        public string Servicio { get; set; } = ""; // Propiedad para el servicio proporcionado
        public string EncargadoId { get; set; } = ""; // Propiedad para el ID del encargado
        public decimal Pago { get; set; } = 0; // Propiedad para el monto de pago
        public string Telefono { get; set; } = ""; // Propiedad para el número de teléfono
        public string Notas { get; set; } = ""; // Propiedad para notas adicionales
        public string FechaHora { get; set; } = ""; // Propiedad para la fecha y hora del registro
    }
}
