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

        public void Delete(ForumPost post)
        {
            _repository.Delete(post);
        }

        public List<ForumPost> GetAll()
        {
            return _repository.GetAll();
        }

        public List<ForumPost> GetAllByUserId(Guid userId)
        {
            return _repository.GetAllByUserId(userId);
        }

        public ForumPost GetByPostId(int postId)
        {
            return _repository.getPostById(postId);
        }

        public void Update(ForumPost post)
        {
            _repository.Update(post);
        }
    }
}
