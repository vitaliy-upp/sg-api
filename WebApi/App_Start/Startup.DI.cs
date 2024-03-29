﻿using Microsoft.Extensions.DependencyInjection;
using PaymentStripe;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Services;
using Domain.DataAccess;
using Domain.DataAccess.ServiceInterfaces;
using Domain.DataAccess.Services;
using PaymentStripe.Interfaces;
using MailSender.Interfaces;
using MailSender.Services;
using Domain.Repository.Interfaces.KidProfile;
using Domain.Repository.Repository.KidProfile;
using FileManagement.Utilities;

namespace WebApi
{
    public partial class Startup
    {
        public void ConfigureDI(IServiceCollection services)
        {
            services.AddHttpContextAccessor();


            // Db Context
            services.AddTransient<IDomainDbContext, DomainDbContext>();


            // Repositories
            services.AddTransient<IChatMessagesDomainService, ChatMessagesDomainRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IUserCustomerRepository, UserCustomerRepository>();
            services.AddTransient<IUserRepository, UserDomainRepository>();
            services.AddTransient<IUserTokensDomainService, UserTokensDomainRepository>();
            services.AddTransient<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            services.AddTransient<ISocialLinkRepository, SocialLinkRepository>();
            services.AddTransient<IStripeProductInfoRepository, StripeProductInfoRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<IKidProfileRepository, KidProfileRepository>();
            services.AddTransient<ISuperPowerRepository, SuperPowerRepository>();
            services.AddTransient<IKidEducationRepository, KidEducationRepository>();


            // Application services
            services.AddTransient<IChattingApplicationService, ChattingApplicationService>();
            services.AddTransient<IInviteApplicationService, InviteApplicationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAttachmentService, AttachmentService>();

            services.AddTransient<IUserTokensApplicationService, UserTokensApplicationService>();
            //services.AddTransient<IStripeApplicationService, StripeApplicationService>();
            //services.AddTransient<ISubscriptionPlanApplicationService, SubscriptionPlanApplicationService>();
            services.AddTransient<IAppUrlProvider, AppUrlProvider>();
            services.AddTransient<IKidProfileService, KidProfileService>();
            services.AddTransient<ISuperPowerService, SuperPowerService>();
            services.AddTransient<IKidEducationService, KidEducationService>();
            services.AddTransient<IKidProfileService, KidProfileService>();


            // Email service
            services.AddTransient<IEmailSenderService, EmailSenderService>();


            // Factories
            services.AddSingleton<IStripeEventHandlerFacroty, StripeEventHandlerFacroty>();

            #region FileManagement 
            services.AddTransient<IFileManager, FileManager> ();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<IFileProvider, S3Provider>();
            #endregion

        }
    }
}
