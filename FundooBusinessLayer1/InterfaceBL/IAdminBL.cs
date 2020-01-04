// ******************************************************************************
//  <copyright file="IAdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAdminBL.cs
//  
//     Purpose:  Creating interface for Admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.InterfaceBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
   
    public interface IAdminBL
    {
        Task<bool> Register(RegistrationModel registrationModel);

        Task<AccountResponse > Login(LoginModel loginModel);

        Dictionary<string, int> GetUserStatistics();

        Task<string> GenerateToken(AccountResponse accountResponse);
      
    }
}
