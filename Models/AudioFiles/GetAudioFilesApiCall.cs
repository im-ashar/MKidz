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
        public DataModel GetAudioFilesById(string folderName, string audioId)
        {
            bool found = false;
            var folderId = string.Empty;

            MainFolderApiCall mainFolderApiCall = new MainFolderApiCall();
            var allFolders = mainFolderApiCall.GetMainFolder();

            foreach (var folder in allFolders)
            {
                if (folder.Name.ToLower() == folderName.ToLower())
                {
                    found = true;
                    folderId = folder.Id;
                    folderName = folder.Name;
                    break;
                }
            }
            if (found)
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

                foreach (var audio in audioFiles)
                {
                    var x = audio.Name.Split('.');
                    if (x[0] == audioId)
                    {
                        audio.FolderId = folderId;
                        audio.FolderName = folderName;
                        return audio;
                    }
                }
            }

            return null;

        }
    }
}
