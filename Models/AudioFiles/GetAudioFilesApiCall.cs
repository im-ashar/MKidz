using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using MKidz.Models.MainFolder;
using System.Text.Json;

namespace MKidz.Models.AudioFiles
{
    public class GetAudioFilesApiCall
    {
        public List<DataModel> GetAudioFiles(string folderId)
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

            // Use the DriveService to access the files in the folder
            var request = service.Files.List();
            request.Q = $"'{folderId}' in parents"; // Search for files in the specified folder
            request.Fields = "nextPageToken, files(id, name, mimeType)"; // Define the fields you want to retrieve
            request.IncludeItemsFromAllDrives = true;
            request.SupportsAllDrives = true;
            request.PageSize = 1000;
            var result = request.Execute();
            var files = result.Files;


            var stringDta = JsonSerializer.Serialize(files);

            List<DataModel> audioFiles = JsonSerializer.Deserialize<List<DataModel>>(stringDta);

            return audioFiles;
        }
    }
}
