﻿#if WINDOWS_PHONE_8||NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

#else

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endif
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace MVVMSidekick.Test
{
    /// <summary>
    /// Summary description for StorageTest
    /// </summary>
    [TestClass]
    public class StorageTest
    {
        public StorageTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        string fileAName = "aaa";
        string fileBName = "bbb";

        [TestMethod]
        public async Task TestStorage()
        {

            var hub = GetHub();

            await hub.SaveAsync(fileAName, "content");
            var isThere = await FileExists(fileAName);
            Assert.IsTrue(isThere);
            var newv = await hub.LoadAsync(fileAName, false);
            Assert.AreEqual(newv, "content");
            newv = await hub.LoadAsync(fileAName, true);
            Assert.AreEqual(newv, "content");

            await hub.SaveAsync(fileAName, "contt");
            newv = await hub.LoadAsync(fileAName, true);
            Assert.AreNotEqual(newv, "content");
            Assert.AreEqual(newv, "contt");
        }
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
#if WPF
        //[TestMethod]
        //public async Task TestDirectoryStorage()
        //{

        //    var hub = MVVMSidekick.Storages.StorageHub<string, string>.CreateJsonDatacontractFileStorageHub(x => x);

        //    await hub.SaveAsync(fileAName, "content");
        //    var p = Path.Combine(Environment.CurrentDirectory, fileAName);
        //    Assert.IsTrue (File.Exists (p));

        //    var newv=await hub.LoadAsync (fileAName ,false  );
        //    Assert .AreEqual  (newv, "content");
        //    newv = await hub.LoadAsync(fileAName, true);
        //    Assert.AreEqual(newv, "content");

        //}


        

        private static Storages.StorageHub<string, string> GetHub()
        {
            var hub =MVVMSidekick.Storages.StorageHub<string, string>.CreateJsonDatacontractFileStorageHub(x => x);
            return hub;
        }

        private async Task<bool> FileExists(string fileAName)
        {
              var p = Path.Combine(Environment.CurrentDirectory, fileAName);
           return(File.Exists (p));
        }

#elif WINDOWS_PHONE
        //[TestMethod]
        //public async Task TestIsolatedStorage()
        //{

        //    var hub = MVVMSidekick.Storages.StorageHub<string, string>.CreateJsonDatacontractIsolatedStorageHub(x => x);

        //    await hub.SaveAsync(fileAName, "content");
          
        //    var newv = await hub.LoadAsync(fileAName, false);
        //    Assert.AreEqual(newv, "content");
        //    newv = await hub.LoadAsync(fileAName, true);
        //    Assert.AreEqual(newv, "content");

        //}

           private static Storages.StorageHub<string, string> GetHub()
        {
            var hub = MVVMSidekick.Storages.StorageHub<string, string>.CreateJsonDatacontractIsolatedStorageHub(x => x);

            return hub;
        }

        private async Task<bool> FileExists(string fileAName)
        {
        var iso = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
          return  iso.FileExists(fileAName);
        }

#elif NETFX_CORE


        private static Storages.StorageHub<string, string> GetHub()
        {
            var hub = MVVMSidekick.Storages.StorageHub<string, string>.CreateJsonDatacontractFileStorageHub(x => x);
            return hub;
        }

        private async Task<bool> FileExists(string fileAName)
        {
            var fd = Windows.Storage.ApplicationData.Current.LocalFolder;
            var fl = await fd.GetFileAsync(fileAName);
            return fl != null;
        }

#endif
    }
}
