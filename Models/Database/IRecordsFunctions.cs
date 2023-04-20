namespace MKidz.Models.Database
{
    public interface IRecordsFunctions
    {
        void Add(Records record);
        bool AlreadyExist(string record);
        void Update(string record);
        List<Records> GetAll();
    }
}
