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
    public class ForumRepository : IForumRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public ForumRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public void Create(ForumPost forumPost)
        {
            _context.ForumPosts.Add(forumPost);
            _context.SaveChanges();
        }

        public List<ForumPost> getAll()
        {
            return _context.ForumPosts
                .Where(fp => fp.IsDeleted != true)
                .Include(fp => fp.User)
                .Include(fp => fp.ForumComments)
                .ToList();
        }

        public List<ForumPost> GetAllByUserId(Guid userId)
        {
            return _context.ForumPosts
                .Where(fp => fp.UserId == userId && fp.IsDeleted != true)
                .Include(fp => fp.User)
                .ToList();
        }

        public ForumPost getPostById(int postId)
        {
            return _context.ForumPosts
                .Include(fp => fp.User)
                .FirstOrDefault(fp => fp.PostId == postId);
        }

        public void Delete(ForumPost post)
        {
            _context.Update(post);
            _context.SaveChanges();
        }

        public void Update(ForumPost post)
        {
            _context.Update(post);
            _context.SaveChanges();
        }
    }
}
