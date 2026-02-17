using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    //Discuss about the use of interfaces in C#, the naming convension......
    public interface IStudentData
    {
        List<FirstYearStudent> Load();
        void Save(List<FirstYearStudent> students);
    }
}
