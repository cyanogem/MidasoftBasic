using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO
{
    public class CommentxPostDTO
    {
        public int ComentId { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BodyComment { get; set; }

        public int UserId { get; set; }
        public string Title { get; set; }
        public string BodyPost { get; set; }


    }
}
