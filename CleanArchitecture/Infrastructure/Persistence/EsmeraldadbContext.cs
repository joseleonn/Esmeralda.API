using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class EsmeraldadbContext : DbContext
{
    public EsmeraldadbContext()
    {
    }

    public EsmeraldadbContext(DbContextOptions<EsmeraldadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Automation> Automations { get; set; }

    public virtual DbSet<Dm> Dms { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Listener> Listeners { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Trigger> Triggers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-wispy-waterfall-a85xffdo.eastus2.azure.neon.tech;Database=esmeraldadb;Username=esmeraldadb_owner;Password=Ofcj6o5Huwqg;SSL Mode=Require");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("INTEGRATIONS", new[] { "INSTAGRAM" })
            .HasPostgresEnum("LISTENERS", new[] { "SMARTAI", "MESSAGE" })
            .HasPostgresEnum("MEDIATYPE", new[] { "IMAGE", "VIDEO", "CAROSEL_ALBUM" })
            .HasPostgresEnum("SUBSCRIPTION_PLAN", new[] { "PRO", "FREE" });

        modelBuilder.Entity<Automation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Automation_pkey");

            entity.ToTable("Automation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(false)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("'Untitled'::text")
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Automations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Automation_userId_fkey");
        });

        modelBuilder.Entity<Dm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dms_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automationId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Reciever).HasColumnName("reciever");
            entity.Property(e => e.SenderId).HasColumnName("senderId");

            entity.HasOne(d => d.Automation).WithMany(p => p.Dms)
                .HasForeignKey(d => d.AutomationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Dms_automationId_fkey");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Integrations_pkey");

            entity.HasIndex(e => e.InstagramId, "Integrations_instagramId_key").IsUnique();

            entity.HasIndex(e => e.Token, "Integrations_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("expiresAt");
            entity.Property(e => e.InstagramId).HasColumnName("instagramId");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Integrations_userId_fkey");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Keyword_pkey");

            entity.ToTable("Keyword");

            entity.HasIndex(e => new { e.AutomationId, e.Word }, "Keyword_automationId_word_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automationId");
            entity.Property(e => e.Word).HasColumnName("word");

            entity.HasOne(d => d.Automation).WithMany(p => p.Keywords)
                .HasForeignKey(d => d.AutomationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Keyword_automationId_fkey");
        });

        modelBuilder.Entity<Listener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Listener_pkey");

            entity.ToTable("Listener");

            entity.HasIndex(e => e.AutomationId, "Listener_automationId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automationId");
            entity.Property(e => e.CommentCount)
                .HasDefaultValue(0)
                .HasColumnName("commentCount");
            entity.Property(e => e.CommentReply).HasColumnName("commentReply");
            entity.Property(e => e.DmCount)
                .HasDefaultValue(0)
                .HasColumnName("dmCount");
            entity.Property(e => e.Prompt).HasColumnName("prompt");

            entity.HasOne(d => d.Automation).WithOne(p => p.Listener)
                .HasForeignKey<Listener>(d => d.AutomationId)
                .HasConstraintName("Listener_automationId_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_pkey");

            entity.ToTable("Post");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automationId");
            entity.Property(e => e.Caption).HasColumnName("caption");
            entity.Property(e => e.Media).HasColumnName("media");
            entity.Property(e => e.Postid).HasColumnName("postid");

            entity.HasOne(d => d.Automation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AutomationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Post_automationId_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription");

            entity.HasIndex(e => e.CustomerId, "Subscription_customerId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "Subscription_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.Subscription)
                .HasForeignKey<Subscription>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Subscription_userId_fkey");
        });

        modelBuilder.Entity<Trigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Trigger_pkey");

            entity.ToTable("Trigger");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automationId");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Automation).WithMany(p => p.Triggers)
                .HasForeignKey(d => d.AutomationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Trigger_automationId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.ClerkId, "User_clerkId_key").IsUnique();

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Firstname, "User_firstname_key").IsUnique();

            entity.HasIndex(e => e.Lastname, "User_lastname_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ClerkId).HasColumnName("clerkId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
