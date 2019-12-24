using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.ServiceRL
{
    public class LabelRL : ILabelRL
    {
        private AuthenticationContext authenticationContext;
        
        public LabelRL(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        public async Task<bool> CreateLabel(RequestLabel requestLabel, string userID)
        {
            try
            {
                if(requestLabel != null)
                {
                    var data = new LabelModel()
                    {
                        UserID = userID,
                        Label = requestLabel.Label,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    authenticationContext.Label.Add(data);
                    await authenticationContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<LabelModel> UpdateLabel(RequestLabel requestLabel,int labelID)
        {
            try
            {
                var LabelInfo = authenticationContext.Label.Where(s => s.LabelID == labelID).FirstOrDefault();

                if(LabelInfo != null)
                {
                    LabelInfo.ModifiedDate = DateTime.Now;

                   if(requestLabel.Label !=null && requestLabel.Label != "")
                    {
                        LabelInfo.Label = requestLabel.Label;
                    }

                    authenticationContext.Label.Update(LabelInfo);
                    await authenticationContext.SaveChangesAsync();

                    return LabelInfo;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
   
        }
    }
}
