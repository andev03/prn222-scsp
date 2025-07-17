using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface ICommentService
    {
        List<ForumComment> GetAllByPostId(int postId);

        void AddComment(ForumComment forumComment);
    }
}
