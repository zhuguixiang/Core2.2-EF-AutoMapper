using System;

namespace AutoMapperEFCore.Dto
{
    public class StudentDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
