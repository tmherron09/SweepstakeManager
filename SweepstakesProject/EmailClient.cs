using System.IO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SweepstakesProject
{
    /// <summary>
    /// Class encapsulating Marketing Email functions. Contained as a Static class to ensure a constant client email connection untill all outside entities have sent their email. Class is initialized by InitializeClient() and closed with EndEmailBlast() to ensure a clean disconnect and disposal of used resources of the MailKil Client.
    /// </summary>
    /// <remarks>Implemented with MimeKit</remarks>
    public static class EmailClient
    {
        /// <summary>
        /// Name of Marketing Firm, used in the FROM: header in Emails.
        /// </summary>
        static string marketingFirmName;
        /// <summary>
        /// Email Address of Employee Logging in. Email is cleared once EndEmailBlast is called.
        /// </summary>
        static string emailAddress;
        /// <summary>
        /// Password to log into email server. Password is immediately overwritten when EndEmailBlast is called.
        /// </summary>
        static string password;
        /// <summary>
        /// MailKit API Client to connect to Gmail server with an account that has less secure apps enabled.
        /// </summary>
        static SmtpClient client;
        /// <summary>
        /// Name of the next sweepstakes to add into email.
        /// </summary>
        static string nextSweepstakes;
        /// <summary>
        /// Disclaimer to add to top of email given this is a student project.
        /// </summary>
        static string EMAILDISCLAIMER = "*NOTICE* You are recieving this email from a student project in Gadolinium. Your email was guessed up given the standard organizational email of 'first name'@organization.com. If you are not one of my instructors, my apologies. As an aside, this Email was sent using a gmail address with less secure apps enabled. 0Auth 2.0 via a console, doable but not as of this stage. Thank you for your time.";

        /// <summary>
        /// Gathers the information needed for a new Email to be sent from a Marketing Firm.
        /// </summary>
        /// <param name="marketingFirmName">Name of Marketing Firm sending Email</param>
        /// <param name="emailAddress">Address Email will be sent from.</param>
        /// <param name="password">Password for unsecured email.</param>
        public static void InitializeEmailClient(string marketingFirmName, string emailAddress, string password)
        {
            EmailClient.marketingFirmName = marketingFirmName;
            EmailClient.emailAddress = emailAddress;
            EmailClient.password = password;
            EmailClient.nextSweepstakes = "*To Be Announced*"; // Default if no Next Sweepstakes is found.
            InitializeClient();
        }
        /// <summary>
        /// Creates the connection to the Gmail server. Keep connection open until EndEmailBlast is called.
        /// </summary>
        private static void InitializeClient()
        {
            client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            client.Authenticate(emailAddress, password);
        }
        /// <summary>
        /// Securly closes the Email Client connection and erases email address and password.
        /// </summary>
        public static void EndEmailBlast()
        {
            client.Disconnect(true);
            client.Dispose();
            emailAddress = null;
            password = "3fiaoifhkhuoisdfRANDOMGARBAGE";
        }
        /// <summary>
        /// Method to send an email to all Contestants who did not win the Sweepstakes. Sends Default Next Sweepstakes in email body.
        /// </summary>
        /// <param name="sweepstakesName">Name of Sweepstakes Ending</param>
        /// <param name="winner">Winning Contestant</param>
        /// <param name="contestantEmailAddress">Recieving contestant's email address</param>
        /// <param name="firstName">Recieving contestant's first name.</param>
        /// <param name="lastName">Recieving contestant's last name</param>
        public static void EndOfSweepstakes(string sweepstakesName, Contestant winner, string contestantEmailAddress, string firstName, string lastName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(marketingFirmName, emailAddress));
            message.To.Add(new MailboxAddress($"{firstName} {lastName}", contestantEmailAddress));
            message.Subject = $"Gadolinium- {sweepstakesName} has finished! Announcing...";

            TextPart body = new TextPart("plain")
            {
                Text = $"{EMAILDISCLAIMER}\n\nHey {firstName},\n   We would like to send out a big thank you to everyone who participated in the {sweepstakesName} this year!\n\nAnd a very big round of applause for {winner.FirstName} {winner.LastName[0]}., our big winner of a {sweepstakesName} Prize Package!\n\nThank you again to everyone who joined us. Please join us next time for out next big Sweepstakes, {nextSweepstakes}!\n\nSincerly, our team here at {marketingFirmName}"
            };

            //Commented out if Attachment has copied from source.
            //MimePart attachment = new MimePart("image", "png")
            //{
            //    Content = new MimeContent(File.OpenRead("timsy.png"), ContentEncoding.Default),
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName("timsy.png")
            //};

            Multipart multipart = new Multipart("mixed");
            multipart.Add(body);
            //multipart.Add(attachment);

            message.Body = multipart;

            client.Send(message);
        }
        /// <summary>
        /// Method to send an email to all Contestants who did not win the Sweepstakes.
        /// </summary>
        /// <param name="sweepstakesName">Name of Sweepstakes Ending</param>
        /// <param name="winner">Winning Contestant</param>
        /// <param name="contestantEmailAddress">Recieving contestant's email address</param>
        /// <param name="firstName">Recieving contestant's first name.</param>
        /// <param name="lastName">Recieving contestant's last name</param>
        /// <param name="nextSweepstakes">Name of Next Sweepstakes in Sweepstakes Manager</param>
        public static void EndOfSweepstakes(string sweepstakesName, Contestant winner, string contestantEmailAddress, string firstName, string lastName, string nextSweepstakes)
        {
            // Creats the Email Message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(marketingFirmName, emailAddress));
            message.To.Add(new MailboxAddress($"{firstName} {lastName}", contestantEmailAddress));
            message.Subject = $"Gadolinium- {sweepstakesName} has finished! Announcing...";

            // Creates a plain text email.
            TextPart body = new TextPart("plain")
            {
                Text = $"{EMAILDISCLAIMER}\n\nHey {firstName},\n   We would like to send out a big thank you to everyone who participated in the {sweepstakesName} this year!\n\nAnd a very big round of applause for {winner.FirstName} {winner.LastName[0]}., our big winner of a {sweepstakesName} Prize Package!\n\nThank you again to everyone who joined us. Please join us next time for out next big Sweepstakes, {nextSweepstakes}!\n\nSincerly, our team here at {marketingFirmName}"
            };

            // Adds local attachment.
            //Commented out if Attachment has copied from source.
            //MimePart attachment = new MimePart("image", "png")
            //{
            //    Content = new MimeContent(File.OpenRead("timsy.png"), ContentEncoding.Default),
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName("timsy.png")
            //};

            Multipart multipart = new Multipart("mixed");
            multipart.Add(body);
            //multipart.Add(attachment);

            message.Body = multipart;

            client.Send(message);
        }
        /// <summary>
        /// Email sent from Marketing Firm directly to Winner of a Sweepstakes.
        /// </summary>
        /// <param name="sweepstakesName">Name of Won Sweepstakes</param>
        /// <param name="winner">Winning Contestant</param>
        public static void WinnerOfSweepstakes(string sweepstakesName, Contestant winner)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(marketingFirmName, emailAddress));
            message.To.Add(new MailboxAddress($"{winner.FirstName} {winner.LastName}", winner.EmailAddress));
            message.Subject = $"Gadolinium- CONGRATS! You've WON {sweepstakesName}!";

            TextPart body = new TextPart("plain")
            {
                Text = $"{EMAILDISCLAIMER}\n\nHey {winner.FirstName},\n   We would like to send out a big thank you, to YOU! You have just been picked as our winner of the {sweepstakesName}!\nYou will be the proud new owner of your very own custom winning email. Why recieve a generic email, when you can get one saying, \"You're a Winner!\" You've already claimed your prize, just by reading this email. Congrats!\n\nAnd we here at {marketingFirmName} would like to give a very big round of applause for all our participants!\n\nThank you again to everyone who joined us. Please join us next time for out next bigsweepstakes, {nextSweepstakes}!\n\nSincerly, our team here at {marketingFirmName}"

            };
            MimePart attachment = new MimePart("image", "png")
            {
                Content = new MimeContent(File.OpenRead("timsy.png"), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName("timsy.png")
            };
            Multipart multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            client.Send(message);
        }
        /// <summary>
        /// Email sent from Marketing Firm directly to Winner of a Sweepstakes.
        /// </summary>
        /// <param name="sweepstakesName">Name of Won Sweepstakes</param>
        /// <param name="winner">Winning Contestant</param>
        /// <param name="nextSweepstakes">Name of Next Sweepstakes in Sweepstakes Manager</param>
        public static void WinnerOfSweepstakes(string sweepstakesName, Contestant winner, string nextSweepstakes)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(marketingFirmName, emailAddress));
            message.To.Add(new MailboxAddress($"{winner.FirstName} {winner.LastName}", winner.EmailAddress));
            message.Subject = $"Gadolinium- CONGRATS! You've WON {sweepstakesName}!";

            TextPart body = new TextPart("plain")
            {
                Text = $"{EMAILDISCLAIMER}\n\nHey {winner.FirstName},\n   We would like to send out a big thank you, to YOU! You have just been picked as our winner of the {sweepstakesName}!\nYou will be the proud new owner of your very own custom winning email. Why recieve a generic email, when you can get one saying, \"You're a Winner!\" You've already claimed your prize, just by reading this email. Congrats!\n\nAnd we here at {marketingFirmName} would like to give a very big round of applause for all our participants!\n\nThank you again to everyone who joined us. Please join us next time for out next bigsweepstakes, {nextSweepstakes}!\n\nSincerly, our team here at {marketingFirmName}"

            };
            MimePart attachment = new MimePart("image", "png")
            {
                Content = new MimeContent(File.OpenRead("timsy.png"), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName("timsy.png")
            };
            Multipart multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            client.Send(message);
        }
        /// <summary>
        /// Method to directly send a self constructed MimeMessage.
        /// </summary>
        /// <param name="message"></param>
        public static void SendEmail(MimeMessage message)
        {
            client.Send(message);
        }

    }


}

