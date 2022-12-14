using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TesteCandidato;
using TesteWeb.Models;

namespace TesteWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ManipulaDadosCep dadosCep = new ManipulaDadosCep();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index(string? cep)
        //{
        //    if (cep != null)
        //    {
        //        var dados = dadosCep.DigitaCEPCasoNaoExistaCadastra(cep);
        //        return View(dados);
        //    }
        //    return View();
        //}
        public IActionResult Index(string? cep = "93180000")
        {
            //if (logradouro != null)
            //{
            //    var dados = dadosCep.BuscaPorLogradouro(logradouro);
            //    return View(dados);
            //}
            //else
            //{
            var lista = dadosCep.DigitaCEPCasoNaoExistaCadastra(cep);
            return View(lista);
            //}
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}