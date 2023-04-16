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
                string[] aNameParts = a.Name.Split('.');
                string[] bNameParts = b.Name.Split('.');
                string aNumericPart, bNumericPart;
                int aNumericValue, bNumericValue;

                // Get the first numeric part of a's name
                aNumericPart = Regex.Match(aNameParts[0], @"\d+").Value;
                // If there is no numeric part, set aNumericValue to int.MaxValue so that it will be sorted to the end
                aNumericValue = aNumericPart == "" ? int.MaxValue : int.Parse(aNumericPart);

                // Get the first numeric part of b's name
                bNumericPart = Regex.Match(bNameParts[0], @"\d+").Value;
                // If there is no numeric part, set bNumericValue to int.MaxValue so that it will be sorted to the end
                bNumericValue = bNumericPart == "" ? int.MaxValue : int.Parse(bNumericPart);

                // Compare the numeric parts of a and b
                int comparisonResult = aNumericValue.CompareTo(bNumericValue);
                if (comparisonResult != 0)
                {
                    // If the numeric parts are different, return the comparison result
                    return comparisonResult;
                }

                // If the numeric parts are the same, compare the rest of the name
                string aNonNumericPart = aNameParts[0].Substring(aNumericPart.Length);
                string bNonNumericPart = bNameParts[0].Substring(bNumericPart.Length);
                comparisonResult = string.Compare(aNonNumericPart, bNonNumericPart, StringComparison.OrdinalIgnoreCase);
                if (comparisonResult != 0)
                {
                    // If the non-numeric parts are different, return the comparison result
                    return comparisonResult;
                }

                // If the non-numeric parts are also the same, compare the file extensions
                return string.Compare(aNameParts[1], bNameParts[1], StringComparison.OrdinalIgnoreCase);
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