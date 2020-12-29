using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class BlogPost
    {
        public BlogPost()
        {
            Tags = new List<BlogTag>();
        }
        public List<BlogTag> Tags {get; set;}
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public DateTime Created { get; set; }
        public DateTime Published { get; set; }
        public bool IsDraft { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public BlogCategory Category { get; set; }
    }
}
