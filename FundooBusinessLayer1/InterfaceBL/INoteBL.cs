using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.InterfaceBL
{
    public interface INoteBL
    {
        Task<bool> CreateNote(RequestNote requestNote, string userID);

        IList<NoteModel> DisplayNotes(string userID);

        Task<NoteModel> UpdateNote(RequestNote noteRequest, int noteID,string userID);

        Task<bool> DeleteNote(int noteID,string userID);
    }
}
