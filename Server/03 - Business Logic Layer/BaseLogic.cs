using System;

namespace Cool
{
    public abstract class BaseLogic : IDisposable
    {
        protected readonly NorthwindContext DB;
        public BaseLogic(NorthwindContext db)
        {
            DB = db;
        }
        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
