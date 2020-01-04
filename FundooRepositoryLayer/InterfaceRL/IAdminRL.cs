// ******************************************************************************
//  <copyright file="AdminRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminRL.cs
//  
//     Purpose:  Implementing different methods for admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.InterfaceRL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
  
    public interface IAdminRL
    {
        Task<bool> Register(RegistrationModel registrationModel);

        Task<AccountResponse> Login(LoginModel loginModel);

        Dictionary<string, int> GetUserStatistics();

        Task<string> GenerateToken(AccountResponse accountResponse);

        IList<AccountResponse> UsersInfo();

        IList<AccountResponse> SearchUser(string name);
    }
}
