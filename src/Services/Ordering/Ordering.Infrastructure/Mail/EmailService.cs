﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail {
    public class EmailService : IEmailService {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) {
            this.emailSettings = emailSettings.Value;
            this.logger = logger;
        }
        public async Task<bool> SendEmail(Email email) {
            var client = new SendGridClient(emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var body = email.Body;
            var from = new EmailAddress { Email = emailSettings.FromAddress, Name = emailSettings.FromName };

            var sendGridMsg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(sendGridMsg);

            logger.LogInformation("Email sent");
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted 
                || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            
            logger.LogError("Email sending failed");
            return false;
        }
    }
}
