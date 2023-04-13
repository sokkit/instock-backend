﻿using instock_server_application.Businesses.Dtos;

namespace instock_server_application.Businesses.Services.Interfaces; 

public abstract class ExternalShopAuthenticator {
    
    public String Username;

    public String Password;
    
    public ExternalShopAuthenticator(string username, string password) {
        Username = username;
        Password = password;
    }
    
    public abstract  Task<ExternalShopAuthenticationTokenDto> LoginToShop(ExternalShopLoginDto loginDetails);
    
    //contains case statements 
}