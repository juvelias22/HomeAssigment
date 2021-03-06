// <copyright file="CategoriesControllerTest.cs">Copyright ©  2020</copyright>
using System;
using System.Web.Mvc;
using HomeAssigment.Controllers;
using HomeAssigment.Models;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeAssigment.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for CategoriesController</summary>
    [PexClass(typeof(CategoriesController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CategoriesControllerTest
    {
        /// <summary>Test stub for Create(Categories)</summary>
        [PexMethod]
        public ActionResult CreateTest(
            [PexAssumeUnderTest]CategoriesController target,
            Categories categories
        )
        {
            ActionResult result = target.Create(categories);
            return result;
            // TODO: add assertions to method CategoriesControllerTest.CreateTest(CategoriesController, Categories)
        }
    }
}
