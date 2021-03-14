namespace DestinyLib.DataContract
{
    using System.Data;

    public interface IParseDataRecord<T>
    {
        T Parse(IDataRecord record);
    }
}
