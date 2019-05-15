using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void ConneсtAndGetDeviceCode()
        {
            var response = KinopubApi.Auth.GetDeviceCodeAsync("xbmc", "cgg3gtifu46urtfp2zp1nqtba0k2ezxh");
            var statusCode = 200;

            Console.WriteLine("Device code: " + response.Data.code);
            Console.WriteLine("User code: " + response.Data.user_code);

            Assert.AreEqual(statusCode, (int)response.StatusCode);
        }
    }
}
