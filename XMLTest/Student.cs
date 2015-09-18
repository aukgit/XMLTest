using System;
using System.Collections.Generic;

namespace XMLTest {
    class Student {
        public string Name { get; set; }
        public string Location { get; set; }

        public static void PrintList(List<Student> students) {
            foreach (var student in students) {
                Console.WriteLine("Name : " + student.Name + " , Loc : " + student.Location);
            }
        }
    }
}
