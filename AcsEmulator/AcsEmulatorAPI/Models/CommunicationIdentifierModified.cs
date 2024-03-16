using AcsEmulatorAPI.Endpoints.CallAutomation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace AcsEmulatorAPI.Models
{
    public abstract class CommunicationIdentifierModified
    {
        public string? RawId { get; set; }
        public CommunicationIdentifierModified() { }

    }

    public class CommunicationUserIdentifierModified: CommunicationIdentifierModified
    {
        public CommunicationUserIdentifierModified(string id)
        {
            this.Id = id;
            this.RawId = id;
        }
        public string Id { get; set; }
    }

    public class PhoneNumberIdentifierModified: CommunicationIdentifierModified
    {

        public PhoneNumberIdentifierModified(string value)
        {
            this.Value = value;
            this.RawId = $"4:{value}";
        }
        public string Value { get; set; }
    }
}
