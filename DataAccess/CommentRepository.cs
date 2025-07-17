
using BusinessObject.Models;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentRepository : ICommentRepository
    {
        private readonly QuitSmokingAppDBContext _context;

        public CommentRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public void Create(ForumComment forumComment)
        {
            _context.Add(forumComment);
            _context.SaveChanges();
        }

        public List<ForumComment> getAllByPostId(int postId)
        {
            return _context.ForumComments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .ToList();
        }
    }
}
