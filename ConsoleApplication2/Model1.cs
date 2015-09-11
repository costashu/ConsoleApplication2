namespace ConsoleApplication2
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=Model1")
		{
		}

		public virtual DbSet<dbSetProperty> dbSetProperty { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<dbSetProperty>()
				.Property(e => e.Property)
				.IsUnicode(false);
		}
	}
}
