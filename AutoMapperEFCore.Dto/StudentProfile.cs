using AutoMapper;
using AutoMapperEFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperEFCore.Dto
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentInfo, StudentDto>();
        }
    }
}
