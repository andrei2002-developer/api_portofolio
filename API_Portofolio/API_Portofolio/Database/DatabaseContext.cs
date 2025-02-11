using API_Portofolio.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace API_Portofolio.Database
{
    public class DatabaseContext : IdentityDbContext<Account>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<IdentityUserToken<string>> AspNetUserTokens { get; set; }
        public DbSet<TypeOfApplication> TypeOfApplications { get; set; }
        public DbSet<SuportedPlatform> SuportedPlatforms { get; set; }
        public DbSet<HostingPreference> HostingPreferences { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StepOneOrder> StepOneOrders { get; set; }
        public DbSet<StepTwoOrder> StepTwoOrders { get; set; }
        public DbSet<StepThreeOrder> StepThreeOrders { get; set; }
        public DbSet<StepFourOrder> StepFourOrders { get; set; }
        public DbSet<ChatType> ChatTypes { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Orders

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Account)
                .WithMany(acc => acc.Orders)
                .HasForeignKey(a => a.Order_IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(p => p.Order_Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Order>()
               .HasOne(p => p.StepOneOrder)
               .WithOne(pd => pd.Order)
               .HasForeignKey<StepOneOrder>(pd => pd.Order_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StepOneOrder>()
                .Property(p => p.StepOneOrder_Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Order>()
              .HasOne(p => p.StepTwoOrder)
              .WithOne(pd => pd.Order)
              .HasForeignKey<StepTwoOrder>(pd => pd.Order_Id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StepTwoOrder>()
               .Property(p => p.StepTwoOrder_Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Order>()
              .HasOne(p => p.StepThreeOrder)
              .WithOne(pd => pd.Order)
              .HasForeignKey<StepThreeOrder>(pd => pd.Order_Id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StepThreeOrder>()
               .Property(p => p.StepThreeOrder_Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Order>()
              .HasOne(p => p.StepFourOrder)
              .WithOne(pd => pd.Order)
              .HasForeignKey<StepFourOrder>(pd => pd.Order_Id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StepFourOrder>()
               .Property(p => p.StepFourOrder_Id)
               .HasDefaultValueSql("gen_random_uuid()");

            // TypeOfApplications

            modelBuilder.Entity<StepOneOrder>()
               .HasOne(a => a.TypeOfApplication)
               .WithMany(acc => acc.StepOneOrders)
               .HasForeignKey(a => a.TypeOfApplication_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TypeOfApplication>()
                .Property(p => p.TypeOfApplication_ReferenceCode)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<TypeOfApplication>()
                .Property(p => p.TypeOfApplication_Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<TypeOfApplication>()
                .HasIndex(p => p.TypeOfApplication_ReferenceCode)
                .IsUnique();

            modelBuilder.Entity<TypeOfApplication>().HasData(
                new TypeOfApplication
                {
                    TypeOfApplication_Id = new Guid("95937444-283b-4b54-b391-53f965978806"),
                    TypeOfApplication_ReferenceCode = new Guid("a6f02127-1525-44ff-a400-7dc52694b953"),
                    TypeOfApplication_Name = "Web Application"
                },
                new TypeOfApplication
                {
                    TypeOfApplication_Id = new Guid("9a2b3f8c-8ed8-44f1-b9a6-e85262661b21"),
                    TypeOfApplication_ReferenceCode = new Guid("c8f4b8cc-8b78-4e8d-9cb4-e0d3c4e28f4a"),
                    TypeOfApplication_Name = "Mobile Application"
                },
                new TypeOfApplication
                {
                    TypeOfApplication_Id = new Guid("283eac3a-2c9a-438d-88da-b9f95bf1b672"),
                    TypeOfApplication_ReferenceCode = new Guid("a3291dee-630d-4fd3-ba82-8645cd3d0b27"),
                    TypeOfApplication_Name = "Desktop Application"
                },
                new TypeOfApplication
                {
                    TypeOfApplication_Id = new Guid("382539cf-215a-4763-a329-8bed44ca9ef8"),
                    TypeOfApplication_ReferenceCode = new Guid("246796ff-9ccc-4c00-9e2e-335694d5671a"),
                    TypeOfApplication_Name = "SaaS"
                },
                new TypeOfApplication
                {
                    TypeOfApplication_Id = new Guid("f78f5ba8-7ac6-4e0c-b435-0a73664b3a54"),
                    TypeOfApplication_ReferenceCode = new Guid("87b81a02-84a4-4919-91ee-58be6e924279"),
                    TypeOfApplication_Name = "IoT Application"
                }
            );

            // Suported Platforms

            modelBuilder.Entity<StepTwoOrder>()
               .HasOne(a => a.SuportedPlatform)
               .WithMany(acc => acc.StepTwoOrders)
               .HasForeignKey(a => a.SuportedPlatform_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SuportedPlatform>()
                .Property(p => p.SuportedPlatform_ReferenceCode)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<SuportedPlatform>()
                .Property(p => p.SuportedPlatform_Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<SuportedPlatform>()
                .HasIndex(p => p.SuportedPlatform_ReferenceCode)
                .IsUnique();

            modelBuilder.Entity<SuportedPlatform>().HasData(
                new SuportedPlatform
                {
                    SuportedPlatform_Id = new Guid("5c6d0b42-eec4-481e-80d3-786c82d725dc"),
                    SuportedPlatform_ReferenceCode = new Guid("024cfbca-eb5f-43d6-8e35-e4f6ab81a6fa"),
                    SuportedPlatform_Name = "Android"
                },
                new SuportedPlatform
                {
                    SuportedPlatform_Id = new Guid("4e1065a7-97d2-4dba-9f9d-3fac61b7435d"),
                    SuportedPlatform_ReferenceCode = new Guid("e4028817-8c05-4bc0-a4e6-bf03e093d0af"),
                    SuportedPlatform_Name = "IOS"
                },
                new SuportedPlatform
                {
                    SuportedPlatform_Id = new Guid("9d216415-036a-4e0f-9c65-4ef34995cd0a"),
                    SuportedPlatform_ReferenceCode = new Guid("c8a504d4-b4fa-4b22-ac83-4ca5017f0fcf"),
                    SuportedPlatform_Name = "WEB Browser"
                },
                new SuportedPlatform
                {
                    SuportedPlatform_Id = new Guid("05133f57-3508-4373-9e96-8edd5131764b"),
                    SuportedPlatform_ReferenceCode = new Guid("f2afae61-0d04-4e11-984f-ff7db3b13995"),
                    SuportedPlatform_Name = "Windows"
                },
                new SuportedPlatform
                {
                    SuportedPlatform_Id = new Guid("d1e46fd5-e0ec-4163-aedd-f29c452b6549"),
                    SuportedPlatform_ReferenceCode = new Guid("6e032983-e444-4b87-b7e6-d6676372bda8"),
                    SuportedPlatform_Name = "Mac"
                }
            );

            // Hosting Preferences

            modelBuilder.Entity<StepFourOrder>()
               .HasOne(a => a.HostingPreference)
               .WithMany(acc => acc.StepFourOrders)
               .HasForeignKey(a => a.HostingPreference_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HostingPreference>()
               .Property(p => p.HostingPreference_ReferenceCode)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<HostingPreference>()
                .Property(p => p.HostingPreference_Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<HostingPreference>()
                .HasIndex(p => p.HostingPreference_ReferenceCode)
                .IsUnique();

            modelBuilder.Entity<HostingPreference>().HasData(
                new HostingPreference
                {
                    HostingPreference_Id = new Guid("40ec495d-d114-48b5-9163-01b4ca87dc9d"),
                    HostingPreference_ReferenceCode = new Guid("3a459302-ab60-4382-ad2e-a6066aff4432"),
                    HostingPreference_Name = "Cloud"
                },
                new HostingPreference
                {
                    HostingPreference_Id = new Guid("d7ed8483-e2f0-4f95-a50d-faa1f8925a20"),
                    HostingPreference_ReferenceCode = new Guid("14aab7e2-4cea-4073-88f5-68b08612b446"),
                    HostingPreference_Name = "On-Premise"
                }
            );

            // Chat Types

            modelBuilder.Entity<ChatType>()
                .Property(p => p.ChatTypeId)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<ChatType>().HasData(
                new ChatType
                {
                    ChatTypeId = new Guid("60bf9886-9060-4c86-a1c8-85ab67446efa"),
                    ChatType_Name = "Personal Conversation"
                },
                new ChatType
                {
                    ChatTypeId = new Guid("5bfb2b27-7424-4500-a2cd-d216771307c1"),
                    ChatType_Name = "Order"
                }
            );

            // Chat

            modelBuilder.Entity<Chat>()
                .Property(p => p.Chat_Id)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Chat>()
                .Property(p => p.Chat_ReferenceCode)
               .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Chat>()
              .HasOne(a => a.ChatType)
              .WithMany(cht => cht.ChatList)
              .HasForeignKey(a => a.ChatTypeId);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User1)
                .WithMany(a => a.ChatsAsUser1)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany(a => a.ChatsAsUser2)
                .HasForeignKey(c => c.IdUser_Secondary)
                .OnDelete(DeleteBehavior.Restrict);

            // Message

            modelBuilder.Entity<Message>()
               .Property(p => p.Message_Id)
              .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Message>()
              .HasOne(a => a.Chat)
              .WithMany(cht => cht.Messages)
              .HasForeignKey(a => a.Chat_Id);

            modelBuilder.Entity<Message>()
             .HasOne(a => a.Account)
             .WithMany(acc => acc.Messages)
             .HasForeignKey(a => a.UserID);
        }
    }
}
