using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;


namespace Human_Resource_Management_Model.HRM
{
    [BsonIgnoreExtraElements]
    public class HR_Account
    {
        [BsonId]
        public string Id { get; set; }

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

        public string AvatarImg { get; set; }
    }
}
