namespace SwitchvoxAPI
{
    public partial class Users
    {
        private readonly SwitchvoxRequest request;

        public CallQueuesUsers CallQueues;

        internal Users(SwitchvoxRequest request)
        {
            this.request = request;

            CallQueues = new CallQueuesUsers(request);
        }
    }
}
