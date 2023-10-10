using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTPApp.Data;
using OTPApp.Models;

namespace OTPApp.Controllers
{
    enum ResponseOTPStatusCode
    {
        STATUS_OTP_OK = 200,
        STATUS_OTP_FAIL = 500,
        STATUS_OTP_TIMEOUT = 408,
        STATUS_OTP_EXPIRED = 409,
        STATUS_OTP_ERROR = 404,
    }
    enum ResponseEmailStatusCode
    {
        STATUS_EMAIL_OK = 200,
        STATUS_EMAIL_FAIL = 500,
        STATUS_EMAIL_INVALID = 400,
    }

    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves Enrollment By Id
        /// </summary>
        [HttpGet("users")]
        public async Task<ActionResult<User>> GetAllUs()
        {
            var query = from user in _context.Users
                select new User{
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Otp = user.Otp,
                    Otpdate = user.Otpdate
                };

            return await query.FirstAsync(); 
        }

        [HttpPost("user/email")]
        public async Task<ActionResult<Object>> SendEmail(int id, string user_email){
            DateTime now = DateTime.Now;
            bool isValidEMail = EmailUtils.checkEmailFormat(user_email);

            if (isValidEMail) {
                // assume OTP is randomly generated via a function then stored in DB or temp DB etc
                string OTP = EmailUtils.GenerateOTP();
                string email_body = "You OTP Code is " + OTP + ". The code is valid for 1 minute";

                var modifiedUser = await _context.Users.FindAsync(id);
                modifiedUser.Otp = OTP;
                modifiedUser.Attempt = 0;
                modifiedUser.Otpdate = now;

                _context.Update<User>(modifiedUser);

                try
                {
                    await _context.SaveChangesAsync();
                    return email_body;
                } catch (DbUpdateConcurrencyException)
                {
                    return ResponseEmailStatusCode.STATUS_EMAIL_FAIL;
                }
            }
            return ResponseEmailStatusCode.STATUS_EMAIL_INVALID;
        }

        [HttpGet("user/checkotp")]
        public async Task<ActionResult<Object>> CheckOTP(int id, string otp){
            
            var modifiedUser = await _context.Users.FindAsync(id);
            var isValid = EmailUtils.CompareOTP(modifiedUser.Otp, otp);

            TimeSpan? timeDifferenceNullable = DateTime.Now - modifiedUser.Otpdate;
            TimeSpan timeDifference = timeDifferenceNullable ?? TimeSpan.Zero;
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            if (timeDifference > oneMinute){
                return ResponseOTPStatusCode.STATUS_OTP_TIMEOUT;
            }   

            if(isValid){
                if (modifiedUser.Attempt > 10){
                    return ResponseOTPStatusCode.STATUS_OTP_EXPIRED;
                }
                return ResponseOTPStatusCode.STATUS_OTP_OK;
            } else {
                modifiedUser.Attempt += 1;
                _context.Update<User>(modifiedUser);

                try
                {
                    await _context.SaveChangesAsync();
                    return ResponseOTPStatusCode.STATUS_OTP_FAIL;
                } catch (DbUpdateConcurrencyException)
                {
                    return ResponseOTPStatusCode.STATUS_OTP_ERROR;
                }
            }
        }
    }
}