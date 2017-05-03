namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.Users namespace.
    /// </summary>
    public partial class Users
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Methods contained in the Switchvox.Users.CallQueues namespace.
        /// </summary>
        public CallQueuesUsers CallQueues;

        internal Users(SwitchvoxClient client)
        {
            this.client = client;

            CallQueues = new CallQueuesUsers(client);
        }
    }
}
