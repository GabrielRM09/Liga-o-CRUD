using System.Diagnostics;
using Ligação.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Ligação.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Cadastra(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha)) 
            {
                ViewData["Mensage"] = "O nome de usuario ou senha não podem ser vazios";
            }
            string connectionString = "server=localhost;Database=cad_login;user=root;password=123456";
            string query = "INSERT INTO new_table (usuario, senha) VALUES (@usuario, @senha)";
            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@senha", senha);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Mensagem"] = linhasAfetadas > 0
                    ? "Usuário cadastrado com sucesso!" 
                    : "Falha ao cadastrar o usuário.";
            }
            catch (Exception ex)
            {
                ViewData["Menssagem"] = $"Erro ao cadastrar o usuario: {ex.Message}";
            }
           
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
