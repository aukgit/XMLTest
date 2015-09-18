using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XMLTest {
    class Program {
        static List<T> GetXml<T>(string xmlString, string typeName) where T : new() {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            string xpath = typeName + "s/" + typeName;

            var nodes = xmlDoc.SelectNodes(xpath);
            if (nodes != null) {
                var list = new List<T>(nodes.Count);
                foreach (XmlNode childrenNodeContainer in nodes) {
                    var listItem = new T(); // creating a new instance of target object.
                    var childs = childrenNodeContainer.ChildNodes;
                    foreach (XmlElement child in childs) {
                        var propertyName = child.Name;

                        var propertyInfo = typeof(T).GetProperty(propertyName);
                        if (propertyInfo != null) {
                            object value = child.InnerText;
                            propertyInfo.SetValue(listItem, value, null);
                        }
                    }
                    list.Add(listItem);
                }
                return list;
            }
            return null;
        }
        static void Main(string[] args) {
            var xmlString = File.ReadAllText(@"test.xml");
            var list = GetXml<Student>(xmlString, "Student");
            Student.PrintList(list);
            Console.ReadKey();
        }
    }
}
