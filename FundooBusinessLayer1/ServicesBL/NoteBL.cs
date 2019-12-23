using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
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

        public async Task<bool> CreateNote(NoteModel noteModel,string userID)
        {
            try
            {
                if (noteModel != null)
                {
                    return await noteRL.CreateNote(noteModel,userID);
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

        public async Task<bool> DeleteNote(int noteID)
        {
            try
            {
                if (noteID != null)
                {
                    return await noteRL.DeleteNote(noteID);
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
    }
}
