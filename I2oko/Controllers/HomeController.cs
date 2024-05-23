using I2oko.Models;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace I2oko.Controllers
{

    public class HomeController : Controller
    {
        private I2okoDB dB = new I2okoDB();

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        string Code = RandomString(5);

        public ActionResult Index()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            ViewBag.CustomText = "از لحظه‌هات لذت ببر - ایدوکو";

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
            if (Login.Password == username.Password)
            {
                string US;
                US = username.UserName;
                Session.Add("UserName", US);
                string FN;
                FN = username.Fname;
                Session.Add("Fname", FN);
                string NA;
                NA = username.Name;
                Session.Add("Name", NA);
                BirthDateY BDY;
                BDY = username.BirthDateY;
                Session.Add("BirthDateY", BDY);
                BirthDateM BDM;
                BDM = username.BirthDateM;
                Session.Add("BirthDateM", BDM);
                BirthDateD BDD;
                BDD = username.BirthDateD;
                Session.Add("BirthDateD", BDD);
                Categuri CG;
                CG = username.Categuri;
                Session.Add("Categuri", CG);
                Sexuality Sex;
                Sex = username.Sexuality;
                Session.Add("Sexuality", Sex);
                string PH;
                PH = username.Phone;
                Session.Add("Phone", PH);
                string Bio;
                Bio = username.Biography;
                Session.Add("Biography", Bio);
                string EM;
                EM = username.Email;
                Session.Add("Email", EM);
                string AD;
                AD = username.Address;
                Session.Add("Address", AD);
                string Pic;
                Pic = username.PicturePath;
                Session.Add("userpic", Pic);
                return RedirectToAction("Proofile");
            }
            else
            {
                ViewBag.Login = "!نام کاربری یا رمز عبور رو اشتباه وارد کردی";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel register)
        {
            UserModel Phone = dB.User.FirstOrDefault(x => x.Phone == register.Phone);
            if (Phone == null)
            {
                string ph;
                ph = register.Phone;
                Session.Add("Phone", ph);
                SmsIr smsIr = new SmsIr("Chwy7lP63h5ma3I2qi8rcuPkNgZroeu7vQOmdodTb28KbQkZbE0e6HYD13RHJnHA");
                var verificationSendResult = await smsIr.VerifySendAsync(ph, 250090, new VerifySendParameter[] { new VerifySendParameter("Code", Code) });
                Session.Add("Code", Code);
                return RedirectToAction("Otp");
            }
            else
            {
                ViewBag.Phone = "یه ایدوکویی با این شماره ثبت نام کرده";
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
            if (Otp.KeyNum == Session["Code"].ToString())
            {
                return RedirectToAction("FinishRegister");
            }
            else
            {
                ViewBag.Key = "!کد فعالسازی رو اشتباه وارد کردی";
                return View();
            }
        }

        [HttpGet]
        public ActionResult FinishRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinishRegister(UserModel FinishRegister)
        {
            UserModel user = dB.User.FirstOrDefault(x => x.UserName == FinishRegister.UserName);
            if (user == null)
            {
                string usr;
                usr = FinishRegister.UserName;
                Session.Add("UserName", usr);
                string PH;
                PH = Session["Phone"].ToString();
                FinishRegister.Phone = PH;
                dB.User.Add(FinishRegister);
                dB.SaveChanges();
                return View("Thanx", FinishRegister);

            }
            else
            {
                ViewBag.UserName = "یه شهروند ایدوکویی با این نام کاربری وجود داره";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Thanx()
        {
            return View();
        }

        List<string> GetImageUrlsFromPostModel()
        {
            List<string> MediaURL = new List<string>();
            var posts = dB.Post.ToList();
            foreach (var post in posts)
            {
                MediaURL.Add(post.MediaURL);
            }
            return MediaURL;
        }

        [HttpGet]
        public ActionResult Proofile()
        {
            ViewBag.ToStudy = Session["UserName"];
            if (ViewBag.ToStudy == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string UnProfile = Session["UserName"].ToString();
                UserModel user = dB.User.FirstOrDefault(x => x.UserName == UnProfile);

                var FavoritePosts = dB.Favorite.Where(p => p.UserNameViewer == user.UserName)
                                       .ToList();

                var likedPosts = dB.Like.Where(p => p.UserNameViewer == user.UserName)
                                       .ToList();

                var userPosts = dB.Post.Where(p => p.UserName == user.UserName)
                                       .OrderByDescending(p => p.DateTime)
                                       .ToList();

                var foodPosts = dB.Food.Where(p => p.UserName == user.UserName)
                                       .OrderByDescending(p => p.DateTime)
                                       .ToList();

                var allPostsProofile = new List<RelationPostModel>();

                allPostsProofile.AddRange(FavoritePosts.Select(post => new RelationPostModel
                {
                    PostIsSaveModel = (bool)post.PostIsSaveModel,
                    FoodIsSaveModel = (bool)post.FoodIsSaveModel
                }));

                allPostsProofile.AddRange(likedPosts.Select(post => new RelationPostModel
                {
                    PostIsLikeModel = (bool)post.PostIsLikeModel,
                    FoodIsLikeModel = (bool)post.FoodIsLikeModel
                }));

                allPostsProofile.AddRange(userPosts.Select(post => new RelationPostModel
                {
                    UserName = post.UserName,
                    MediaURL = post.MediaURL,
                    Text = post.Text,
                    DateTime = post.DateTime,
                    PostID = post.PostID,
                    PicturePath = user.PicturePath,
                    PostLikeNumber = post.PostLikeNumber,
                    ViewPostNumber = post.ViewPostNumber
                }));

                allPostsProofile.AddRange(foodPosts.Select(post => new RelationPostModel
                {
                    UserName = post.UserName,
                    Subject = post.Subject.ToString(),
                    Name = post.Name,
                    OriginalityPlace = post.OriginalityPlace,
                    PictureURL = post.PictureURL,
                    VideoURL = post.VideoURL,
                    Biography = post.Biography,
                    Points = post.Points,
                    HardshipLevel = post.HardshipLevel.ToString(),
                    CookingTimeHour = post.CookingTimeHour.ToString(),
                    CookingTimeMinute = post.CookingTimeMinute.ToString(),
                    people = post.people.ToString(),
                    Recipe = post.Recipe,
                    DateTime = post.DateTime,
                    FoodID = post.FoodID,
                    PicturePath = user.PicturePath,
                    FoodLikeNumber = post.FoodLikeNumber,
                    ViewFoodNumber = post.ViewFoodNumber
                }));

                allPostsProofile = allPostsProofile.OrderByDescending(post => post.DateTime).ToList();

                int PostCount = dB.Post.Where(x => x.UserName == UnProfile).Count();
                int FoodCount = dB.Food.Where(x => x.UserName == UnProfile).Count();
                int PostFoodCount = PostCount + FoodCount;

                ViewBag.NumberOfPosts = PostFoodCount;
                ViewBag.Posts = allPostsProofile;
                int FollowersCount = dB.Followers.Where(f => f.FollowersUserName == UnProfile).Count();
                ViewBag.FollowersCount = FollowersCount;
                int FollowingsCount = dB.Followings.Where(x => x.FollowingsUserName == UnProfile).Count();
                ViewBag.FollowingsCount = FollowingsCount;
                ViewBag.UserNameNotif = user.UserName;


                if (user != null)
                {
                    ViewBag.NotifIcon = user.NotifIcon.ToString();
                }

                return View();
            }
        }

        [HttpPost]
        public ActionResult Proofile(UserModel Proofile)
        {
            string UnProfile = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(x => x.UserName == UnProfile);

            int FollowersCount = dB.Followers.Where(f => f.FollowersUserName == UnProfile).Count();
            ViewBag.FollowersCount = FollowersCount;
            int FollowingsCount = dB.Followings.Where(x => x.FollowingsUserName == UnProfile).Count();
            ViewBag.FollowingsCount = FollowingsCount;
            ViewBag.NotifIcon = user.NotifIcon.ToString();

            try
            {
                var croppedImageBase64 = Request.Form["croppedImage"];
                if (!string.IsNullOrEmpty(croppedImageBase64))
                {
                    byte[] imageBytes = Convert.FromBase64String(croppedImageBase64.Replace("data:image/png;base64,", ""));
                    var imagePath = Path.Combine(Server.MapPath("/Content/UserPic/"), UnProfile + " " + "ProfilePic.jpg");
                    System.IO.File.WriteAllBytes(imagePath, imageBytes);
                    user.Picture = imageBytes;
                    user.PicturePath = "/Content/UserPic/" + UnProfile + " " + "ProfilePic.jpg";
                    Session.Remove("userpic");
                    Session["userpic"] = user.PicturePath;
                    dB.SaveChanges();
                    ViewBag.Message = "تصویر با موفقیت دریافت شد و در دیتابیس ذخیره شد!";

                    return RedirectToAction("Proofile", "Home", new { userName = user.UserName });
                }
                else
                {
                    ViewBag.Message = "خطا: تصویر مورد نظر خالی است!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = "خطا در دریافت و ذخیره تصویر: " + ex.Message;
            }

            if (ModelState.IsValid)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgetPassword(UserModel ForgetPassword)
        {
            UserModel user = dB.User.FirstOrDefault(x => x.Phone == ForgetPassword.Phone);
            if (user != null)
            {
                string PH;
                PH = ForgetPassword.Phone;
                Session.Add("Phone", PH);
                string US;
                US = ForgetPassword.UserName;
                Session.Add("UserName", US);
                SmsIr smsIr = new SmsIr("Chwy7lP63h5ma3I2qi8rcuPkNgZroeu7vQOmdodTb28KbQkZbE0e6HYD13RHJnHA");
                var verificationSendResult = await smsIr.VerifySendAsync(PH, 250090, new VerifySendParameter[] { new VerifySendParameter("Code", Code) });
                Session.Add("CodeForget", Code);
                return RedirectToAction("OtpForget");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult OtpForget()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OtpForget(UserModel OtpForget)
        {
            if (OtpForget.KeyNum == Session["CodeForget"].ToString())
            {
                return RedirectToAction("FinishRegisterForget");
            }
            else
            {
                ViewBag.Key = "!کد فعالسازی رو اشتباه وارد کردی";
                return View();
            }
        }

        [HttpGet]
        public ActionResult FinishRegisterForget()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinishRegisterForget(UserModel FinishRegisterForget)
        {
            string PHONE;
            PHONE = Session["Phone"].ToString();
            UserModel user = dB.User.FirstOrDefault(x => x.Phone == PHONE);
            if (user.Phone == PHONE)
            {
                user.Password = FinishRegisterForget.Password;
                dB.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.UserName = "!نام کاربری یا شماره‌ی تلفن رو اشتباه وارد کردی";
                return View();
            }
        }

        [HttpPost]
        public ActionResult BlockUser(string FollowingsUserName, string FollowersUserName)
        {
            string UnBlock = Session["UserName"].ToString();

            var following = dB.Followings.FirstOrDefault(f => f.UserName == UnBlock && f.FollowingsUserName == FollowingsUserName);
            var follower = dB.Followers.FirstOrDefault(f => f.UserName == UnBlock && f.FollowersUserName == FollowersUserName);

            if (following != null)
            {
                if (!following.IsBlocked)
                {
                    following.IsBlocked = true;
                    dB.SaveChanges();
                    return RedirectToAction("Friend");
                }
                else
                {
                    following.IsBlocked = false;
                    dB.SaveChanges();
                    return RedirectToAction("Friend");
                }
            }

            if (follower != null)
            {
                if (!follower.IsBlocked)
                {
                    follower.IsBlocked = true;
                    dB.SaveChanges();
                    return RedirectToAction("Contact");
                }
                else
                {
                    follower.IsBlocked = false;
                    dB.SaveChanges();
                    return RedirectToAction("Contact");
                }
            }

            return View();
        }



        [HttpGet]
        public ActionResult Friend()
        {
            string UnFriend = Session["UserName"].ToString();
            var followings = dB.Followings.Where(f => f.UserName == UnFriend).ToList();

            foreach (var following in followings)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == following.FollowingsUserName);
                following.ProfilePicturePath = user.PicturePath;
            }

            ViewBag.Followings = followings;
            return View();
        }


        [HttpPost]
        public ActionResult Friend(string FollowingsUserName)
        {
            string UnFriend = Session["UserName"].ToString();
            var friendToRemove = dB.Followings.FirstOrDefault(f => f.UserName == UnFriend && f.FollowingsUserName == FollowingsUserName);
            var ContactToRemove = dB.Followers.FirstOrDefault(f => f.UserName == FollowingsUserName && f.FollowersUserName == UnFriend);

            var followw = dB.Followers.FirstOrDefault(x => x.UserName == UnFriend && x.FollowersUserName == FollowingsUserName);
            if (followw != null)
            {
                followw.ContactToAdd = false;
            }

            dB.Followings.Remove(friendToRemove);
            dB.Followers.Remove(ContactToRemove);
            dB.SaveChanges();

            return Json(new { FollowingsUserName = FollowingsUserName });
        }

        [HttpGet]
        public ActionResult Contact()
        {
            string UnContact = Session["UserName"].ToString();
            var followers = dB.Followers.Where(f => f.UserName == UnContact).ToList();

            foreach (var follower in followers)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == follower.FollowersUserName);

                if (user != null)
                {
                    follower.ProfilePicturePath = user.PicturePath;
                }
            }

            ViewBag.Followers = followers;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(FollowersModel Follower, FollowingsModel Following,
            string FollowersUserName, string FollowersUserNameRemove, string FollowersRemoveContact)
        {
            string UnContact = Session["UserName"].ToString();
            var ExistingFollowers = dB.Followers.FirstOrDefault(f => f.UserName == FollowersUserNameRemove && f.FollowersUserName == UnContact);
            var ExistingFollowins = dB.Followings.FirstOrDefault(x => x.UserName == UnContact && x.FollowingsUserName == FollowersUserNameRemove);
            var ContactToRemove = dB.Followers.FirstOrDefault(f => f.UserName == UnContact && f.FollowersUserName == FollowersRemoveContact);
            var FriendToRemove = dB.Followings.FirstOrDefault(x => x.UserName == FollowersRemoveContact && x.FollowingsUserName == UnContact);

            if (ContactToRemove != null && FriendToRemove != null)
            {
                dB.Followers.Remove(ContactToRemove);
                dB.Followings.Remove(FriendToRemove);
                dB.SaveChanges();
            }

            if (ExistingFollowers != null && ExistingFollowins != null)
            {
                var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnContact && x.FollowersUserName == FollowersUserNameRemove);
                if (followerContactToAdd != null)
                {
                    followerContactToAdd.ContactToAdd = false;
                }

                var followingContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnContact);
                if (followingContactToAdd != null)
                {
                    followingContactToAdd.ContactToAdd = false;
                }

                dB.Followers.Remove(ExistingFollowers);
                dB.Followings.Remove(ExistingFollowins);
                dB.SaveChanges();

                return Json(new { FollowersUserNameRemove = FollowersUserNameRemove });
            }
            return Json(new { FollowersRemoveContact = FollowersRemoveContact });
        }

        [HttpPost]
        public ActionResult ContactToAdd(FollowersModel Follower, string FollowersUserName, FollowingsModel Following)
        {
            string UnContact = Session["UserName"].ToString();
            var ContactToRemove = dB.Followers.FirstOrDefault(f => f.UserName == UnContact && f.FollowersUserName == FollowersUserName);
            var FriendToRemove = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnContact);

            Follower.UserName = FollowersUserName;
            Follower.FollowersUserName = UnContact;
            Follower.FollowersDateTime = DateTime.Now;
            Following.FollowingsUserName = FollowersUserName;
            Following.UserName = UnContact;
            Following.FollowingsDateTime = DateTime.Now;
            Follower.ContactToAdd = true;
            Following.ContactToAdd = true;

            var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnContact && x.FollowersUserName == FollowersUserName);
            if (followerContactToAdd != null)
            {
                followerContactToAdd.ContactToAdd = true;
            }

            var followingContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnContact);
            if (followingContactToAdd != null)
            {
                followingContactToAdd.ContactToAdd = true;
            }

            dB.Followers.Add(Follower);
            dB.Followings.Add(Following);

            dB.SaveChanges();

            return Json(new { FollowersUserName = FollowersUserName });
        }

        [HttpGet]
        public ActionResult FriendShowProfile()
        {
            string Owner = Session["Owner"].ToString();
            var followings = dB.Followings.Where(f => f.UserName == Owner).ToList();

            foreach (var following in followings)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == following.FollowingsUserName);

                if (user != null)
                {
                    following.ProfilePicturePath = user.PicturePath;
                }
            }

            ViewBag.Followings = followings;
            return View();
        }


        [HttpGet]
        public ActionResult ContactShowProfile()
        {
            string Owner = Session["Owner"].ToString();
            var followers = dB.Followers.Where(f => f.UserName == Owner).ToList();

            foreach (var follower in followers)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == follower.FollowersUserName);

                if (user != null)
                {
                    follower.ProfilePicturePath = user.PicturePath;
                }
            }

            ViewBag.Followers = followers;
            return View();
        }

        [HttpGet]
        public ActionResult Newpost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Newpost(HttpPostedFileBase file, PostModel UserPost)
        {
            string UnNewpost = Session["UserName"].ToString();
            PostModel post = dB.Post.FirstOrDefault(x => x.UserName == UnNewpost);
            if (file != null && file.ContentLength > 0)
            {
                string FileNameMedia = Path.GetFileName(file.FileName);
                string FilePathMedia = Path.Combine(Server.MapPath("/Content/UserMedia/"), UnNewpost + " " + FileNameMedia);
                file.SaveAs(FilePathMedia);
                UserPost.Media = FileNameMedia;
                UserPost.MediaURL = "/Content/UserMedia/" + UnNewpost + " " + FileNameMedia;
                UserPost.DateTime = DateTime.Now;
                UserPost.UserName = UnNewpost;
                UserPost.ViewPostNumber = 0;

                dB.Post.Add(UserPost);
                dB.SaveChanges();
            }

            if (UserPost.Text != null)
            {
                string[] lines = UserPost.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = HttpUtility.HtmlEncode(lines[i]);
                }
                string formattedText = string.Join("<br />", lines);
                Session.Add("Text", formattedText);
                dB.SaveChanges();
            }

            if (UserPost.Place != null)
            {
                string Place;
                Place = UserPost.Place;
                Session.Add("Place", Place);
                dB.SaveChanges();
            }
            return RedirectToAction("Proofile");
        }

        [HttpGet]
        public ActionResult NewPostRecipe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPostRecipe(HttpPostedFileBase file, HttpPostedFileBase videoFile, FoodModel foodPosts)
        {
            string UnNewPostRecipe = Session["UserName"].ToString();
            FoodModel food = dB.Food.FirstOrDefault(x => x.UserName == UnNewPostRecipe);

            if (food == null)
            {
                food = new FoodModel();
                food.DateTime = DateTime.Now;
                food.UserName = UnNewPostRecipe;
                dB.Food.Add(food);
            }

            if (file != null && file.ContentLength > 0 && videoFile != null && videoFile.ContentLength > 0)
            {
                string FileNameMedia = Path.GetFileName(file.FileName);
                string FilePathMedia = Path.Combine(Server.MapPath("/Content/UserMedia/"), UnNewPostRecipe + " " + FileNameMedia);
                string FileNameVideo = Path.GetFileName(videoFile.FileName);
                string FilePathVideo = Path.Combine(Server.MapPath("/Content/UserMedia/"), UnNewPostRecipe + " " + FileNameVideo);

                food.Picture = FileNameMedia;
                food.PictureURL = "/Content/UserMedia/" + UnNewPostRecipe + " " + FileNameMedia;
                food.Video = FileNameVideo;
                food.VideoURL = "/Content/UserMedia/" + UnNewPostRecipe + " " + FileNameVideo;

                file.SaveAs(FilePathMedia);
                videoFile.SaveAs(FilePathVideo);

                food.DateTime = DateTime.Now;
                food.ViewFoodNumber = 0;

                dB.Food.Add(food);
                dB.SaveChanges();
            }

            if (foodPosts.Subject != 0)
            {
                Subject Subject;
                Subject = foodPosts.Subject;
                Session.Add("Subject", Subject);
                food.Subject = foodPosts.Subject;
                dB.SaveChanges();
            }

            if (foodPosts.Name != null)
            {
                string Name;
                Name = foodPosts.Name;
                Session.Add("Name", Name);
                food.Name = foodPosts.Name;
                dB.SaveChanges();
            }

            if (foodPosts.OriginalityPlace != null)
            {
                string OriginalityPlace;
                OriginalityPlace = foodPosts.OriginalityPlace;
                Session.Add("OriginalityPlace", OriginalityPlace);
                food.OriginalityPlace = foodPosts.OriginalityPlace;
                dB.SaveChanges();
            }

            if (foodPosts.Biography != null)
            {
                string[] lines = foodPosts.Biography.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = HttpUtility.HtmlEncode(lines[i]);
                }
                string formattedBiography = string.Join("<br />", lines);
                Session.Add("BiographyFood", formattedBiography);
                food.Biography = foodPosts.Biography;
                dB.SaveChanges();
            }

            if (foodPosts.Points != null)
            {
                string[] lines = foodPosts.Points.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = HttpUtility.HtmlEncode(lines[i]);
                }
                string formattedPoints = string.Join("<br />", lines);
                Session.Add("Points", formattedPoints);
                food.Points = foodPosts.Points;
                dB.SaveChanges();
            }

            if (foodPosts.HardshipLevel != 0)
            {
                HardshipLevel HardshipLevel;
                HardshipLevel = foodPosts.HardshipLevel;
                Session.Add("HardshipLevel", HardshipLevel);
                food.HardshipLevel = foodPosts.HardshipLevel;
                dB.SaveChanges();
            }

            if (foodPosts.CookingTimeHour != 0)
            {
                CookingTimeHour CookingTimeHour;
                CookingTimeHour = foodPosts.CookingTimeHour;
                Session.Add("CookingTimeHour", CookingTimeHour);
                food.CookingTimeHour = foodPosts.CookingTimeHour;
                dB.SaveChanges();
            }

            if (foodPosts.CookingTimeMinute != 0)
            {
                CookingTimeMinute CookingTimeMinute;
                CookingTimeMinute = foodPosts.CookingTimeMinute;
                Session.Add("CookingTimeMinute", CookingTimeMinute);
                food.CookingTimeMinute = foodPosts.CookingTimeMinute;
                dB.SaveChanges();
            }

            if (foodPosts.people != 0)
            {
                people people;
                people = foodPosts.people;
                Session.Add("people", people);
                food.people = foodPosts.people;
                dB.SaveChanges();
            }

            if (foodPosts.Recipe != null)
            {
                string[] lines = foodPosts.Recipe.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = HttpUtility.HtmlEncode(lines[i]);
                }
                string formattedRecipe = string.Join("<br />", lines);
                Session.Add("Recipe", formattedRecipe);
                food.Recipe = foodPosts.Recipe;
                dB.SaveChanges();
            }
            return RedirectToAction("Proofile");
        }

        [HttpGet]
        public ActionResult NewPostRecipeIngredient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPostRecipeIngredient(RelationFIModel Ingredient, FoodSpecifiacationModel FoodSpc, FoodModel Food)
        {
            string UnNewPostRecipeIngredient = Session["UserName"].ToString();
            if (Ingredient.NumberOfIngredients != null)
            {
                string NumberOfIngredients;
                NumberOfIngredients = Ingredient.NumberOfIngredients;
                Session.Add("NumberOfIngredients", NumberOfIngredients);
                Ingredient.NumberOfIngredients = NumberOfIngredients;
                Ingredient.UserName = UnNewPostRecipeIngredient;
                int foodIdValue = Food.FoodID;
                int foodSpecIdValue = FoodSpc.FoodSpecID;
                Ingredient.FoodID = new List<FoodModel> { new FoodModel { FoodID = foodIdValue } };
                Ingredient.FoodSpecID = new List<FoodSpecifiacationModel> { new FoodSpecifiacationModel { FoodSpecID = foodSpecIdValue } };
                dB.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Favorite()
        {
            string UnFavorite = Session["UserName"] as string;

            var favoritePosts = dB.Favorite
                                   .Where(f => f.UserNameViewer == UnFavorite && f.PostID != 0)
                                   .Select(f => f.PostID)
                                   .ToList();

            var favoriteFoods = dB.Favorite
                                   .Where(f => f.UserNameViewer == UnFavorite && f.FoodID != 0)
                                   .Select(f => f.FoodID)
                                   .ToList();

            var postDetails = dB.Post
                                 .Where(p => favoritePosts.Contains(p.PostID))
                                 .ToList()
                                 .Select(post => new PostModel
                                 {
                                     UserName = post.UserName,
                                     MediaURL = post.MediaURL,
                                     Text = post.Text,
                                     PostID = post.PostID,
                                     PicturePath = dB.User.FirstOrDefault(u => u.UserName == post.UserName)?.PicturePath,
                                     PostLikeNumber = post.PostLikeNumber,
                                     ViewPostNumber = post.ViewPostNumber,
                                     PostIsLikeModel = dB.Like.Any(l => l.PostID == post.PostID && l.UserNameViewer == UnFavorite)
                                 })
                                 .ToList();

            var foodDetails = dB.Food
                                 .Where(f => favoriteFoods.Contains(f.FoodID))
                                 .ToList()
                                 .Select(food => new FoodModel
                                 {
                                     UserName = food.UserName,
                                     Subject = food.Subject,
                                     Name = food.Name,
                                     OriginalityPlace = food.OriginalityPlace,
                                     PictureURL = food.PictureURL,
                                     VideoURL = food.VideoURL,
                                     Biography = food.Biography,
                                     Points = food.Points,
                                     HardshipLevel = food.HardshipLevel,
                                     CookingTimeHour = food.CookingTimeHour,
                                     CookingTimeMinute = food.CookingTimeMinute,
                                     people = food.people,
                                     Recipe = food.Recipe,
                                     DateTime = food.DateTime,
                                     FoodID = food.FoodID,
                                     PicturePath = dB.User.FirstOrDefault(u => u.UserName == food.UserName)?.PicturePath,
                                     FoodLikeNumber = food.FoodLikeNumber,
                                     ViewFoodNumber = food.ViewFoodNumber,
                                     FoodIsLikeModel = dB.Like.Any(l => l.FoodID == food.FoodID && l.UserNameViewer == UnFavorite)
                                 })
                                 .ToList();

            ViewBag.PostDetails = postDetails;
            ViewBag.FoodDetails = foodDetails;

            return View();
        }


        [HttpPost]
        public ActionResult Favorite(FavoriteModel Favorite)
        {
            return View();
        }


        [HttpPost]
        public ActionResult FavoriteToAdd(FavoriteModel Favorite, int PostIDSave = 0, int FoodIDSave = 0)
        {
            string UnFavoriteToAdd = Session["UserName"].ToString();
            var post = dB.Post.FirstOrDefault(p => p.PostID == PostIDSave);
            var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDSave);
            var user = dB.User.FirstOrDefault(p => p.UserName == UnFavoriteToAdd);

            if (PostIDSave != 0)
            {
                string UserNameOwner;
                UserNameOwner = post.UserName;
                Favorite.UserNameViewer = user.UserName;
                Favorite.UserNameOwner = UserNameOwner;
                Favorite.PostID = PostIDSave;
                post.Save++;
                Favorite.PostIsSaveModel = true;
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                ViewBag.PostID = post.PostID;
                ViewBag.UserNameLoginPost = UnFavoriteToAdd;
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.ViewPostNumber = post.ViewPostNumber;
                ViewBag.UsernamePost = post.UserName;
                ViewBag.TextPost = post.Text;

                var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnFavoriteToAdd && l.PostID == PostIDSave);
                if (like != null)
                {
                    ViewBag.PostIsLikeModel = like.PostIsLikeModel.ToString();
                }
                else
                {
                    ViewBag.PostIsLikeModel = "False";
                }

                var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDSave);
                if (ViewPostNumber != null)
                {
                    ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                    var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.Favorite.Add(Favorite);
                dB.SaveChanges();
                return Json(new { PostIDSave = PostIDSave });
            }
            else if (FoodIDSave != 0)
            {
                string UserNameOwner;
                UserNameOwner = food.UserName;
                Favorite.UserNameViewer = user.UserName;
                Favorite.UserNameOwner = UserNameOwner;
                Favorite.FoodID = FoodIDSave;
                food.Save++;
                Favorite.FoodIsSaveModel = true;
                ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                ViewBag.FoodID = food.FoodID;
                ViewBag.UserNameLoginFood = UnFavoriteToAdd;
                ViewBag.FoodLikeNumber = food.FoodLikeNumber.ToString();
                ViewBag.ViewFoodNumber = food.ViewFoodNumber;
                ViewBag.UsernameFood = food.UserName;
                ViewBag.Recipe = food.Recipe;
                ViewBag.Subject = food.Subject;
                ViewBag.NameRecipe = food.Name;
                ViewBag.OriginalityPlace = food.OriginalityPlace;
                ViewBag.BiographyRecipe = food.Biography;
                ViewBag.PointsRecipe = food.Points;
                ViewBag.HardshipLevel = food.HardshipLevel;
                ViewBag.CookingTimeHour = food.CookingTimeHour;
                ViewBag.CookingTimeMinute = food.CookingTimeMinute;
                ViewBag.People = food.people;

                var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnFavoriteToAdd && l.FoodID == FoodIDSave);
                if (like != null)
                {
                    ViewBag.FoodIsLikeModel = like.FoodIsLikeModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsLikeModel = "False";
                }

                var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDSave);
                if (ViewFoodNumber != null)
                {
                    ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                    var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.Favorite.Add(Favorite);
                dB.SaveChanges();
                return View("ShowPostRecipe");
            }
            return View();
        }

        [HttpPost]
        public ActionResult FavoriteToRemove(FavoriteModel Favorite, int PostIDSave = 0, int FoodIDSave = 0)
        {
            string UnFavoriteToRemove = Session["UserName"].ToString();
            var post = dB.Post.FirstOrDefault(p => p.PostID == PostIDSave);
            var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDSave);
            var user = dB.User.FirstOrDefault(p => p.UserName == UnFavoriteToRemove);

            if (PostIDSave != 0)
            {
                var ToRemove = dB.Favorite.FirstOrDefault(f => f.PostID == PostIDSave && f.UserNameViewer == UnFavoriteToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = post.UserName;
                    ToRemove.UserNameViewer = user.UserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.PostID = PostIDSave;
                    post.Save--;
                    ToRemove.PostIsSaveModel = false;
                    ViewBag.PostLikeNumber = post.PostLikeNumber;
                    ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                    ViewBag.PostID = post.PostID;
                    ViewBag.UserNameLoginPost = UnFavoriteToRemove;
                    ViewBag.PostLikeNumber = post.PostLikeNumber;
                    ViewBag.ViewPostNumber = post.ViewPostNumber;
                    ViewBag.UsernamePost = post.UserName;
                    ViewBag.TextPost = post.Text;

                    var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnFavoriteToRemove && l.PostID == PostIDSave);
                    if (like != null)
                    {
                        ViewBag.PostIsLikeModel = like.PostIsLikeModel.ToString();
                    }
                    else
                    {
                        ViewBag.PostIsLikeModel = "False";
                    }

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDSave);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }
                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                        var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.Favorite.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { PostIDSave = PostIDSave });
            }
            else if (FoodIDSave != 0)
            {
                var ToRemove = dB.Favorite.FirstOrDefault(f => f.FoodID == FoodIDSave && f.UserNameViewer == UnFavoriteToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = food.UserName;
                    ToRemove.UserNameViewer = user.UserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.FoodID = FoodIDSave;
                    food.Save--;
                    ToRemove.FoodIsSaveModel = false;
                    ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                    ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                    ViewBag.FoodID = food.FoodID;
                    ViewBag.UserNameLoginFood = UnFavoriteToRemove;
                    ViewBag.FoodLikeNumber = food.FoodLikeNumber.ToString();
                    ViewBag.ViewFoodNumber = food.ViewFoodNumber;
                    ViewBag.UsernameFood = food.UserName;
                    ViewBag.Recipe = food.Recipe;
                    ViewBag.Subject = food.Subject;
                    ViewBag.NameRecipe = food.Name;
                    ViewBag.OriginalityPlace = food.OriginalityPlace;
                    ViewBag.BiographyRecipe = food.Biography;
                    ViewBag.PointsRecipe = food.Points;
                    ViewBag.HardshipLevel = food.HardshipLevel;
                    ViewBag.CookingTimeHour = food.CookingTimeHour;
                    ViewBag.CookingTimeMinute = food.CookingTimeMinute;
                    ViewBag.People = food.people;

                    var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnFavoriteToRemove && l.FoodID == FoodIDSave);
                    if (like != null)
                    {
                        ViewBag.FoodIsLikeModel = like.FoodIsLikeModel.ToString();
                    }
                    else
                    {
                        ViewBag.FoodIsLikeModel = "False";
                    }

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDSave);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }
                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                        var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.Favorite.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return View("ShowPostRecipe");
            }
            return View();
        }


        [HttpPost]
        public ActionResult LikeToAdd(LikeModel LikeToAdd, CommentLikeModel CommentLikeToAdd,
            int PostIDLike = 0, int FoodIDLike = 0, int CommentPostID = 0, int CommentFoodID = 0,
            int CommentNotifPostID = 0, int CommentNotifFoodID = 0)
        {
            string UnLikeToAdd = Session["UserName"].ToString();
            var post = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
            var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
            var user = dB.User.FirstOrDefault(p => p.UserName == UnLikeToAdd);

            if (PostIDLike != 0)
            {
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == post.UserName);
                string UserNameOwner;
                UserNameOwner = post.UserName;
                LikeToAdd.UserNameViewer = user.UserName;
                LikeToAdd.UserNameOwner = UserNameOwner;
                LikeToAdd.PostID = PostIDLike;
                post.PostLikeNumber++;
                LikeToAdd.PostIsLikeModel = true;
                LikeToAdd.LikeDateTime = DateTime.Now;
                PostNotifIcon.NotifIcon = true;
                ViewBag.PostIsLikeModel = LikeToAdd.PostIsLikeModel.ToString();
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.PostID = post.PostID;
                ViewBag.UserNameLoginPost = UnLikeToAdd;
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.ViewPostNumber = post.ViewPostNumber;
                ViewBag.UsernamePost = post.UserName;
                ViewBag.TextPost = post.Text;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.PostID == PostIDLike);
                if (Favorite != null)
                {
                    ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.PostIsSaveModel = "False";
                }

                var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                if (ViewPostNumber != null)
                {
                    ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                    var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.Like.Add(LikeToAdd);
                dB.SaveChanges();
                return Json(new { PostIDLike = PostIDLike });
            }
            else if (CommentPostID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentPostID);
                var CommentPost = dB.Post.FirstOrDefault(p => p.PostID == Comment.CommentToPostID);
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);

                string UserNameOwner;
                UserNameOwner = Comment.FromUserName;
                CommentLikeToAdd.UserNameOwner = UserNameOwner;
                CommentLikeToAdd.UserNameLiker = user.UserName;
                CommentLikeToAdd.PostID = CommentPost.PostID;
                CommentLikeToAdd.CommentID = Comment.ID;
                CommentLikeToAdd.CommentLikeDateTime = DateTime.Now;
                CommentLikeToAdd.CommentProfilePicturePath = user.PicturePath;
                CommentLikeToAdd.CommentLikePostPicturePath = Comment.UserCommentPicture;
                CommentLikeToAdd.CommentText = Comment.CommentText;
                CommentLikeToAdd.CommentPostLike = true;
                Comment.CommentPostLikeNumber++;
                Comment.CommentPostLike = true;
                PostNotifIcon.NotifIcon = true;
                ViewBag.CommentPostLike = Comment.CommentPostLike.ToString();
                ViewBag.CommentPostLikeNumber = Comment.CommentPostLikeNumber;
                ViewBag.PostLikeNumber = CommentPost.PostLikeNumber;
                ViewBag.PostID = CommentPost.PostID;
                ViewBag.UserNameLoginPost = UnLikeToAdd;
                ViewBag.PostLikeNumber = CommentPost.PostLikeNumber.ToString();
                ViewBag.ViewPostNumber = CommentPost.ViewPostNumber;
                ViewBag.UsernamePost = CommentPost.UserName;
                ViewBag.TextPost = CommentPost.Text;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.PostID == PostIDLike);
                if (Favorite != null)
                {
                    ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.PostIsSaveModel = "False";
                }

                var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                if (ViewPostNumber != null)
                {
                    ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToPostID == CommentPost.PostID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }

                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == CommentPost.PostID);
                    var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.CommentLike.Add(CommentLikeToAdd);
                dB.SaveChanges();

                return Json(new { Comment = Comment });
            }
            else if (CommentNotifPostID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentNotifPostID);
                var CommentPost = dB.Post.FirstOrDefault(p => p.PostID == Comment.CommentToPostID);
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);

                string UserNameOwner;
                UserNameOwner = Comment.FromUserName;
                CommentLikeToAdd.UserNameOwner = UserNameOwner;
                CommentLikeToAdd.UserNameLiker = user.UserName;
                CommentLikeToAdd.PostID = CommentPost.PostID;
                CommentLikeToAdd.CommentID = Comment.ID;
                CommentLikeToAdd.CommentLikeDateTime = DateTime.Now;
                CommentLikeToAdd.CommentProfilePicturePath = user.PicturePath;
                CommentLikeToAdd.CommentLikePostPicturePath = Comment.UserCommentPicture;
                CommentLikeToAdd.CommentText = Comment.CommentText;
                CommentLikeToAdd.CommentPostLike = true;
                Comment.CommentPostLikeNumber++;
                Comment.CommentPostLike = true;
                PostNotifIcon.NotifIcon = true;
                ViewBag.CommentPostLike = CommentLikeToAdd.CommentPostLike;
                ViewBag.CommentPostLikeNumber = Comment.CommentPostLikeNumber;
                ViewBag.PostLikeNumber = CommentPost.PostLikeNumber;
                ViewBag.PostID = CommentPost.PostID;
                ViewBag.UserNameLoginPost = UnLikeToAdd;
                ViewBag.PostLikeNumber = CommentPost.PostLikeNumber.ToString();
                ViewBag.ViewPostNumber = CommentPost.ViewPostNumber;
                ViewBag.UsernamePost = CommentPost.UserName;
                ViewBag.TextPost = CommentPost.Text;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.PostID == PostIDLike);
                if (Favorite != null)
                {
                    ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.PostIsSaveModel = "False";
                }

                var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                if (ViewPostNumber != null)
                {
                    ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToPostID == CommentPost.PostID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }

                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == CommentPost.PostID);
                    var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.CommentLike.Add(CommentLikeToAdd);
                dB.SaveChanges();
                return Json(new { CommentNotifPostID = CommentNotifPostID });
            }
            else if (FoodIDLike != 0)
            {
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == food.UserName);
                string UserNameOwner;
                UserNameOwner = food.UserName;
                LikeToAdd.UserNameViewer = user.UserName;
                LikeToAdd.UserNameOwner = UserNameOwner;
                LikeToAdd.FoodID = FoodIDLike;
                food.FoodLikeNumber++;
                LikeToAdd.FoodIsLikeModel = true;
                LikeToAdd.LikeDateTime = DateTime.Now;
                FoodNotifIcon.NotifIcon = true;
                ViewBag.FoodIsLikeModel = LikeToAdd.FoodIsLikeModel.ToString();
                ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                ViewBag.FoodID = food.FoodID;
                ViewBag.UserNameLoginFood = UnLikeToAdd;
                ViewBag.FoodLikeNumber = food.FoodLikeNumber.ToString();
                ViewBag.ViewFoodNumber = food.ViewFoodNumber;
                ViewBag.UsernameFood = food.UserName;
                ViewBag.Recipe = food.Recipe;
                ViewBag.Subject = food.Subject;
                ViewBag.NameRecipe = food.Name;
                ViewBag.OriginalityPlace = food.OriginalityPlace;
                ViewBag.BiographyRecipe = food.Biography;
                ViewBag.PointsRecipe = food.Points;
                ViewBag.HardshipLevel = food.HardshipLevel;
                ViewBag.CookingTimeHour = food.CookingTimeHour;
                ViewBag.CookingTimeMinute = food.CookingTimeMinute;
                ViewBag.People = food.people;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.FoodID == FoodIDLike);
                if (Favorite != null)
                {
                    ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsSaveModel = "False";
                }

                var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                if (ViewFoodNumber != null)
                {
                    ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                    var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.Like.Add(LikeToAdd);
                dB.SaveChanges();
                return Json(new { FoodIDLike = FoodIDLike });
            }
            else if (CommentFoodID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentFoodID);
                var CommentFood = dB.Food.FirstOrDefault(p => p.FoodID == Comment.CommentToFoodID);
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);

                string UserNameOwner;
                UserNameOwner = Comment.FromUserName;
                CommentLikeToAdd.UserNameOwner = UserNameOwner;
                CommentLikeToAdd.UserNameLiker = user.UserName;
                CommentLikeToAdd.FoodID = CommentFood.FoodID;
                CommentLikeToAdd.CommentID = Comment.ID;
                CommentLikeToAdd.CommentLikeDateTime = DateTime.Now;
                CommentLikeToAdd.CommentProfilePicturePath = user.PicturePath;
                CommentLikeToAdd.CommentLikeFoodPicturePath = Comment.UserCommentPicture;
                CommentLikeToAdd.CommentText = Comment.CommentText;
                CommentLikeToAdd.CommentFoodLike = true;
                Comment.CommentFoodLikeNumber++;
                Comment.CommentFoodLike = true;
                FoodNotifIcon.NotifIcon = true;
                ViewBag.CommentFoodLike = Comment.CommentFoodLike.ToString();
                ViewBag.CommentFoodLikeNumber = Comment.CommentFoodLikeNumber;
                ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber;
                ViewBag.FoodID = CommentFood.FoodID;
                ViewBag.UserNameLoginFood = UnLikeToAdd;
                ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber.ToString();
                ViewBag.ViewFoodNumber = CommentFood.ViewFoodNumber;
                ViewBag.UsernameFood = CommentFood.UserName;
                ViewBag.Recipe = CommentFood.Recipe;
                ViewBag.Subject = CommentFood.Subject;
                ViewBag.NameRecipe = CommentFood.Name;
                ViewBag.OriginalityPlace = CommentFood.OriginalityPlace;
                ViewBag.BiographyRecipe = CommentFood.Biography;
                ViewBag.PointsRecipe = CommentFood.Points;
                ViewBag.HardshipLevel = CommentFood.HardshipLevel;
                ViewBag.CookingTimeHour = CommentFood.CookingTimeHour;
                ViewBag.CookingTimeMinute = CommentFood.CookingTimeMinute;
                ViewBag.People = CommentFood.people;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.FoodID == FoodIDLike);
                if (Favorite != null)
                {
                    ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsSaveModel = "False";
                }

                var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                if (ViewFoodNumber != null)
                {
                    ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToFoodID == CommentFood.FoodID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }

                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == CommentFood.FoodID);
                    var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.CommentLike.Add(CommentLikeToAdd);
                dB.SaveChanges();
                return Json(new { Comment = Comment });
            }
            else if (CommentNotifFoodID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentNotifFoodID);
                var CommentFood = dB.Food.FirstOrDefault(p => p.FoodID == Comment.CommentToFoodID);
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);

                string UserNameOwner;
                UserNameOwner = Comment.FromUserName;
                CommentLikeToAdd.UserNameOwner = UserNameOwner;
                CommentLikeToAdd.UserNameLiker = user.UserName;
                CommentLikeToAdd.FoodID = CommentFood.FoodID;
                CommentLikeToAdd.CommentID = Comment.ID;
                CommentLikeToAdd.CommentLikeDateTime = DateTime.Now;
                CommentLikeToAdd.CommentProfilePicturePath = user.PicturePath;
                CommentLikeToAdd.CommentLikeFoodPicturePath = Comment.UserCommentPicture;
                CommentLikeToAdd.CommentText = Comment.CommentText;
                CommentLikeToAdd.CommentFoodLike = true;
                Comment.CommentFoodLikeNumber++;
                Comment.CommentFoodLike = true;
                FoodNotifIcon.NotifIcon = true;
                ViewBag.CommentFoodLike = CommentLikeToAdd.CommentFoodLike;
                ViewBag.CommentFoodLikeNumber = Comment.CommentFoodLikeNumber;
                ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber;
                ViewBag.FoodID = CommentFood.FoodID;
                ViewBag.UserNameLoginFood = UnLikeToAdd;
                ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber.ToString();
                ViewBag.ViewFoodNumber = CommentFood.ViewFoodNumber;
                ViewBag.UsernameFood = CommentFood.UserName;
                ViewBag.Recipe = CommentFood.Recipe;
                ViewBag.Subject = CommentFood.Subject;
                ViewBag.NameRecipe = CommentFood.Name;
                ViewBag.OriginalityPlace = CommentFood.OriginalityPlace;
                ViewBag.BiographyRecipe = CommentFood.Biography;
                ViewBag.PointsRecipe = CommentFood.Points;
                ViewBag.HardshipLevel = CommentFood.HardshipLevel;
                ViewBag.CookingTimeHour = CommentFood.CookingTimeHour;
                ViewBag.CookingTimeMinute = CommentFood.CookingTimeMinute;
                ViewBag.People = CommentFood.people;

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToAdd && l.FoodID == FoodIDLike);
                if (Favorite != null)
                {
                    ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsSaveModel = "False";
                }

                var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                if (ViewFoodNumber != null)
                {
                    ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                }

                var comments = dB.Comment.Where(c => c.CommentToFoodID == CommentFood.FoodID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }

                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == CommentFood.FoodID);
                    var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                dB.CommentLike.Add(CommentLikeToAdd);
                dB.SaveChanges();
                return Json(new { CommentNotifFoodID = CommentNotifFoodID });
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult LikeToRemove(LikeModel LikeToRemove, CommentLikeModel CommentLikeToRemove,
        //   int PostIDLike = 0, int FoodIDLike = 0, int CommentPostID = 0, int CommentFoodID = 0,
        //   int CommentNotifPostID = 0, int CommentNotifFoodID = 0)
        //{
        //    string UnLikeToRemove = Session["UserName"].ToString();
        //    var post = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
        //    var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
        //    var user = dB.User.FirstOrDefault(p => p.UserName == UnLikeToRemove);

        //    Owner of i2oko: Seyed Vahid Hosseini
        //    Supervisor : Payam KHodagholi
        //    Back-end programmer: Seyed Vahid Hosseini
        //    Front-end programmer: Matin Kaboli

        //    if (PostIDLike != 0)
        //    {
        //        var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == post.UserName);
        //        var ToRemove = dB.Like.FirstOrDefault(f => f.PostID == PostIDLike && f.UserNameViewer == UnLikeToRemove);
        //        if (ToRemove != null)
        //        {
        //            string UserNameOwner;
        //            UserNameOwner = post.UserName;
        //            ToRemove.UserNameViewer = user.UserName;
        //            ToRemove.UserNameOwner = UserNameOwner;
        //            ToRemove.PostID = PostIDLike;
        //            post.PostLikeNumber--;
        //            ViewBag.Comments = comments;

        //            dB.Like.Remove(ToRemove);
        //            dB.SaveChanges();
        //        }
        //        return View("ShowPost");
        //    }

        [HttpPost]
        public ActionResult LikeToRemove(LikeModel LikeToRemove, CommentLikeModel CommentLikeToRemove,
            int PostIDLike = 0, int FoodIDLike = 0, int CommentPostID = 0, int CommentFoodID = 0,
            int CommentNotifPostID = 0, int CommentNotifFoodID = 0)
        {
            string UnLikeToRemove = Session["UserName"].ToString();
            var post = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
            var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
            var user = dB.User.FirstOrDefault(p => p.UserName == UnLikeToRemove);

            if (PostIDLike != 0)
            {
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == post.UserName);
                var ToRemove = dB.Like.FirstOrDefault(f => f.PostID == PostIDLike && f.UserNameViewer == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = post.UserName;
                    ToRemove.UserNameViewer = user.UserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.PostID = PostIDLike;
                    post.PostLikeNumber--;
                    ToRemove.PostIsLikeModel = false;
                    PostNotifIcon.NotifIcon = false;
                    ViewBag.PostLikeNumber = post.PostLikeNumber;
                    ViewBag.PostIsLikeModel = ToRemove.PostIsLikeModel.ToString();
                    ViewBag.PostID = post.PostID;
                    ViewBag.UserNameLoginPost = UnLikeToRemove;
                    ViewBag.PostLikeNumber = post.PostLikeNumber;
                    ViewBag.ViewPostNumber = post.ViewPostNumber;
                    ViewBag.UsernamePost = post.UserName;
                    ViewBag.TextPost = post.Text;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.PostID == PostIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.PostIsSaveModel = "False";
                    }

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }
                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                        var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.Like.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { PostIDLike = PostIDLike });
            }
            else if (CommentPostID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentPostID);
                var CommentPost = dB.Post.FirstOrDefault(p => p.PostID == Comment.CommentToPostID);
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);
                var ToRemove = dB.CommentLike.FirstOrDefault(f => f.CommentID == CommentPostID && f.UserNameLiker == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = Comment.FromUserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.UserNameLiker = user.UserName;
                    ToRemove.PostID = CommentPost.PostID;
                    ToRemove.CommentID = Comment.ID;
                    ToRemove.CommentLikeDateTime = DateTime.Now;
                    ToRemove.CommentProfilePicturePath = user.PicturePath;
                    ToRemove.CommentLikePostPicturePath = Comment.UserCommentPicture;
                    ToRemove.CommentPostLike = false;
                    Comment.CommentPostLikeNumber--;
                    Comment.CommentPostLike = false;
                    PostNotifIcon.NotifIcon = false;
                    ViewBag.CommentPostLike = Comment.CommentPostLike.ToString();
                    ViewBag.CommentPostLikeNumber = Comment.CommentPostLikeNumber;
                    ViewBag.PostLikeNumber = CommentPost.PostLikeNumber;
                    ViewBag.PostID = CommentPost.PostID;
                    ViewBag.UserNameLoginPost = UnLikeToRemove;
                    ViewBag.PostLikeNumber = CommentPost.PostLikeNumber.ToString();
                    ViewBag.ViewPostNumber = CommentPost.ViewPostNumber;
                    ViewBag.UsernamePost = CommentPost.UserName;
                    ViewBag.TextPost = CommentPost.Text;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.PostID == PostIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.PostIsSaveModel = "False";
                    }

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToPostID == CommentPost.PostID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }

                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == CommentPost.PostID);
                        var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.CommentLike.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { Comment = Comment });
            }
            else if (CommentNotifPostID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentNotifPostID);
                var CommentPost = dB.Post.FirstOrDefault(p => p.PostID == Comment.CommentToPostID);
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);
                var ToRemove = dB.CommentLike.FirstOrDefault(f => f.CommentID == CommentNotifPostID && f.UserNameLiker == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = Comment.FromUserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.UserNameLiker = user.UserName;
                    ToRemove.PostID = CommentPost.PostID;
                    ToRemove.CommentID = Comment.ID;
                    ToRemove.CommentLikeDateTime = DateTime.Now;
                    ToRemove.CommentProfilePicturePath = user.PicturePath;
                    ToRemove.CommentLikePostPicturePath = Comment.UserCommentPicture;
                    ToRemove.CommentText = Comment.CommentText;
                    ToRemove.CommentPostLike = false;
                    Comment.CommentPostLikeNumber--;
                    Comment.CommentPostLike = false;
                    PostNotifIcon.NotifIcon = false;
                    ViewBag.CommentPostLike = ToRemove.CommentPostLike;
                    ViewBag.CommentPostLikeNumber = Comment.CommentPostLikeNumber;
                    ViewBag.PostLikeNumber = CommentPost.PostLikeNumber;
                    ViewBag.PostID = CommentPost.PostID;
                    ViewBag.UserNameLoginPost = UnLikeToRemove;
                    ViewBag.PostLikeNumber = CommentPost.PostLikeNumber.ToString();
                    ViewBag.ViewPostNumber = CommentPost.ViewPostNumber;
                    ViewBag.UsernamePost = CommentPost.UserName;
                    ViewBag.TextPost = CommentPost.Text;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.PostID == PostIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.PostIsSaveModel = "False";
                    }

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostIDLike);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToPostID == CommentPost.PostID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }

                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == CommentPost.PostID);
                        var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.CommentLike.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { CommentNotifPostID = CommentNotifPostID });
            }

            else if (FoodIDLike != 0)
            {
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == food.UserName);
                var ToRemove = dB.Like.FirstOrDefault(f => f.FoodID == FoodIDLike && f.UserNameViewer == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = food.UserName;
                    ToRemove.UserNameViewer = user.UserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.FoodID = FoodIDLike;
                    food.FoodLikeNumber--;
                    ToRemove.FoodIsLikeModel = false;
                    FoodNotifIcon.NotifIcon = false;
                    ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                    ViewBag.FoodIsLikeModel = ToRemove.FoodIsLikeModel.ToString();
                    ViewBag.FoodID = food.FoodID;
                    ViewBag.UserNameLoginFood = UnLikeToRemove;
                    ViewBag.FoodLikeNumber = food.FoodLikeNumber.ToString();
                    ViewBag.ViewFoodNumber = food.ViewFoodNumber;
                    ViewBag.UsernameFood = food.UserName;
                    ViewBag.Recipe = food.Recipe;
                    ViewBag.Subject = food.Subject;
                    ViewBag.NameRecipe = food.Name;
                    ViewBag.OriginalityPlace = food.OriginalityPlace;
                    ViewBag.BiographyRecipe = food.Biography;
                    ViewBag.PointsRecipe = food.Points;
                    ViewBag.HardshipLevel = food.HardshipLevel;
                    ViewBag.CookingTimeHour = food.CookingTimeHour;
                    ViewBag.CookingTimeMinute = food.CookingTimeMinute;
                    ViewBag.People = food.people;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.FoodID == FoodIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.FoodIsSaveModel = "False";
                    }

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }
                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                        var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.Like.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { FoodIDLike = FoodIDLike });
            }
            else if (CommentFoodID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentFoodID);
                var CommentFood = dB.Food.FirstOrDefault(p => p.FoodID == Comment.CommentToFoodID);
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);
                var ToRemove = dB.CommentLike.FirstOrDefault(f => f.CommentID == CommentFoodID && f.UserNameLiker == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = Comment.FromUserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.UserNameLiker = user.UserName;
                    ToRemove.FoodID = CommentFood.FoodID;
                    ToRemove.CommentID = Comment.ID;
                    ToRemove.CommentLikeDateTime = DateTime.Now;
                    ToRemove.CommentProfilePicturePath = user.PicturePath;
                    ToRemove.CommentLikeFoodPicturePath = Comment.UserCommentPicture;
                    ToRemove.CommentFoodLike = false;
                    Comment.CommentFoodLikeNumber--;
                    Comment.CommentFoodLike = false;
                    PostNotifIcon.NotifIcon = false;
                    ViewBag.CommentFoodLike = Comment.CommentFoodLike.ToString();
                    ViewBag.CommentFoodLikeNumber = Comment.CommentFoodLikeNumber;
                    ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber;
                    ViewBag.FoodID = CommentFood.FoodID;
                    ViewBag.UserNameLoginFood = UnLikeToRemove;
                    ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber.ToString();
                    ViewBag.ViewFoodNumber = CommentFood.ViewFoodNumber;
                    ViewBag.UsernameFood = CommentFood.UserName;
                    ViewBag.Recipe = CommentFood.Recipe;
                    ViewBag.Subject = CommentFood.Subject;
                    ViewBag.NameRecipe = CommentFood.Name;
                    ViewBag.OriginalityPlace = CommentFood.OriginalityPlace;
                    ViewBag.BiographyRecipe = CommentFood.Biography;
                    ViewBag.PointsRecipe = CommentFood.Points;
                    ViewBag.HardshipLevel = CommentFood.HardshipLevel;
                    ViewBag.CookingTimeHour = CommentFood.CookingTimeHour;
                    ViewBag.CookingTimeMinute = CommentFood.CookingTimeMinute;
                    ViewBag.People = CommentFood.people;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.FoodID == FoodIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.FoodIsSaveModel = "False";
                    }

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToFoodID == CommentFood.FoodID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }

                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == CommentFood.FoodID);
                        var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.CommentLike.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { Comment = Comment });
            }
            else if (CommentNotifFoodID != 0)
            {
                var Comment = dB.Comment.FirstOrDefault(p => p.ID == CommentNotifFoodID);
                var CommentFood = dB.Food.FirstOrDefault(p => p.FoodID == Comment.CommentToFoodID);
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Comment.FromUserName);
                var ToRemove = dB.CommentLike.FirstOrDefault(f => f.CommentID == CommentNotifFoodID && f.UserNameLiker == UnLikeToRemove);
                if (ToRemove != null)
                {
                    string UserNameOwner;
                    UserNameOwner = Comment.FromUserName;
                    ToRemove.UserNameOwner = UserNameOwner;
                    ToRemove.UserNameLiker = user.UserName;
                    ToRemove.FoodID = CommentFood.FoodID;
                    ToRemove.CommentID = Comment.ID;
                    ToRemove.CommentLikeDateTime = DateTime.Now;
                    ToRemove.CommentProfilePicturePath = user.PicturePath;
                    ToRemove.CommentLikeFoodPicturePath = Comment.UserCommentPicture;
                    ToRemove.CommentText = Comment.CommentText;
                    ToRemove.CommentFoodLike = false;
                    Comment.CommentFoodLikeNumber--;
                    Comment.CommentFoodLike = false;
                    FoodNotifIcon.NotifIcon = false;
                    ViewBag.CommentFoodLike = ToRemove.CommentFoodLike;
                    ViewBag.CommentFoodLikeNumber = Comment.CommentFoodLikeNumber;
                    ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber;
                    ViewBag.FoodID = CommentFood.FoodID;
                    ViewBag.UserNameLoginFood = UnLikeToRemove;
                    ViewBag.FoodLikeNumber = CommentFood.FoodLikeNumber.ToString();
                    ViewBag.ViewFoodNumber = CommentFood.ViewFoodNumber;
                    ViewBag.UsernameFood = CommentFood.UserName;
                    ViewBag.Recipe = CommentFood.Recipe;
                    ViewBag.Subject = CommentFood.Subject;
                    ViewBag.NameRecipe = CommentFood.Name;
                    ViewBag.OriginalityPlace = CommentFood.OriginalityPlace;
                    ViewBag.BiographyRecipe = CommentFood.Biography;
                    ViewBag.PointsRecipe = CommentFood.Points;
                    ViewBag.HardshipLevel = CommentFood.HardshipLevel;
                    ViewBag.CookingTimeHour = CommentFood.CookingTimeHour;
                    ViewBag.CookingTimeMinute = CommentFood.CookingTimeMinute;
                    ViewBag.People = CommentFood.people;

                    var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnLikeToRemove && l.FoodID == FoodIDLike);
                    if (Favorite != null)
                    {
                        ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                    }
                    else
                    {
                        ViewBag.FoodIsSaveModel = "False";
                    }

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodIDLike);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var comments = dB.Comment.Where(c => c.CommentToFoodID == CommentFood.FoodID).ToList().OrderByDescending(c => c.DateTime);
                    foreach (var comment in comments)
                    {
                        var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                        if (UserCommentPicture != null)
                        {
                            comment.UserCommentPicture = UserCommentPicture.PicturePath;
                        }

                        var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == CommentFood.FoodID);
                        var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                        ViewBag.CommentNumber = CommentNumber.CommentNumber;
                    }
                    ViewBag.Comments = comments;

                    dB.CommentLike.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { CommentNotifFoodID = CommentNotifFoodID });
            }
            return View();
        }

        [HttpPost]
        public ActionResult PostToRemove(int PostID = 0, int FoodID = 0,
            int CommentDeletePostID = 0, int CommentDeleteFoodID = 0,
            int CommentNotifDeletePostID = 0, int CommentNotifDeleteFoodID = 0)
        {
            string UnPostToRemove = Session["UserName"].ToString();
            var post = dB.Post.FirstOrDefault(p => p.PostID == PostID);
            var food = dB.Food.FirstOrDefault(p => p.FoodID == FoodID);
            var user = dB.User.FirstOrDefault(p => p.UserName == UnPostToRemove);

            if (PostID != 0)
            {
                var ToRemove = dB.Post.FirstOrDefault(f => f.PostID == PostID && f.UserName == UnPostToRemove);
                if (ToRemove != null)
                {
                    ToRemove.UserName = user.UserName;
                    ToRemove.PostID = PostID;

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostID);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var PostRemoveToLike = dB.Like.Where(l => l.PostID == PostID);
                    foreach (var like in PostRemoveToLike)
                    {
                        dB.Like.Remove(like);
                    }

                    var PostRemoveToFavorite = dB.Favorite.Where(f => f.PostID == PostID);
                    foreach (var favorite in PostRemoveToFavorite)
                    {
                        dB.Favorite.Remove(favorite);
                    }

                    dB.Post.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return RedirectToAction("Proofile", "Home", new { userName = UnPostToRemove });
            }
            else if (FoodID != 0)
            {
                var ToRemove = dB.Food.FirstOrDefault(f => f.FoodID == FoodID && f.UserName == UnPostToRemove);
                if (ToRemove != null)
                {
                    ToRemove.UserName = user.UserName;
                    ToRemove.FoodID = FoodID;

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodID);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var FoodRemoveToLike = dB.Like.Where(l => l.FoodID == FoodID);
                    foreach (var like in FoodRemoveToLike)
                    {
                        dB.Like.Remove(like);
                    }

                    var FoodRemoveToFavorite = dB.Favorite.Where(f => f.FoodID == FoodID);
                    foreach (var favorite in FoodRemoveToFavorite)
                    {
                        dB.Favorite.Remove(favorite);
                    }

                    dB.Food.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return RedirectToAction("Proofile", "Home", new { userName = UnPostToRemove });
            }
            else if (CommentDeletePostID != 0)
            {
                var ToRemove = dB.Comment.FirstOrDefault(f => f.ID == CommentDeletePostID);
                var CommentNumber = dB.Post.FirstOrDefault(f => f.PostID == ToRemove.CommentToPostID);

                if (ToRemove != null)
                {
                    ToRemove.ID = CommentDeletePostID;

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostID);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var CommentPostRemove = dB.CommentLike.Where(l => l.CommentID == CommentDeletePostID);
                    foreach (var Commentlike in CommentPostRemove)
                    {
                        dB.CommentLike.Remove(Commentlike);
                    }

                    CommentNumber.CommentNumber--;
                    dB.Comment.Remove(ToRemove);
                    dB.SaveChanges();
                }

                return Json(ToRemove.ID);
            }
            else if (CommentDeleteFoodID != 0)
            {
                var ToRemove = dB.Comment.FirstOrDefault(f => f.ID == CommentDeleteFoodID);
                var CommentNumber = dB.Food.FirstOrDefault(f => f.FoodID == ToRemove.CommentToFoodID);

                if (ToRemove != null)
                {
                    ToRemove.ID = CommentDeleteFoodID;

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodID);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var CommentFoodRemove = dB.CommentLike.Where(l => l.CommentID == CommentDeleteFoodID);
                    foreach (var Commentlike in CommentFoodRemove)
                    {
                        dB.CommentLike.Remove(Commentlike);
                    }

                    CommentNumber.CommentNumber--;
                    dB.Comment.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(ToRemove.ID);
            }
            else if (CommentNotifDeletePostID != 0)
            {
                var ToRemove = dB.Comment.FirstOrDefault(f => f.ID == CommentNotifDeletePostID);
                var CommentNumber = dB.Post.FirstOrDefault(f => f.PostID == ToRemove.CommentToPostID);

                if (ToRemove != null)
                {
                    ToRemove.ID = CommentNotifDeletePostID;

                    var ViewPostNumber = dB.Post.FirstOrDefault(p => p.PostID == PostID);
                    if (ViewPostNumber != null)
                    {
                        ViewBag.ViewPostNumber = ViewPostNumber.ViewPostNumber;
                    }

                    var CommentPostRemove = dB.CommentLike.Where(l => l.CommentID == CommentNotifDeletePostID);
                    foreach (var Commentlike in CommentPostRemove)
                    {
                        dB.CommentLike.Remove(Commentlike);
                    }

                    CommentNumber.CommentNumber--;
                    dB.Comment.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { CommentNotifDeletePostID = CommentNotifDeletePostID });
            }
            else if (CommentNotifDeleteFoodID != 0)
            {
                var ToRemove = dB.Comment.FirstOrDefault(f => f.ID == CommentNotifDeleteFoodID);
                var CommentNumber = dB.Food.FirstOrDefault(f => f.FoodID == ToRemove.CommentToFoodID);

                if (ToRemove != null)
                {
                    ToRemove.ID = CommentNotifDeleteFoodID;

                    var ViewFoodNumber = dB.Food.FirstOrDefault(p => p.FoodID == FoodID);
                    if (ViewFoodNumber != null)
                    {
                        ViewBag.ViewFoodNumber = ViewFoodNumber.ViewFoodNumber;
                    }

                    var CommentFoodRemove = dB.CommentLike.Where(l => l.CommentID == CommentNotifDeleteFoodID);
                    foreach (var Commentlike in CommentFoodRemove)
                    {
                        dB.CommentLike.Remove(Commentlike);
                    }

                    CommentNumber.CommentNumber--;
                    dB.Comment.Remove(ToRemove);
                    dB.SaveChanges();
                }
                return Json(new { CommentNotifDeleteFoodID = CommentNotifDeleteFoodID });
            }
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
        public ActionResult EditProfile(HttpPostedFileBase file, UserModel EditProfile)
        {
            string UnEditProfile = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(x => x.UserName == UnEditProfile);
            if (EditProfile.Name != null)
            {
                string NA;
                NA = EditProfile.Name;
                Session.Add("Name", NA);
                user.Name = EditProfile.Name;
                dB.SaveChanges();
            }

            if (EditProfile.Fname != null)
            {
                string FN;
                FN = EditProfile.Fname;
                Session.Add("Fname", FN);
                user.Fname = EditProfile.Fname;
                dB.SaveChanges();
            }

            if (EditProfile.Phone != null)
            {
                string PH;
                PH = EditProfile.Phone;
                Session.Add("Phone", PH);
                user.Phone = EditProfile.Phone;
                dB.SaveChanges();
            }

            if (EditProfile.BirthDateY != 0)
            {
                BirthDateY BDY;
                BDY = EditProfile.BirthDateY;
                Session.Add("BirthDateY", BDY);
                user.BirthDateY = EditProfile.BirthDateY;
                dB.SaveChanges();
            }

            if (EditProfile.BirthDateM != 0)
            {
                BirthDateM BDM;
                BDM = EditProfile.BirthDateM;
                Session.Add("BirthDateM", BDM);
                user.BirthDateM = EditProfile.BirthDateM;
                dB.SaveChanges();
            }

            if (EditProfile.BirthDateD != 0)
            {
                BirthDateD BDD;
                BDD = EditProfile.BirthDateD;
                Session.Add("BirthDateD", BDD);
                user.BirthDateD = EditProfile.BirthDateD;
                dB.SaveChanges();
            }

            if (EditProfile.Categuri != 0)
            {
                Categuri CG;
                CG = EditProfile.Categuri;
                Session.Add("Categuri", CG);
                user.Categuri = EditProfile.Categuri;
                dB.SaveChanges();
            }

            if (EditProfile.Sexuality != 0)
            {
                Sexuality Sex;
                Sex = EditProfile.Sexuality;
                Session.Add("Sexuality", Sex);
                user.Sexuality = EditProfile.Sexuality;
                dB.SaveChanges();
            }

            if (EditProfile.Biography != null)
            {
                string BioDisine;
                BioDisine = EditProfile.Biography.Replace("\n", "<br />");
                user.Biography = BioDisine;
                string Bio;
                Bio = user.Biography;
                Session.Add("Biography", Bio);
                dB.SaveChanges();
            }

            if (EditProfile.Email != null)
            {
                string EM;
                EM = EditProfile.Email;
                Session.Add("Email", EM);
                user.Email = EditProfile.Email;
                dB.SaveChanges();
            }

            if (EditProfile.Address != null)
            {
                string AD;
                AD = EditProfile.Address;
                Session.Add("Address", AD);
                user.Address = EditProfile.Address;
                dB.SaveChanges();
            }
            return RedirectToAction("Proofile");
        }

        private string ProfilePicture(string userName)
        {
            string filename = userName + " " + "ProfilePic" + ".jpg";
            string path = Path.Combine(Server.MapPath("/Content/UserPic/"), filename);
            return "/Content/UserPic/" + filename;
        }

        [HttpGet]
        public async Task<ActionResult> Window()
        {
            var allUsersWithEnoughPosts = await dB.User
                .OrderBy(u => Guid.NewGuid())
                .Where(u => dB.Post.Count(p => p.UserName == u.UserName) + dB.Food.Count(f => f.UserName == u.UserName) >= 3)
                .ToListAsync();

            foreach (var user in allUsersWithEnoughPosts)
            {
                var userPosts = await dB.Post
                    .Where(p => p.UserName == user.UserName)
                    .OrderByDescending(p => p.DateTime)
                    .Take(3)
                    .ToListAsync();

                var foodPosts = await dB.Food
                    .Where(p => p.UserName == user.UserName)
                    .OrderByDescending(p => p.DateTime)
                    .Take(3)
                    .ToListAsync();

                var likedPosts = dB.Like.Where(p => p.UserNameViewer == user.UserName)
                        .ToList();

                var FavoritePosts = dB.Favorite.Where(p => p.UserNameViewer == user.UserName)
                       .ToList();

                var allPostsWindow = userPosts
                    .Select(post => new RelationPostModel
                    {
                        UserName = post.UserName,
                        MediaURL = post.MediaURL,
                        Text = post.Text,
                        DateTime = post.DateTime,
                        PostID = post.PostID,
                        PicturePath = user.PicturePath,
                        PostLikeNumber = post.PostLikeNumber,
                        ViewPostNumber = post.ViewPostNumber
                    })
                    .Concat(foodPosts.Select(post => new RelationPostModel
                    {
                        UserName = post.UserName,
                        Subject = post.Subject.ToString(),
                        Name = post.Name,
                        OriginalityPlace = post.OriginalityPlace,
                        PictureURL = post.PictureURL,
                        VideoURL = post.VideoURL,
                        Biography = post.Biography,
                        Points = post.Points,
                        HardshipLevel = post.HardshipLevel.ToString(),
                        CookingTimeHour = post.CookingTimeHour.ToString(),
                        CookingTimeMinute = post.CookingTimeMinute.ToString(),
                        people = post.people.ToString(),
                        Recipe = post.Recipe,
                        DateTime = post.DateTime,
                        FoodID = post.FoodID,
                        PicturePath = user.PicturePath,
                        FoodLikeNumber = post.FoodLikeNumber,
                        ViewFoodNumber = post.ViewFoodNumber
                    }))
                    .OrderByDescending(post => post.DateTime)
                    .Take(3)
                    .ToList();

                allPostsWindow.AddRange(likedPosts.Select(post => new RelationPostModel
                {
                    PostIsLikeModel = (bool)post.PostIsLikeModel,
                    FoodIsLikeModel = (bool)post.FoodIsLikeModel
                }));

                allPostsWindow.AddRange(FavoritePosts.Select(post => new RelationPostModel
                {
                    PostIsSaveModel = (bool)post.PostIsSaveModel,
                    FoodIsSaveModel = (bool)post.FoodIsSaveModel
                }));

                user.LastThreePosts = allPostsWindow;

                if (user.PicturePath == null)
                {
                    user.PicturePath = ProfilePicture(user.UserName);
                }
            }

            ViewBag.AllUser = allUsersWithEnoughPosts;

            return View();
        }




        [HttpPost]
        public async Task<ActionResult> Window(UserModel User)
        {
            string UnWindow = Session["UserName"].ToString();

            var user = await dB.User
                .FirstOrDefaultAsync(x => x.UserName == UnWindow);

            var userPosts = await dB.Post
                .Where(p => p.UserName == user.UserName)
                .OrderByDescending(p => p.DateTime)
                .Take(3)
                .ToListAsync();

            var foodPosts = await dB.Food
                .Where(p => p.UserName == user.UserName)
                .OrderByDescending(p => p.DateTime)
                .Take(3)
                .ToListAsync();

            var likedPosts = dB.Like.Where(p => p.UserNameViewer == user.UserName)
                    .ToList();

            var FavoritePosts = dB.Favorite.Where(p => p.UserNameViewer == user.UserName)
                       .ToList();

            var allPostsWindow = userPosts
                .Select(post => new RelationPostModel
                {
                    UserName = post.UserName,
                    MediaURL = post.MediaURL,
                    Text = post.Text,
                    DateTime = post.DateTime,
                    PostID = post.PostID,
                    PicturePath = user.PicturePath,
                    PostLikeNumber = post.PostLikeNumber,
                    ViewPostNumber = post.ViewPostNumber
                })
                .Concat(foodPosts.Select(post => new RelationPostModel
                {
                    UserName = post.UserName,
                    Subject = post.Subject.ToString(),
                    Name = post.Name,
                    OriginalityPlace = post.OriginalityPlace,
                    PictureURL = post.PictureURL,
                    VideoURL = post.VideoURL,
                    Biography = post.Biography,
                    Points = post.Points,
                    HardshipLevel = post.HardshipLevel.ToString(),
                    CookingTimeHour = post.CookingTimeHour.ToString(),
                    CookingTimeMinute = post.CookingTimeMinute.ToString(),
                    people = post.people.ToString(),
                    Recipe = post.Recipe,
                    DateTime = post.DateTime,
                    FoodID = post.FoodID,
                    PicturePath = user.PicturePath,
                    FoodLikeNumber = post.FoodLikeNumber,
                    ViewFoodNumber = post.ViewFoodNumber
                }))
                .OrderByDescending(post => post.DateTime)
                .Take(3)
                .ToList();

            allPostsWindow.AddRange(likedPosts.Select(post => new RelationPostModel
            {
                PostIsLikeModel = (bool)post.PostIsLikeModel,
                FoodIsLikeModel = (bool)post.FoodIsLikeModel
            }));

            allPostsWindow.AddRange(FavoritePosts.Select(post => new RelationPostModel
            {
                PostIsSaveModel = (bool)post.PostIsSaveModel,
                FoodIsSaveModel = (bool)post.FoodIsSaveModel
            }));

            ViewBag.Posts = allPostsWindow;

            return RedirectToAction("ShowProfile", "Home", new { userName = UnWindow });
        }



        [HttpGet]
        public ActionResult Delicious()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delicious(DeliciousModel Delicions)
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


        [HttpGet]
        public ActionResult Notif(FollowersModel Follower, string PostID)
        {
            string UnNotif = Session["UserName"].ToString();
            ViewBag.UserNameLoginPost = UnNotif;

            var CommentPostToLike = dB.CommentLike.Where(c => c.UserNameOwner == UnNotif && c.CommentLikeID != 0).ToList();
            foreach (var LikePostComment in CommentPostToLike)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == LikePostComment.UserNameLiker);
                var comment = dB.Comment.FirstOrDefault(p => p.ID == LikePostComment.CommentID);
                var CommentpostLike = dB.Post.FirstOrDefault(p => p.PostID == LikePostComment.PostID);
                var CommentFoodLike = dB.Food.FirstOrDefault(p => p.FoodID == LikePostComment.FoodID);
                if (user != null && comment != null)
                {
                    LikePostComment.CommentProfilePicturePath = user.PicturePath;
                    LikePostComment.UserNameLiker = user.UserName;
                    if(CommentpostLike != null && CommentpostLike.MediaURL != null)
                    {
                        LikePostComment.CommentLikePostPicturePath = CommentpostLike.MediaURL;
                    }
                    else if (CommentFoodLike != null && CommentFoodLike.PictureURL != null)
                    {
                        LikePostComment.CommentLikeFoodPicturePath = CommentFoodLike.PictureURL;
                    }
                }
            }

            var commentToPosts = dB.Comment.Where(c => c.ToUserName == UnNotif && c.CommentToPostID != 0).ToList();
            foreach (var commentToPost in commentToPosts.ToList())
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == commentToPost.FromUserName);
                var post = dB.Post.FirstOrDefault(p => p.PostID == commentToPost.CommentToPostID);
                if (user != null && post != null)
                {
                    commentToPost.UserCommentPicture = user.PicturePath;
                    commentToPost.CommentToPostPicturePath = post.MediaURL;
                    commentToPost.FromUserName = user.UserName;
                }
                else
                {
                    commentToPosts.Remove(commentToPost);
                }
                var likedComment = dB.CommentLike.Any(cl => cl.UserNameLiker == UnNotif && cl.CommentID == commentToPost.ID);
                commentToPost.CommentPostLike = likedComment;
                ViewBag.CommentPostLike = likedComment;
            }

            var commentToFoods = dB.Comment.Where(c => c.ToUserName == UnNotif && c.CommentToFoodID != 0).ToList();
            foreach (var commentToFood in commentToFoods.ToList())
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == commentToFood.FromUserName);
                var food = dB.Food.FirstOrDefault(f => f.FoodID == commentToFood.CommentToFoodID);
                if (user != null && food != null)
                {
                    commentToFood.UserCommentPicture = user.PicturePath;
                    commentToFood.CommentToFoodPicturePath = food.PictureURL;
                    commentToFood.FromUserName = user.UserName;
                }
                else
                {
                    commentToFoods.Remove(commentToFood);
                }
                var likedComment = dB.CommentLike.Any(cl => cl.UserNameLiker == UnNotif && cl.CommentID == commentToFood.ID);
                commentToFood.CommentFoodLike = likedComment;
                ViewBag.CommentFoodLike = commentToFood.CommentFoodLike;
            }

            var followers = dB.Followers.Where(f => f.UserName == UnNotif).ToList();
            foreach (var follower in followers)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == follower.FollowersUserName);
                if (user != null)
                {
                    follower.ProfilePicturePath = user.PicturePath;
                    follower.UserNameViewer = user.UserName;
                }
            }

            var postLikers = dB.Like.Where(l => l.UserNameOwner == UnNotif && l.PostID != 0).ToList();
            foreach (var likePost in postLikers)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == likePost.UserNameViewer);
                var post = dB.Post.FirstOrDefault(p => p.PostID == likePost.PostID);
                if (user != null)
                {
                    likePost.ProfilePicturePath = user.PicturePath;
                    likePost.LikePostPicturePath = post.MediaURL;
                    DateTime LikeDateTime = likePost.LikeDateTime;
                }
            }

            var foodLikers = dB.Like.Where(l => l.UserNameOwner == UnNotif && l.FoodID != 0).ToList();
            foreach (var likeFood in foodLikers)
            {
                var user = dB.User.FirstOrDefault(u => u.UserName == likeFood.UserNameViewer);
                var food = dB.Food.FirstOrDefault(p => p.FoodID == likeFood.FoodID);
                if (user != null)
                {
                    likeFood.ProfilePicturePath = user.PicturePath;
                    likeFood.LikeFoodPicturePath = food.PictureURL;
                    DateTime LikeDateTime = likeFood.LikeDateTime;
                }
            }


            var allData = new List<object>();
            allData.AddRange(CommentPostToLike);
            allData.AddRange(followers);
            allData.AddRange(postLikers);
            allData.AddRange(foodLikers);
            allData.AddRange(commentToPosts);
            allData.AddRange(commentToFoods);

            var sortedAllData = allData.OrderByDescending(item =>
            {
                if (item is FollowersModel follower)
                {
                    return follower.FollowersDateTime;
                }
                else if (item is CommentLikeModel CommentPostLiker)
                {
                    return CommentPostLiker.CommentLikeDateTime;
                }
                else if (item is LikeModel postLiker)
                {
                    return postLiker.LikeDateTime;
                }
                else if (item is CommentModel commentToPost)
                {
                    return commentToPost.DateTime;
                }
                else if (item is CommentModel commentToFood)
                {
                    return commentToFood.DateTime;
                }
                else if (item is LikeModel foodLiker)
                {
                    return foodLiker.LikeDateTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }).ToList();

            var UserNotifIcone = dB.User.FirstOrDefault(x => x.UserName == UnNotif);
            if (UserNotifIcone != null && UserNotifIcone.NotifIcon == true)
            {
                ViewBag.NotifIcon = UserNotifIcone.NotifIcon.ToString();
            }

            ViewBag.UserNameLoginPost = UnNotif;
            ViewBag.AllData = sortedAllData;
            return View();
        }


        [HttpPost]
        public ActionResult Notif(string FollowersUserName, string FollowersUserNameRemove, FollowersModel Follower, FollowingsModel Following)
        {
            string UnNotif = Session["UserName"].ToString();

            var ExistingFollowers = dB.Followers.FirstOrDefault(f => f.UserName == FollowersUserNameRemove && f.FollowersUserName == UnNotif);
            var ExistingFollowins = dB.Followings.FirstOrDefault(x => x.UserName == UnNotif && x.FollowingsUserName == FollowersUserNameRemove);

            if (ExistingFollowers != null && ExistingFollowins != null)
            {
                var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnNotif && x.FollowersUserName == FollowersUserNameRemove);
                if (followerContactToAdd != null)
                {
                    followerContactToAdd.ContactToAdd = false;
                }

                var followingContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnNotif);
                if (followingContactToAdd != null)
                {
                    followingContactToAdd.ContactToAdd = false;
                }

                dB.Followers.Remove(ExistingFollowers);
                dB.Followings.Remove(ExistingFollowins);
                dB.SaveChanges();

                return Json(new { FollowersUserNameRemove = FollowersUserNameRemove });
            }
            else
            {
                Follower.UserName = FollowersUserName;
                Follower.FollowersUserName = UnNotif;
                Following.FollowingsUserName = FollowersUserName;
                Following.UserName = UnNotif;
                Follower.ContactToAdd = true;
                Following.ContactToAdd = true;

                var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnNotif && x.FollowersUserName == FollowersUserName);
                if (followerContactToAdd != null)
                {
                    followerContactToAdd.ContactToAdd = true;
                }

                var followingContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnNotif);
                if (followingContactToAdd != null)
                {
                    followingContactToAdd.ContactToAdd = true;
                }

                dB.Followers.Add(Follower);
                dB.Followings.Add(Following);

                dB.SaveChanges();

                return Json(new { FollowersUserName = FollowersUserName });
            }
        }

        [HttpPost]
        public ActionResult NotifIcon(string NotifIcon)
        {
            string UnNotifIcon = Session["UserName"].ToString();
            var user = dB.User.FirstOrDefault(x => x.UserName == UnNotifIcon);
            if (user != null && NotifIcon.ToString() == "True")
            {
                user.NotifIcon = false;
                ViewBag.NotifIcon = user.NotifIcon.ToString();
                dB.SaveChanges();
            }
            return RedirectToAction("Proofile");
        }


        [HttpGet]
        public ActionResult ShowPost(CommentModel Comment, string PostID)
        {
            string UnShowPost = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(u => u.UserName == UnShowPost);

            var post = dB.Post.FirstOrDefault(p => p.PostID.ToString() == PostID);

            if (post != null)
            {
                UserModel UserPicturePath = dB.User.FirstOrDefault(u => u.UserName == post.UserName);
                UserModel UserLoginPicture = dB.User.FirstOrDefault(u => u.UserName == UnShowPost);

                post.ViewPostNumber++;
                Session["UserLoginPicture"] = UserLoginPicture.PicturePath;
                Session["ImagePathPost"] = post.MediaURL;
                Session["UserPicturePost"] = UserPicturePath.PicturePath;
                ViewBag.PostID = post.PostID;
                ViewBag.UserNameLoginPost = UnShowPost;
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.ViewPostNumber = post.ViewPostNumber;
                ViewBag.UsernamePost = post.UserName;
                ViewBag.TextPost = post.Text;


                var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var likedComment = dB.CommentLike.Any(cl => cl.UserNameLiker == UnShowPost && cl.CommentID == comment.ID);
                    comment.CommentPostLike = likedComment;

                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                    var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnShowPost && l.PostID.ToString() == PostID);
                if (like != null)
                {
                    ViewBag.PostIsLikeModel = like.PostIsLikeModel.ToString();
                }
                else
                {
                    ViewBag.PostIsLikeModel = "False";
                }

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnShowPost && l.PostID.ToString() == PostID);
                if (Favorite != null)
                {
                    ViewBag.PostIsSaveModel = Favorite.PostIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.PostIsSaveModel = "False";
                }
                dB.SaveChanges();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ShowPost(CommentModel Comment, string CommentToPostID, LikeModel likee)
        {
            string UnShowPost = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(u => u.UserName == UnShowPost);
            PostModel post = dB.Post.FirstOrDefault(p => p.PostID.ToString() == CommentToPostID);

            if (post != null && Comment != null)
            {
                var PostNotifIcon = dB.User.FirstOrDefault(p => p.UserName == post.UserName);
                PostNotifIcon.NotifIcon = true;
                Comment.FromUserName = UnShowPost;
                Comment.ToUserName = post.UserName;
                Comment.DateTime = DateTime.Now;
                Comment.CommentToPostID = post.PostID;
                post.CommentNumber++;
                ViewBag.PostLikeNumber = post.PostLikeNumber;
                ViewBag.ViewPostNumber = post.ViewPostNumber;


                if (Comment.CommentText != null)
                {
                    string[] lines = Comment.CommentText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = HttpUtility.HtmlEncode(lines[i]);
                    }
                    string CommentText = string.Join("<br />", lines);

                    dB.SaveChanges();
                }

                dB.Comment.Add(Comment);
                dB.SaveChanges();
            }

            var comments = dB.Comment.Where(c => c.CommentToPostID == post.PostID).ToList().OrderByDescending(c => c.DateTime);
            foreach (var comment in comments)
            {
                var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                if (UserCommentPicture != null)
                {
                    comment.UserCommentPicture = UserCommentPicture.PicturePath;
                }
                var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToPostID == post.PostID);
                var CommentNumber = dB.Post.FirstOrDefault(p => p.PostID == FindComment.CommentToPostID);
                ViewBag.CommentNumber = CommentNumber.CommentNumber;
            }
            ViewBag.Comments = Comment;

            return Json(new { Comment = Comment, UserNameOwnerPost = post.UserName });
        }


        [HttpGet]
        public ActionResult ShowPostRecipe(CommentModel Comment, string FoodID)
        {
            string UnShowPostRecipe = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(u => u.UserName == UnShowPostRecipe);

            var food = dB.Food.FirstOrDefault(p => p.FoodID.ToString() == FoodID);

            if (food != null)
            {
                UserModel UserPicturePath = dB.User.FirstOrDefault(u => u.UserName == food.UserName);
                UserModel UserLoginPicture = dB.User.FirstOrDefault(u => u.UserName == UnShowPostRecipe);

                food.ViewFoodNumber++;
                Session["UserLoginPicture"] = UserLoginPicture.PicturePath;
                Session["PictureFood"] = food.PictureURL;
                Session["VideoFood"] = food.VideoURL;
                Session["UserPictureFood"] = UserPicturePath.PicturePath;
                ViewBag.FoodID = food.FoodID;
                ViewBag.UserNameLoginFood = UnShowPostRecipe;
                ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                ViewBag.ViewFoodNumber = food.ViewFoodNumber;
                ViewBag.UsernameFood = food.UserName;
                ViewBag.Recipe = food.Recipe;
                ViewBag.Subject = food.Subject;
                ViewBag.NameRecipe = food.Name;
                ViewBag.OriginalityPlace = food.OriginalityPlace;
                ViewBag.BiographyRecipe = food.Biography;
                ViewBag.PointsRecipe = food.Points;
                ViewBag.HardshipLevel = food.HardshipLevel;
                ViewBag.CookingTimeHour = food.CookingTimeHour;
                ViewBag.CookingTimeMinute = food.CookingTimeMinute;
                ViewBag.People = food.people;

                var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
                foreach (var comment in comments)
                {
                    var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                    if (UserCommentPicture != null)
                    {
                        comment.UserCommentPicture = UserCommentPicture.PicturePath;
                    }
                    var likedComment = dB.CommentLike.Any(cl => cl.UserNameLiker == UnShowPostRecipe && cl.CommentID == comment.ID);
                    comment.CommentFoodLike = likedComment;
                    var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                    var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                    ViewBag.CommentNumber = CommentNumber.CommentNumber;
                }
                ViewBag.Comments = comments;

                var like = dB.Like.FirstOrDefault(l => l.UserNameViewer == UnShowPostRecipe && l.FoodID.ToString() == FoodID);
                if (like != null)
                {
                    ViewBag.FoodIsLikeModel = like.FoodIsLikeModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsLikeModel = "False";
                }

                var Favorite = dB.Favorite.FirstOrDefault(l => l.UserNameViewer == UnShowPostRecipe && l.FoodID.ToString() == FoodID);
                if (Favorite != null)
                {
                    ViewBag.FoodIsSaveModel = Favorite.FoodIsSaveModel.ToString();
                }
                else
                {
                    ViewBag.FoodIsSaveModel = "False";
                }

                dB.SaveChanges();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ShowPostRecipe(CommentModel Comment, LikeModel likee, string CommentToFoodID)
        {
            string UnShowPostRecipe = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(u => u.UserName == UnShowPostRecipe);
            FoodModel food = dB.Food.FirstOrDefault(p => p.FoodID.ToString() == CommentToFoodID);

            if (food != null && Comment != null)
            {
                var FoodNotifIcon = dB.User.FirstOrDefault(p => p.UserName == food.UserName);
                FoodNotifIcon.NotifIcon = true;
                Comment.FromUserName = UnShowPostRecipe;
                Comment.ToUserName = food.UserName;
                Comment.DateTime = DateTime.Now;
                Comment.CommentToFoodID = food.FoodID;
                food.CommentNumber++;
                ViewBag.FoodLikeNumber = food.FoodLikeNumber;
                ViewBag.ViewFoodNumber = food.ViewFoodNumber;

                if (Comment.CommentText != null)
                {
                    string[] lines = Comment.CommentText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = HttpUtility.HtmlEncode(lines[i]);
                    }
                    string CommentText = string.Join("<br />", lines);

                    dB.SaveChanges();
                }

                dB.Comment.Add(Comment);
                dB.SaveChanges();
            }

            var comments = dB.Comment.Where(c => c.CommentToFoodID == food.FoodID).ToList().OrderByDescending(c => c.DateTime);
            foreach (var comment in comments)
            {
                var UserCommentPicture = dB.User.FirstOrDefault(u => u.UserName == comment.FromUserName);
                if (UserCommentPicture != null)
                {
                    comment.UserCommentPicture = UserCommentPicture.PicturePath;
                }
                var FindComment = dB.Comment.FirstOrDefault(x => x.CommentToFoodID == food.FoodID);
                var CommentNumber = dB.Food.FirstOrDefault(p => p.FoodID == FindComment.CommentToFoodID);
                ViewBag.CommentNumber = CommentNumber.CommentNumber;
            }
            ViewBag.Comments = Comment;

            return Json(new { Comment = Comment, UserNameOwnerPost = food.UserName });
        }


        [HttpGet]
        public ActionResult ShowProfile(string userName, FollowingsModel Following)
        {
            string UnShowProfile = Session["UserName"].ToString();
            UserModel user = dB.User.FirstOrDefault(x => x.UserName == userName);

            var likedPosts = dB.Like.Where(p => p.UserNameViewer == user.UserName)
                                    .ToList();

            var FavoritePosts = dB.Favorite.Where(p => p.UserNameViewer == user.UserName)
                       .ToList();

            var userPosts = dB.Post.Where(p => p.UserName == userName)
                                   .OrderByDescending(p => p.DateTime)
                                   .ToList();

            var foodPosts = dB.Food.Where(p => p.UserName == userName)
                                   .OrderByDescending(p => p.DateTime)
                                   .ToList();

            var allPostsShowProfile = new List<RelationPostModel>();

            allPostsShowProfile.AddRange(likedPosts.Select(post => new RelationPostModel
            {
                PostIsLikeModel = (bool)post.PostIsLikeModel,
                FoodIsLikeModel = (bool)post.FoodIsLikeModel
            }));

            allPostsShowProfile.AddRange(FavoritePosts.Select(post => new RelationPostModel
            {
                PostIsSaveModel = (bool)post.PostIsSaveModel,
                FoodIsSaveModel = (bool)post.FoodIsSaveModel
            }));

            allPostsShowProfile.AddRange(userPosts.Select(post => new RelationPostModel
            {
                UserName = post.UserName,
                MediaURL = post.MediaURL,
                Text = post.Text,
                DateTime = post.DateTime,
                PostID = post.PostID,
                PicturePath = user.PicturePath,
                PostLikeNumber = post.PostLikeNumber,
                ViewPostNumber = post.ViewPostNumber
            }));

            allPostsShowProfile.AddRange(foodPosts.Select(post => new RelationPostModel
            {
                UserName = post.UserName,
                Subject = post.Subject.ToString(),
                Name = post.Name,
                OriginalityPlace = post.OriginalityPlace,
                PictureURL = post.PictureURL,
                VideoURL = post.VideoURL,
                Biography = post.Biography,
                Points = post.Points,
                HardshipLevel = post.HardshipLevel.ToString(),
                CookingTimeHour = post.CookingTimeHour.ToString(),
                CookingTimeMinute = post.CookingTimeMinute.ToString(),
                people = post.people.ToString(),
                Recipe = post.Recipe,
                DateTime = post.DateTime,
                FoodID = post.FoodID,
                PicturePath = user.PicturePath,
                FoodLikeNumber = post.FoodLikeNumber,
                ViewFoodNumber = post.ViewFoodNumber
            }));

            allPostsShowProfile = allPostsShowProfile.OrderByDescending(post => post.DateTime).ToList();

            Session["Owner"] = user.UserName;
            string Owner = Session["Owner"].ToString();
            var ContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == UnShowProfile && x.FollowingsUserName == Owner);

            int PostCount = dB.Post.Where(x => x.UserName == Owner).Count();
            int FoodCount = dB.Food.Where(x => x.UserName == Owner).Count();
            int PostFoodCount = PostCount + FoodCount;

            ViewBag.NumberOfPosts = PostFoodCount;
            ViewBag.Posts = allPostsShowProfile;
            int FollowersCount = dB.Followers.Where(f => f.FollowersUserName == Owner).Count();
            ViewBag.FollowersCount = FollowersCount;
            int FollowingsCount = dB.Followings.Where(x => x.FollowingsUserName == Owner).Count();
            ViewBag.FollowingsCount = FollowingsCount;
            ViewBag.ProfilePicture = user.PicturePath;
            ViewBag.userName = user.UserName;
            ViewBag.Categuri = user.Categuri;
            ViewBag.Biography = user.Biography;

            if (ContactToAdd != null)
            {
                ViewBag.ContactToAdd = ContactToAdd.ContactToAdd.ToString();
            }



            return View();
        }


        [HttpPost]
        public ActionResult ShowProfile(UserModel user, FollowersModel Follower, FollowingsModel Following,
            string FollowersUserName, string FollowersUserNameRemove)
        {
            string UnShowProfile = Session["UserName"].ToString();
            user = dB.User.FirstOrDefault(x => x.UserName == UnShowProfile);
            string Owner = Session["Owner"].ToString();
            var UserNotifIcon = dB.User.FirstOrDefault(p => p.UserName == Owner);


            if (user.UserName == Owner)
            {
                ViewBag.UserMessage = "دوست من، تو میتونی دوست خودت باشی ولی اینکار رو اینجا نمیتونی انجام بدی!";
            }
            else
            {
                var ExistingFollowers = dB.Followers.FirstOrDefault(f => f.UserName == FollowersUserNameRemove && f.FollowersUserName == UnShowProfile);
                var ExistingFollowins = dB.Followings.FirstOrDefault(x => x.UserName == UnShowProfile && x.FollowingsUserName == FollowersUserNameRemove);
                user.NotifIcon = false;

                if (ExistingFollowers != null && ExistingFollowins != null)
                {
                    UserNotifIcon.NotifIcon = false;

                    var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnShowProfile && x.FollowersUserName == FollowersUserNameRemove);
                    if (followerContactToAdd != null)
                    {
                        followerContactToAdd.ContactToAdd = false;
                    }

                    var followingContactToAdd = dB.Followings.FirstOrDefault(x => x.UserName == FollowersUserName && x.FollowingsUserName == UnShowProfile);
                    if (followingContactToAdd != null)
                    {
                        followingContactToAdd.ContactToAdd = false;
                    }

                    dB.Followers.Remove(ExistingFollowers);
                    dB.Followings.Remove(ExistingFollowins);
                    dB.SaveChanges();

                    return Json(new { FollowersUserNameRemove = FollowersUserNameRemove });
                }
                else
                {
                    Follower.UserName = FollowersUserName;
                    Follower.FollowersUserName = UnShowProfile;
                    Follower.FollowersDateTime = DateTime.Now;
                    Following.FollowingsUserName = FollowersUserName;
                    Following.UserName = UnShowProfile;
                    Following.FollowingsDateTime = DateTime.Now;
                    UserNotifIcon.NotifIcon = true;
                    Following.ContactToAdd = true;
                    ViewBag.ContactToAdd = Following.ContactToAdd.ToString();

                    var followerContactToAdd = dB.Followers.FirstOrDefault(x => x.UserName == UnShowProfile && x.FollowersUserName == FollowersUserName);
                    if (followerContactToAdd != null)
                    {
                        followerContactToAdd.ContactToAdd = true;
                    }

                    dB.Followers.Add(Follower);
                    dB.Followings.Add(Following);

                    dB.SaveChanges();

                    return Json(new { FollowersUserName = FollowersUserName });
                }
            }
            return RedirectToAction("ShowProfile", "Home", new { userName = Owner });
        }


        public ActionResult News()
        {
            return View();
        }
    }
}

