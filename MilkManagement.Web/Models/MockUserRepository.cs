using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MilkManagement.Web.Models
{
    public class MockUserRepository : IUserRepository
    {
        private readonly IList<User> _userList;

        public MockUserRepository()
        {
            _userList = new List<User>
            {
                new User {Id=1,Name="Kumar", DoorNoAndStreet="9/4A,Kalaignar Nagar West",City="Millgate,Melur",District="Madurai",State="TN",PrimaryEmailAddress="",PrimaryMobileNumber=""},
                new User {Id=2,Name="Kumar1", DoorNoAndStreet="9/5A,Kalaignar Nagar West",City="Millgate,Melur",District="Madurai",State="TN",PrimaryEmailAddress="",PrimaryMobileNumber=""},
                new User {Id=3,Name="Kumar2", DoorNoAndStreet="9/6A,Kalaignar Nagar West",City="Millgate,Melur",District="Madurai",State="TN",PrimaryEmailAddress="",PrimaryMobileNumber=""},
                new User {Id=4,Name="Kumar3", DoorNoAndStreet="9/7A,Kalaignar Nagar West",City="Millgate,Melur",District="Madurai",State="TN",PrimaryEmailAddress="",PrimaryMobileNumber=""}
            };
        }
        /// <summary>
        /// Gets one user by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public async Task<User> GetUser(int id)
        {
            var data = _userList.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(data);
        }
    }
}
