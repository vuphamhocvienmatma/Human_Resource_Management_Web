using Human_Resource_Management_Model.MongoClass;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace Human_Resource_Management_Model.HRM
{
    [BsonCollection("HR_Employee")]
    public class HR_Account : MongoDocument
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Tên không được để trống!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Họ không được để trống!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống!")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string Mobile1 { get; set; }

        public string Mobile2 { get; set; }

        public string AccountStatus { get; set; }

        public bool isAdmin { get; set; } //là quyền root hệ thống

        public bool IsDeleted { get; set; } = false;

        [Required(ErrorMessage = "Nhập ngày sinh!")]
        public DateTime BirthDay { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string OrganizationId { get; set; } // Thuộc đơn vị

        public string DutyId { get; set; } //Nghiệp vụ, chức vụ

        public string AvatarImagePath { get; set; }
    }


    public class HR_AccountViewModel
    {
        [Display(Name ="Mã cán bộ")]
        public Guid Id { get; set; }

        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Mobile1 { get; set; }

        [Display(Name = "Ngày sinh nhật")]
        public DateTime BirthDay { get; set; }
     
        [Display(Name = "Ảnh đại diện")]
        public string AvatarImagePath { get; set; }

        [Display(Name = "Chức vụ")]
        public string DutyId { get; set; } //Nghiệp vụ, chức vụ
    }


    public class HR_AccountEditModel 
    {
        [Display(Name = "Mã cán bộ")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Tên không được để trống!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Họ không được để trống!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống!")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string Mobile1 { get; set; }

        public string Mobile2 { get; set; }

        public string AccountStatus { get; set; }

        public bool isAdmin { get; set; } //là quyền root hệ thống
       
        [Required(ErrorMessage = "Nhập ngày sinh!")]
        public DateTime BirthDay { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string OrganizationId { get; set; } // Thuộc đơn vị

        public string DutyId { get; set; } //Nghiệp vụ, chức vụ

        public IFormFile AvatarImage { get; set; }

        public string AvatarImagePath { get; set; }
    }

    public class AuthModel
    {
        [Required(ErrorMessage = "Nhập tài khoản")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string Password { get; set; } = "";

        public bool isRemember { get; set; } = true;
    }

    
}