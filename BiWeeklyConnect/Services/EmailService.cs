namespace BiWeeklyConnect.Services
{
    public class EmailService
    {
        private readonly IAmazonSimpleEmailService _ses;
        public EmailService(IAmazonSimpleEmailService ses)
        {
            _ses = ses;
        }
        public async Task SendEmailAsync(string to1, string to2, string body)
        {
            var sendRequest = new SendEmailRequest
            {
                Source = "srija.bandari10@gmail.com",
                Destination = new Destination
                {
                    ToAddresses = new List<string> { to1, to2 }
                },
                Message = new Message
                {
                    Subject = new Content(""),
                    Body = new Body
                    {
                        Text = new Content(body)
                    }
                }
            }
}
