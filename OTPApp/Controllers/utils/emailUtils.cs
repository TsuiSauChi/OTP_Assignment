using System;
using System.Security.Cryptography;

public class EmailUtils{
    public static bool checkEmailFormat(string user_email){
        // example email -> test@abc.dso.org.sg
        // potential issue -> .dso.org.sg.test@abc -> invalid email address but will still return true -> can be resolved in generate_OTP_email function status_email_fail
        string allowedString = ".dso.org.sg";

        if (!user_email.Contains(allowedString)) {
            // if does not contain the allowed domains
            return false;
        }
        return true;
    }

    public static string GenerateOTP(){
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[6];
            rng.GetBytes(randomBytes);
            int otpValue = BitConverter.ToInt32(randomBytes, 0) & 0x7FFFFFFF; // Ensure it's positive
            string otpString = (otpValue % 1000000).ToString("D6");

            return otpString;
        }
    }

    public static bool CompareOTP(string userOTP, string generatedOTP) {
        if (userOTP == generatedOTP) {
            return true;
        }
        else {
            return false;
        }
    }
}