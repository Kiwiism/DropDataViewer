using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDV.Controllers
{
    public class DropDataController : Controller
    {
        private readonly Data.DropDataContext _context;

        public DropDataController(Data.DropDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DropperIdSortParm = sortOrder == "dropperid" ? "dropperid_desc" : "dropperid";
            ViewBag.ItemIdSortParm = sortOrder == "itemid" ? "itemid_desc" : "itemid";
            ViewBag.ChanceSortParm = sortOrder == "chance" ? "chance_desc" : "chance";

            var dropData = from d in _context.drop_data select d;

            switch (sortOrder)
            {
                case "id_desc":
                    dropData = dropData.OrderByDescending(d => d.Id);
                    break;
                case "itemid":
                    dropData = dropData.OrderBy(d => d.ItemId);
                    break;
                case "itemid_desc":
                    dropData = dropData.OrderByDescending(d => d.ItemId);
                    break;
                case "dropperid":
                    dropData = dropData.OrderBy(d => d.DropperId);
                    break;
                case "dropperid_desc":
                    dropData = dropData.OrderByDescending(d => d.DropperId);
                    break;
                case "chance":
                    dropData = dropData.OrderBy(d => d.Chance);
                    break;
                case "chance_desc":
                    dropData = dropData.OrderByDescending(d => d.Chance);
                    break;
                default:
                    dropData = dropData.OrderBy(d => d.Id);
                    break;
            }

            ViewData["dropperXmlData"] = _context.dropperXmlData;
            ViewData["itemXmlData"] = _context.itemXmlData;
            return View(await dropData.AsNoTracking().ToListAsync());
        }
    }
}
