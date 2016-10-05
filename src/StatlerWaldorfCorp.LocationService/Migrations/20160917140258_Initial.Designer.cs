using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StatlerWaldorfCorp.LocationService.Persistence;

namespace StatlerWaldorfCorp.LocationService.Migrations
{
    [DbContext(typeof(LocationDbContext))]
    [Migration("20160917140258_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:.uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("StatlerWaldorfCorp.LocationService.Models.LocationRecord", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Altitude");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<Guid>("MemberID");

                    b.Property<long>("Timestamp");

                    b.HasKey("ID");

                    b.ToTable("LocationRecords");
                });
        }
    }
}
