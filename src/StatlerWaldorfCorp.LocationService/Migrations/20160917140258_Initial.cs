using System;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatlerWaldorfCorp.LocationService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.CreatePostgresExtension("uuid-ossp");

            migrationBuilder.CreateTable(
                name: "LocationRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Altitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    MemberID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationRecords", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPostgresExtension("uuid-ossp");

            migrationBuilder.DropTable(
                name: "LocationRecords");
        }
    }
}
