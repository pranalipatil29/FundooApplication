using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using FundooRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.ServicesBL
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task<bool> CreateLabel(RequestLabel requestLabel, string userID)
        {
           try
            {
                if(requestLabel != null)
                {
                    return await labelRL.CreateLabel(requestLabel, userID);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<LabelModel> UpdateLabel(RequestLabel requestLabel, int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await labelRL.UpdateLabel(requestLabel, labelID,userID);
                }
                else
                {
                    throw new Exception("Please enter LabelID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteLabel(int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await labelRL.DeleteLabel(labelID, userID);
                }
                else
                {
                    throw new Exception("Pleaase enter label ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IList<LabelModel> DisplayLabels(string userID)
        {
            try
            {
                if (userID != null)
                {
                    return labelRL.DisplayLabels(userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
