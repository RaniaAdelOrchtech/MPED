using Moq;
using MPMAR.Business;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.XUnitTest
{
    public class NavItemVersionRepositoryTests
    {
        public NavItemVersionRepositoryTests()
        {
            MockNavItemVersionRepository = MockData.CreateMockNavItemVersionRepository();
        }

        public readonly INavItemVersionRepository MockNavItemVersionRepository;

        [Fact]
        public void NavItemVersion_AddNavItemVersion_ReturnsNewAddedNavItemVersion()
        {
            int maxId = MockNavItemVersionRepository.Get().Max(x => x.Id) + 1;
            // Create a new navItem
            NavItemVersion navItemVersion = new NavItemVersion
            {
                ArName = "عنصر قائمة جديد" + " " + maxId,
                EnName = "New Nav Item" + " " + maxId,
                Order = maxId,
                CreationDate = DateTime.Now.AddDays(-1),
                CreatedById = new Guid().ToString(),
                IsActive = true,
                IsDeleted = false
            };

            int navItemVersionCountBeforeAdding = MockNavItemVersionRepository.Get().Count();
            int expectedId = MockNavItemVersionRepository.Get().Max(x => x.Id) + 1;

            // try saving our new navItem
            MockNavItemVersionRepository.Add(navItemVersion);

            // demand a recount
            int navItemVersionCountAfterAdding = MockNavItemVersionRepository.Get().Count();
            Assert.Equal(navItemVersionCountBeforeAdding + 1, navItemVersionCountAfterAdding); // Verify the expected Number post-insert

            // verify that our new navItem has been saved
            NavItemVersion testNavItemVersion = MockNavItemVersionRepository.Get(expectedId);
            Assert.NotNull(testNavItemVersion); // Test if null
            Assert.IsType<NavItemVersion>(testNavItemVersion); // Test type
            Assert.Equal(expectedId, testNavItemVersion.Id); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItemVersion_UpdateNavItemVersion_ReturnsEditedNavItemVersion()
        {
            // Get navItem to update
            NavItemVersion navItemVersion = MockNavItemVersionRepository.Get(1);

            string expectedNavItemVersionEnName = "Edit New Nav Item 1";
            navItemVersion.EnName = expectedNavItemVersionEnName;

            // try updating our new navItem
            MockNavItemVersionRepository.Update(navItemVersion);

            // verify that our navItem has been updated
            NavItemVersion testNavItemVersion = MockNavItemVersionRepository.Get(1);
            Assert.NotNull(testNavItemVersion); // Test if null
            Assert.IsType<NavItemVersion>(testNavItemVersion); // Test type
            Assert.Equal(expectedNavItemVersionEnName, testNavItemVersion.EnName); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItemVersion_DeleteNavItemVersion_ReturnsDeletedNavItemVersion()
        {
            // Get navItem to delete
            NavItemVersion navItemVersionToDelete = MockNavItemVersionRepository.Get(3);

            // try deleting our new navItem
            NavItemVersion expectedNavItemVersion = MockNavItemVersionRepository.Delete(navItemVersionToDelete.Id);

            // verify that our navItem has been deleted
            NavItemVersion testNavItemVersion = MockNavItemVersionRepository.Get(3);
            Assert.Null(testNavItemVersion);
            Assert.IsType<NavItemVersion>(expectedNavItemVersion); // Test type
            Assert.Equal(expectedNavItemVersion.Id, navItemVersionToDelete.Id); // Verify it has the expected navItemId
        }

        [Fact]
        public void NavItemVersion_GetNavItemVersionWithNotExistId_ReturnsNull()
        {
            // Get navItem with not exist Id
            NavItemVersion expectedNavItemVersion = MockNavItemVersionRepository.Get(100);

            // verify that our navItem is null
            Assert.Null(expectedNavItemVersion); // Test if null
        }
    }
}
