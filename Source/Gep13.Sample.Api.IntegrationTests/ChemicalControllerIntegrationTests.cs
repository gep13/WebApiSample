﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChemicalControllerIntegrationTests.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the ChemicalControllerIntegrationTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Api.IntegrationTests
{
    using System.Net;

    using Microsoft.Owin.Testing;

    using NUnit.Framework;

    public class ChemicalControllerIntegrationTests
    {
        private TestServer testServer;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            this.testServer = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            this.testServer.Dispose();
        }

        [Test]
        public void ResponseShouldReturnOk()
        {
            var response = this.testServer.HttpClient.GetAsync("/api/Chemical").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}