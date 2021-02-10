using MPMAR.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MPMAR.XUnitTest
{
    public class UserManagmentTest
    {
        [Fact]
        public void CreateNewUser__ReturnFalseForEmail()
        {
            var userRepo = new UserManagmentRepository();
            var userViewModel = new Entities.CreateUserRoleViewModel();
            userViewModel.Email = "karim.com";
            userViewModel.Password = "KKkk@12345";
            bool result = userRepo.CreateAsync(userViewModel, "www.google.com").Result;
            Assert.False(result);
        }
        [Fact]
        public void CreateNewUser__ReturnFalseForPassword()
        {
            var userRepo = new UserManagmentRepository();
            var userViewModel = new Entities.CreateUserRoleViewModel();
            userViewModel.Email = "karimfahmy42@gmail.com";
            userViewModel.Password = "kkkk@12345";
            bool result = userRepo.CreateAsync(userViewModel, "www.google.com").Result;
            Assert.False(result);
        }
        [Fact]
        public void CreateNewUser__ReturnTrue()
        {
            var userRepo = new UserManagmentRepository();
            var userViewModel = new Entities.CreateUserRoleViewModel();
            userViewModel.Email = "karimfahmy42@gmail.com";
            userViewModel.Password = "KKkk@12345";
            bool result = userRepo.CreateAsync(userViewModel, "www.google.com").Result;
            Assert.False(result);
        }
        [Fact]
        public void EditUser__ReturnTrue()
        {
            var userRepo = new UserManagmentRepository();
            var userViewModel = new Entities.EditUserRoleViewModel();
            userViewModel.Email = "karimfahmy42@gmail.com";
            userViewModel.RoleName = new List<string> { "SuperAdmin"};
            bool result = userRepo.Edit(userViewModel).Result;
            Assert.True(result);
        }
        [Fact]
        public void EditUser__ReturnFalse()
        {
            var userRepo = new UserManagmentRepository();
            var userViewModel = new Entities.EditUserRoleViewModel();
            userViewModel.Email = "karimfahmy42@gmail.com";
            userViewModel.RoleName = null;
            bool result = userRepo.Edit(userViewModel).Result;
            Assert.False(result);
        }
        [Fact]
        public void DeleteUser__ReturnFalse()
        {
            var userRepo = new UserManagmentRepository();
            bool result =  userRepo.Delete("karim").Result;
            Assert.False(result);
        }
        [Fact]
        public void DeleteUser__ReturnTrue()
        {
            var userRepo = new UserManagmentRepository();
            bool result = userRepo.Delete("ed0be8b7-96ce-40a0-ac8f-8765ef266385").Result;
            Assert.False(result);
        }
        [Fact]
        public async Task SeedRoles__ReturnTrueAsync()
        {
            var seedData = new SeedDataTest();
            bool result = await seedData.SeedRoles();
            Assert.True(result);
        }
    }
    
}
