using FundooApp.Controllers;
using FundooBusinessLayer.InterfaceBL;
using FundooBusinessLayer.ServicesBL;
using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using FundooRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestCases
{
    public class NotesTestCases
    {
        NoteController noteController;
        INoteBL noteBL;
        INoteRL noteRL;

        public NotesTestCases()
        {
            noteBL = new NoteBL(noteRL);
            noteController = new NoteController(noteBL);
        }

        [Fact]
        public void TestNoteCreation()
        {
            var repository = new Mock<INoteRL>();

            var data = new NoteRequest()
            {
                Title = "sdjgf",
                Collaborator = "gsdj",
                Color = "red",
                Description = "note1",
                Image = "jhd",
                IsArchive = false,
                IsPin = true,
                IsTrash = false,
                Reminder = DateTime.Now
            };

            //  var createdResponse = noteController.CreateNote(data);
            // Assert.IsType<NotFoundResult>(createdResponse);

            noteController.ModelState.AddModelError("Title", "Required");
            var badResponse = noteController.CreateNote(data);

            Assert.IsType<BadRequestObjectResult>(badResponse);

            //  Assert.NotEmpty(data);
        }
    }
}
