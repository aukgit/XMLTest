using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XMLTest {
    class Program {
        static List<T> GetXmlToList<T>(string xmlString, string typeName) where T : new() {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var xpath = typeName + "s/" + typeName;

            var nodes = xmlDoc.SelectNodes(xpath);
            if (nodes == null || nodes.Count == 0) {
                xpath = "ArrayOf" + typeName + "/" + typeName;
                nodes = xmlDoc.SelectNodes(xpath);
            }
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
        //static string GetListToXml<T>(T list, string typeName) {
        //    const BindingFlags typeOfPropertise = BindingFlags.Public | BindingFlags.Instance;
        //    var propertise =
        //                typeof(T)
        //                .GetProperties(typeOfPropertise)
        //                .ToList();
        //    var sb = new StringBuilder(propertise.Count + 2);
        //    var xml = new XElement(typeName + "s", );
        //}

        public static string GetListToXml2<T>(T obj) {
            var stringWriter = new StringWriter();
            var tempXml = new XmlSerializer(typeof(T));
            tempXml.Serialize(stringWriter, obj);
            var tempStr = stringWriter.ToString();
            return tempStr;
        }

        public class X {
            public List<Student> Students { get; set; }
        }
        static void Main(string[] args) {
            var fileName = @"test.xml";

            var xmlString = File.ReadAllText(fileName);
            var list = GetXmlToList<Student>(xmlString, "Student");
            Student.PrintList(list);

            //var x = new X() {
            //    Students = list
            //};
            var xml = GetListToXml2<List<Student>>(list);
            File.WriteAllText(fileName, xml);
            Console.WriteLine(xml);
            Console.ReadKey();
        }
    }
}
