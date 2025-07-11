using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class AuthValidator
{
    public static bool IsValidUsername(string username)
    {
        if(string.IsNullOrWhiteSpace(username)) return false;
        // Username: 3–16 ký tự, chỉ gồm a-z, A-Z, 0-9 và _
        string pattern = @"^[a-zA-Z0-9_]{4,16}$";
        return Regex.IsMatch(username, pattern);
    }
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        // Tối thiểu 8 ký tự, ít nhất 1 chữ hoa và 1 ký tự đặc biệt, không chứa unicode hay khoảng trắng
        string pattern = @"^(?=.*[A-Z])(?=.*[!@#$%^&*()\[\]{}\-_+=:;,.<>?/])[a-zA-Z0-9!@#$%^&*()\[\]{}\-_+=:;,.<>?/]{8,20}$";
        return Regex.IsMatch(password, pattern);
    }
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        // Chỉ chấp nhận email có dạng xxx@gmail.com
        string pattern = @"^[^@\s]+@gmail\.com$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    public static bool IsValidConfirmPassword(string mainPass, string confirmPass)
    {
        return string.Equals(mainPass, confirmPass);
    }

}
