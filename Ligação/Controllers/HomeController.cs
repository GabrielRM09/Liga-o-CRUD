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
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = "Server=localhost;Database=cad_login;user=root;password=123456";
            string query = "Delete from new_table where @id = id";
            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();
                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuario Deletado com Sucesso"
                    : "Falha ao Deletar o Usuario";
            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao deletar o usuario: {ex.Message}";
            }
            return View();
        }
        public IActionResult Ler()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ler(int id)
        {
            string connectionString = "Server=localhost;Database=cad_login;user=root;password=123456";
            string query = "Select * from new_table where id = @id";
            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ViewData["Message"] = "Usuario Encontrado";
                    ViewData["usuarioe"] = reader["usuario"].ToString();
                    ViewData["senhae"] = reader["senha"].ToString();
                }
                else
                {
                    ViewData["Message"] = "Usuario Não Encontrado";
                }
            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao buscar o usuario: {ex.Message}";
            }

            return View();
        }
        public IActionResult UPDATE()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UPDATE(int id, string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha)) 
            {
                ViewData["Message"] = "O nome de usuario ou senha não podem ser vazios";
            }

            string connectionString = "Server=localhost;Database=cad_login;user=root;password=123456";
            string query = "UPDATE new_table SET usuario=@usuario, senha=@senha where id =@id";
            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();
                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuário atualizado com sucesso!"
                    : "Falha ao atualizar o usuário.";

            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao atualizar o usuario: {ex.Message}";
            }
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha)) 
            {
                ViewData["Menssage"] = "O nome de usuario ou senha não podem ser vazios";
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

                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuário cadastrado com sucesso!" 
                    : "Falha ao cadastrar o usuário.";
            }
            catch (Exception ex)
            {
                ViewData["Menssage"] = $"Erro ao cadastrar o usuario: {ex.Message}";
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
