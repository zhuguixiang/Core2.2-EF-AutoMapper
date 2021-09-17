using AutoMapper;
using AutoMapperEFCore.Common;
using AutoMapperEFCore.Core;
using AutoMapperEFCore.Dto;
using AutoMapperEFCore.Model;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperEFCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentManage _studentManage;

        private IMapper _mapper;

        public StudentController(StudentManage student,
            IMapper mapper)
        {
            _studentManage = student;
            _mapper = mapper;
        }

        [HttpPost("GetStudentLit")]
        public ActionResult<List<StudentDto>> GetStudentLit()
        {
            var list = _studentManage.GetStudentList();
            if (list.Any() == false)
                return new ActionResult<List<StudentDto>>(new List<StudentDto>());

            var result = _mapper.Map<List<StudentInfo>, List<StudentDto>>(list);

            LogService.Instance.Debug("结束");

            return new ActionResult<List<StudentDto>>(result);
        }
    }
}
