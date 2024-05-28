using System.Net;
using System.Net.Mail;
using System.Net.Mime;


namespace Services {
    public class MailService {

        private SmtpClient smtpClient;
        private MailAddress fromAddress;

        public MailService(){
            smtpClient=new SmtpClient("smtp.gmail.com"){
                Port=587,
                EnableSsl=true,
                Credentials=new NetworkCredential(
                    "ahmed.ayachi@etudiant-fst.utm.tn",
                    "yroz gjcz fxhf anxr"
                ),
            };
            fromAddress=new MailAddress("saharcvapi@no-reply.com","SaharCVAPI");
        }

        public string sendLoginVerificationCode(){
            throw new NotImplementedException();
        }

        public string sendPasswordResetLink(){
            throw new NotImplementedException();
        }

        public async Task<bool> sendMail(MailInfo info){
            MailAddress address=new(info.toEmail);
            MailMessage mail=new(this.fromAddress,address){
                Subject=info.subject,
                SubjectEncoding=System.Text.Encoding.UTF8,
                Body=info.body,
                BodyEncoding=System.Text.Encoding.UTF8,
                IsBodyHtml=info.asHTML,
            };
            //try{
                await smtpClient.SendMailAsync(mail);
                return true;
            /* }
            catch{
                return false;
            } */
        }

        public class MailInfo {
            public string toEmail {get;set;}
            public string? subject {get;set;}
            public string body {get;set;}
            public bool asHTML {get;set;}=false;
        }
    }
}
