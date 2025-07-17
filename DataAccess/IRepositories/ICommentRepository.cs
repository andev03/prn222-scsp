using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ICommentRepository
    {
        List<ForumComment> getAllByPostId(int postId);

        void Create(ForumComment forumComment);
    }
}
