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
            var statusCode = 200;

            Debug.WriteLine("Device code: " + response.Result.Data.code);
            Debug.WriteLine("User code: " + response.Result.Data.user_code);

            Assert.AreEqual(statusCode, (int)response.Result.StatusCode);
        }
    }
}
