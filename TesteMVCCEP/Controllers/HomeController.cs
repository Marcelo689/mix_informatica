using System.Collections.Generic;
using System.Web.Mvc;

namespace TesteMVCCEP.Controllers
{
    public class HomeController : Controller
    {
        private ManipulaDadosCep dadosCep = new ManipulaDadosCep();
        //public ActionResult Index(string cep = "93180000")
        //{
        //    //if (logradouro != null)
        //    //{
        //    //    var dados = dadosCep.BuscaPorLogradouro(logradouro);
        //    //    return View(dados);
        //    //}
        //    //else
        //    //{
        //    var lista = dadosCep.DigitaCEPCasoNaoExistaCadastra(cep);
        //    return View(lista);
        //    //}
        //}
        public ActionResult Index(string cep, string logradouro)
        {
            if (!string.IsNullOrWhiteSpace(cep))
            {
                if(cep.Length == 8)
                {
                    var dados1 = dadosCep.DigitaCEPCasoNaoExistaCadastra(cep);
                    return View(dados1);
                }
            }
            if (!string.IsNullOrWhiteSpace(logradouro))
            {
                var dados2 = dadosCep.BuscaPorLogradouro(logradouro);
                return View(dados2);
            }

            var dados = dadosCep.DigitaCEPCasoNaoExistaCadastra("93180000");
            return View(dados);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}