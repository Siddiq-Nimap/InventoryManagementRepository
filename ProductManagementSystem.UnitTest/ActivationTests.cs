using CrudOperations.Business_Layer.Activation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ProductManagementSystem.UnitTest
{
    [TestClass]
    public class ActivationTests
    {
        [TestMethod]
        public async Task ActivateAsync_IsGreaterThanZero_ReturnsTrue()
        {
            //Arange
            var activation = new Activation();

            //Act
         var data =  await  activation.ActivateAsync(2);

            //Assert

            Assert.IsTrue(data);
            
        }

        [TestMethod]
        public async Task DeActivateAsync_IsGreaterThanZero_ReturnsTrue()
        {
            //Arange
            var activation = new Activation();

            //Act
            var data = await activation.DeActivateAsync(2);

            //Assert

            Assert.IsTrue(data);

        }
    }
}
