#MadMimi.Net
MadMimi DotNet Library. This library is pretty simple to use, All you need to do is create a MailMessage (Sysetm.Net.Mail) object and pass your API key and Username. Also, Using this library you should be able to easily integrate your apps/projects without breaking your existing SMPT logic.

#Usage
```csharp
 var mailer = new Mailer("username", "apikey");
  using (var message = new MailMessage())
  {
      message.From = new MailAddress("from email", "display name");
      message.To.Add("sender email");
      message.Subject = "Test";
      message.Body = "<h1>This is test</h1>";
      await mailer.SendAsync("test", message);
  }
  var status = mailer.TrackStatus;
  var mailTransactionId = mailer.TransactionId;
```
