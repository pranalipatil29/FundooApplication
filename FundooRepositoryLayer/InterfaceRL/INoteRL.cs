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
        Task<bool> CreateNote(RequestNote noteRequest,string userID);

        Task<bool> DeleteNote(int noteID);

        Task<NoteModel> UpdateNote(RequestNote noteRequest, int noteID);

        IList<NoteModel> DisplayNotes(string userID);
    }
}
