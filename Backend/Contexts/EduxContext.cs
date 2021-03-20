using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EduxProject.Domains;

namespace EduxProject.Contexts
{
    public partial class EduxContext : DbContext
    {
        public EduxContext()
        {
        }

        public EduxContext(DbContextOptions<EduxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlunoTurma> AlunoTurma { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Curtida> Curtida { get; set; }
        public virtual DbSet<Dica> Dica { get; set; }
        public virtual DbSet<Instituicao> Instituicao { get; set; }
        public virtual DbSet<Objetivo> Objetivo { get; set; }
        public virtual DbSet<ObjetivoAluno> ObjetivoAluno { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<ProfessorTurma> ProfessorTurma { get; set; }
        public virtual DbSet<Ranking> Ranking { get; set; }
        public virtual DbSet<Turma> Turma { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog=Edux; User Id=sa; Password=sa132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoTurma>(entity =>
            {
                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.AlunoTurma)
                    .HasForeignKey(d => d.IdTurma)
                    .HasConstraintName("FK__AlunoTurm__IdTur__5165187F");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.AlunoTurma)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__AlunoTurm__IdUsu__5070F446");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Tipo)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.Property(e => e.Titulo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.Curso)
                    .HasForeignKey(d => d.IdInstituicao)
                    .HasConstraintName("FK__Curso__IdInstitu__398D8EEE");
            });

            modelBuilder.Entity<Curtida>(entity =>
            {
                entity.HasOne(d => d.IdDicaNavigation)
                    .WithMany(p => p.Curtida)
                    .HasForeignKey(d => d.IdDica)
                    .HasConstraintName("FK__Curtida__IdDica__4CA06362");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Curtida)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Curtida__IdUsuar__4D94879B");
            });

            modelBuilder.Entity<Dica>(entity =>
            {
                entity.Property(e => e.Imagem)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Texto)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Dica)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Dica__IdUsuario__49C3F6B7");
            });

            modelBuilder.Entity<Instituicao>(entity =>
            {
                entity.Property(e => e.Bairro)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .HasColumnName("UF")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Objetivo>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Objetivo)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Objetivo__IdCate__412EB0B6");
            });

            modelBuilder.Entity<ObjetivoAluno>(entity =>
            {
                entity.Property(e => e.DataAlcancado).HasColumnType("datetime");

                entity.Property(e => e.Nota).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdAlunoTurmaNavigation)
                    .WithMany(p => p.ObjetivoAluno)
                    .HasForeignKey(d => d.IdAlunoTurma)
                    .HasConstraintName("FK__ObjetivoA__IdAlu__59FA5E80");

                entity.HasOne(d => d.IdObjetivoNavigation)
                    .WithMany(p => p.ObjetivoAluno)
                    .HasForeignKey(d => d.IdObjetivo)
                    .HasConstraintName("FK__ObjetivoA__IdObj__5AEE82B9");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.Property(e => e.Permissao)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfessorTurma>(entity =>
            {
                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.ProfessorTurma)
                    .HasForeignKey(d => d.IdTurma)
                    .HasConstraintName("FK__Professor__IdTur__5535A963");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ProfessorTurma)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Professor__IdUsu__5441852A");
            });

            modelBuilder.Entity<Ranking>(entity =>
            {
                entity.Property(e => e.NomeAluno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NotaTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ObjetivosOculto).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.Ranking)
                    .HasForeignKey(d => d.IdTurma)
                    .HasConstraintName("FK__Ranking__IdTurma__6477ECF3");
            });

            modelBuilder.Entity<Turma>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Turma)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK__Turma__IdCurso__3C69FB99");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataUltimoAcesso).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Imagem)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK__Usuario__IdPerfi__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
