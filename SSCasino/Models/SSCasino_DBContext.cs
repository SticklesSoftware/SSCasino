//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// SSCasino_DBContext.cs
//      This class describes database entity objects and their relationships to the entity framework. 
//========================================================================================================================

using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSCasino.Models
{
    public class SSCasino_DBContext : DbContext
    {
        #region Entity Object Definitions
        //================================================================================================================
        //================================================================================================================

        // Casino
        public DbSet<CasinoInfo> CasinoInfo { get; set; }

        // Cards
        public DbSet<CardPack> CardPacks { get; set; }
        public DbSet<PlayingCard> PlayingCardsView { get; set; }

        // APIs
        public DbSet<YouTubeSearchResult> YouTubeSearchResults { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Entity Object Definitions

        #region Entity Object Descriptions
        //================================================================================================================
        //================================================================================================================

        private void DefineEntityKeys(DbModelBuilder modelBuilder)
        //================================================================================================================
        // Define entity object keys
        //
        // Parameters
        //      modelBuilder: Reference to the database model builder class
        //================================================================================================================
        {
            // Casino
            modelBuilder.Entity<CasinoInfo>().HasKey(t => t.CasinoId);

            // Cards
            modelBuilder.Entity<CardPack>().HasKey(t => t.CardPackId);
            modelBuilder.Entity<PlayingCard>().HasKey(t => new { t.CardPackId, t.CardNo });

            // APIs
            modelBuilder.Entity<YouTubeSearchResult>().HasKey(t => new { t.SessionKey, t.ResultNo });
        }

        private void DefineEntityMaps(DbModelBuilder modelBuilder)
        //================================================================================================================
        // Define mapping between an entity object and its database counterpart
        //
        // Parameters
        //      modelBuilder: Reference to the database model builder class
        //
        // Developer Notes
        //      The entity framework by default looks for a table with a name that has the same or pluralized version of 
        //      the entity name. Tables that do not follow this format or entities that are mapped to views must be mapped 
        //      explicitly. For clarity, all tables define the explicit mapping.
        //================================================================================================================
        {
            // Casino
            modelBuilder.Entity<CasinoInfo>().ToTable("CasinoInfo");

            // Cards
            modelBuilder.Entity<CardPack>().ToTable("CardPacks");
            modelBuilder.Entity<PlayingCard>().ToTable("PlayingCards_View");

            // APIs
            modelBuilder.Entity<YouTubeSearchResult>().ToTable("YouTubeSearchResults");
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Entity Object Descriptions

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //================================================================================================================
        // Override the OnModelCreating method to define our database entities
        //
        // Parameters
        //      modelBuilder: Reference to the database model builder class
        //================================================================================================================
        {
            // Define the database entities
            DefineEntityKeys(modelBuilder);
            DefineEntityMaps(modelBuilder);

            // Developer Note
            //
            // Since we are using the "database first" method in the entity framework, we do not need to have an initializer
            // for the database. Initializers are only used for "code first" methodologies and are used to build the database
            // if it does not already exist.
            Database.SetInitializer<SSCasino_DBContext>(null);

            // Invoke the base class method
            base.OnModelCreating(modelBuilder);
        }
    }
}