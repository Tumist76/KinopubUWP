﻿using System;
using System.Diagnostics;
using Kinopub.Api;
using Kinopub.Testing.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VideoGetTest
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void ConneсtAndGetDeviceCode()
        {
            var token = AuthToken.GetAuthToken();
            if (token == null) return;
            var DuckTales = new GetContentService(token).GetItem(21614);

            var expectedTitle = "Утиные истории / DuckTales";

            Debug.WriteLine("Plot: " + DuckTales.Result.Item.Plot);

            Assert.AreEqual(expectedTitle, DuckTales.Result.Item.Title);
        }
    }
}
