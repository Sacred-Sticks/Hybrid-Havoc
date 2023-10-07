namespace Kickstarter.Progression
{
    public interface ISerializable
    {
        public string Serialize();
        public bool Deserialize(string serializedData);
    }
}
