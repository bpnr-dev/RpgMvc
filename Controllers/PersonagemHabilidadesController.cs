using System.Net.Http.Headers;
using RpgMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;


namespace RpgMvc.Controllers
{
    public class PersonagemHabilidadesController : Controller
    {
        public string uriBase = "http://lzsouza.somee.com/RpgApi/PersonagemHabilidades/";
        //xyz será substituído pelo nome do seu site na API.
    
        [HttpGet("PersonagemHabilidades/{id}")]
        public async Task<ActionResult> IndexAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemHabilidadeViewModel> lista = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<PersonagemHabilidadeViewModel>>(serialized));

                    return View(lista);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
                
            }
        }
    
        [HttpGet("Delete/{habilidadeId}/{personagemId}")]
        public async Task<ActionResult> DeleteAsync(int habilidadeId, int personagemId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string uriComplentar = "DeletePersonagemHabilidade";
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                PersonagemHabilidadeViewModel ph = new PersonagemHabilidadeViewModel();
                ph.HabilidadeId = habilidadeId;
                ph.PersonagemId = personagemId;

                var content = new StringContent(JsonConvert.SerializeObject(ph));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplentar, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    TempData["Mensagem"] = "Habilidade removida com sucesso";
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }
            return RedirectToAction("Index", new {Id = personagemId});

        }
    
        [HttpGet]
        public async Task<ActionResult> CreateAsync(int id, string nome)
        {
            try
            {
                string uriComplentar = "GetHabilidades";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + UriComplentar);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}