using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.InterfaceRL
{
   public interface INoteRL
    {
        Task<bool> CreateNote(RequestNote requestNote, string userID);

        Task<bool> DeleteNote(int noteID, string userID);

        Task<NoteModel> UpdateNote(RequestNote noteRequest, int noteID, string userID);

        IList<NoteModel> DisplayNotes(string userID);
    }
}
