// ******************************************************************************
//  <copyright file="NotesTestCases.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NotesTestCases.cs
//  
//     Purpose:  Creating Test cases for Notes
//     @author  Pranali Patil
//     @version 1.0
//     @since   27-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace XUnitTestCases
{
    using System;
    using FundooApp.Controllers;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooCommonLayer.Model.Request;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

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
            var repository = new Mock<INoteRL>().Object;

            var data = new NoteRequest()
            {
              Title="jsdgf",
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
