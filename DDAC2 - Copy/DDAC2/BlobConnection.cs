using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Mvc;

namespace DDAC2
{
    public class BlobConnection
    {
      
        public CloudBlobContainer blobContainer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(configuration["ConnectionStrings:AzureStorageConnectionString-1"]);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("test-blob-container");
            return cloudBlobContainer;
        }
        public void UploadBlob(string vin, string path)
        {
            CloudBlobContainer container = blobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference(vin + ".jpg");
            blob.Properties.ContentType = "image/jpg";
            using (var fileStream = System.IO.File.OpenRead("@" + path))
            {
                blob.UploadFromStreamAsync(fileStream).Wait();
            }
        }
      
        public string DownloadBlob()
        {
            CloudBlobContainer container = blobContainer();
            // select object from computer and move it to the blob storage
            CloudBlockBlob blob = container.GetBlockBlobReference("myBlob.jpg");
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\Darryl Yew\Desktop\bobo.jpg")) { blob.DownloadToStreamAsync(fileStream).Wait(); }
            blob = container.GetBlockBlobReference("rose2.jpg");
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\Darryl Yew\Desktop\bobo2.jpg")) { blob.DownloadToStreamAsync(fileStream).Wait(); }
            return "Success Upload a binary object to the blob storage";
        }
        public string Deleteblob()
        {
            CloudBlobContainer container = blobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("myBlob.jpg");
            var success = blob.DeleteIfExistsAsync().Result;
            if (success == false)
            {
                return "failed";
            }
            return "completed";
        }
    }
}
