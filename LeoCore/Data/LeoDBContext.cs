using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LeoCore.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LeoCore.Data.Models;

namespace LeoCore.Data  
{
    public class LeoDBContext : DbContext

    {
        public LeoDBContext(DbContextOptions<LeoDBContext> opt) : base(opt)
        {
            
        }

        public virtual DbSet<PERSON_FORMATION> PERSON_FORMATION { get; set; }

        public virtual DbSet<PERSON_QUESTIONNAIRE> PERSON_QUESTIONNAIRE { get; set; }

        public virtual DbSet<PERSON_QUESTIONNAIRE_ANSWER> PERSON_QUESTIONNAIRE_ANSWER { get; set; }

        public virtual DbSet<PERSON_TRAINING> PERSON_TRAINING { get; set; }

        public virtual DbSet<QUESTION> QUESTION { get; set; }

        public virtual DbSet<QUESTIONNAIRE> QUESTIONNAIRE { get; set; }

        public virtual DbSet<QUESTIONNAIRE_QUESTION> QUESTIONNAIRE_QUESTION { get; set; }

        public virtual DbSet<SPECIALISATION> SPECIALISATION { get; set; }

        public virtual DbSet<TRAINING> TRAINING { get; set; }

        public virtual DbSet<TRAINING_QUESTIONNNAIRE> TRAINING_QUESTIONNNAIRE { get; set; }

        public virtual DbSet<TRAINING_TRAINER> TRAINING_TRAINER { get; set; }

        public virtual DbSet<USERCODE> USERCODE { get; set; }

