﻿using HugHubPricing.Models;

namespace HugHubPricing.Interfaces
{
    public interface IQSResponseValidator
    {
        bool ValidateResponse(dynamic systemResponse);
    }
}