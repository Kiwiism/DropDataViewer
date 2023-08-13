using Microsoft.AspNetCore.Mvc;

namespace DDV.Controllers
{
    public class ItemController : Controller
    {
        private readonly Data.DropDataContext _context;

        public ItemController(Data.DropDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var itemIds = _context.drop_data
                .Select(d => d.ItemId)
                .Distinct()
                .ToList();

            ViewData["itemXmlData"] = _context.itemXmlData;
            return View(itemIds);
        }

        public IActionResult Detail(int itemId, string sortOrder)
        {
            var dropData = _context.drop_data.Where(d => d.ItemId == itemId);

            ViewBag.DropperIdSortParm = sortOrder == "dropperid" ? "dropperid_desc" : "dropperid";
            ViewBag.ChanceSortParm = sortOrder == "chance" ? "chance_desc" : "chance";
            
            switch (sortOrder)
            {
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
                    dropData = dropData.OrderBy(d => d.DropperId);
                    break;
            }

            ViewData["itemId"] = itemId.ToString();
            ViewData["itemName"] = _context.itemXmlData.ContainsKey(itemId.ToString()) ?
                _context.itemXmlData[itemId.ToString()].Key : "-";
            ViewData["dropperXmlData"] = _context.dropperXmlData;
            return View(dropData.ToList());
        }

        public IActionResult Debug()
        {
            ViewData["itemXmlData"] = _context.itemXmlData;
            return View(_context.itemXmlData);
        }
    }
}
