using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Failmail.Web.ViewModels
{
    public class BucketViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "A bucket name may contain only alphanumeric characters, dashes and underscores.")]
        public string BucketName { get; set; }
    }
}