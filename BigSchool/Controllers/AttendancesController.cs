﻿using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeID == userID && a.CourseID == attendanceDto.CourseID))
                return BadRequest("The Attendance already exist!");
            var attendance = new Attendance
            {
                CourseID = attendanceDto.CourseID,
                AttendeeID = userID
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok(attendance);
        }
    }
}
