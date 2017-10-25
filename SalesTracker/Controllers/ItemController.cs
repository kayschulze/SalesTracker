using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesTracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SalesTracker.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly SalesTrackerContext _db;
        private readonly UserManager<SalesAssociate> _userManager;

        public ItemController(UserManager<SalesAssociate> userManager, SalesTrackerContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            //return View(_db.Questions.ToList()); 
            return View(_db.Items.Include(x => x.SalesAssociate).
                        ToList().OrderByDescending(x => x.ItemId));
        }

        [HttpPost]
        public async Task<IActionResult> Sell(int ItemId)
        {
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);


            var thisItem = _db.Items.FirstOrDefault(x => x.ItemId == ItemId);

            thisItem.SalesAssociate = currentUser;
            _db.Entry(thisItem).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        public IActionResult NewItem(string ItemName, string ItemDescription, int ItemPrice, int SalesAssociateId)
        {
            Item MyNewItem = new Item(ItemName,ItemDescription,ItemPrice,SalesAssociateId = 0);
            _db.Items.Add(MyNewItem);
            _db.SaveChanges();
            return Json(MyNewItem);
        }
    }
}
