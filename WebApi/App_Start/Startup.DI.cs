using Microsoft.Extensions.DependencyInjection;
using PaymentStripe;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Services;
using Domain.DataAccess;
using Domain.DataAccess.ServiceInterfaces;
using Domain.DataAccess.Services;
using PaymentStripe.Interfaces;
using MailSender.Interfaces;
using MailSender.Services;

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
            services.AddTransient<IChatMessagesDomainService, ChatMessagesDomainService>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IRoleDomainService, RoleDomainService>();
            services.AddTransient<IUserCustomerRepository, UserCustomerRepository>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddTransient<IUserTokensDomainService, UserTokensDomainService>();
            services.AddTransient<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            services.AddTransient<ISocialLinkRepository, SocialLinkRepository>();
            services.AddTransient<IStripeProductInfoRepository, StripeProductInfoRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();


            // Application services
            services.AddTransient<IChattingApplicationService, ChattingApplicationService>();
            services.AddTransient<IInviteApplicationService, InviteApplicationService>();
            services.AddTransient<IUserApplicationService, UserApplicationService>();
            services.AddTransient<IMediaApplicationService, MediaApplicationService>();

            services.AddTransient<IUserTokensApplicationService, UserTokensApplicationService>();
            services.AddTransient<IStripeApplicationService, StripeApplicationService>();
            services.AddTransient<ISubscriptionPlanApplicationService, SubscriptionPlanApplicationService>();
            services.AddTransient<IAppUrlProviderApplicationService, AppUrlProviderApplicationService>();


            // Email service
            services.AddTransient<IEmailSenderService, EmailSenderService>();


            // Factories
            services.AddSingleton<IStripeEventHandlerFacroty, StripeEventHandlerFacroty>();
        }
    }
}
