﻿public class User
{
    public int ID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Student"; // Student eller Admin
}
