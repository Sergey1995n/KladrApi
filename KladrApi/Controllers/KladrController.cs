using System.Collections.Generic;
using KladrApi.Dtos;
using KladrApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace KladrApi.Controllers
{
    [Route("api/kladr/[action]")]
    public class KladrController : Controller
    {

        private KladrService _kladrService;
        
        public KladrController(KladrService kladrService)
        {
            _kladrService = kladrService;
        }

        [HttpGet]
        [ActionName("item")]
        public KladrDto getItem(string id)
        {
            return _kladrService.getItem(id);
        }


        [HttpGet]
        [ActionName("kladr")]
        public List<KladrDto> findItems(string query, KladrType? type, string parentId)
        {
            return _kladrService.findItems(query, type, parentId);
        }
    }
}