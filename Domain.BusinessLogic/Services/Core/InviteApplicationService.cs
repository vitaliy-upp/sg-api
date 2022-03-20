using AutoMapper;
using DataAccess.UserManagement;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Settings;
using Domain.DataAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MailSender.Interfaces;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.Services
{
    public class InviteApplicationService : BaseBusinessService, IInviteApplicationService
    {
        private readonly DataAccess.ServiceInterfaces.IUserRepository _userDomainService;
        private readonly IEmailSenderService _emailSenderService;

        private readonly IMapper _mapper;
        private readonly IAppUrlProvider _appUrlProviderApplicationService;
        private readonly EmailProviderSettings _emailProviderSettings;

        public InviteApplicationService(IHttpContextAccessor httpContextAccessor
            , IAppUrlProvider appUrlProviderApplicationService
            , DataAccess.ServiceInterfaces.IUserRepository userDomainService
            , IEmailSenderService emailSenderService
            , IConfiguration configuration
            , IMapper mapper
            ) : base(httpContextAccessor)
        {
            _userDomainService = userDomainService;
            _emailSenderService = emailSenderService;
            _appUrlProviderApplicationService = appUrlProviderApplicationService;

            _emailProviderSettings = configuration.GetSection(nameof(EmailProviderSettings)).Get<EmailProviderSettings>();

            _mapper = mapper;
        }

        public Task<InviteModel> FindByTokenAsync(string token)
        {
            throw new NotImplementedException();

        }

        public void Create(CreateInviteModel model)
        {
            AddNewInvitation(model, true);
            //var conf = _eventDomainService.GetById(model.EventId);
            //if (conf == null)
            //{
            //    throw new ArgumentException("Event was not found.");
            //}

            //if (CurrentUserRole != Enums.UserRolesEnum.SuperAdmin)
            //{
            //    var user = _userDomainService.GetByEmail(CurrentUserEmail);
            //    CheckUserPermission(conf, user.Id);
            //}

            //model.Participants = PreventInviteYourself(model.Participants);

            //var invites = _inviteToEventDomainService.GetByEventId(model.EventId);
            //var dbEmail = invites.Select(p => p.UserEmail).ToList();

            //var newEmails = model.Participants.Select(p => p.Email).ToList();
            //var toDelete = invites.Where(i => newEmails.Contains(i.UserEmail) == false).ToList();
            //var toAdd = model.Participants.Where(i => dbEmail.Contains(i.Email) == false)
            //    .Select(add => EntityFactory.CreateInvite(add.Email, conf.Id, conf.EndDateTime, add.Role))
            //    .ToList();

            //CheckAvailablePlacesOnEvent(conf, toAdd.Count);

            //_inviteToEventDomainService.Delete(toDelete.Select(x => x.Id).ToList());
            //_inviteToEventDomainService.CreateInvintations(toAdd);

            //SendEmailToNewInvited(toAdd, conf);
        }

        public void BulkUpload(BulkUploadInviteModel model)
        {
            using (TextReader reader = new System.IO.StreamReader(model.File.OpenReadStream()))
            {
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.ReadingExceptionOccurred = (exc) =>
                {
                    return false;
                };

                var records = csvReader.GetRecords<InviteCsvUploadModel>().ToList();

                var participants = records.Select(r => new EventParticipant() { Email = r.Email, Role = r.Role }).ToList();
                var createInviteModel = new CreateInviteModel() { EventId = model.EventId, Participants = participants };
                AddNewInvitation(createInviteModel);
            }

        }

        public InviteModel FindByToken(string token)
        {

            throw new NotImplementedException();
        }

        public InviteModel FindByUserEmailAndEventId(string email, int eventId)
        {

            throw new NotImplementedException();
        }

        public InviteModel FindByUserEmail(string email)
        {
            //var dbInvite = _inviteToEventDomainService.GetByUserEmail(email);
            //return _mapper.Map<InviteModel>(dbInvite);

            throw new NotImplementedException();
        }

        public IList<InviteModel> FindByEventId(int id)
        {

            throw new NotImplementedException();
        }

        public void Delete(int id)
        {

            throw new NotImplementedException();
        }

        #region PRIVATE METHODS

        private void AddNewInvitation(CreateInviteModel model, bool deleteUnspecified = false)
        {

            throw new NotImplementedException();
        }

        private void CheckUserPermission(int conferenceId, int userId)
        {

            throw new NotImplementedException();
        }

        private int FindUserByEmailOrDefault(string email)
        {
            //TODO: make is async
            var user = _userDomainService.GetByEmailAsync(email).Result;
            if (user != null)
            {
                return user.Id;
            }
            return 0;
        }

        private int GetAllowedSpeakers(User user)
        {
            if (user?.Company.SubscriptionPlan == null)
            {
                throw new ArgumentException("You can not invite peoples");
            }

            var speakersOnStage = user?.Company.SubscriptionPlan?.SubscriptionFeatures.FirstOrDefault(f => f.Feature.Key == FeatureKeyVariables.SpeakersOnStage)?.NumberValue;

            return (int)speakersOnStage.Value;
        }


        // TODO: make it normal
        private string NormalizeDescription(string description)
        {
            description = description.Replace(System.Environment.NewLine, "<br />");
            return description;
        }

      
        #endregion
    }
}
