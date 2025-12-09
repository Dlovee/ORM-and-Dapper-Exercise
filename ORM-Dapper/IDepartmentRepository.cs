using System;
using System.Collections.Generic;
namespace IntroSQL
{
    public interface IDepartmentRepository
    {
        // Saying we need a method called GetAllDepartments that returns a collection
        // That confirms to IEnumerable<T>
        public IEnumerable<Department> GetAllDepartments(); //Stubbed out method
    }
}