﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace gioiasApi.Models
{
    public partial class Student
    {
        [Key]
        public int StudentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string Roll { get; set; }
    }
}