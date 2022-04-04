using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// This interface provides strongly typed access to MongoDB connection information.
    /// These 3 properties correspond to the 3 setting in StudentStoreDatabaseSettings in appsettings.json
    /// </summary>
    public interface IStudentStoreDatabaseSettings
    {
        string StudentCoursesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