        public virtual DbSet<USERCODEGROUP> USERCODEGROUP{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Ruddi20231108 niet nodig want je doet dit al in Program.cs   builder.Services.AddDbContext<QuestionContext>

            // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseMySql("server=127.0.0.1:6603;database=LeoDB;user=root;password=gygsof-tavpe5-nEhpat");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci");
                //.HasCharSet("utf8mb4");


            modelBuilder.Entity<PERSON_FORMATION>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("PERSON_FORMATION");

                entity.Property(e => e.CERTIFICATE).HasColumnType("blob");
                entity.Property(e => e.FOLLOWED_OR_TEACHED).HasMaxLength(255);
                entity.Property(e => e.INTERN_OR_EXTERN).HasMaxLength(255);
                entity.Property(e => e.NAME_TRAINER).HasMaxLength(255);
                entity.Property(e => e.SUBJECT)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.TYPE_TRAINER).HasMaxLength(455);
            });

            modelBuilder.Entity<PERSON_QUESTIONNAIRE>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("PERSON_QUESTIONNAIRE");

                entity.HasIndex(e => new { e.ID, e.CLIENT_ID }, "ID_UNIQUE").IsUnique();

                entity.HasIndex(e => e.QUESTIONNAIRE_ID, "PQE_QEN_FK_idx");

                entity.HasIndex(e => e.TRAINING_ID, "PQE_TRA_FK_idx");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.CLIENT_ID)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.QUESTIONNAIRE).WithMany(p => p.PERSON_QUESTIONNAIREs)
                    .HasForeignKey(d => d.QUESTIONNAIRE_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PQE_QEN_FK");

                entity.HasOne(d => d.TRAINING).WithMany(p => p.PERSON_QUESTIONNAIREs)
                    .HasForeignKey(d => d.TRAINING_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PQE_TRA_FK");
            });

            modelBuilder.Entity<PERSON_QUESTIONNAIRE_ANSWER>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("PERSON_QUESTIONNAIRE_ANSWER");

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.HasIndex(e => e.PERSON_QUESTIONNAIRE_ID, "PQA_PQN_FK_idx");

                entity.HasIndex(e => e.QUESTION_ID, "PQA_QES_FK_idx");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.ANSWER_TEXT).HasMaxLength(255);
                entity.Property(e => e.PERSON_QUESTIONNAIRE_ANSWERcol).HasMaxLength(45);
                entity.Property(e => e.QTEXT_AS_WAS).HasMaxLength(255);
                entity.Property(e => e.QTYPEANSWER_AS_WAS)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.PERSON_QUESTIONNAIRE).WithMany(p => p.PERSON_QUESTIONNAIRE_ANSWERs)
                    .HasForeignKey(d => d.PERSON_QUESTIONNAIRE_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PQA_PQN_FK");

                entity.HasOne(d => d.QUESTION).WithMany(p => p.PERSON_QUESTIONNAIRE_ANSWERs)
                    .HasForeignKey(d => d.QUESTION_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PQA_QES_FK");
            });

            modelBuilder.Entity<PERSON_TRAINING>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("PERSON_TRAINING");

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.HasIndex(e => new { e.ID, e.TRAINING_ID, e.CLIENT_ID }, "PERSON_TRAINING_IDX");

                entity.HasIndex(e => e.TRAINING_ID, "PTR_TRA_FK_idx");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.CLIENT_ID)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.COMPLETED)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.RESULTSCORE).HasComment("getal");

                entity.HasOne(d => d.TRAINING).WithMany(p => p.PERSON_TRAININGs)
                    .HasForeignKey(d => d.TRAINING_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PTR_TRA_FK");
            });

            modelBuilder.Entity<QUESTION>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("QUESTION");

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.INFO).HasMaxLength(255);
                entity.Property(e => e.TEXT)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.TYPE_ANSWER)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();
            });

            modelBuilder.Entity<QUESTIONNAIRE>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("QUESTIONNAIRE");

                entity.HasIndex(e => e.ID, "QUESTIONNAIRE_IDX");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.DESCRIPTION)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.INFO).HasMaxLength(255);
                entity.Property(e => e.QUESTIONNAIREcol).HasMaxLength(45);
            });

            modelBuilder.Entity<QUESTIONNAIRE_QUESTION>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("QUESTIONNAIRE_QUESTION");

                entity.HasIndex(e => e.QUESTIONNAIRE_ID, "QEQ_QEN_FK_idx");

                entity.HasIndex(e => e.QUESTION_ID, "QEQ_QES_FK_idx");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.MANDATORY)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.QUESTIONNAIRE).WithMany(p => p.QUESTIONNAIRE_QUESTIONs)
                    .HasForeignKey(d => d.QUESTIONNAIRE_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("QEQ_QEN_FK");

                entity.HasOne(d => d.QUESTION).WithMany(p => p.QUESTIONNAIRE_QUESTIONs)
                    .HasForeignKey(d => d.QUESTION_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("QEQ_QES_FK");
            });

            modelBuilder.Entity<SPECIALISATION>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("SPECIALISATION", tb => tb.HasComment("		"));

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.DESCRIPTION)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<TRAINING>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("TRAINING", tb => tb.HasComment("	"));

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.APPLICANT_CLIENTID)
                    .HasMaxLength(255)
                    .HasComment("google account van de inbrenger/lesgever");
                entity.Property(e => e.COSTPRICE).HasMaxLength(100);
                entity.Property(e => e.EV_ANDERS_FT).HasMaxLength(255);
                entity.Property(e => e.EV_ANDERS_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.EV_AWS_INCO_SUBJ).HasMaxLength(100);
                entity.Property(e => e.EV_AWS_ONCO_SUBJ).HasMaxLength(100);
                entity.Property(e => e.EV_AWS_SUBJ).HasMaxLength(100);
                entity.Property(e => e.EV_AWS_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.EV_PERS_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.EV_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.EV_WW_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.EV_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.EV_ZG_COMPL)
                    .HasMaxLength(100)
                    .HasComment("usertable");
                entity.Property(e => e.EV_ZG_COMPL_SUBJ).HasMaxLength(100);
                entity.Property(e => e.EV_ZG_REFDOM)
                    .HasMaxLength(100)
                    .HasComment("usertable");
                entity.Property(e => e.EV_ZG_SUBJ)
                    .HasMaxLength(100)
                    .HasComment("usertable");
                entity.Property(e => e.EV_ZG_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.LINK)
                    .HasMaxLength(255)
                    .HasComment("flowsparks");
                entity.Property(e => e.LOCATION).HasMaxLength(100);
                entity.Property(e => e.LOCATION_ANDERS).HasMaxLength(100);
                entity.Property(e => e.METHODOLOGY)
                    .HasMaxLength(100)
                    .HasComment("usertable METH");
                entity.Property(e => e.NC_EVD_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.NC_EVD_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NC_KATZ_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.NC_KATZ_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NC_NOMC_SUBJ).HasMaxLength(255);
                entity.Property(e => e.NC_NOMC_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NC_ROL_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.NC_ROL_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NC_THUISZORG_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.NC_THUISZORG_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NC_VVD_SUBJECT).HasMaxLength(255);
                entity.Property(e => e.NC_VVD_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.NOMENCL_CONV_YN)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.QR).HasColumnType("blob");
                entity.Property(e => e.SUBJECT).HasMaxLength(255);
                entity.Property(e => e.TRAINER_EMAIL).HasMaxLength(255);
                entity.Property(e => e.TRAINER_INT_EXT)
                    .HasMaxLength(100)
                    .HasComment("usertable INTEXT");
                entity.Property(e => e.TRAINING_TYPE)
                    .HasMaxLength(100)
                    .HasComment("usertable TT");
                entity.Property(e => e.TRAINING_TYPE_FT)
                    .HasMaxLength(255)
                    .HasComment("vrije tekst bij andere");
            });

            modelBuilder.Entity<TRAINING_QUESTIONNNAIRE>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("TRAINING_QUESTIONNNAIRE");

                entity.HasIndex(e => e.QUESTIONNAIRE_ID, "TQE_QEN_FK_idx");

                entity.HasIndex(e => e.TRAINING_ID, "TQE_TRA_FK_idx");

                entity.HasIndex(e => new { e.ID, e.TRAINING_ID, e.QUESTIONNAIRE_ID }, "TRAINING_QUESTIONNNAIRE_IDX");

                entity.HasIndex(e => e.ID, "id_UNIQUE").IsUnique();

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.INFO).HasMaxLength(255);

                entity.HasOne(d => d.QUESTIONNAIRE).WithMany(p => p.TRAINING_QUESTIONNNAIREs)
                    .HasForeignKey(d => d.QUESTIONNAIRE_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TQE_QEN_FK");

                entity.HasOne(d => d.TRAINING).WithMany(p => p.TRAINING_QUESTIONNNAIREs)
                    .HasForeignKey(d => d.TRAINING_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TQE_TRA_FK");
            });

            modelBuilder.Entity<TRAINING_TRAINER>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("PRIMARY");

                entity.ToTable("TRAINING_TRAINER");

                entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();

                entity.HasIndex(e => new { e.ID, e.TRAINING_ID, e.TRAINER_ID }, "TRAINING_TRAINER_IDX");

                entity.HasIndex(e => e.TRAINING_ID, "TTR_TRA_FK_idx");

                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.CLIENT_ID).HasMaxLength(100);
                entity.Property(e => e.MANDATORY)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength();

                entity.HasOne(d => d.TRAINING).WithMany(p => p.TRAINING_TRAINERs)
                    .HasForeignKey(d => d.TRAINING_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TTR_TRA_FK");
            });

            modelBuilder.Entity<USERCODE>(entity =>
            {
                entity.HasKey(e => e.USERCODEID).HasName("PRIMARY");

                entity.ToTable("USERCODE");

                entity.HasIndex(e => e.USERCODEGROUPID, "FK_USERCODE_USERCODEGROUP_idx");

                entity.Property(e => e.USERCODEID).HasMaxLength(50);
                entity.Property(e => e.DEPENDENCYSET).HasMaxLength(200);
                entity.Property(e => e.DESCRIPTION)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PARENTUSERCODEGROUPID).HasMaxLength(15);
                entity.Property(e => e.PARENTUSERCODEID).HasMaxLength(15);
                entity.Property(e => e.REMARK).HasMaxLength(800);
                entity.Property(e => e.SOFTDELETED)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'0'")
                    .IsFixedLength();
                entity.Property(e => e.USERCODEGROUPID)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.USERCODEGROUP).WithMany(p => p.USERCODEs)
                    .HasForeignKey(d => d.USERCODEGROUPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERCODE_USERCODEGROUP");
            });

            modelBuilder.Entity<USERCODEGROUP>(entity =>
            {
                entity.HasKey(e => e.USERCODEGROUPID).HasName("PRIMARY");

                entity.ToTable("USERCODEGROUP");

                entity.Property(e => e.USERCODEGROUPID).HasMaxLength(15);
                entity.Property(e => e.DEPENDENCYSET).HasMaxLength(200);
                entity.Property(e => e.DESCRIPTION)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.REMARK).HasMaxLength(800);
                entity.Property(e => e.SOFTDELETED)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("'0'")
                    .IsFixedLength();
            });
          
        }
       
    }


}



