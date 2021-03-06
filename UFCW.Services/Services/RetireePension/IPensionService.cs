﻿using System;
using System.Threading.Tasks;
using UFCW.Services.Models.Pension;

namespace UFCW.Services.Services.Pension
{
    public interface IPensionService
    {
        Task<Retiree> FetchRetiree(String Token, String SSN, String Email); //Fetch Retiree
        Task<SummaryPlanDoc[]> FetchSummaryPlanDoc(string Token, string SSN, string Email);//Fetch Sumary Plan Docs

	}
}
