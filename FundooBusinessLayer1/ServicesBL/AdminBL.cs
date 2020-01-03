﻿// ******************************************************************************
//  <copyright file="AdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminBL.cs
//  
//     Purpose:  Implementing different methods for admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.ServicesBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
   
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

      public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
                if(loginModel != null)
                {
                    var result = await this.adminRL.Login(loginModel);

                    return result;
                }
                else
                {
                    throw new Exception("EmailID and Password is required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try
            {
                if (accountResponse != null)
                {
                    var result = await this.adminRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }

    }
}
