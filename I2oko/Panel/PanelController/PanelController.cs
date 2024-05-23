using I2oko.Models;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace I2oko.Panel.PanelController
{
    public class PanelController : Controller
    {
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewPostRecipePl()
        {
            return View();
        }
        public ActionResult NewPostRecipeIngredientPl()
        {
            return View();
        }
        public ActionResult AddIngredientPl()
        {
            return View();
        }
    }
}