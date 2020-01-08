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
    using System.Threading.Tasks;
    using System.Web.Http.Results;
    using FundooApp.Controllers;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Request.Note;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using FundooRepositoryLayer.ServiceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class NotesTestCases
    {
        NoteController noteController;
        INoteBL noteBL;

        public NotesTestCases()
        {
            var repository = new Mock<INoteRL>();
            this.noteBL = new NoteBL(repository.Object);
            noteController = new NoteController(this.noteBL);
        }

        /// <summary>
        /// Tests the note creation for bad request.
        /// </summary>
        [Fact]
        public async Task TestNoteCreationForBadRequest()
        {
            var data = new NoteRequest()
            {
                Collaborator = "ujklj",
                Color = "red",
                Description = "note1",
                Image = "jhd",
                IsArchive = false,
                IsPin = true,
                IsTrash = false,
                Reminder = DateTime.Now
            };

            noteController.ModelState.AddModelError("Title", "Required");
            var result = await noteController.CreateNote(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the note creation for ok request.
        /// </summary>
        [Fact]
        public async Task TestNoteCreationForOkRequest()
        {
            var data = new NoteRequest()
            {
                Title = "kfh",
                Collaborator = "kfg",
                Color = "red",
                Description = "note1",
                Image = "jhd",
                IsArchive = false,
                IsPin = true,
                IsTrash = false,
                Reminder = DateTime.Now
            };

            var createdResponse = await noteController.CreateNote(data) as CreatedAtActionResult;
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        /// <summary>
        /// Tests the delete note for success .
        /// </summary>
        [Fact]
        public async Task TestDeleteNoteForSuccess()
        {
           
            var result = await noteController.DeleteNote(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete note for bad object result.
        /// </summary>
        [Fact]
        public async Task TestDeleteNoteForBadObjectResult()
        {
           
            var result = await noteController.DeleteNote(9);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the get note for success.
        /// </summary>
        [Fact]
        public async Task TestGetNoteForSuccess()
        {
            var result = await noteController.GetNote(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the get note for bad object result.
        /// </summary>
        [Fact]
        public async Task TestGetNoteForBadObjectResult()
        {
            var result = await noteController.GetNote(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the archive for success.
        /// </summary>
        [Fact]
        public async Task TestArchiveForSuccess()
        {
            var result = await noteController.Archive(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the archive for bad result.
        /// </summary>
        [Fact]
        public async Task TestArchiveForBadResult()
        {
            var result = await noteController.Archive(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the archive for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestArchiveForInvalidNoteID()
        {
            var result = await noteController.Archive(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the pin for bad result.
        /// </summary>
        [Fact]
        public async Task TestPinForBadResult()
        {
            var result = await noteController.IsPin(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the pin for success.
        /// </summary>
        [Fact]
        public async Task TestPinForSuccess()
        {
            var result = await noteController.Archive(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the pin for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestPinForInvalidNoteID()
        {
            var result = await noteController.IsPin(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the move to trash for bad result.
        /// </summary>
        [Fact]
        public async Task TestMoveToTrashForBadResult()
        {
            var result = await noteController.MoveToTrash(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the move to trash for success.
        /// </summary>
        [Fact]
        public async Task TestMoveToTrashForSuccess()
        {
            var result = await noteController.MoveToTrash(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the move to trash for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestMoveToTrashForInvalidNoteID()
        {
            var result = await noteController.MoveToTrash(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the restore note for bad result.
        /// </summary>
        [Fact]
        public async Task TestRestoreNoteForBadResult()
        {
            var result = await noteController.RestoreNote(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the restore note for success.
        /// </summary>
        [Fact]
        public async Task TestRestoreNoteForSuccess()
        {
            var result = await noteController.RestoreNote(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the restore note for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestRestoreNoteForInvalidNoteID()
        {
            var result = await noteController.RestoreNote(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete reminder for bad result.
        /// </summary>
        [Fact]
        public async Task TestDeleteReminderForBadResult()
        {
            var result = await noteController.Reminder(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete reminder for success.
        /// </summary>
        [Fact]
        public async Task TestDeleteReminderForSuccess()
        {
            var result = await noteController.Reminder(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete reminder for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestDeleteReminderForInvalidNoteID()
        {
            var result = await noteController.Reminder(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete image for bad result.
        /// </summary>
        [Fact]
        public async Task TestDeleteImageForBadResult()
        {
            var result = await noteController.DeleteImage(0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the deleteimage for success.
        /// </summary>
        [Fact]
        public async Task TestDeleteimageForSuccess()
        {
            var result = await noteController.DeleteImage(2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete image for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestDeleteImageForInvalidNoteID()
        {
            var result = await noteController.DeleteImage(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the add label on note for bad result.
        /// </summary>
        [Fact]
        public async Task TestAddLabelOnNoteForBadResult()
        {
            var result = await noteController.AddLabelOnNote(0,0);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the add label on note for success.
        /// </summary>
        [Fact]
        public async Task TestAddLabelOnNoteForSuccess()
        {
            var result = await noteController.AddLabelOnNote(2,2);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the add label on note for invalid note identifier.
        /// </summary>
        [Fact]
        public async Task TestAddLabelOnNoteForInvalidNoteID()
        {
            var result = await noteController.AddLabelOnNote(-1,2);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the add label on note for invalid label identifier.
        /// </summary>
        [Fact]
        public async Task TestAddLabelOnNoteForInvalidLabelID()
        {
            var result = await noteController.AddLabelOnNote(2,-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the change color for success.
        /// </summary>
        [Fact]
        public async Task TestChangeColorForSuccess()
        {
            ChangeColorRequest color = new ChangeColorRequest()
            {
                Color = "#F00"
            };

            var result = await noteController.ChangeColor(2, color);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the change color for bad request.
        /// </summary>
        [Fact]
        public async Task TestChangeColorForBadRequest()
        {
            ChangeColorRequest color = new ChangeColorRequest()
            {
                Color = "gsfdhvh"
            };

            var result = await noteController.ChangeColor(2, color);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the change color for null request.
        /// </summary>
        [Fact]
        public async Task TestChangeColorForNullRequest()
        {
            ChangeColorRequest color = new ChangeColorRequest()
            {

            };

            var result = await this.noteBL.ChangeColor(2, color.Color, "8e9a3160-f95e-4424-91ca-3164d34a29ac");
            Assert.Null(result);
        }

        /// <summary>
        /// Tests the set reminder for success.
        /// </summary>
        [Fact]
        public async Task TestSetReminderForSuccess()
        {
            ReminderRequest reminder = new ReminderRequest()
            {
                Reminder = DateTime.Now.AddHours(1)
            };

            var result = await this.noteController.SetReminder(2, reminder);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the set reminder for bad request.
        /// </summary>
        [Fact]
        public async Task TestSetReminderForBadRequest()
        {
            ReminderRequest reminder = new ReminderRequest()
            {
                Reminder = DateTime.Now
            };

            var result = await this.noteController.SetReminder(2, reminder);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
