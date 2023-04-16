﻿using Amazon.DynamoDBv2.DataModel;
using instock_server_application.Businesses.Dtos;
using instock_server_application.Businesses.Models;
using instock_server_application.Businesses.Repositories.Interfaces;

namespace instock_server_application.Businesses.Repositories; 

public class MilestoneRepository : IMilestoneRepository {
    private readonly IDynamoDBContext _context;

    public MilestoneRepository(IDynamoDBContext context) {
        _context = context;
    }

    public async Task<MilestoneDto> SaveNewMilestone(MilestoneDto milestoneDto) {
        MilestoneModel milestone = new MilestoneModel(milestoneDto);

        await _context.SaveAsync(milestone);

        return milestoneDto;
    }
    
    public async Task<List<MilestoneDto>> GetAllMilestones(string businessId) {
        List<MilestoneModel> listOfMilestoneModels = await _context.ScanAsync<MilestoneModel>(
            new [] {
                MilestoneModel.ByBusinessId(businessId)
            }).GetRemainingAsync();
        
        // Convert list of items
        List<MilestoneDto> listOfMilestoneDto = new List<MilestoneDto>();
        
        foreach (MilestoneModel milestoneModel in listOfMilestoneModels) {
            listOfMilestoneDto.Add(
                new MilestoneDto(
                    milestoneModel.MilestoneId,
                    milestoneModel.BusinessId,
                    milestoneModel.ItemSku,
                    milestoneModel.ItemName,
                    milestoneModel.ImageFilename,
                    milestoneModel.TotalSales,
                    milestoneModel.DateTime,
                    milestoneModel.DisplayMilestone
                )
            );
        }
        
        return listOfMilestoneDto;
    }

    public async Task<MilestoneDto> HideMilestone(HideMilestoneDto hideMilestoneDto) {
        MilestoneModel milestone = await _context.LoadAsync<MilestoneModel>(hideMilestoneDto.MilestoneId, hideMilestoneDto.BusinessId);
        milestone.DisplayMilestone = false;
        await _context.SaveAsync(milestone);

        MilestoneDto milestoneDto = new MilestoneDto(milestone);

        return milestoneDto;
    }
}