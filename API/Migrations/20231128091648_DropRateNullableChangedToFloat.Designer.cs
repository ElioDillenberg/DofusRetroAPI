﻿// <auto-generated />
using System;
using DofusRetroAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DofusRetroAPI.Migrations
{
    [DbContext(typeof(DofusRetroDbContext))]
    [Migration("20231128091648_DropRateNullableChangedToFloat")]
    partial class DropRateNullableChangedToFloat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("BaseLocalizedTextSequence");

            modelBuilder.Entity("DofusRetroAPI.Entities.Drops.Drop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DropCap")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MonsterId")
                        .HasColumnType("int");

                    b.Property<int?>("ProspectionThreshold")
                        .HasColumnType("int");

                    b.Property<float?>("Rate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("MonsterId");

                    b.ToTable("Drops");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Conditions.ItemCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConditionSign")
                        .HasColumnType("int");

                    b.Property<int>("ConditionType")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemConditions");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Effects.ItemEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EffectType")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MaxValue")
                        .HasColumnType("int");

                    b.Property<int>("MinValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemEffects");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.PetEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ImprovedMax")
                        .HasColumnType("int");

                    b.Property<int>("ItemEffectId")
                        .HasColumnType("int");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemEffectId");

                    b.HasIndex("PetId");

                    b.ToTable("PetEffects");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.PetFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EffectIncrease")
                        .HasColumnType("int");

                    b.Property<int>("PetEffectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetEffectId");

                    b.ToTable("PetFoods");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PetFood");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Weapons.WeaponCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionPoints")
                        .HasColumnType("int");

                    b.Property<int>("CriticalStrikeBaseChance")
                        .HasColumnType("int");

                    b.Property<int>("CriticalStrikeBonus")
                        .HasColumnType("int");

                    b.Property<int>("MaxRange")
                        .HasColumnType("int");

                    b.Property<int>("MinRange")
                        .HasColumnType("int");

                    b.Property<bool>("OneHand")
                        .HasColumnType("bit");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeaponId")
                        .IsUnique();

                    b.ToTable("WeaponCharacteristic");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Image")
                        .HasColumnType("int");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("Pods")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.ItemDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemDescriptions");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Localization.BaseLocalizedText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [BaseLocalizedTextSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.Monster", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Breed")
                        .HasColumnType("int");

                    b.Property<int>("Ecosystem")
                        .HasColumnType("int");

                    b.Property<int?>("Image")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedMonsterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("RelatedMonsterId");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.MonsterCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionPointAvoidance")
                        .HasColumnType("int");

                    b.Property<int>("ActionPoints")
                        .HasColumnType("int");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("AirResistancePercentage")
                        .HasColumnType("int");

                    b.Property<int>("Chance")
                        .HasColumnType("int");

                    b.Property<int>("EarthResistancePercentage")
                        .HasColumnType("int");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int>("FireResistancePercentage")
                        .HasColumnType("int");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int");

                    b.Property<int>("Initiative")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MonsterId")
                        .HasColumnType("int");

                    b.Property<int>("MovementPointAvoidance")
                        .HasColumnType("int");

                    b.Property<int>("MovementPoints")
                        .HasColumnType("int");

                    b.Property<int>("NeutralResistancePercentage")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("WaterResistancePercentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MonsterId");

                    b.ToTable("MonsterCharacteristics");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Recipes.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Recipes.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.Set", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetBonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumberOfItems")
                        .HasColumnType("int");

                    b.Property<int>("SetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("SetBonuses");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetBonusEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Max")
                        .HasColumnType("int");

                    b.Property<int>("Min")
                        .HasColumnType("int");

                    b.Property<int>("SetBonusId")
                        .HasColumnType("int");

                    b.Property<int>("StatType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SetBonusId");

                    b.ToTable("SetBonusEffect");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.ResourceEaterFood", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Equipments.Pets.PetFood");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasIndex("PetId");

                    b.HasIndex("ResourceId");

                    b.HasDiscriminator().HasValue("ResourceEaterFood");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.SoulEaterFood", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Equipments.Pets.PetFood");

                    b.Property<int>("MonsterId")
                        .HasColumnType("int");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.HasIndex("MonsterId");

                    b.HasIndex("PetId");

                    b.ToTable("PetFoods", t =>
                        {
                            t.Property("PetId")
                                .HasColumnName("SoulEaterFood_PetId");
                        });

                    b.HasDiscriminator().HasValue("SoulEaterFood");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Cards.Card", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Item");

                    b.Property<int>("CardFamily")
                        .HasColumnType("int");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Consumables.Consumable", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Item");

                    b.ToTable("Consumables");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Equipment", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Item");

                    b.Property<int?>("SetId")
                        .HasColumnType("int");

                    b.HasIndex("SetId");

                    b.ToTable((string)null);
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Resources.Resource", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Item");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Conditions.ItemConditionText", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Localization.BaseLocalizedText");

                    b.Property<int>("ItemConditionId")
                        .HasColumnType("int");

                    b.HasIndex("ItemConditionId");

                    b.ToTable("ItemConditionTexts");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Effects.ItemEffectTypeText", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Localization.BaseLocalizedText");

                    b.Property<int>("EffectType")
                        .HasColumnType("int");

                    b.ToTable("ItemEffectTypeTexts");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.ItemName", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Localization.BaseLocalizedText");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemNames");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.MonsterName", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Localization.BaseLocalizedText");

                    b.Property<int>("MonsterId")
                        .HasColumnType("int");

                    b.HasIndex("MonsterId");

                    b.ToTable("MonsterNames");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetName", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Localization.BaseLocalizedText");

                    b.Property<int>("SetId")
                        .HasColumnType("int");

                    b.HasIndex("SetId");

                    b.ToTable("SetNames");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Gear.Gear", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Equipments.Equipment");

                    b.ToTable("Gears");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.Pet", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Equipments.Equipment");

                    b.Property<bool>("SoulEater")
                        .HasColumnType("bit");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Weapons.Weapon", b =>
                {
                    b.HasBaseType("DofusRetroAPI.Entities.Items.Equipments.Equipment");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Drops.Drop", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany("Drops")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DofusRetroAPI.Entities.Monsters.Monster", "Monster")
                        .WithMany()
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Monster");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Conditions.ItemCondition", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany("Conditions")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Effects.ItemEffect", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany("Effects")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.PetEffect", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Effects.ItemEffect", "Effect")
                        .WithMany()
                        .HasForeignKey("ItemEffectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DofusRetroAPI.Entities.Items.Equipments.Pets.Pet", null)
                        .WithMany("PetEffects")
                        .HasForeignKey("PetId");

                    b.Navigation("Effect");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.PetFood", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Equipments.Pets.PetEffect", "PetEffect")
                        .WithMany()
                        .HasForeignKey("PetEffectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PetEffect");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Weapons.WeaponCharacteristic", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Equipments.Weapons.Weapon", "Weapon")
                        .WithOne("WeaponCharacteristic")
                        .HasForeignKey("DofusRetroAPI.Entities.Items.Equipments.Weapons.WeaponCharacteristic", "WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.ItemDescription", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany("Descriptions")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.Monster", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Monsters.Monster", "RelatedMonster")
                        .WithMany()
                        .HasForeignKey("RelatedMonsterId");

                    b.Navigation("RelatedMonster");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.MonsterCharacteristic", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Monsters.Monster", "Monster")
                        .WithMany("Characteristics")
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monster");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Recipes.Ingredient", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DofusRetroAPI.Entities.Recipes.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Recipes.Recipe", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithOne("Recipe")
                        .HasForeignKey("DofusRetroAPI.Entities.Recipes.Recipe", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetBonus", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Sets.Set", "Set")
                        .WithMany("SetBonuses")
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Set");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetBonusEffect", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Sets.SetBonus", "SetBonus")
                        .WithMany("SetBonusEffects")
                        .HasForeignKey("SetBonusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SetBonus");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.ResourceEaterFood", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Equipments.Pets.Pet", null)
                        .WithMany("ResourceFoodTable")
                        .HasForeignKey("PetId");

                    b.HasOne("DofusRetroAPI.Entities.Items.Resources.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.SoulEaterFood", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Monsters.Monster", "Monster")
                        .WithMany()
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DofusRetroAPI.Entities.Items.Equipments.Pets.Pet", null)
                        .WithMany("MonsterFoodTable")
                        .HasForeignKey("PetId");

                    b.Navigation("Monster");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Equipment", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Sets.Set", "Set")
                        .WithMany("Equipments")
                        .HasForeignKey("SetId");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Conditions.ItemConditionText", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Conditions.ItemCondition", "ItemCondition")
                        .WithMany("ConditionTexts")
                        .HasForeignKey("ItemConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemCondition");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.ItemName", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Items.Item", "Item")
                        .WithMany("Names")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.MonsterName", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Monsters.Monster", "Monster")
                        .WithMany("MonsterNames")
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monster");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetName", b =>
                {
                    b.HasOne("DofusRetroAPI.Entities.Sets.Set", "Set")
                        .WithMany("SetNames")
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Set");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Conditions.ItemCondition", b =>
                {
                    b.Navigation("ConditionTexts");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Item", b =>
                {
                    b.Navigation("Conditions");

                    b.Navigation("Descriptions");

                    b.Navigation("Drops");

                    b.Navigation("Effects");

                    b.Navigation("Names");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Monsters.Monster", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("MonsterNames");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Recipes.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.Set", b =>
                {
                    b.Navigation("Equipments");

                    b.Navigation("SetBonuses");

                    b.Navigation("SetNames");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Sets.SetBonus", b =>
                {
                    b.Navigation("SetBonusEffects");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Pets.Pet", b =>
                {
                    b.Navigation("MonsterFoodTable");

                    b.Navigation("PetEffects");

                    b.Navigation("ResourceFoodTable");
                });

            modelBuilder.Entity("DofusRetroAPI.Entities.Items.Equipments.Weapons.Weapon", b =>
                {
                    b.Navigation("WeaponCharacteristic");
                });
#pragma warning restore 612, 618
        }
    }
}
