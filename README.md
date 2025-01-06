#PROYECTO DE Joshua Daniel Onofre Miranda
Gestión de Órdenes de Servicio - Refaccionaria de Motos
Este proyecto es una aplicación web para la gestión de órdenes de servicio de una refaccionaria de motos. Permite registrar, visualizar, editar y gestionar las órdenes de servicio de los clientes, así como realizar un seguimiento de los pagos y detalles de los servicios solicitados.

Requerimientos del Sistema
Requerimientos de Hardware:
Procesador: 1 GHz o superior
Memoria RAM: 4 GB o superior
Espacio en Disco Duro: 500 MB de espacio disponible
Pantalla: Resolución mínima de 1024x768 píxeles
Conexión a Internet: Requerida para actualizaciones y funciones en la nube (si aplica)
Requerimientos de Software:
Sistema Operativo: Windows 10 o superior, macOS, Linux
Navegador Web: Google Chrome, Mozilla Firefox, Microsoft Edge o Safari (versión actualizada)
.NET Core Runtime: Versión 3.1 o superior
Base de Datos: SQL Server, MySQL o PostgreSQL (según configuración del sistema)
Instalación
Clonar el repositorio:

bash
Copiar código
git clone 
cd Motos
Restaurar las dependencias:

bash
Copiar código
dotnet restore
Ejecutar el proyecto:

bash
Copiar código
dotnet run
Acceder al software:
Abra su navegador y diríjase a http://localhost:5211 para comenzar a usar el sistema.

Uso del Software
Pantalla Principal
La pantalla principal contiene las siguientes secciones clave:

Nueva Orden: Haga clic aquí para registrar una nueva orden de servicio para una moto.
Ver Ordenes: Acceda a la lista de órdenes registradas y consulte el estado de cada una.
Historial: Visualice las órdenes de servicio completadas o cerradas.
Configuración de Usuario: Editar sus datos y rol.
Botones y Funcionalidades
Botón "Guardar": Registra una nueva orden de servicio. Asegúrese de completar todos los campos necesarios.
Botón "Cancelar": Cancela la operación actual y regresa a la pantalla principal sin guardar cambios.
Estructura de Archivos
La estructura básica de carpetas es la siguiente:

bash
Copiar código
GestorOrdenesServicio/
├── Pages/                     # Contiene las páginas Razor
│   ├── Index.cshtml           # Página principal
│   ├── NuevaOrden.cshtml      # Página para crear nuevas órdenes
│   ├── VerOrdenes.cshtml      # Página para visualizar órdenes existentes
├── wwwroot/                   # Archivos estáticos como imágenes, estilos, JS
├── _Imports.razor             # Importaciones globales de Razor
├── appsettings.json           # Configuración de la aplicación
├── Program.cs                 # Código de configuración y arranque de la app
└── gestor-ordenes-servicio.csproj  # Proyecto y dependencias de la app

Licencia
Este proyecto está bajo la licencia Microsoft.NET.Sdk.Web - consulta el archivo LICENSE para más detalles.




