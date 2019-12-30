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
    using FundooApp.Controllers;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooCommonLayer.Model;
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
        public async Task TestNoteCreationForBadRequest()
        {
            //  var repository = new Mock<INoteRL>().Object;

            var data = new NoteRequest()
            {
                Color = "red",
                Description = "note1",
                Image = "jhd",
                IsArchive = false,
                IsPin = true,
                IsTrash = false,
                Reminder = DateTime.Now

            };

            var result = await noteController.CreateNote(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestNoteCreationForIsEmpty()
        {
            var data = new NoteRequest()
            {
                Color = "red",
                Description = "note1",
                Image = "jhd",
                IsArchive = false,
                IsPin = true,
                IsTrash = false,
                Reminder = DateTime.Now
            };

            var result = await noteController.CreateNote(data);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
