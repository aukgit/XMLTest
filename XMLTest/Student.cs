using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLTest {
    [Serializable]
    [XmlType("Student")]
    public class Student {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Location")]
        public string Location { get; set; }

        public static void PrintList(List<Student> students) {
            foreach (var student in students) {
                Console.WriteLine("Name : " + student.Name + " , Loc : " + student.Location);
            }
        }
    }
}
