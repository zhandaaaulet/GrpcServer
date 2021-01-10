using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSE1Server.Services
{
    public class StudentService : Student.StudentBase
    {
        private readonly ILogger<StudentService> _logger;
        private readonly List<StudentInfo> students = new List<StudentInfo>
        {
            new StudentInfo { User = new UserInfo { Id = 1, Name = "Bake", Surname = "Balenbayev", Gender = true}, Gpa = 2.7},
            new StudentInfo { User = new UserInfo { Id = 2, Name = "Sake", Surname = "Tugenbayev", Gender = true}, Gpa = 3.1},
            new StudentInfo { User = new UserInfo { Id = 3, Name = "Dake", Surname = "Myrkynbayev", Gender = true}, Gpa = 1.8},
            new StudentInfo { User = new UserInfo { Id = 4, Name = "Make", Surname = "Sailaubayev", Gender = true}, Gpa = 3.9},
            new StudentInfo { User = new UserInfo { Id = 5, Name = "Balengul", Surname = "Sarsenbayeva", Gender = false}, Gpa = 3.3}
        };

        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }

        public override Task<StudentInfo> GetStudent(StudentLookup request, ServerCallContext context)
        {
            var student = students.Where(x => x.User.Id == request.Id).FirstOrDefault();
            return Task.FromResult(student);
        }

        public override async Task GetAllStudents(AllStudentsLookup request, 
            IServerStreamWriter<StudentInfo> responseStream, 
            ServerCallContext context)
        {
            foreach (var student in students)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(student);
            }
        }
    }
}
