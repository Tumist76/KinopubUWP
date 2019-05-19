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

            Debug.WriteLine("Device code: " + response.Result.Data.code);
            Debug.WriteLine("User code: " + response.Result.Data.user_code);

            Assert.AreEqual(expectedLength, response.Result.Data.user_code.Length);
        }
    }
}
