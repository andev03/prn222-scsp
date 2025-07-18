using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IForumRepository
    {
        List<ForumPost> getAll();

        ForumPost getPostById(int postId);

        void Create(ForumPost forumPost);
        List<ForumPost> GetAllByUserId(Guid userId);
        void Update(ForumPost post);
        void Delete(ForumPost post);
    }
}
