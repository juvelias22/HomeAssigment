using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Web;
using System.Web.Mvc;

namespace HomeAssigment.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }

        static string ApplicationName = "Enterprise-Home-Assigment";

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string accessToken = "SW-I0OEdJxAAAAAAAAAAfphwh-KYekJ2hnYYLwSAOdhqOZfIH6nspKGb2IMYDriB";

            using (DropboxClient client =
                new DropboxClient(accessToken, new DropboxClientConfig(ApplicationName)))
            {
                string[] splitInputFileName = file.FileName.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                string fileNameAndExtension = splitInputFileName[splitInputFileName.Length - 1];

                string[] fileNameAndExtensionSplit = fileNameAndExtension.Split('.');
                string originalFileName = fileNameAndExtensionSplit[0];
                string originalExtension = fileNameAndExtensionSplit[1];

                string fileName = @"/Images/" + originalFileName + Guid.NewGuid().ToString().Replace("-", "") + "." + originalExtension;



                var updated = client.Files.UploadAsync(fileName,
                                                        mode: WriteMode.Overwrite.Instance,
                                                        body: file.InputStream).Result;

                var result = client.Sharing.CreateSharedLinkWithSettingsAsync(fileName).Result;

                return RedirectToAction("Index", "Home", new { ImageUrl = result.Url });
            }

        }

        public ActionResult ViewImage(string imageUrl)
        {

            throw new NotImplementedException();
        }


         
    }
}