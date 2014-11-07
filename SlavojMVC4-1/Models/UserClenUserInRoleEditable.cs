
namespace SlavojMVC4_1.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;
    using Foolproof;

    public class UserClenUserInRoleEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int UserInRoleId { get; set; }

        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int UserId { get; set; }


        [Display(Name = "Role")]
        [UIHint("GridForeignKey"), Required]
        [Range(1, int.MaxValue, ErrorMessage = "Role musí být vyplněna.")]
        public int RoleId { get; set; }
    }
}