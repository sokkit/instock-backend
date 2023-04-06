﻿using Amazon.S3;
using Amazon.S3.Transfer;
using instock_server_application.AwsS3.Dtos;
using instock_server_application.AwsS3.Models;
using instock_server_application.AwsS3.Services.Interfaces;

namespace instock_server_application.AwsS3.Services; 

public class StorageService : IStorageService {
    private readonly IAmazonS3 _client;
    
    public StorageService(IAmazonS3 client) {
        _client = client;
    }
    
    public async Task<S3ResponseDto> UploadFileAsync(S3Object s3Object) {
        var response = new S3ResponseDto();

        try {
            var uploadRequest = new TransferUtilityUploadRequest() {
                InputStream = s3Object.InputStream,
                Key = s3Object.Name,
                BucketName = s3Object.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            var transferUtility = new TransferUtility(_client);
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Message = $"{s3Object.Name} has been uploaded successfully";
        }
        catch (AmazonS3Exception e) {
            response.StatusCode = (int)e.StatusCode;
            response.Message = e.Message;
        }
        catch (Exception e) {
            response.StatusCode = 500;
            response.Message = e.Message;
        }

        return response;
    }
}