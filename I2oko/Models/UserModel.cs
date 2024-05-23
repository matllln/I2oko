using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public enum Sexuality
    {
        [Display(Name = "")] ___,
        [Display(Name = "مرد")] آقا,
        [Display(Name = "زن")] خانم,
        [Display(Name = "نامشخص")] نامشخص
    }
    public enum Categuri
    {
        [Display(Name = "")] ___,
        [Display(Name = "کارآفرین")] کارآفرین,
        [Display(Name = "پزشک")] پزشک,
        [Display(Name = "مهندس")] مهندس,
        [Display(Name = "سر آشپز")] سرآشپز,
        [Display(Name = "مغازه دار")] مغازه‌دار,
        [Display(Name = "رستوران")] رستوران,
        [Display(Name = "کارمند")] کارمند,
        [Display(Name = "خانه دار")] خانه‌دار,
        [Display(Name = "هنرمند")] هنرمند,
        [Display(Name = "ورزشکار")] ورزشکار,
        [Display(Name = "بلاگر")] بلاگر,
        [Display(Name = "دانش آموز")] دانش‌اموز,
        [Display(Name = "معلم")] معلم,
        [Display(Name = "نویسنده")] نویسنده,
        [Display(Name = "فعال سیاسی")] فعال‌سیاسی,
        [Display(Name = "فعال اقتصادی")] فعال‌اقتصادی,
        [Display(Name = "فعال فرهنگی")] فعال‌فرهنگی,
        [Display(Name = "فعال زیبایی")] فعال‌زیبایی
    }
    public enum BirthDateY
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1301 ")] سیصدویک,
        [Display(Name = " 1302 ")] سیصدودو,
        [Display(Name = " 1303 ")] سیصدوسه,
        [Display(Name = " 1304 ")] سیصدوچهار,
        [Display(Name = " 1305 ")] سیصدوپنج,
        [Display(Name = " 1306 ")] سیصدوشش,
        [Display(Name = " 1307 ")] سیصدوهفت,
        [Display(Name = " 1308 ")] سیصدوهشت,
        [Display(Name = " 1309 ")] سیصدونه,
        [Display(Name = " 1310 ")] سیصدوده,
        [Display(Name = " 1311 ")] سیصدویازده,
        [Display(Name = " 1312 ")] سیصدودوازده,
        [Display(Name = " 1313 ")] سیصدوسیزده,
        [Display(Name = " 1314 ")] سیصدوچهارده,
        [Display(Name = " 1315 ")] سیصدوپانزده,
        [Display(Name = " 1316 ")] سیصدوشانزده,
        [Display(Name = " 1317 ")] سیصدوهفده,
        [Display(Name = " 1318 ")] سیصدوهجده,
        [Display(Name = " 1319 ")] سیصدونونزده,
        [Display(Name = " 1320 ")] سیصدوبیست,
        [Display(Name = " 1321 ")] سیصدوبیست‌ویک,
        [Display(Name = " 1322 ")] سیصدوبیست‌ودو,
        [Display(Name = " 1323 ")] سیصدوبیست‌وسه,
        [Display(Name = " 1324 ")] سیصدوبیست‌وچهار,
        [Display(Name = " 1325 ")] سیصدوبیست‌وپنج,
        [Display(Name = " 1326 ")] سیصدوبیست‌وشش,
        [Display(Name = " 1327 ")] سیصدوبیست‌وهفت,
        [Display(Name = " 1328 ")] سیصدوبیست‌وهشت,
        [Display(Name = " 1329 ")] سیصدوبیست‌ونه,
        [Display(Name = " 1330 ")] سیصدوسی‌,
        [Display(Name = " 1331 ")] سیصدوسی‌ویک,
        [Display(Name = " 1332 ")] سیصدوسی‌ودو,
        [Display(Name = " 1333 ")] سیصدوسی‌وسه,
        [Display(Name = " 1334 ")] سیصدوسی‌وچهار,
        [Display(Name = " 1335 ")] سیصدوسی‌وپنج,
        [Display(Name = " 1336 ")] سیصدوسی‌وشش,
        [Display(Name = " 1337 ")] سیصدوسی‌وهفت,
        [Display(Name = " 1338 ")] سیصدوسی‌وهشت,
        [Display(Name = " 1339 ")] سیصدوسی‌ونه,
        [Display(Name = " 1340 ")] سیصدوچهل‌,
        [Display(Name = " 1341 ")] سیصدوچهل‌ویک,
        [Display(Name = " 1342 ")] سیصدوچهل‌ودو,
        [Display(Name = " 1343 ")] سیصدوچهل‌وسه,
        [Display(Name = " 1344 ")] سیصدوچهل‌وچهار,
        [Display(Name = " 1345 ")] سیصدوچهل‌وپنج,
        [Display(Name = " 1346 ")] سیصدوچهل‌وشش,
        [Display(Name = " 1347 ")] سیصدوچهل‌وهفت,
        [Display(Name = " 1348 ")] سیصدوچهل‌وهشت,
        [Display(Name = " 1349 ")] سیصدوچهل‌ونه,
        [Display(Name = " 1350 ")] سیصدوپنجاه‌,
        [Display(Name = " 1351 ")] سیصدوپنجاه‌ویک,
        [Display(Name = " 1352 ")] سیصدوپنجاه‌ودو,
        [Display(Name = " 1353 ")] سیصدوپنجاه‌وسه,
        [Display(Name = " 1354 ")] سیصدوپنجاه‌وچهار,
        [Display(Name = " 1355 ")] سیصدوپنجاه‌وپنج,
        [Display(Name = " 1356 ")] سیصدوپنجاه‌وشش,
        [Display(Name = " 1357 ")] سیصدوپنجاه‌وهفت,
        [Display(Name = " 1358 ")] سیصدوپنجاه‌وهشت,
        [Display(Name = " 1359 ")] سیصدوپنجاه‌ونه,
        [Display(Name = " 1360 ")] سیصدوشصد‌,
        [Display(Name = " 1361 ")] سیصدوشصد‌ویک,
        [Display(Name = " 1362 ")] سیصدوشصد‌ودو,
        [Display(Name = " 1363 ")] سیصدوشصد‌وسه,
        [Display(Name = " 1364 ")] سیصدوشصد‌وچهار,
        [Display(Name = " 1365 ")] سیصدوشصد‌وپنج,
        [Display(Name = " 1366 ")] سیصدوشصد‌وشش,
        [Display(Name = " 1367 ")] سیصدوشصد‌وهفت,
        [Display(Name = " 1368 ")] سیصدوشصد‌وهشت,
        [Display(Name = " 1369 ")] سیصدوشصد‌ونه,
        [Display(Name = " 1370 ")] سیصدوهفتاد,
        [Display(Name = " 1371 ")] سیصدوهفتاد‌ویک,
        [Display(Name = " 1372 ")] سیصدوهفتاد‌ودو,
        [Display(Name = " 1373 ")] سیصدوهفتاد‌وسه,
        [Display(Name = " 1374 ")] سیصدوهفتاد‌وچهار,
        [Display(Name = " 1375 ")] سیصدوهفتاد‌وپنج,
        [Display(Name = " 1376 ")] سیصدوهفتاد‌وشش,
        [Display(Name = " 1377 ")] سیصدوهفتاد‌وهفت,
        [Display(Name = " 1378 ")] سیصدوهفتاد‌وهشت,
        [Display(Name = " 1379 ")] سیصدوهفتاد‌ونه,
        [Display(Name = " 1380 ")] سیصدوهشتاد‌,
        [Display(Name = " 1381 ")] سیصدوهشتاد‌ویک,
        [Display(Name = " 1382 ")] سیصدوهشتاد‌ودو,
        [Display(Name = " 1383 ")] سیصدوهشتاد‌وسه,
        [Display(Name = " 1384 ")] سیصدوهشتاد‌وچهار,
        [Display(Name = " 1385 ")] سیصدوهشتاد‌وپنج,
        [Display(Name = " 1386 ")] سیصدوهشتاد‌وشش,
        [Display(Name = " 1387 ")] سیصدوهشتاد‌وهفت,
        [Display(Name = " 1388 ")] سیصدوهشتاد‌وهشت,
        [Display(Name = " 1389 ")] سیصدوهشتاد‌ونه,
        [Display(Name = " 1390 ")] سیصدونود,
        [Display(Name = " 1391 ")] سیصدونودویک,
        [Display(Name = " 1392 ")] سیصدونودودو,
        [Display(Name = " 1393 ")] سیصدونودوسه,
        [Display(Name = " 1394 ")] سیصدونودوچهار,
        [Display(Name = " 1395 ")] سیصدونودوپنج,
        [Display(Name = " 1396 ")] سیصدونودوشش,
        [Display(Name = " 1397 ")] سیصدونودوهفت,
        [Display(Name = " 1398 ")] سیصدونودوهشت,
        [Display(Name = " 1399 ")] سیصدونودونه,
        [Display(Name = " 1400 ")] چهارصد,
        [Display(Name = " 1401 ")] چهارصدویک,
        [Display(Name = " 1402 ")] چهارصدودو,
        [Display(Name = " 1403 ")] چهارصدوسه
    }
    public enum BirthDateM
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1 ")] فروردین,
        [Display(Name = " 2 ")] اردیبهشت,
        [Display(Name = " 3 ")] خرداد,
        [Display(Name = " 4 ")] تیر,
        [Display(Name = " 5 ")] مرداد,
        [Display(Name = " 6 ")] شهریور,
        [Display(Name = " 7 ")] مهر,
        [Display(Name = " 8 ")] آبان,
        [Display(Name = " 9 ")] آذر,
        [Display(Name = " 10 ")] دی,
        [Display(Name = " 11 ")] بهمن,
        [Display(Name = " 12 ")] اسفند
    }
    public enum BirthDateD
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1 ")] یکم,
        [Display(Name = " 2 ")] دوم,
        [Display(Name = " 3 ")] سوم,
        [Display(Name = " 4 ")] چهارم,
        [Display(Name = " 5 ")] پنجم,
        [Display(Name = " 6 ")] ششم,
        [Display(Name = " 7 ")] هفتم,
        [Display(Name = " 8 ")] هشتم,
        [Display(Name = " 9 ")] نهم,
        [Display(Name = " 10 ")] دهم,
        [Display(Name = " 11 ")] یازدهم,
        [Display(Name = " 12 ")] دوازدهم,
        [Display(Name = " 13 ")] سیزدهم,
        [Display(Name = " 14 ")] چهاردهم,
        [Display(Name = " 15 ")] پانزدهم,
        [Display(Name = " 16 ")] شانزدهم,
        [Display(Name = " 17 ")] هفدهم,
        [Display(Name = " 18 ")] هجدهم,
        [Display(Name = " 19 ")] نونزدهم,
        [Display(Name = " 20 ")] بیستوم,
        [Display(Name = " 21 ")] بیست‌ویکم,
        [Display(Name = " 22 ")] بیست‌ودوم,
        [Display(Name = " 23 ")] بیست‌وسوم,
        [Display(Name = " 24 ")] بیست‌وچهارم,
        [Display(Name = " 25 ")] بیست‌وپنجم,
        [Display(Name = " 26 ")] بیست‌وششم,
        [Display(Name = " 27 ")] بیست‌وهفتم,
        [Display(Name = " 28 ")] بیست‌وهشتم,
        [Display(Name = " 29 ")] بیست‌ونهم,
        [Display(Name = " 30 ")] سیوم,
        [Display(Name = " 31 ")] سی‌ویکم
    }
    public class UserModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Fname { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Sexuality Sexuality { get; set; }
        public BirthDateY BirthDateY { get; set; }
        public BirthDateM BirthDateM { get; set; }
        public BirthDateD BirthDateD { get; set; }
        public Categuri Categuri { get; set; }
        public int SkilLevel { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string KeyNum { get; set; }
        public bool RememberMe { get; set; }
        public string UploadeData { get; set; }
        [Column(TypeName = "varbinary(max)")]
        public byte[] Picture { get; set; }
        public string PicturePath { get; set; }
        public List<RelationPostModel> LastThreePosts { get; set; }
        public RelationPostModel Post1 { get; internal set; }
        public RelationPostModel Post2 { get; internal set; }
        public RelationPostModel Post3 { get; internal set; }
        public bool NotifIcon { get; set; }
    }
}