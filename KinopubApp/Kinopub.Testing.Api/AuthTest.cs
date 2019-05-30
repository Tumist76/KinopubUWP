using System;
using System.Diagnostics;
using Kinopub.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void ConneсtAndGetDeviceCode()
        {
            var response = Auth.GetDeviceCodeAsync("xbmc", "cgg3gtifu46urtfp2zp1nqtba0k2ezxh");

            var expectedLength = 5;

            Debug.WriteLine("Device Code: " + response.Result.Data.Code);
            Debug.WriteLine("User Code: " + response.Result.Data.UserCode);

            Assert.AreEqual(expectedLength, response.Result.Data.UserCode.Length);
        }
    }
}
