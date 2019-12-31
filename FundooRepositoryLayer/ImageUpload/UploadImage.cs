using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooCommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.ImageUpload
{
    public class UploadImage
    {
        //private readonly IFormFile formFile;
        //private readonly IConfiguration configuration;
        private readonly ApplicationSetting applicationSettings;
        private string apiKey,apiSecretKey,cloudName;

        public UploadImage(string apiKey, string apiSecretKey, string cloudName)
        {
            this.apiKey = apiKey;
            this.apiSecretKey = apiSecretKey;
            this.cloudName = cloudName;
        }

        public string Upload(IFormFile formFile)
        {
            try
            {               
                Account account = new Account()
                {
                    Cloud = this.cloudName,
                    ApiKey = this.apiKey,
                    ApiSecret = this.apiSecretKey
                };

                Cloudinary cloudinary = new Cloudinary(account);

                var file = formFile.FileName;

                var stream = formFile.OpenReadStream();

                ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
                {
                    File = new FileDescription(file, stream)
                });

                return result.Uri.ToString();
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
