﻿// <auto-generated />
using System;
using AcsEmulatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcsEmulatorAPI.Migrations
{
    [DbContext(typeof(AcsDbContext))]
    partial class AcsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("AcsEmulatorAPI.Models.AddedParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AddParticipantsChatMessageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantRawId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ShareHistoryTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddParticipantsChatMessageId");

                    b.HasIndex("ParticipantRawId");

                    b.ToTable("AddedParticipant");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.CallConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AnsweredBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("CallConnectionState")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CallbackUri")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CognitiveServicesEndpoint")
                        .HasColumnType("TEXT");

                    b.Property<string>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServerCallId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceCallerIdNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceDisplayName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CallConnections");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.CallConnectionTarget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CallConnectionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("RawId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CallConnectionId");

                    b.ToTable("CallConnectionTargets", (string)null);
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ChatThreadId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderRawId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SequenceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VersionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatThreadId");

                    b.HasIndex("SenderRawId");

                    b.ToTable("ChatMessages", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.ChatThread", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedByRawId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByRawId");

                    b.ToTable("ChatThreads");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.EmailMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bcc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cc")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DisableUserEngagementTracking")
                        .HasColumnType("INTEGER");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("Headers")
                        .HasColumnType("TEXT");

                    b.Property<string>("Html")
                        .HasColumnType("TEXT");

                    b.Property<string>("OperationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("OperationStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlainText")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReplyTo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmailMessages");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.EmailMessageAttachment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentBytesBase64")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailMessageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmailMessageId");

                    b.ToTable("EmailMessageAttachment");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.SmsMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EnableDeliveryReport")
                        .HasColumnType("INTEGER");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SmsMessages");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.User", b =>
                {
                    b.Property<string>("RawId")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.HasKey("RawId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.UserChatThread", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChatThreadId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ShareHistoryTime")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ChatThreadId");

                    b.HasIndex("ChatThreadId");

                    b.ToTable("UserChatThread");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.AddParticipantsChatMessage", b =>
                {
                    b.HasBaseType("AcsEmulatorAPI.Models.ChatMessage");

                    b.ToTable("AddParticipantsChatMessages", (string)null);
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.AddedParticipant", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.AddParticipantsChatMessage", null)
                        .WithMany("AddedParticipants")
                        .HasForeignKey("AddParticipantsChatMessageId");

                    b.HasOne("AcsEmulatorAPI.Models.User", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantRawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.CallConnectionTarget", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.CallConnection", "CallConnection")
                        .WithMany("Targets")
                        .HasForeignKey("CallConnectionId");

                    b.Navigation("CallConnection");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.ChatMessage", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.ChatThread", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatThreadId");

                    b.HasOne("AcsEmulatorAPI.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderRawId");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.ChatThread", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByRawId");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.EmailMessageAttachment", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.EmailMessage", null)
                        .WithMany("Attachments")
                        .HasForeignKey("EmailMessageId");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.UserChatThread", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.ChatThread", "ChatThread")
                        .WithMany("UserChatThreads")
                        .HasForeignKey("ChatThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcsEmulatorAPI.Models.User", "User")
                        .WithMany("UserChatThreads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatThread");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.AddParticipantsChatMessage", b =>
                {
                    b.HasOne("AcsEmulatorAPI.Models.ChatMessage", null)
                        .WithOne()
                        .HasForeignKey("AcsEmulatorAPI.Models.AddParticipantsChatMessage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.CallConnection", b =>
                {
                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.ChatThread", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChatThreads");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.EmailMessage", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.User", b =>
                {
                    b.Navigation("UserChatThreads");
                });

            modelBuilder.Entity("AcsEmulatorAPI.Models.AddParticipantsChatMessage", b =>
                {
                    b.Navigation("AddedParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
