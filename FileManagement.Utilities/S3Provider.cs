using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.Utilities
{
    public class S3Provider : IFileProvider
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            await SendMyFileToS3(file.OpenReadStream(), "", "", "");
            return "";
        }

        public async Task SendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            using (IAmazonS3 client = new AmazonS3Client(RegionEndpoint.EUWest1))
            {
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    request.BucketName = bucketName; //no subdirectory just bucket name
                }
                else
                { 
                    request.BucketName = bucketName + @"/" + subDirectoryInBucket;
                }
                request.Key = fileNameInS3;
                request.FilePath = localFilePath;
                await utility.UploadAsync(request);
            }
        }

        public async Task SendMyFileToS3(Stream fileStream, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            using (IAmazonS3 client = new AmazonS3Client(RegionEndpoint.EUWest1))
            {
                TransferUtility utility = new TransferUtility(client);
                string s3BucketName ;
                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    s3BucketName = bucketName; //no subdirectory just bucket name
                }
                else
                {
                    s3BucketName = bucketName + @"/" + subDirectoryInBucket;
                }
                await utility.UploadAsync(fileStream, s3BucketName, fileNameInS3);

            }
        }
    }
}
