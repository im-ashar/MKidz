using Microsoft.AspNetCore.Mvc;
using MKidz.Models.Database;

namespace MKidz.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly IRecordsFunctions _recordsFunctions;
        public DatabaseController(IRecordsFunctions recordsFunctions)
        {
            _recordsFunctions = recordsFunctions;
        }
        public void AddAudioToDB(string audioName)
        {
            if (_recordsFunctions.AlreadyExist(audioName))
            {
                _recordsFunctions.Update(audioName);
            }
            else
            {
                _recordsFunctions.Add(new Records { AudioName = audioName, AudioCount = 1 });
            }
        }
        public IActionResult Admin(string user,string pass)
        {
            if (user == "mkidz" && pass == "mkidz786")
            {
                return View(_recordsFunctions.GetAll());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
