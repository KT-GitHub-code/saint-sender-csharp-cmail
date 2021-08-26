using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.ObjectModel;

namespace SaintSender.Core.Models
{
    static public class Authentication
    {
        private static Account _account;

        public static Account Account { get => _account; }

        public static StatusCodes AuthenticateAccount(string email, string password)
        {
            if (email == "" || password == "")
            {
                return StatusCodes.auth_missingcred;
            }
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                try
                {
                    client.Authenticate(email, password);
                    _account = new Account(email, password);
                }
                catch (Exception e)
                {
                    if (e is MailKit.Security.AuthenticationException)
                    {
                        return StatusCodes.auth_invalidcred;
                    }
                    else if (e is System.Net.Sockets.SocketException)
                    {
                        return StatusCodes.auth_nonet;
                    }
                }
                return StatusCodes.auth_success;
            }
        }

        public static ObservableCollection<Email> GetInbox()
        {
            ObservableCollection<Email> emails = new ObservableCollection<Email>();
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(_account.Email, _account.Password);
                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                    Console.WriteLine(message.TextBody);
                }

                foreach (var email in inbox)
                {
                    string message = email.TextBody;
                    string sender = email.From.ToString();
                    DateTime date = email.Date.Date;
                    string subject = email.Subject;
                    bool read = false;

                    emails.Add(new Email(message, sender, date, subject, read));
                }
                emails.Reverse();

                client.Disconnect(true);
                return emails;
            }
        }
    }
}
