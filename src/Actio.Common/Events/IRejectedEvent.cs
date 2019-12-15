namespace Actio.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        /// <summary>
        /// Why the failure does happened
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// Error code
        /// </summary>
        string Code { get; }
    }
}