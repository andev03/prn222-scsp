using BusinessLogic.IServices;
using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ForumService : IForumService
    {
        private IForumRepository _repository;
        public ForumService(IForumRepository repository)
        {
            _repository = repository;
        }

        public void Create(ForumPost forumPost)
        {
            _repository.Create(forumPost);
        }

        public List<ForumPost> GetAll()
        {
            return _repository.getAll();
        }

        public ForumPost GetByPostId(int postId)
        {
            return _repository.getPostById(postId);
        }
    }
}
