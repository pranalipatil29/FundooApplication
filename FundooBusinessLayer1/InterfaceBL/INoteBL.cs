using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.InterfaceBL
{
    public interface INoteBL
    {
        Task<bool> CreateNote(NoteModel noteModel,string userID);

        //Task<bool> DisplayNotes(NoteModel noteModel);

        //Task<bool> UpdateNote(NoteModel noteModel);

        Task<bool> DeleteNote(int userID);
    }
}
