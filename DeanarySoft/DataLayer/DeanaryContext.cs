using System;
using System.Collections.Generic;
using DeanarySoft.DataLayer.DataBaseClasses;
using Microsoft.EntityFrameworkCore;

namespace DeanarySoft.DataLayer;

public partial class DeanaryContext : DbContext
{

    private string connectionString;

    public DeanaryContext() {
    }
    public DeanaryContext(string connectionString) {
        this.connectionString = connectionString;
    }

    public DeanaryContext(DbContextOptions<DeanaryContext> options) : base(options) {
    }

    public virtual DbSet<Contactphone> Contactphones { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Fullrequestinformation> Fullrequestinformations { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<TypeStatus> TypeStatuses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contactphone>(entity =>
        {
            entity.HasKey(e => new { e.Contact, e.StaffId }).HasName("contactphones_pkey");

            entity.ToTable("contactphones", "deanary");

            entity.Property(e => e.Contact).HasColumnName("contact");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Contactphones)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("contactphones_staff_id_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("equipment_pkey");

            entity.ToTable("equipment", "deanary");

            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.CommissioningDate).HasColumnName("commissioning_date");
            entity.Property(e => e.DeadlinePeriod).HasColumnName("deadline_period");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ModelId).HasColumnName("model_id");

            entity.HasOne(d => d.Model).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("equipment_model_id_fkey");
        });

        modelBuilder.Entity<Fullrequestinformation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("fullrequestinformation", "deanary");

            entity.Property(e => e.IdОборудования).HasColumnName("ID оборудования");
            entity.Property(e => e.IdСотрудника).HasColumnName("ID сотрудника");
            entity.Property(e => e.ДатаВводаВЭксплуотацию).HasColumnName("Дата ввода в эксплуотацию");
            entity.Property(e => e.ДатаВозврата).HasColumnName("Дата возврата");
            entity.Property(e => e.ДатаВыдачи).HasColumnName("Дата выдачи");
            entity.Property(e => e.Кафедра).HasMaxLength(40);
            entity.Property(e => e.Модель).HasMaxLength(30);
            entity.Property(e => e.Производитель).HasMaxLength(30);
            entity.Property(e => e.ТипОборудования)
                .HasMaxLength(20)
                .HasColumnName("Тип оборудования");
            entity.Property(e => e.УровеньДоступа).HasColumnName("Уровень доступа");
            entity.Property(e => e.УровеньДоступаОборудования).HasColumnName("Уровень доступа оборудования");
            entity.Property(e => e.ФиСотрудника).HasColumnName("ФИ сотрудника");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("models_pkey");

            entity.ToTable("models", "deanary");

            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.AccessLevel).HasColumnName("access_level");
            entity.Property(e => e.EquipmentType)
                .HasMaxLength(20)
                .HasColumnName("equipment_type");
            entity.Property(e => e.Manufactor)
                .HasMaxLength(30)
                .HasColumnName("manufactor");
            entity.Property(e => e.Model1)
                .HasMaxLength(30)
                .HasColumnName("model");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("requests", "deanary");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Equipment).WithMany()
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("requests_equipment_id_fkey");

            entity.HasOne(d => d.Staff).WithMany()
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("requests_staff_id_fkey");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff", "deanary");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.AccessLevel).HasColumnName("access_level");
            entity.Property(e => e.Department)
                .HasMaxLength(40)
                .HasColumnName("department");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<TypeStatus>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("statuses_pkey");

            entity.ToTable("statuses", "deanary");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.StatusType)
                .HasMaxLength(20)
                .HasColumnName("status_type");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("status", "deanary");

            entity.Property(e => e.DateOfAssignment).HasColumnName("date_of_assignment");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Equipment).WithMany()
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("status_equipment_id_fkey");

            entity.HasOne(d => d.Type).WithMany()
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("status_type_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
