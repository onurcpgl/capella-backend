﻿using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IClassificationService
    {
        Task Save(ClassificationDto classificationDto);
        Task<List<Classification>> GetAllClassifications();
        Task<ClassificationDto> GetClassificationByCode(string code);


    }
}
