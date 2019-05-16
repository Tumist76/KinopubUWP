
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Kinopub.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kinopub.Testing.UI
{
    [TestClass]
    public class AuthTest : INotifyPropertyChanged
    {
        AuthorizationModel model = new AuthorizationModel();

        public AuthTest()
        {
            model.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        }

        [TestMethod]
        public async void GetDeviceCode()
        {
            
            await model.GetDeviceCode();
            Assert.IsNotNull(model.DeviceCodeRequest.Result.Data);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
