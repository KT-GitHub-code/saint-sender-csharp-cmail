﻿using MailKit;
using System.Windows;
using SaintSender.Core.Models;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SaintSender.Core.Models.SaintSender.Core.Models;

namespace SaintSender.DesktopUI.ViewModels
{
    class InboxViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Email> _emails;

        public string test { get; set; }
        public Email SelectedEmail { get; set; }

        public ObservableCollection<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Emails)));
            }
        }

        public InboxViewModel()
        {
            //Emails = new ObservableCollection<Email>();
            Emails = Authentication.GetInbox();
            //IMailFolder inbox = Authentication.GetInbox();

            //this.Emails.Add(new Email("lorem ipsum dolor sit amet", "mr.sender", DateTime.Now, "serious subject", false));
            //this.Emails.Add(new Email("stuff yooooooooooooooooooooooo", "ms.sender", DateTime.Now, "lol", false));
            //this.Emails.Add(new Email("lorem stuff", "mr.sender", DateTime.Now, "serious subject", false));

            //this.Emails.Add(new Email(inbox.ToString(), "mr.inbox", DateTime.Now, "inbox subject", false));
            //this.Emails.Add(new Email(inbox.Attributes.ToString() , "mr.inbox", DateTime.Now, "inbox subject", false));
            //this.Emails.Add(new Email(inbox.Count.ToString(), "mr.inbox", DateTime.Now, "inbox subject", false));
            //this.Emails.Add(new Email(inbox.Name, "mr.inbox", DateTime.Now, "inbox subject", false));
            
            //this.Emails.Add(new Email(inbox., "mr.inbox", DateTime.Now, "inbox subject", false));

            //for (int i = 0; i < inbox.Count; i++)
            //{
            //    string message = inbox.GetMessage(i).TextBody;
            //    string sender = inbox.GetMessage(i).From.ToString();
            //    DateTime date = inbox.GetMessage(i).Date.Date;
            //    string subject = inbox.GetMessage(i).Subject;
            //    bool read = false;

            //    this.Emails.Add(new Email(message, sender, date, subject, read));
            //}

            //foreach (var email in inbox)
            //{
            //    string message = email.TextBody;
            //    string sender = email.From.ToString();
            //    DateTime date = email.Date.Date;
            //    string subject = email.Subject;
            //    bool read = false;

            //    this.Emails.Add(new Email(message, sender, date, subject, read));
            //}


        }

        internal void OpenDetails()
        {
            // hardcoded email
            //SelectedEmail = Emails[0];

            Details details = new Details(SelectedEmail);
            details.Show();
        }

        internal void OpenSendEmailWindow()
        {
            SendEmailWindow sendEmailWindow = new SendEmailWindow();
            sendEmailWindow.Show();
        }

        public void ForgetAccount()
        {
            Isolate.DeleteFromIsolatedStorage();
        }
    }
}
