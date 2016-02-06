namespace SwitchvoxAPI
{
    public partial class Users
    {
        private readonly SwitchvoxRequest request;

        public CallQueues CallQueues;

        internal Users(SwitchvoxRequest request)
        {
            this.request = request;

            CallQueues = new CallQueues(request);
        }
    }
}
