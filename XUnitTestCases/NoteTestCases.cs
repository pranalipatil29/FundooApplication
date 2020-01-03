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
       // private readonly INoteRL noteRL;


        public NotesTestCases()
        {
            var repository = new Mock<INoteRL>();

            this.noteBL = new NoteBL(repository.Object);
            noteController = new NoteController(this.noteBL);
        }

        [Fact]
        public async Task TestNoteCreationForBadRequest()
        {
            //  var repository = new Mock<INoteRL>().Object;

            var data = new NoteRequest()
            {

                Collaborator="ujklj",
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

        //[Fact]
        //public async Task TestNoteCreationForOkRequest()
        //{
        //    var data = new NoteRequest()
        //    {
        //        Title = "kfh",
        //        Collaborator = "kfg",
        //        Color = "red",
        //        Description = "note1",
        //        Image = "jhd",
        //        IsArchive = false,
        //        IsPin = true,
        //        IsTrash = false,
        //        Reminder = DateTime.Now
        //    };

        //    var createdResponse = await noteController.CreateNote(data) as CreatedAtActionResult;
        //    Assert.IsType<CreatedAtActionResult>(createdResponse);
        //}

        //[Fact]
        //public async Task TestDeleteNoteForSuccess()
        //{
        //    var repository = new Mock<INoteRL>().Object;
        //    var business = new NoteBL(repository);

        //    var result = await noteController.DeleteNote(2);
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public async Task DeleteNoteForSuccess()
        //{
        //    bool result = await this.noteBL.DeleteNote(2, "e6ac5ba3-a6d4-400a-bf42-10d7e410ab7a");
        //    Assert.True(result);
        //}

        [Fact]
        public async Task TestDeleteNote()
        {

            var result = await noteController.DeleteNote(1005);
            Assert.IsType<OkObjectResult>(result);
        }       
    }
}
