namespace MKidz.Models.Database
{
    public class RecordsFunctions : IRecordsFunctions
    {
        private readonly RecordsDBContext _context;

        public RecordsFunctions(RecordsDBContext context)
        {
            _context = context;
        }
        public void Add(Records record)
        {
            _context.Records.Add(record);
            _context.SaveChanges();
        }

        public bool AlreadyExist(string record)
        {
            return _context.Records.Any(r => r.AudioName == record);
        }

        public List<Records> GetAll()
        {
           return _context.Records.ToList();
        }

        public void Update(string record)
        {
            var obj = _context.Records.SingleOrDefault(r => r.AudioName == record);
            if (obj != null)
            {
                obj.AudioCount += 1;
                _context.SaveChanges();
            }
        }
    }
}
