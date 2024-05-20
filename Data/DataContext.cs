using CVAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CVAPI.Data {
    public class DataContext:DbContext {

        public DbSet<CV>cvs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<CVModif> cvModifs { get; set; }
        public DbSet<CVExport> cvExports { get; set; }
        public DbSet<CVVersion> cvVersions { get; set; }

        public DataContext(DbContextOptions<DataContext> options):base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //User <=> CV
            modelBuilder.Entity<User>().
            HasMany(user=>user.cvs).
            WithOne(cv=>cv.user).
            HasForeignKey(cv=>cv.userId).
            OnDelete(DeleteBehavior.NoAction);

            //User <=> CVModif
            modelBuilder.Entity<User>().
            HasMany(user=>user.cvModifs).
            WithOne(cvModif=>cvModif.user).
            HasForeignKey(cvModif=>cvModif.editorId).
            OnDelete(DeleteBehavior.NoAction);

            //User <=> CVExport
            modelBuilder.Entity<User>().
            HasMany(user=>user.cvExports).
            WithOne(cvExport=>cvExport.user).
            HasForeignKey(cvExport=>cvExport.exporterId).
            OnDelete(DeleteBehavior.NoAction);

            //CV <=> User
            modelBuilder.Entity<CV>().
            HasOne(cv=>cv.user).
            WithMany(user=>user.cvs).
            HasForeignKey(cv=>cv.userId).
            OnDelete(DeleteBehavior.NoAction);

            //CV <=> CVModif
            modelBuilder.Entity<CV>().
            HasMany(cv=>cv.cvModifs).
            WithOne(cvModif=>cvModif.cv).
            HasForeignKey(cvModif=>cvModif.cvId).
            OnDelete(DeleteBehavior.NoAction);

            //CV <=> CVVersion
            modelBuilder.Entity<CV>().
            HasMany(cv=>cv.versions).
            WithOne(version=>version.cv).
            HasForeignKey(version=>version.cvId).
            OnDelete(DeleteBehavior.NoAction);

            //CVVersion <=> CVExport
            modelBuilder.Entity<CVVersion>().
            HasMany(version=>version.cvExports).
            WithOne(cvExport=>cvExport.version).
            HasForeignKey(cvExport=>cvExport.versionId).
            OnDelete(DeleteBehavior.NoAction);

            //CVVersion <=> CVExport
            modelBuilder.Entity<CVVersion>().
            HasMany(version=>version.cvExports).
            WithOne(cvexport=>cvexport.version).
            HasForeignKey(cvexport=>cvexport.versionId).
            OnDelete(DeleteBehavior.NoAction);

           /* //CVVersion <=> CV (potentially useless relation)
            modelBuilder.Entity<CVVersion>().
            HasOne(version=>version.cv).
            WithMany(cv=>cv.versions).
            HasForeignKey(version=>version.cvId).
            OnDelete(DeleteBehavior.NoAction); */

            /* //CVModif <=> CV (potentially useless relation)
            modelBuilder.Entity<CVModif>().
            HasOne(cvModif=>cvModif.cv).
            WithMany(cv=>cv.cvModifs).
            HasForeignKey(cvModif=>cvModif.cvId).
            OnDelete(DeleteBehavior.NoAction); */

            /* //CVModif <=> User (potentially useless relation)
            modelBuilder.Entity<CVModif>().
            HasOne(cvModif=>cvModif.user).
            WithMany(user=>user.cvModifs).
            HasForeignKey(cvModif=>cvModif.editorId).
            OnDelete(DeleteBehavior.NoAction); */

            /* //CVExport <=> CV (potentially unneeded relation)
            modelBuilder.Entity<CVExport>().
            HasOne(cvexport=>cvexport.cv).
            WithMany(cv=>cv.cvExports). 
            HasForeignKey(cvExport=>cvExport.cvId).
            OnDelete(DeleteBehavior.NoAction); */

            /* //CVExport <=> User
            modelBuilder.Entity<CVExport>().
            HasOne(cvexport=>cvexport.user).
            WithMany(user=>user.cvExports).
            HasForeignKey(cvexport=>cvexport.exporterId).
            OnDelete(DeleteBehavior.NoAction); */

            /* //CVExport <=> Version
            modelBuilder.Entity<CVExport>().
            HasOne(cvexport=>cvexport.version).
            WithMany(version=>version.cvExports).
            HasForeignKey(cvexport=>cvexport.versionId).
            OnDelete(DeleteBehavior.NoAction); */
            //base.OnModelCreating(modelBuilder);
        }
    } 
}
    

