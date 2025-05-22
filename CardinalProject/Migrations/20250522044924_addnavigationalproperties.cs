using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardinalProject.Migrations
{
    /// <inheritdoc />
    public partial class addnavigationalproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderServices_ServiceId",
                table: "ServiceProviderServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderServices_ServiceProviderId",
                table: "ServiceProviderServices",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviderServices_ServiceId",
                table: "ServiceProviderServices");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviderServices_ServiceProviderId",
                table: "ServiceProviderServices");
        }
    }
}
