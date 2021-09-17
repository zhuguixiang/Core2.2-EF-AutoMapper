using Microsoft.EntityFrameworkCore;

namespace AutoMapperEFCore.Model
{
    public class SqlDbContext : DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        public SqlDbContext(DbContextOptions options) : base(options)
        {
        }

        //数据库实体
        public DbSet<StudentInfo> StudentInfo { get; set; }


        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static SqlDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            var context = new SqlDbContext(optionsBuilder);
            return context;
        }
    }
}
