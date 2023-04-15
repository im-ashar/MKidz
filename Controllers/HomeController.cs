using Microsoft.AspNetCore.Mvc;
using MKidz.Models;
using MKidz.Models.AudioFiles;
using MKidz.Models.MainFolder;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MKidz.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Audio(string name, string id)
        {

            GetAudioFilesApiCall api = new GetAudioFilesApiCall();
            var audio = api.GetAudioFilesById(name, id);
            if (audio != null)
            {
                return View("SingleAudioFile", audio);

            }
            return BadRequest("File Not Found");
        }

        [HttpGet]
        public IActionResult Index()
        {
            MainFolderApiCall apiCall = new MainFolderApiCall();
            var data = apiCall.GetMainFolder();

            return View(data);
        }
        public IActionResult AudioFiles(string folderId, string folderName)
        {
            GetAudioFilesApiCall api = new GetAudioFilesApiCall();
            var audioFiles = api.GetAudioFiles(folderId);
            ViewBag.folderName = folderName;
            audioFiles.Sort((a, b) =>
            {
                int aNumber, bNumber;
                if (int.TryParse(Regex.Match(a.Name, @"\d+").Value, out aNumber) && int.TryParse(Regex.Match(b.Name, @"\d+").Value, out bNumber))
                {
                    return aNumber.CompareTo(bNumber);
                }
                else
                {
                    return a.Name.CompareTo(b.Name);
                }
            });



            return PartialView(audioFiles);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}