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
    public class NoteBL:INoteBL
    {
        public readonly INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public async Task<bool> CreateNote(RequestNote requestNote, string userID)
        {
            try
            {
                if (requestNote != null)
                {
                    return await noteRL.CreateNote(requestNote, userID);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                if (noteID != 0)
                {
                    return await noteRL.DeleteNote(noteID,userID);
                }
                else
                {
                    throw new Exception("Pleaase enter Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<NoteModel> UpdateNote(RequestNote requestNote, int noteID, string userID)
        {
            try
            {
                if(noteID != 0)
                {
                    return await noteRL.UpdateNote(requestNote, noteID, userID);
                }
                else
                {
                    throw new Exception("Pleaase enter Note ID");
                }                    
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IList<NoteModel> DisplayNotes(string userID)
        {
            try
            {
                if(userID != null)
                {
                    return noteRL.DisplayNotes(userID);
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
