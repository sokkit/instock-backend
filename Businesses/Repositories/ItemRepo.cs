﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using instock_server_application.Businesses.Models;
using instock_server_application.Businesses.Repositories.Interfaces;

namespace instock_server_application.Businesses.Repositories; 

public class ItemRepo : IItemRepo{
    private readonly IAmazonDynamoDB _client;

    public ItemRepo(IAmazonDynamoDB client) {
        _client = client;
    }
    public async Task<List<Dictionary<string, AttributeValue>>> GetAllItems(string businessId) {
        var request = new QueryRequest {
            TableName = Item.TableName,
            IndexName = "BusinessId",
            KeyConditionExpression = "BusinessId = :Id",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":Id", new AttributeValue(businessId)}
            }
        };
        var response = await _client.QueryAsync(request);
        return response.Items;
    }
}