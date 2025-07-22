using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IForumService
    {
        public List<ForumPost> GetAll();
        ForumPost GetByPostId(int postId);

        void Create(ForumPost forumPost);
        List<ForumPost> GetAllByUserId(Guid userId);
        void Update(ForumPost post);
        void Delete(ForumPost post);
    }
}
