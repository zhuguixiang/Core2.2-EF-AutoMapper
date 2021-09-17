using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoMapperEFCore.Model
{
    [Table("STUDENTINFO")]
    public class StudentInfo
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("AGE")]
        public int Age { get; set; }

        [Column("GENDER")]
        public string Gender { get; set; }

        [Column("BIRTHDAY")]
        public DateTime BirthDay { get; set; } = DateTime.Now;
    }
}
