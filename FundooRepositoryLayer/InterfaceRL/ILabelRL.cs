using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.InterfaceRL
{
   public interface ILabelRL
    {
        Task<bool> CreateLabel(RequestLabel requestLabel, string userID);

        Task<LabelModel> UpdateLabel(RequestLabel requestLabel, int labelID);
    }
}
