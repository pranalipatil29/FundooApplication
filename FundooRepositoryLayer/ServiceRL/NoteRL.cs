using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
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

        public async Task<bool> CreateNote(RequestNote requestNote, string userID)
        {
            try
            {
                if (requestNote != null)
                {
                    var data = new NoteModel()
                    {
                        UserID = userID,
                        Title = requestNote.Title,
                        Description = requestNote.Description,
                        Reminder = requestNote.Reminder,
                        Collaborator = requestNote.Collaborator,
                        Color = requestNote.Color,
                        Image = requestNote.Image,
                        IsArchive = requestNote.IsArchive,
                        IsPin = requestNote.IsPin,
                        IsTrash = requestNote.IsTrash,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
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

        public async Task<NoteModel> UpdateNote(RequestNote noteRequest, int noteID)
        {
            try
            {
                var note = authenticationContext.Note.Where(s => s.NoteID == noteID).FirstOrDefault();

                if (note != null)
                {
                    note.ModifiedDate = DateTime.Now;

                    if (noteRequest.Title != null && noteRequest.Title != "")
                    {
                        note.Title = noteRequest.Title;
                    }

                    if (noteRequest.Description != null && noteRequest.Description != "")
                    {
                        note.Description = noteRequest.Description;
                    }
                    if (noteRequest.Collaborator != null && noteRequest.Collaborator != "")
                    {
                        note.Collaborator = noteRequest.Collaborator;
                    }
                    if (noteRequest.Color != null && noteRequest.Color != "")
                    {
                        note.Color = noteRequest.Color;
                    }
                    if (noteRequest.Image != null && noteRequest.Image != "")
                    {
                        note.Image = noteRequest.Image;
                    }
                    if (! noteRequest.IsArchive.Equals(note.IsArchive))
                    {
                        note.IsArchive = noteRequest.IsArchive;
                    }
                    if (! noteRequest.IsPin.Equals(note.IsPin))
                    {
                        note.IsPin = noteRequest.IsPin;
                    }
                    if (! noteRequest.IsTrash.Equals(note.IsTrash))
                    {
                        note.IsTrash = noteRequest.IsTrash;
                    }
                    if (noteRequest.Reminder >= DateTime.Now)
                    {
                        note.Reminder = noteRequest.Reminder;
                    }
                    
                    authenticationContext.Note.Update(note);
                    await authenticationContext.SaveChangesAsync();

                    return note;
                }
                else
                {
                    return null;
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
                var data = authenticationContext.Note.Where(s => s.UserID == userID);

                return data.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
