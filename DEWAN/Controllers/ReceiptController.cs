using DEWAN.Data;
using DEWAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEWAN.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext context;
        public ReceiptController(ApplicationDbContext _context) 
        {
            context = _context;
        }

        //------Items Creation---------------------------------------------------
        //------Items Creation---------------------------------------------------
        public IActionResult CreateItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItem(item item)
        {
            if (ModelState.IsValid)
            {
                context.items.Add(item);
                context.SaveChanges();
                return RedirectToAction("Create","Receipt");
            }
            return View(item);
        }
        //-------------------------------------------------------------------*

        //----------Receipt Creation------------------------------------------
        //----------Receipt Creation------------------------------------------
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(receipt receipt)
        {

            if (ModelState.IsValid)
            {
                context.receipts.Add(receipt);
                context.SaveChanges();
                HttpContext.Session.SetInt32("ReceiptId", receipt.receiptId);
                return RedirectToAction("AddItem");
            }
            else
            {
                return View(receipt);
            }
        }
        //------------------------------------------------------------------*

        //---------Receipt Diplay--------------------------------------------
        //---------Receipt Diplay--------------------------------------------
        public IActionResult DisplayReceipt(receipt receipt)
        {
            int receiptId = (int)HttpContext.Session.GetInt32("ReceiptId");
            ViewBag.ItemList = context.items.Where(i=>i.recieptId==receiptId).ToList();
            return View(receipt);
        }
        //------------------------------------------------------------------*

        //----------Adding items to Receipt----------------------------------
        //----------Adding items to Receipt----------------------------------
        public IActionResult AddItem()
        {
            List<item> itemList=context.items.ToList();
            if (itemList.Any())
            {
                return View(itemList);
            }
            else
            {
                return RedirectToAction("CreateItem");
            }
        }
        [HttpPost]
        public IActionResult AddItem(int itemId, int quantity)
        {
            //id = 4;
            item item = context.items.FirstOrDefault(i=>i.itemId== itemId);
            if (item == null)
            {
                //ModelState.AddModelError("", "The item you entered can't be found");
                return View();
            }

            if (quantity > item.balance)
            {
                ModelState.AddModelError("", "Not enough balance for this item.");
                return View(item);
            }
            else
            {
                int receiptId = (int)HttpContext.Session.GetInt32("ReceiptId");
                if (receiptId == null)
                    return RedirectToAction("Create");
                receipt receipt= context.receipts.Find(receiptId);
                if (receipt == null)
                {
                    ModelState.AddModelError("", "The session has ended");
                    return RedirectToAction("Create");
                }
                else
                {
                    item.recieptId=receiptId;
                    context.items.Update(item);
                    context.SaveChanges();
                    List<item> items = context.items.Where(item => item.recieptId == receiptId).ToList();
                    receipt.totalAmount+= item.price*quantity;
                    context.receipts.Update(receipt);
                    item.quantity = quantity;
                    context.items.Update(item);
                    context.SaveChanges();
                    return RedirectToAction("DisplayReceipt",receipt);
                }
            }
        }

        public ActionResult CompleteTransaction()
        {
            int receiptId = (int)HttpContext.Session.GetInt32("ReceiptId");
            receipt receipt = context.receipts.Find(receiptId);
            if (receipt == null)
            {
                ModelState.AddModelError("", "The session has ended");
                return RedirectToAction("Create");
            }
            List<item> ListItems = context.items.Where(item => item.recieptId == receiptId).ToList();
            foreach (item item in ListItems)
            {
                item.balance -= item.quantity;
                item.quantity = 0;
                context.items.Update(item);
                context.SaveChanges();
            }
            
            return View();
        }
    }
}


        
        
    

