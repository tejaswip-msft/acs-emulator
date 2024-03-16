using SQLitePCL;

namespace AcsEmulatorAPI.Models
{
    public class CommunicationPropertiesResult
    {
        public CommunicationPropertiesResult() { }

        public CommunicationUserIdentifierModified AnswerBy {  get; set; }
        public string CallConnectionId { get; set; }
        public CallConnectionState CallConnectionState { get; set;}
        public Uri CallbackUri { get; set; }
        public string CorrelationId { get; set; }
        public string ServerCallId { get; set;}
        public CommunicationUserIdentifierModified Source {  get; set; }
        public PhoneNumberIdentifierModified SourceCallerIdNumber { get; set; }
        public string SourceDisplayName { get; set; }
        public PhoneNumberIdentifierModified[] Targets { get; set; }
    }
}
