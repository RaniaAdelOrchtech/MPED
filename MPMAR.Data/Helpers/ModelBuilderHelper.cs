using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.Helpers
{
    public static class ModelBuilderHelper
    {
        public static void AddNavigationProperties(this ModelBuilder modelBuilder)
        {
            modelBuilder.AddApplicationUserNavigationProperties();
            modelBuilder.AddNavItemNavigationProperties();
            modelBuilder.AddPageRouteNavigationProperties();
        }

        public static void AddApplicationUserNavigationProperties(this ModelBuilder modelBuilder)
        {
            #region Relation with NavItem
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedNavItems)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ModifiedNavItems)
                .WithOne(p => p.ModifiedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedNavItems)
                .WithOne(p => p.ApprovedBy);
            #endregion

            #region Relation with NavItemVersion
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedNavItemVersions)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedNavItemVersions)
                .WithOne(p => p.ApprovedBy);
            #endregion

            #region Relation with PageRoute
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedPageRoutes)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ModifiedPageRoutes)
                .WithOne(p => p.ModifiedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedPageRoutes)
                .WithOne(p => p.ApprovedBy);
            #endregion

            #region Relation with PageRouteVersion
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedPageRouteVersions)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedPageRouteVersions)
                .WithOne(p => p.ApprovedBy);
            #endregion

            #region Relation with PageSection
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedPageSections)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ModifiedPageSections)
                .WithOne(p => p.ModifiedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedPageSections)
                .WithOne(p => p.ApprovedBy);
            #endregion

            #region Relation with PageSectionVersion
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CreatedPageSectionVersions)
                .WithOne(p => p.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ApprovedPageSectionVersions)
                .WithOne(p => p.ApprovedBy);
            #endregion
        }

        public static void AddNavItemNavigationProperties(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NavItem>()
                .HasMany(e => e.NavItemList)
                .WithOne(e => e.ParentNavItem)
                .HasForeignKey(e => e.ParentNavItemId);

            //modelBuilder.Entity<NavItem>()
            //    .HasOne(x => x.NavItemVersion)
            //    .WithOne(p => p.NavItem)
            //    .OnDelete(DeleteBehavior.NoAction);
        }

        public static void AddPageRouteNavigationProperties(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageRoute>()
                .HasOne(x => x.NavItem)
                .WithMany(p => p.PageRoutes)
                .HasForeignKey(e => e.NavItemId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        public static void AddPageSectionNavigationProperties(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageSection>()
                .HasOne(x => x.PageRoute)
                .WithMany(p => p.PageSections)
                .HasForeignKey(e => e.PageRouteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PageSection>()
                .HasOne(x => x.PageSectionVersion)
                .WithOne(p => p.PageSection)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
