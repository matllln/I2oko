using I2oko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace I2oko.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        private I2okoDB dB = new I2okoDB();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel Login)
        {
            UserModel username = dB.User.FirstOrDefault(x => x.UserName == Login.UserName);
            if (username != null && Login.Password == username.Password)
            {
                string US;
                US = username.UserName;
                Session.Add("UserName", US);
                string FN;
                FN = username.Fname;
                Session.Add("Fname", FN);
                int BD;
                BD = username.BirthDate;
                Session.Add("BirthDate", BD);
                Categuri CG;
                CG = username.Categuri;
                Session.Add("Categuri", CG);
                Sexuality Sex;
                Sex = username.Sexuality;
                Session.Add("Sexuality", Sex);
                string Bio;
                Bio = username.Biography;
                Session.Add("Biography", Bio);
                string EM;
                EM = username.Email;
                Session.Add("Email", EM);
                string AD;
                AD = username.Address;
                Session.Add("Address", AD);
                return View("Proofile", Login);
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel register)
        {
            if (ModelState.IsValid)
            {
                string ph;
                ph = register.Phone;
                Session.Add("phone", ph);                               
                return View("Otp", register);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Otp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Otp(UserModel Otp)
        {
            return View();
        }
        [HttpGet]
        public ActionResult FinishRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FinishRegister(UserModel FinishRegister)
        {
            if (ModelState.IsValid)
            {
                string user;
                user = FinishRegister.UserName;
                Session.Add("UserName", user);
                dB.User.Add(FinishRegister);
                dB.SaveChanges();
                return View("Thanx", FinishRegister);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Thanx()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Proofile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Proofile(UserModel Profile)
        {
            if (ModelState.IsValid)
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                return View("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult Friend()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Newpost()
        {
            return View();
        }
        public ActionResult Favorite()
        {
            return View();
        }
        public ActionResult Diet()
        {
            return View();
        }
        public ActionResult BasedOnFood()
        {
            return View();
        }
        public ActionResult BasedOnTheCostAmount()
        {
            return View();
        }
        public ActionResult BasedOnTheDesiredMenu()
        {
            return View();
        }
        public ActionResult ShopingList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(UserModel EditProfile)
        {
            string UN;
            UN = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(x => x.UserName == UN);
            string FN;
            FN = EditProfile.Fname;
            Session.Add("Fname", FN);
            int BD;
            BD = EditProfile.BirthDate;
            Session.Add("BirthDate", BD);
            Categuri CG;
            CG = EditProfile.Categuri;
            Session.Add("Categuri", CG);
            Sexuality Sex;
            Sex = EditProfile.Sexuality;
            Session.Add("Sexuality", Sex);
            string Bio;
            Bio = EditProfile.Biography;
            Session.Add("Biography", Bio);
            string EM;
            EM = EditProfile.Email;
            Session.Add("Email", EM);
            string AD;
            AD = EditProfile.Address;
            Session.Add("Address", AD);
            user.Picture = EditProfile.Picture;
            user.Fname = EditProfile.Fname;
            user.BirthDate = EditProfile.BirthDate;
            user.Categuri = EditProfile.Categuri;
            user.Sexuality = EditProfile.Sexuality;
            user.Biography = EditProfile.Biography;
            user.Email = EditProfile.Email;
            user.Address = EditProfile.Address;
            //dB.User.Add(EditProfile);
            dB.SaveChanges();
            return View("Proofile",EditProfile);
        }
        public ActionResult Window()
        {
            return View();
        }
        public ActionResult Delicious()
        {
            return View();
        }
        public ActionResult ListDelicious()
        {
            return View();
        }
        public ActionResult Foods()
        {
            return View();
        }
        public ActionResult ListAppetizer()
        {
            return View();
        }
        public ActionResult ListOfJampickles()
        {
            return View();
        }
        public ActionResult SweetDessertList()
        {
            return View();
        }
        public ActionResult ListOfDrinks()
        {
            return View();
        }
        public ActionResult ListOfColorFlavors()
        {
            return View();
        }
        public ActionResult BeautifulTable()
        {
            return View();
        }
        public ActionResult Pet()
        {
            return View();
        }
        public ActionResult PetList()
        {
            return View();
        }
        public ActionResult PetList2()
        {
            return View();
        }
    }
}