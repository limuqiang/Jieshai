namespace Jieshai.Web
{
    internal class UserLoginResult
    {
        private object loginInvalid;

        public UserLoginResult(object loginInvalid)
        {
            this.loginInvalid = loginInvalid;
        }

        public string Message { get; set; }
    }
}