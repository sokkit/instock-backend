﻿namespace instock_server_application.AwsS3.Dtos; 

public class S3ResponseDto {
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";
}