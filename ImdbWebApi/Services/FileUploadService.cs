using ImdbWebApi.Exceptions;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApiComplete;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

namespace ImdbWebApi.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ConnectionString _connectionString;    

        public FileUploadService(IOptions<ConnectionString> options)
        {
            _connectionString = options.Value;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new BadRequestException("File not selected.");
                }

                using var stream = file.OpenReadStream();
                var task = await new FirebaseStorage(_connectionString.FirebaseBucketKey)
                        .Child("images")
                        .Child(Guid.NewGuid().ToString() + ".jpg")
                        .PutAsync(stream);

                return task;
            }
            catch (CustomException)
            {
                throw new InternalServerException("Something went wrong while file uploading.");
            }
        }
    }
}
