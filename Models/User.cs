using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace FirstApplication.Models;

[Table("tblUserLogin")]
  public class User
  {
      public int UserId { get; set; }
      public int RoleId { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string EmailId { get; set; }
      public Boolean IsActive { get; set; }
      public DateTime? LastLogInDateTime { get; set; }

  }
