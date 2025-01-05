using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

public class LoginModel : PageModel
{
    [BindProperty]
    public string NombreUsuario { get; set; } = string.Empty;

    [BindProperty]
    public string Contrasena { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public IActionResult OnPost()
    {
        // Verifica si los campos están vacíos
        if (string.IsNullOrWhiteSpace(NombreUsuario) || string.IsNullOrWhiteSpace(Contrasena))
        {
            ErrorMessage = "Por favor, ingrese el usuario y la contraseña.";
            return Page();
        }

        try
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=localhost;Database=BaseRefaccionaria;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener la contraseña asociada al usuario
                string sql = "SELECT Contrasena FROM administradores WHERE NombreUsuario = @NombreUsuario";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);

                    var result = command.ExecuteScalar();

                    if (result != null && Contrasena == result.ToString())
                    {
                        // Guarda una sesión indicando que el usuario está autenticado
                        HttpContext.Session.SetString("UserAuthenticated", "true");

                        // Redirige a la página principal de clientes
                        return RedirectToPage("/OperacionesC/Index");
                    }
                }

                ErrorMessage = "Usuario o contraseña incorrectos.";
                return Page();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error al iniciar sesión: " + ex.Message;
            Console.WriteLine(ex.StackTrace);
            return Page();
        }
    }
}
