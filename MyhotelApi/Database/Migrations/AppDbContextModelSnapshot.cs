﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyhotelApi.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyhotelApi.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Amenity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("AdditionalFee")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsFree")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("RoomId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.House", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("GalleryPaths")
                        .HasColumnType("text[]");

                    b.Property<string>("HouseAvatarPath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("PricePerNight")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("numeric");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<long?>("Stars")
                        .HasColumnType("bigint");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Reservation", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<int?>("NumGuests")
                        .HasColumnType("integer");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("numeric");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Capacity")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Gallery")
                        .HasColumnType("text[]");

                    b.Property<Guid?>("HouseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("PricePerNight")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<string>("RoomAvatarPath")
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<long?>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Amenity", b =>
                {
                    b.HasOne("MyhotelApi.Objects.Entities.House", "House")
                        .WithMany("Amenities")
                        .HasForeignKey("HouseId");

                    b.HasOne("MyhotelApi.Objects.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("House");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.House", b =>
                {
                    b.HasOne("MyhotelApi.Objects.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Reservation", b =>
                {
                    b.HasOne("MyhotelApi.Objects.Entities.House", "House")
                        .WithMany("Reservations")
                        .HasForeignKey("HouseId");

                    b.HasOne("MyhotelApi.Objects.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("House");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Review", b =>
                {
                    b.HasOne("MyhotelApi.Objects.Entities.House", "House")
                        .WithMany("Reviews")
                        .HasForeignKey("HouseId");

                    b.HasOne("MyhotelApi.Objects.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("House");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Room", b =>
                {
                    b.HasOne("MyhotelApi.Objects.Entities.House", "House")
                        .WithMany("Rooms")
                        .HasForeignKey("HouseId");

                    b.HasOne("MyhotelApi.Objects.Entities.Reservation", "Reservation")
                        .WithMany("Rooms")
                        .HasForeignKey("ReservationId");

                    b.Navigation("House");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.House", b =>
                {
                    b.Navigation("Amenities");

                    b.Navigation("Reservations");

                    b.Navigation("Reviews");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("MyhotelApi.Objects.Entities.Reservation", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
