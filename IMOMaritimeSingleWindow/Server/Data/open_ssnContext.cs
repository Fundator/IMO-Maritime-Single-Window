using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IMOMaritimeSingleWindow.Models
{
    public partial class open_ssnContext : DbContext
    {
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationPerson> ApplicationPerson { get; set; }
        public virtual DbSet<ApplicationPersonHistory> ApplicationPersonHistory { get; set; }
        public virtual DbSet<ApplicationRight> ApplicationRight { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContactMedium> ContactMedium { get; set; }
        public virtual DbSet<Council> Council { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<CustomsCargo> CustomsCargo { get; set; }
        public virtual DbSet<CustomsCargoType> CustomsCargoType { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Dpg> Dpg { get; set; }
        public virtual DbSet<DpgOnBoard> DpgOnBoard { get; set; }
        public virtual DbSet<DpgType> DpgType { get; set; }
        public virtual DbSet<ImoHazardClass> ImoHazardClass { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationSource> LocationSource { get; set; }
        public virtual DbSet<LocationType> LocationType { get; set; }
        public virtual DbSet<MarpolCategory> MarpolCategory { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonRole> PersonRole { get; set; }
        public virtual DbSet<PortCall> PortCall { get; set; }
        public virtual DbSet<PortCallHasPortCallPurpose> PortCallHasPortCallPurpose { get; set; }
        public virtual DbSet<PortCallPurpose> PortCallPurpose { get; set; }
        public virtual DbSet<PortCallStatus> PortCallStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleApplicationRight> RoleApplicationRight { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<ShipBreadthType> ShipBreadthType { get; set; }
        public virtual DbSet<ShipCertificate> ShipCertificate { get; set; }
        public virtual DbSet<ShipCertificateType> ShipCertificateType { get; set; }
        public virtual DbSet<ShipContact> ShipContact { get; set; }
        public virtual DbSet<ShipFlagCode> ShipFlagCode { get; set; }
        public virtual DbSet<ShipHistory> ShipHistory { get; set; }
        public virtual DbSet<ShipHullType> ShipHullType { get; set; }
        public virtual DbSet<ShipLengthType> ShipLengthType { get; set; }
        public virtual DbSet<ShipMmsiMidCode> ShipMmsiMidCode { get; set; }
        public virtual DbSet<ShipPowerType> ShipPowerType { get; set; }
        public virtual DbSet<ShipSource> ShipSource { get; set; }
        public virtual DbSet<ShipStatus> ShipStatus { get; set; }
        public virtual DbSet<ShipType> ShipType { get; set; }
        public virtual DbSet<ShipTypeGroup> ShipTypeGroup { get; set; }



        public open_ssnContext(DbContextOptions<open_ssnContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("application");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasColumnName("application_name")
                    .HasColumnType("varchar");

                entity.Property(e => e.IsPasswordRequired).HasColumnName("is_password_required");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name")
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<ApplicationPerson>(entity =>
            {
                entity.ToTable("application_person");

                entity.HasIndex(e => e.ApplicationId)
                    .HasName("ifk_rel_38");

                entity.HasIndex(e => e.PersonId)
                    .HasName("ifk_rel_37");

                entity.Property(e => e.ApplicationPersonId).HasColumnName("application_person_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationPerson)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_person_application_id_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ApplicationPerson)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_person_person_id_fkey");
            });

            modelBuilder.Entity<ApplicationPersonHistory>(entity =>
            {
                entity.ToTable("application_person_history");

                entity.HasIndex(e => e.ApplicationPersonId)
                    .HasName("ifk_rel_43");

                entity.Property(e => e.ApplicationPersonHistoryId).HasColumnName("application_person_history_id");

                entity.Property(e => e.ApplicationPersonId).HasColumnName("application_person_id");

                entity.HasOne(d => d.ApplicationPerson)
                    .WithMany(p => p.ApplicationPersonHistory)
                    .HasForeignKey(d => d.ApplicationPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_person_history_application_person_id_fkey");
            });

            modelBuilder.Entity<ApplicationRight>(entity =>
            {
                entity.ToTable("application_right");

                entity.HasIndex(e => e.ApplicationId)
                    .HasName("ifk_rel_36");

                entity.Property(e => e.ApplicationRightId).HasColumnName("application_right_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ApplicationRightName)
                    .IsRequired()
                    .HasColumnName("application_right_name");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationRight)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_right_application_id_fkey");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name");

                entity.Property(e => e.CompanyOrgNo).HasColumnName("company_org_no");

                entity.Property(e => e.InvoiceReceiverNo).HasColumnName("invoice_receiver_no");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsInvoiceReceiver).HasColumnName("is_invoice_receiver");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.Remark).HasColumnName("remark");
            });

            modelBuilder.Entity<ContactMedium>(entity =>
            {
                entity.ToTable("contact_medium");

                entity.Property(e => e.ContactMediumId).HasColumnName("contact_medium_id");

                entity.Property(e => e.ContactMediumType).HasColumnName("contact_medium_type");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.SystemName).HasColumnName("system_name");
            });

            modelBuilder.Entity<Council>(entity =>
            {
                entity.ToTable("council");

                entity.HasIndex(e => e.CountyId)
                    .HasName("ifk_rel_08");

                entity.Property(e => e.CouncilId).HasColumnName("council_id");

                entity.Property(e => e.CouncilName)
                    .IsRequired()
                    .HasColumnName("council_name");

                entity.Property(e => e.CouncilNo)
                    .IsRequired()
                    .HasColumnName("council_no");

                entity.Property(e => e.CountyId).HasColumnName("county_id");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Council)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("council_county_id_fkey");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CallCode).HasColumnName("call_code");

                entity.Property(e => e.Country1)
                    .IsRequired()
                    .HasColumnName("country");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ThreeCharCode)
                    .IsRequired()
                    .HasColumnName("three_char_code");

                entity.Property(e => e.TwoCharCode)
                    .IsRequired()
                    .HasColumnName("two_char_code");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("county");

                entity.HasIndex(e => e.CountryId)
                    .HasName("ifk_rel_07");

                entity.Property(e => e.CountyId).HasColumnName("county_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CountyGeometry).HasColumnName("county_geometry");

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasColumnName("county_name");

                entity.Property(e => e.CountyNo)
                    .IsRequired()
                    .HasColumnName("county_no");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("county_country_id_fkey");
            });

            modelBuilder.Entity<CustomsCargo>(entity =>
            {
                entity.ToTable("customs_cargo");

                entity.HasIndex(e => e.CustomsCargoTypeId)
                    .HasName("ifk_rel_44_2");

                entity.HasIndex(e => e.PortCallId)
                    .HasName("ifk_rel_45");

                entity.Property(e => e.CustomsCargoId).HasColumnName("customs_cargo_id");

                entity.Property(e => e.CargoHandlingAgent).HasColumnName("cargo_handling_agent");

                entity.Property(e => e.CustomsCargoTypeId).HasColumnName("customs_cargo_type_id");

                entity.Property(e => e.LocationInPort).HasColumnName("location_in_port");

                entity.Property(e => e.PortCallId).HasColumnName("port_call_id");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.HasOne(d => d.CustomsCargoType)
                    .WithMany(p => p.CustomsCargo)
                    .HasForeignKey(d => d.CustomsCargoTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customs_cargo_customs_cargo_type_id_fkey");

                entity.HasOne(d => d.PortCall)
                    .WithMany(p => p.CustomsCargo)
                    .HasForeignKey(d => d.PortCallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customs_cargo_port_call_id_fkey");
            });

            modelBuilder.Entity<CustomsCargoType>(entity =>
            {
                entity.ToTable("customs_cargo_type");

                entity.Property(e => e.CustomsCargoTypeId).HasColumnName("customs_cargo_type_id");

                entity.Property(e => e.CustomsCargoType1)
                    .IsRequired()
                    .HasColumnName("customs_cargo_type");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.LocationId)
                    .HasName("ifk_rel_06");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentNo).HasColumnName("department_no");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.ShortName).HasColumnName("short_name");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_location_id_fkey");
            });

            modelBuilder.Entity<Dpg>(entity =>
            {
                entity.ToTable("dpg");

                entity.HasIndex(e => e.DpgTypeId)
                    .HasName("ifk_rel_41_2");

                entity.HasIndex(e => e.ImoHazardClassId)
                    .HasName("ifk_rel_42_2");

                entity.HasIndex(e => e.MarpolCategoryId)
                    .HasName("ifk_rel_43_2");

                entity.Property(e => e.DpgId).HasColumnName("dpg_id");

                entity.Property(e => e.DpgTypeId).HasColumnName("dpg_type_id");

                entity.Property(e => e.FlashPoint)
                    .HasColumnName("flash_point")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ImoHazardClassId).HasColumnName("imo_hazard_class_id");

                entity.Property(e => e.MarpolCategoryId).HasColumnName("marpol_category_id");

                entity.Property(e => e.MarpolOilType).HasColumnName("marpol_oil_type");

                entity.Property(e => e.PackingGroup).HasColumnName("packing_group");

                entity.Property(e => e.TextualReference).HasColumnName("textual_reference");

                entity.Property(e => e.UnNumber).HasColumnName("un_number");

                entity.HasOne(d => d.DpgType)
                    .WithMany(p => p.Dpg)
                    .HasForeignKey(d => d.DpgTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dpg_dpg_type_id_fkey");

                entity.HasOne(d => d.ImoHazardClass)
                    .WithMany(p => p.Dpg)
                    .HasForeignKey(d => d.ImoHazardClassId)
                    .HasConstraintName("dpg_imo_hazard_class_id_fkey");

                entity.HasOne(d => d.MarpolCategory)
                    .WithMany(p => p.Dpg)
                    .HasForeignKey(d => d.MarpolCategoryId)
                    .HasConstraintName("dpg_marpol_category_id_fkey");
            });

            modelBuilder.Entity<DpgOnBoard>(entity =>
            {
                entity.ToTable("dpg_on_board");

                entity.HasIndex(e => e.DpgId)
                    .HasName("ifk_rel_46");

                entity.HasIndex(e => e.PortCallId)
                    .HasName("ifk_rel_47");

                entity.Property(e => e.DpgOnBoardId).HasColumnName("dpg_on_board_id");

                entity.Property(e => e.DpgId).HasColumnName("dpg_id");

                entity.Property(e => e.GrossWeight)
                    .HasColumnName("gross_weight")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.LocationOnBoard).HasColumnName("location_on_board");

                entity.Property(e => e.NetWeight)
                    .HasColumnName("net_weight")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.PlacedInContainer).HasColumnName("placed_in_container");

                entity.Property(e => e.PortCallId).HasColumnName("port_call_id");

                entity.Property(e => e.TransportUnitIdentification).HasColumnName("transport_unit_identification");

                entity.HasOne(d => d.Dpg)
                    .WithMany(p => p.DpgOnBoard)
                    .HasForeignKey(d => d.DpgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dpg_on_board_dpg_id_fkey");

                entity.HasOne(d => d.PortCall)
                    .WithMany(p => p.DpgOnBoard)
                    .HasForeignKey(d => d.PortCallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dpg_on_board_port_call_id_fkey");
            });

            modelBuilder.Entity<DpgType>(entity =>
            {
                entity.ToTable("dpg_type");

                entity.Property(e => e.DpgTypeId).HasColumnName("dpg_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DpgTypeName)
                    .IsRequired()
                    .HasColumnName("dpg_type_name");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ImoHazardClass>(entity =>
            {
                entity.ToTable("imo_hazard_class");

                entity.HasIndex(e => e.ParentImoHazardClassId)
                    .HasName("ifk_rel_40_2");

                entity.Property(e => e.ImoHazardClassId).HasColumnName("imo_hazard_class_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImoHazardClassName).HasColumnName("imo_hazard_class_name");

                entity.Property(e => e.ParentImoHazardClassId).HasColumnName("parent_imo_hazard_class_id");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");

                entity.HasOne(d => d.ParentImoHazardClass)
                    .WithMany(p => p.InverseParentImoHazardClass)
                    .HasForeignKey(d => d.ParentImoHazardClassId)
                    .HasConstraintName("imo_hazard_class_parent_imo_hazard_class_id_fkey");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.CouncilId)
                    .HasName("ifk_rel_10");

                entity.HasIndex(e => e.CountryId)
                    .HasName("ifk_rel_11");

                entity.HasIndex(e => e.LocationInLocationId)
                    .HasName("ifk_rel_09");

                entity.HasIndex(e => e.LocationSourceId)
                    .HasName("ifk_rel_12");

                entity.HasIndex(e => e.LocationTypeId)
                    .HasName("ifk_rel_13");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.CouncilId).HasColumnName("council_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.LocationCode).HasColumnName("location_code");

                entity.Property(e => e.LocationInLocationId).HasColumnName("location_in_location_id");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasColumnName("location_name");

                entity.Property(e => e.LocationNo).HasColumnName("location_no");

                entity.Property(e => e.LocationSourceId).HasColumnName("location_source_id");

                entity.Property(e => e.LocationTypeId).HasColumnName("location_type_id");

                entity.Property(e => e.PostCode).HasColumnName("post_code");

                entity.HasOne(d => d.Council)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CouncilId)
                    .HasConstraintName("location_council_id_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_country_id_fkey");

                entity.HasOne(d => d.LocationInLocation)
                    .WithMany(p => p.InverseLocationInLocation)
                    .HasForeignKey(d => d.LocationInLocationId)
                    .HasConstraintName("location_location_in_location_id_fkey");

                entity.HasOne(d => d.LocationSource)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.LocationSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_location_source_id_fkey");

                entity.HasOne(d => d.LocationType)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.LocationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_location_type_id_fkey");
            });

            modelBuilder.Entity<LocationSource>(entity =>
            {
                entity.ToTable("location_source");

                entity.Property(e => e.LocationSourceId).HasColumnName("location_source_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LocationSourceName)
                    .IsRequired()
                    .HasColumnName("location_source_name");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.ToTable("location_type");

                entity.Property(e => e.LocationTypeId).HasColumnName("location_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LocationType1)
                    .IsRequired()
                    .HasColumnName("location_type");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<MarpolCategory>(entity =>
            {
                entity.ToTable("marpol_category");

                entity.Property(e => e.MarpolCategoryId).HasColumnName("marpol_category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.MarpolCategory1)
                    .IsRequired()
                    .HasColumnName("marpol_category");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");
            });

            modelBuilder.Entity<PersonRole>(entity =>
            {
                entity.ToTable("person_role");

                entity.HasIndex(e => e.PersonId)
                    .HasName("ifk_rel_39");

                entity.HasIndex(e => e.RoleId)
                    .HasName("ifk_rel_40");

                entity.Property(e => e.PersonRoleId).HasColumnName("person_role_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonRole)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("person_role_person_id_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PersonRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("person_role_role_id_fkey");
            });

            modelBuilder.Entity<PortCall>(entity =>
            {
                entity.ToTable("port_call");

                entity.HasIndex(e => e.LocationId)
                    .HasName("ifk_rel_44");

                entity.HasIndex(e => e.NextLocationId)
                    .HasName("ifk_rel_15");

                entity.HasIndex(e => e.PortCallStatusId)
                    .HasName("ifk_rel_02");

                entity.HasIndex(e => e.PreviousLocationId)
                    .HasName("ifk_rel_14");

                entity.HasIndex(e => e.ShipId)
                    .HasName("ifk_rel_32");

                entity.Property(e => e.PortCallId).HasColumnName("port_call_id");

                entity.Property(e => e.LocationAta).HasColumnName("location_ata");

                entity.Property(e => e.LocationAtd).HasColumnName("location_atd");

                entity.Property(e => e.LocationEta).HasColumnName("location_eta");

                entity.Property(e => e.LocationEtd).HasColumnName("location_etd");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.NextLocationAta).HasColumnName("next_location_ata");

                entity.Property(e => e.NextLocationEta).HasColumnName("next_location_eta");

                entity.Property(e => e.NextLocationId).HasColumnName("next_location_id");

                entity.Property(e => e.PortCallStatusId).HasColumnName("port_call_status_id");

                entity.Property(e => e.PreviousLocationAtd).HasColumnName("previous_location_atd");

                entity.Property(e => e.PreviousLocationEtd).HasColumnName("previous_location_etd");

                entity.Property(e => e.PreviousLocationId).HasColumnName("previous_location_id");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PortCallLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_location_id_fkey");

                entity.HasOne(d => d.NextLocation)
                    .WithMany(p => p.PortCallNextLocation)
                    .HasForeignKey(d => d.NextLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_next_location_id_fkey");

                entity.HasOne(d => d.PortCallStatus)
                    .WithMany(p => p.PortCall)
                    .HasForeignKey(d => d.PortCallStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_port_call_status_id_fkey");

                entity.HasOne(d => d.PreviousLocation)
                    .WithMany(p => p.PortCallPreviousLocation)
                    .HasForeignKey(d => d.PreviousLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_previous_location_id_fkey");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.PortCall)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_ship_id_fkey");
            });

            modelBuilder.Entity<PortCallHasPortCallPurpose>(entity =>
            {
                entity.ToTable("port_call_has_port_call_purpose");

                entity.HasIndex(e => e.PortCallId)
                    .HasName("ifk_rel_04");

                entity.HasIndex(e => e.PortCallPurposeId)
                    .HasName("ifk_rel_05");

                entity.Property(e => e.PortCallHasPortCallPurposeId)
                    .HasColumnName("port_call_has_port_call_purpose_id")
                    .HasDefaultValueSql("nextval('port_call_has_port_call_purpo_port_call_has_port_call_purpo_seq'::regclass)");

                entity.Property(e => e.PortCallId).HasColumnName("port_call_id");

                entity.Property(e => e.PortCallPurposeId).HasColumnName("port_call_purpose_id");

                entity.HasOne(d => d.PortCall)
                    .WithMany(p => p.PortCallHasPortCallPurpose)
                    .HasForeignKey(d => d.PortCallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_has_port_call_purpose_port_call_id_fkey");

                entity.HasOne(d => d.PortCallPurpose)
                    .WithMany(p => p.PortCallHasPortCallPurpose)
                    .HasForeignKey(d => d.PortCallPurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("port_call_has_port_call_purpose_port_call_purpose_id_fkey");
            });

            modelBuilder.Entity<PortCallPurpose>(entity =>
            {
                entity.ToTable("port_call_purpose");

                entity.Property(e => e.PortCallPurposeId).HasColumnName("port_call_purpose_id");

                entity.Property(e => e.PortCallPurpose1)
                    .IsRequired()
                    .HasColumnName("port_call_purpose");

                entity.Property(e => e.SystemName).HasColumnName("system_name");
            });

            modelBuilder.Entity<PortCallStatus>(entity =>
            {
                entity.ToTable("port_call_status");

                entity.Property(e => e.PortCallStatusId).HasColumnName("port_call_status_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.PortCallStatus1).HasColumnName("port_call_status");

                entity.Property(e => e.SystemName).HasColumnName("system_name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<RoleApplicationRight>(entity =>
            {
                entity.ToTable("role_application_right");

                entity.HasIndex(e => e.ApplicationRightId)
                    .HasName("ifk_rel_42");

                entity.HasIndex(e => e.RoleId)
                    .HasName("ifk_rel_41");

                entity.Property(e => e.RoleApplicationRightId).HasColumnName("role_application_right_id");

                entity.Property(e => e.ApplicationRightId).HasColumnName("application_right_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.ApplicationRight)
                    .WithMany(p => p.RoleApplicationRight)
                    .HasForeignKey(d => d.ApplicationRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_application_right_application_right_id_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleApplicationRight)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_application_right_role_id_fkey");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("ship");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("ifk_rel_22");

                entity.HasIndex(e => e.ShipBreadthTypeId)
                    .HasName("ifk_rel_29");

                entity.HasIndex(e => e.ShipFlagCodeId)
                    .HasName("ifk_rel_27");

                entity.HasIndex(e => e.ShipHullTypeId)
                    .HasName("ifk_rel_31");

                entity.HasIndex(e => e.ShipLengthTypeId)
                    .HasName("ifk_rel_28");

                entity.HasIndex(e => e.ShipPowerTypeId)
                    .HasName("ifk_rel_30");

                entity.HasIndex(e => e.ShipSourceId)
                    .HasName("ifk_rel_25");

                entity.HasIndex(e => e.ShipStatusId)
                    .HasName("ifk_rel_24");

                entity.HasIndex(e => e.ShipTypeId)
                    .HasName("ifk_rel_17");

                entity.Property(e => e.ShipId)
                    .HasColumnName("ship_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.CallSign).HasColumnName("call_sign");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.DeadweightTonnage).HasColumnName("deadweight_tonnage");

                entity.Property(e => e.Draught).HasColumnName("draught");

                entity.Property(e => e.GrossTonnage).HasColumnName("gross_tonnage");

                entity.Property(e => e.HasSideThrusters).HasColumnName("has_side_thrusters");

                entity.Property(e => e.HasSideThrustersBack).HasColumnName("has_side_thrusters_back");

                entity.Property(e => e.HasSideThrustersFront).HasColumnName("has_side_thrusters_front");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImoNo).HasColumnName("imo_no");

                entity.Property(e => e.MmsiNo).HasColumnName("mmsi_no");

                entity.Property(e => e.Power).HasColumnName("power");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.Property(e => e.ShipBreadthTypeId).HasColumnName("ship_breadth_type_id");

                entity.Property(e => e.ShipFlagCodeId).HasColumnName("ship_flag_code_id");

                entity.Property(e => e.ShipHullTypeId).HasColumnName("ship_hull_type_id");

                entity.Property(e => e.ShipLength).HasColumnName("ship_length");

                entity.Property(e => e.ShipLengthTypeId).HasColumnName("ship_length_type_id");

                entity.Property(e => e.ShipName)
                    .IsRequired()
                    .HasColumnName("ship_name");

                entity.Property(e => e.ShipPowerTypeId).HasColumnName("ship_power_type_id");

                entity.Property(e => e.ShipSourceId).HasColumnName("ship_source_id");

                entity.Property(e => e.ShipStatusId).HasColumnName("ship_status_id");

                entity.Property(e => e.ShipTypeId).HasColumnName("ship_type_id");

                entity.Property(e => e.YearOfBuild).HasColumnName("year_of_build");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("ship_company_id_fkey");

                entity.HasOne(d => d.ShipBreadthType)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipBreadthTypeId)
                    .HasConstraintName("ship_ship_breadth_type_id_fkey");

                entity.HasOne(d => d.ShipFlagCode)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipFlagCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_ship_flag_code_id_fkey");

                entity.HasOne(d => d.ShipHullType)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipHullTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_ship_hull_type_id_fkey");

                entity.HasOne(d => d.ShipLengthType)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipLengthTypeId)
                    .HasConstraintName("ship_ship_length_type_id_fkey");

                entity.HasOne(d => d.ShipPowerType)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipPowerTypeId)
                    .HasConstraintName("ship_ship_power_type_id_fkey");

                entity.HasOne(d => d.ShipSource)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_ship_source_id_fkey");

                entity.HasOne(d => d.ShipStatus)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_ship_status_id_fkey");

                entity.HasOne(d => d.ShipType)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.ShipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_ship_type_id_fkey");
            });

            modelBuilder.Entity<ShipBreadthType>(entity =>
            {
                entity.ToTable("ship_breadth_type");

                entity.Property(e => e.ShipBreadthTypeId).HasColumnName("ship_breadth_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipBreadthType1)
                    .IsRequired()
                    .HasColumnName("ship_breadth_type");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipCertificate>(entity =>
            {
                entity.ToTable("ship_certificate");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("ifk_rel_20");

                entity.HasIndex(e => e.CountryId)
                    .HasName("ifk_rel_34");

                entity.HasIndex(e => e.ShipCertificateTypeId)
                    .HasName("ifk_rel_19");

                entity.HasIndex(e => e.ShipId)
                    .HasName("ifk_rel_21");

                entity.Property(e => e.ShipCertificateId).HasColumnName("ship_certificate_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ExpireDate).HasColumnName("expire_date");

                entity.Property(e => e.IssueDate).HasColumnName("issue_date");

                entity.Property(e => e.ShipCertificateTypeId).HasColumnName("ship_certificate_type_id");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ShipCertificate)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_certificate_company_id_fkey");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ShipCertificate)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_certificate_country_id_fkey");

                entity.HasOne(d => d.ShipCertificateType)
                    .WithMany(p => p.ShipCertificateNavigation)
                    .HasForeignKey(d => d.ShipCertificateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_certificate_ship_certificate_type_id_fkey");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ShipCertificate)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_certificate_ship_id_fkey");
            });

            modelBuilder.Entity<ShipCertificateType>(entity =>
            {
                entity.ToTable("ship_certificate_type");

                entity.Property(e => e.ShipCertificateTypeId).HasColumnName("ship_certificate_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipCertificate).HasColumnName("ship_certificate");
            });

            modelBuilder.Entity<ShipContact>(entity =>
            {
                entity.ToTable("ship_contact");

                entity.HasIndex(e => e.ContactMediumId)
                    .HasName("ifk_rel_23");

                entity.HasIndex(e => e.ShipId)
                    .HasName("ifk_rel_26");

                entity.Property(e => e.ShipContactId).HasColumnName("ship_contact_id");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.ContactMediumId).HasColumnName("contact_medium_id");

                entity.Property(e => e.ContactValue).HasColumnName("contact_value");

                entity.Property(e => e.IsPreferred).HasColumnName("is_preferred");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.HasOne(d => d.ContactMedium)
                    .WithMany(p => p.ShipContact)
                    .HasForeignKey(d => d.ContactMediumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_contact_contact_medium_id_fkey");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ShipContact)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_contact_ship_id_fkey");
            });

            modelBuilder.Entity<ShipFlagCode>(entity =>
            {
                entity.ToTable("ship_flag_code");

                entity.HasIndex(e => e.CountryId)
                    .HasName("ifk_rel_35");

                entity.Property(e => e.ShipFlagCodeId).HasColumnName("ship_flag_code_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipFlagCode1)
                    .IsRequired()
                    .HasColumnName("ship_flag_code");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ShipFlagCode)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_flag_code_country_id_fkey");
            });

            modelBuilder.Entity<ShipHistory>(entity =>
            {
                entity.ToTable("ship_history");

                entity.HasIndex(e => e.ShipId)
                    .HasName("ifk_rel_18");

                entity.Property(e => e.ShipHistoryId).HasColumnName("ship_history_id");

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.CallSign).HasColumnName("call_sign");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.DeadweightTonnage).HasColumnName("deadweight_tonnage");

                entity.Property(e => e.Draught).HasColumnName("draught");

                entity.Property(e => e.GrossTonnage).HasColumnName("gross_tonnage");

                entity.Property(e => e.HasSideThrusters).HasColumnName("has_side_thrusters");

                entity.Property(e => e.HasSideThrustersBack).HasColumnName("has_side_thrusters_back");

                entity.Property(e => e.HasSideThurstersFront).HasColumnName("has_side_thursters_front");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImoNo).HasColumnName("imo_no");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.MmsiNo).HasColumnName("mmsi_no");

                entity.Property(e => e.Power).HasColumnName("power");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.Property(e => e.ShipBreadthTypeId).HasColumnName("ship_breadth_type_id");

                entity.Property(e => e.ShipFlagCodeId).HasColumnName("ship_flag_code_id");

                entity.Property(e => e.ShipHullTypeId).HasColumnName("ship_hull_type_id");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.Property(e => e.ShipLength).HasColumnName("ship_length");

                entity.Property(e => e.ShipLengthTypeId).HasColumnName("ship_length_type_id");

                entity.Property(e => e.ShipName)
                    .IsRequired()
                    .HasColumnName("ship_name");

                entity.Property(e => e.ShipPowerTypeId).HasColumnName("ship_power_type_id");

                entity.Property(e => e.ShipSourceId).HasColumnName("ship_source_id");

                entity.Property(e => e.ShipStatusId).HasColumnName("ship_status_id");

                entity.Property(e => e.ShipTypeId).HasColumnName("ship_type_id");

                entity.Property(e => e.ValidFromDate).HasColumnName("valid_from_date");

                entity.Property(e => e.ValidToDate).HasColumnName("valid_to_date");

                entity.Property(e => e.YearOfBuild).HasColumnName("year_of_build");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ShipHistory)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_history_ship_id_fkey");
            });

            modelBuilder.Entity<ShipHullType>(entity =>
            {
                entity.ToTable("ship_hull_type");

                entity.Property(e => e.ShipHullTypeId).HasColumnName("ship_hull_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipHullType1)
                    .IsRequired()
                    .HasColumnName("ship_hull_type");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipLengthType>(entity =>
            {
                entity.ToTable("ship_length_type");

                entity.Property(e => e.ShipLengthTypeId).HasColumnName("ship_length_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipLengthType1)
                    .IsRequired()
                    .HasColumnName("ship_length_type");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipMmsiMidCode>(entity =>
            {
                entity.ToTable("ship_mmsi_mid_code");

                entity.HasIndex(e => e.CountryId)
                    .HasName("ifk_rel_33");

                entity.Property(e => e.ShipMmsiMidCodeId).HasColumnName("ship_mmsi_mid_code_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.MidCode).HasColumnName("mid_code");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ShipMmsiMidCode)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_mmsi_mid_code_country_id_fkey");
            });

            modelBuilder.Entity<ShipPowerType>(entity =>
            {
                entity.ToTable("ship_power_type");

                entity.Property(e => e.ShipPowerTypeId).HasColumnName("ship_power_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipPowerType1)
                    .IsRequired()
                    .HasColumnName("ship_power_type");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipSource>(entity =>
            {
                entity.ToTable("ship_source");

                entity.Property(e => e.ShipSourceId).HasColumnName("ship_source_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipSource1)
                    .IsRequired()
                    .HasColumnName("ship_source");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipStatus>(entity =>
            {
                entity.ToTable("ship_status");

                entity.Property(e => e.ShipStatusId).HasColumnName("ship_status_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipStatus1)
                    .IsRequired()
                    .HasColumnName("ship_status");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.Entity<ShipType>(entity =>
            {
                entity.ToTable("ship_type");

                entity.HasIndex(e => e.ShipTypeGroupId)
                    .HasName("ifk_rel_16");

                entity.Property(e => e.ShipTypeId).HasColumnName("ship_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipType1).HasColumnName("ship_type");

                entity.Property(e => e.ShipTypeGroupId).HasColumnName("ship_type_group_id");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");

                entity.HasOne(d => d.ShipTypeGroup)
                    .WithMany(p => p.ShipType)
                    .HasForeignKey(d => d.ShipTypeGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ship_type_ship_type_group_id_fkey");
            });

            modelBuilder.Entity<ShipTypeGroup>(entity =>
            {
                entity.ToTable("ship_type_group");

                entity.Property(e => e.ShipTypeGroupId).HasColumnName("ship_type_group_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ShipTypeGroup1)
                    .IsRequired()
                    .HasColumnName("ship_type_group");

                entity.Property(e => e.ShipTypeGroupCode).HasColumnName("ship_type_group_code");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasColumnName("system_name");
            });

            modelBuilder.HasSequence("port_call_has_port_call_purpo_port_call_has_port_call_purpo_seq")
                .HasMin(1)
                .HasMax(2147483647);
        }
    }
}