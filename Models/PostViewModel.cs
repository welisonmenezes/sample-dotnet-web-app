using System;

namespace Trilha_Jr_1.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserID { get; set; }
    }
}