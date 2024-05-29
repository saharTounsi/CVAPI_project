using System.Net;
using System.Net.Mail;
using CVAPI.Services;
using CVAPI.Models;


namespace Services {
    public class MailService {

        private SmtpClient smtpClient;
        private MailAddress fromAddress;

        public MailService(){
            smtpClient=new SmtpClient("smtp.gmail.com"){
                Port=587,
                EnableSsl=true,
                Credentials=new NetworkCredential(
                    "sahar.tounsi@etudiant-fsegt.utm.tn",
                    "xrse mmwe xsyh shrk"
                ),
            };
            fromAddress=new MailAddress("saharcvapi@no-reply.com","SaharCVAPI");
        }

        public async Task sendUserPassword(string email,string password){
            await this.sendMail(new(){
                subject="Login Credentials",
                toEmail=email,
                body=$@"
                    <p>Your login credentials</p>
                    <ul>
                        <li>Email: {email}</li>
                        <li>Password: {password}</li>
                    </ul>
                ",
                asHTML=true,
            });
        }

        public async Task<string> sendLoginOTP(string toEmail){
            string otp=new Random().nextString(5);
            MailInfo mailinfo=getLoginOTPMailInfo(otp,toEmail);
            await this.sendMail(mailinfo);
            return otp;
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

        private MailInfo getLoginOTPMailInfo(string otp,string toEmail){
            return new MailInfo(){
                toEmail=toEmail,
                subject="Your Login OTP",
                body=$@"
                    <p>This is your login OTP: {otp}</p>
                ",
                asHTML=true,
            };
        }

        private string getPasswordResetPMailBody(VerificationLink link){
            string ipaddress=NetworkService.getLocalIPAddress();
            return $@"
                <a 
                    href='{link.toURL()}' target='_blank'
                    style='text-decoration:none;color:#262626;box-sizing:border-box;display:flex;width:fit-content;padding:8px 16px;background:linear-gradient(180deg,rgba(255,255,255,0.13) 0%,rgba(17,184,15,0.1) 100%),#ffffff;border:1px solid rgba(75,173,58,0.6);border-radius:4px' 
                >Reset Password</a>
            ";
        }
    }
}
