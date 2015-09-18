using System;
using System.IO;
using System.Xml;

namespace XMLTest {
    class Program {
        static void Main(string[] args) {
            var xmlString = File.ReadAllText(@"test.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            string xpath = "Students/Student";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNodeContainer in nodes) {
                var childs = childrenNodeContainer.ChildNodes;
                Console.WriteLine();
                foreach (XmlElement child in childs) {
                    Console.WriteLine(child.Name + " : " + child.InnerText);
                }
            } 
            Console.ReadKey();
        }
    }
}
