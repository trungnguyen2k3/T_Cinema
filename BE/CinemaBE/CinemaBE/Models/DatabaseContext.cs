using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinemaBE.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SysAccount> SysAccounts { get; set; }

    public virtual DbSet<SysCinema> SysCinemas { get; set; }

    public virtual DbSet<SysCombo> SysCombos { get; set; }

    public virtual DbSet<SysLog> SysLogs { get; set; }

    public virtual DbSet<SysSubtitle> SysSubtitles { get; set; }

    public virtual DbSet<SysType> SysTypes { get; set; }

    public virtual DbSet<TblBooking> TblBookings { get; set; }

    public virtual DbSet<TblBookingDetail> TblBookingDetails { get; set; }

    public virtual DbSet<TblComboDetail> TblComboDetails { get; set; }

    public virtual DbSet<TblImage> TblImages { get; set; }

    public virtual DbSet<TblMovie> TblMovies { get; set; }

    public virtual DbSet<TblMovieType> TblMovieTypes { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    public virtual DbSet<TblSeat> TblSeats { get; set; }

    public virtual DbSet<TblShowTime> TblShowTimes { get; set; }

    public virtual DbSet<TblTicketPrice> TblTicketPrices { get; set; }

    public virtual DbSet<TblTimeSlot> TblTimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=cinema;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SysAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Acco__3214EC070176B79F");

            entity.ToTable("Sys_Account");

            entity.HasIndex(e => e.Username, "UQ__Sys_Acco__536C85E4CB1D273E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Sys_Acco__A9D10534EF697087").IsUnique();

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SysCinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Cine__3214EC0745A49190");

            entity.ToTable("Sys_Cinema");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<SysCombo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Comb__3214EC07C195CC88");

            entity.ToTable("Sys_Combo");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<SysLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Log__3214EC07FDAFAA31");

            entity.ToTable("Sys_Log");

            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AfterData).IsUnicode(false);
            entity.Property(e => e.BeforeData).IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.SysLogs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Sys_Log__Account__797309D9");
        });

        modelBuilder.Entity<SysSubtitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Subt__3214EC07D79F9A48");

            entity.ToTable("Sys_Subtitle");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<SysType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sys_Type__3214EC07A6E24295");

            entity.ToTable("Sys_Type");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Book__3214EC0746ACCDB8");

            entity.ToTable("Tbl_Booking");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Tbl_Booki__Accou__628FA481");

            entity.HasOne(d => d.ShowTime).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.ShowTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Booki__ShowT__6383C8BA");
        });

        modelBuilder.Entity<TblBookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Book__3214EC070BA15909");

            entity.ToTable("Tbl_BookingDetail");

            entity.HasIndex(e => new { e.ShowTimeId, e.SeatId }, "UQ_ShowTimeSeat").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Booki__Booki__6754599E");

            entity.HasOne(d => d.Seat).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Booki__SeatI__68487DD7");

            entity.HasOne(d => d.ShowTime).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.ShowTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Booki__ShowT__693CA210");
        });

        modelBuilder.Entity<TblComboDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Comb__3214EC0700B06FA2");

            entity.ToTable("Tbl_ComboDetail");

            entity.HasOne(d => d.Booking).WithMany(p => p.TblComboDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Combo__Booki__6EF57B66");

            entity.HasOne(d => d.Combo).WithMany(p => p.TblComboDetails)
                .HasForeignKey(d => d.ComboId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Combo__Combo__6FE99F9F");
        });

        modelBuilder.Entity<TblImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Imag__3214EC07094D117B");

            entity.ToTable("Tbl_Image");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path).IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMovie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Movi__3214EC07874698C6");

            entity.ToTable("Tbl_Movie");

            entity.Property(e => e.Director).HasMaxLength(100);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Poster).IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMovieType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Movi__3214EC07E83D42C1");

            entity.ToTable("Tbl_MovieType");

            entity.HasOne(d => d.Movie).WithMany(p => p.TblMovieTypes)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Movie__Movie__4D94879B");

            entity.HasOne(d => d.Type).WithMany(p => p.TblMovieTypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Movie__TypeI__4E88ABD4");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Paym__3214EC07646B4977");

            entity.ToTable("Tbl_Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 2)");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Payme__Booki__73BA3083");
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Room__3214EC071DF97166");

            entity.ToTable("Tbl_Room");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoomType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Cinema).WithMany(p => p.TblRooms)
                .HasForeignKey(d => d.CinemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Room__Cinema__412EB0B6");
        });

        modelBuilder.Entity<TblSeat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Seat__3214EC071E41CE41");

            entity.ToTable("Tbl_Seat");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SeatRow)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.SeatType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Room).WithMany(p => p.TblSeats)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Seat__RoomId__44FF419A");
        });

        modelBuilder.Entity<TblShowTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Show__3214EC07E1BAF962");

            entity.ToTable("Tbl_ShowTime");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Movie).WithMany(p => p.TblShowTimes)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_ShowT__Movie__5BE2A6F2");

            entity.HasOne(d => d.Room).WithMany(p => p.TblShowTimes)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_ShowT__RoomI__5CD6CB2B");

            entity.HasOne(d => d.Subtitle).WithMany(p => p.TblShowTimes)
                .HasForeignKey(d => d.SubtitleId)
                .HasConstraintName("FK__Tbl_ShowT__Subti__5DCAEF64");
        });

        modelBuilder.Entity<TblTicketPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Tick__3214EC078B3E6734");

            entity.ToTable("Tbl_TicketPrice");

            entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");
            entity.Property(e => e.SeatType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.TblTicketPrices)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Ticke__TimeS__5812160E");
        });

        modelBuilder.Entity<TblTimeSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Time__3214EC071C57B53A");

            entity.ToTable("Tbl_TimeSlot");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
