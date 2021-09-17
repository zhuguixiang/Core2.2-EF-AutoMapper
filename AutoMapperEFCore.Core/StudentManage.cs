using AutoMapperEFCore.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapperEFCore.Core
{
    public class StudentManage
    {

        //使用CreateContext()，则不需要注入SqlDbContext
        //private readonly SqlDbContext _context;

        //public StudentManage(SqlDbContext context)
        //{
        //    _context = context;
        //}

        public List<StudentInfo> GetStudentList()
        {
            //return _context.StudentInfo.Where(x => x.Gender == "男")
            //           .AsNoTracking()
            //           .ToList();

            using (var db = SqlDbContext.CreateContext())
            {
                return db.StudentInfo.Where(x => x.Gender == "男")
                       .AsNoTracking()
                       .ToList();
            }
        }
    }
}
