using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.InterfaceBL
{
   public interface ILabelBL
    {
        Task<bool> CreateLabel(RequestLabel requestLabel,string userID);

        Task<LabelModel> UpdateLabel(RequestLabel requestLabel, int labelID,string userID);

        IList<LabelModel> DisplayLabels(string userID);


        Task<bool> DeleteLabel(int labelID, string userID);

    }
}
