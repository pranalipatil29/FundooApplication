using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.InterfaceRL
{
   public interface INoteRL
    {
        Task<bool> CreateNote(NoteModel noteModel,string userID);

        Task<bool> DeleteNote(int NoteID);
    }
}
