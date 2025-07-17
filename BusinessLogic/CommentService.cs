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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public void AddComment(ForumComment forumComment)
        {
            _repository.Create(forumComment);
        }

        public List<ForumComment> GetAllByPostId(int postId)
        {
            return _repository.getAllByPostId(postId);
        }
    }
}
