//namespace DALEF
//{
//    public class ManagerDALEF
//    {
//        private readonly string _connectionString;
//        public ManagerDALEF(string connectionString)
//        {
//            _connectionString = connectionString;
//        }
//        public List<Manager> GetAllManager()
//        {
//            using (var context = new CourseProjectContext(_connectionString))
//            {
//                var managers = context.tblManagers.Include();
//            }
//        }
//    }
//}
