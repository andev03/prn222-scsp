using BusinessLogic.IService;
using BusinessLogic.IServices;
using DataAccess;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class DenpendenceInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IUserBadgeRepository, UserBadgeRepository>();
            services.AddScoped<ISmokingRecordRepository, SmokingRecordRepository>();
            services.AddScoped<IUserRepostitory, UserRepostitory>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IQuitPlanRepository, QuitPlanRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<IUserBadgeService, UserBadgeService>();
            services.AddScoped<ISmokingRecordService, SmokingRecordService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IQuitPlanService, QuitPlanService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            return services;
        }
    }
}
