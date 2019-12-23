using FundooCommonLayer.Model;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.ServiceRL
{
    public class NoteRL : INoteRL
    {
        private AuthenticationContext authenticationContext;

        public NoteRL(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        public async Task<bool> CreateNote(NoteModel noteModel, string userID)
        {
            try
            {
                if (noteModel != null)
                {
                    var data = new NoteModel()
                    {
                        UserID = userID,
                        Title = noteModel.Title,
                        Description = noteModel.Description,
                        Reminder = noteModel.Reminder,
                        Collaborator = noteModel.Collaborator,
                        Color = noteModel.Color,
                        Image = noteModel.Image,
                        IsArchive = noteModel.IsArchive,
                        IsPin = noteModel.IsPin,
                        IsTrash = noteModel.IsTrash,
                        CreatedDate = noteModel.CreatedDate,
                        ModifiedDate = noteModel.ModifiedDate
                    };

                    authenticationContext.Note.Add(data);
                    await authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
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
                var user = authenticationContext.Note.Where(s => s.NoteID == noteID).FirstOrDefault();

                if (user != null)
                {
                    authenticationContext.Note.Remove(user);
                    await authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
