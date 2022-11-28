using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KladrApi.Controllers
{

    [Route("api/address/[action]")]
    public class AddressController : Controller  // BaseController<AddressController>
    {

        DataBaseContext _db;

        public AddressController(DataBaseContext db)
        {
            _db = db;
        }


        /// <summary> Получить список регионов </summary>
        /// <param name="okcm">Код страны ОКСМ</param>
        /// <returns> List<KLADR> </returns>
        [HttpGet("{okcm=0}")]
        [ActionName("Regions")]
        public async Task<IActionResult> GetRegions(int okcm = 0)
        {
            //okcm = 643 = РФ
            string name = Request.Query["name"].ToString();

            var items = _db.KLADR.Where(k => k.list == 1);
            if (okcm != 0)
            {
                items = items.Where(f => f.okcm == okcm);
            }


            if (name != "")
                items = items.Where(f => f.Name.Contains(name));

            var result = await items
                .OrderBy(order => (order.priority == null ? 999 : (int)order.priority))
                .ThenBy(order => order.Name)
                .ToListAsync();

            return Ok(result);

        }

        [HttpGet("{id}")]
        [ActionName("Districts")]
        public async Task<IActionResult> GetDistricts(int id)
        {

            if (id == 0)
                return null;

            var items = _db.KLADR.Where(k => k.ParentId == id && k.list == 2);

            var result = await items.OrderBy(order => (order.priority == null ? 999 : (int)order.priority)).ThenBy(order => order.Name).ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ActionName("Cities")]
        public async Task<IActionResult> GetCities(int id)
        {

            string name = Request.Query["name"].ToString();

            if (id == 0 || name == "")
                return null;

            // string sql = string.Format("SELECT * FROM KLADR Where id_parent like '{0}%'  AND list IN (3,4) AND name  Like '{1}%'", id.ToString(), name);
            //var items = _db.KLADR.FromSqlRaw(sql);

            var items = _db.KLADR.Include(kl => kl.Parent).Where(
                f => EF.Functions.Like(f.ParentId.ToString(), id + "%") &&
                EF.Functions.Like(f.Name, name + "%") &&
                (f.list == 3 || f.list == 4)

                );

            var result = await items.Take(15).ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ActionName("obj")]
        public async Task<IActionResult> GetObjectById(int id)
        {

            if (id == 0)
                return null;

            string name = Request.Query["name"].ToString();
            int list = Request.Query["list"].ToString().ToInt32();

            IQueryable<KLADR> items;


            if (list == 0)
            {
                if (id == 77 || id == 78 || id == 92 || id == 99)
                    items = _db.KLADR.Where(f => f.Id == id || (f.ParentId == id && (f.list == 3 || f.list == 4)));
                else
                    items = _db.KLADR.Where(f => f.ParentId == id && (f.list == 3 || f.list == 4));
            }
            else
            {
                items = _db.KLADR.Where(f => f.ParentId == id && f.list == list);
            }

            //if (list==0)
            //    items= items.Where(f => f.list==3 || f.list == 4);
            //else
            //    items = items.Where(f => f.list == list);



            var result = await items.OrderBy(order => (order.priority == null ? 999 : (int)order.priority)).ThenBy(order => order.Name).ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ActionName("street")]
        public async Task<IActionResult> GetStreet(long id)
        {

            if (id == 0)
                return null;

            string name = Request.Query["name"].ToString();

            var items = _db.KLADR_ST.Where(k => k.id_parent == id);

            if (name != "")
                items = items.Where(f => f.Name.StartsWith(name));


            var result = await items.Take(100).OrderBy(order => order.Name).ToListAsync();
            return Ok(result);
        }


    }
}
