using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ForumApp.Infrastructure.Constants;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ForumApp.Infrastructure.Data.Models
{
    public class Post
    {
        [Comment("Post Identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Post Title")]
        [Required]
        [MinLength(ValidationConstants.MinLengthTitle)]
        [MaxLength(ValidationConstants.MaxLengthTitle)]
        public string Title { get; set; } = null!;

        [Comment("Post Content")]
        [Required]
        [MinLength(ValidationConstants.MinLengthContent)]
        [MaxLength(ValidationConstants.MaxLengthContent)]
        public string Content { get; set; } = null!;
    }
}
