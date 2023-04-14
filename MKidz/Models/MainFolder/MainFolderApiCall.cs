using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.Text.Json;

namespace MKidz.Models.MainFolder
{
    public class MainFolderApiCall
    {
        public List<DataModel> GetMainFolder()
        {
            // Load the private key downloaded in step 2
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "credentials.json");
            GoogleCredential credential;
            using (var stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(new[] { DriveService.Scope.Drive });
            }

            // Create a new DriveService using the GoogleCredential
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MKidz"
            });

            // Use the DriveService to access the user's data in Google Drive
            string folderId = "1h-WA9fPEel1Z9v8ISsS46i2pNdgNX-bH"; // The ID of the folder you want to access

            // Use the DriveService to access the files in the folder
            var request = service.Files.List();
            request.Q = "'" + folderId + "' in parents"; // Search for files in the specified folder
            request.Fields = "nextPageToken, files(id, name, mimeType)"; // Define the fields you want to retrieve
            request.IncludeItemsFromAllDrives = true;
            request.SupportsAllDrives = true;
            request.PageSize = 1000;
            var result = request.Execute();
            var files = result.Files;
           

            var stringDta = JsonSerializer.Serialize(files);

            List<DataModel> mainFolder = JsonSerializer.Deserialize<List<DataModel>>(stringDta);
            foreach(var file in mainFolder.ToList())
            {
                file.FolderImage = GetFolderPicture(file.Id);
            }
            return mainFolder;
        }
        public string GetFolderPicture(string folderId)
        {
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "credentials.json");
            GoogleCredential credential;
            using (var stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(new[] { DriveService.Scope.Drive });
            }

            // Create a new DriveService using the GoogleCredential
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MKidz"
            });

            // Use the DriveService to access the user's data in Google Drive
             // The ID of the folder you want to access

            // Use the DriveService to access the files in the folder
            var request = service.Files.List();
            request.Q = $"('{folderId}' in parents) and (mimeType='image/jpeg')"; // Search for files in the specified folder
            request.Fields = "nextPageToken, files(id, name, mimeType)"; // Define the fields you want to retrieve
            request.IncludeItemsFromAllDrives = true;
            request.SupportsAllDrives = true;
            var result = request.Execute();
            var files = result.Files;
            return $"https://drive.google.com/uc?export=download&id={files[0].Id}";
        }
    }
}
