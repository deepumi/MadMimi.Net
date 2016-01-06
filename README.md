#MadMimi.Net
MadMimi DotNet Library. This library is pretty simple to use, All you need to create a MailMessage (Sysetm.Net.Mail) object and pass your API key and username. Alo, You can take advantage of the existing Smtp Mail message logic with out changing much code change you can integrate madmimi.net to your existing apps.

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
