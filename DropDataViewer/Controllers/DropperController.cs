using Microsoft.AspNetCore.Mvc;

namespace DDV.Controllers
{
    public class DropperController : Controller
    {
        private readonly Data.DropDataContext _context;

        public DropperController(Data.DropDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dropperIds = _context.drop_data
                .Select(d => d.DropperId)
                .Distinct()
                .ToList();

            ViewData["dropperXmlData"] = _context.dropperXmlData;
            return View(dropperIds);
        }

        public IActionResult Detail(int dropperId, string sortOrder)
        {
            var dropData = _context.drop_data.Where(d => d.DropperId == dropperId);

            ViewBag.ItemIdSortParm = sortOrder == "itemid" ? "itemid_desc" : "itemid";
            ViewBag.ChanceSortParm = sortOrder == "chance" ? "chance_desc" : "chance";

            switch (sortOrder)
            {
                case "itemid":
                    dropData = dropData.OrderBy(d => d.ItemId);
                    break;
                case "itemid_desc":
                    dropData = dropData.OrderByDescending(d => d.ItemId);
                    break;
                case "chance":
                    dropData = dropData.OrderBy(d => d.Chance);
                    break;
                case "chance_desc":
                    dropData = dropData.OrderByDescending(d => d.Chance);
                    break;
                default:
                    dropData = dropData.OrderBy(d => d.ItemId);
                    break;
            }

            ViewData["dropperId"] = dropperId.ToString();
            ViewData["dropperName"] = _context.dropperXmlData.ContainsKey(dropperId.ToString()) ?
                _context.dropperXmlData[dropperId.ToString()] : "-";
            ViewData["itemXmlData"] = _context.itemXmlData;
            return View(dropData.ToList());
        }

        public IActionResult Debug()
        {
            ViewData["dropperXmlData"] = _context.dropperXmlData;
            return View(_context.dropperXmlData);
        }
    }
}
