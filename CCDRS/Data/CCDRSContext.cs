﻿/*
    Copyright 2022 University of Toronto
    This file is part of CCDRS.
    CCDRS is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    CCDRS is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with CCDRS.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using CCDRS.Model;
using Microsoft.EntityFrameworkCore;
namespace CCDRS.Data;

public partial class CCDRSContext : DbContext
{
    public CCDRSContext()
    {
    }

    public CCDRSContext(DbContextOptions<CCDRSContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Allow pages to access the Direction class as a service.
    /// </summary>
    public virtual DbSet<Direction> Directions { get; set; }

    /// <summary>
    /// Allow pages to access Region class as a service.
    /// </summary>
    public virtual DbSet<Region> Regions { get; set; }

    /// <summary>
    /// Allow pages to access VehicleCountType class as a service.
    /// </summary>
    public virtual DbSet<VehicleCountType> VehicleCountTypes { get; set; }

    /// <summary>
    /// Allow pages to access survey class as a service.
    /// </summary>
    public virtual DbSet<Survey> Surveys { get; set; }

    /// <summary>
    /// Allow pages to access station class as a service.
    /// </summary>
    public virtual DbSet<Station> Stations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("direction_pkey");

            entity.ToTable("direction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(1)
                .HasColumnName("abbreviation");
            entity.Property(e => e.Compass).HasColumnName("compass");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_pkey");

            entity.ToTable("region");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_pkey");

            entity.ToTable("vehicle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<VehicleCountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_count_type_pkey");

            entity.ToTable("vehicle_count_type");

            entity.HasIndex(e => new { e.VehicleId, e.Occupancy }, "vehicle_count_type_vehicle_id_occupancy_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountType).HasColumnName("count_type");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Occupancy).HasColumnName("occupancy");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("survey_pkey");

            entity.ToTable("survey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("station_pkey");

            entity.ToTable("station");

            entity.HasIndex(e => new { e.RegionId, e.StationCode }, "station_region_id_station_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Direction)
                .HasMaxLength(1)
                .HasColumnName("direction");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.StationCode).HasColumnName("station_code");
            entity.Property(e => e.StationNum).HasColumnName("station_num");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
