//using Codice.Client.BaseCommands.Config;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.Xml;
//using UnityEditor;
//using UnityEngine;

//namespace Game
//{
//    public class CsprojProcessor : AssetPostprocessor
//    {
//        /// <summary>
//        /// �����ɵ�C#��Ŀ�ļ�(.csproj)���д���
//        /// �ĵ�:https://learn.microsoft.com/zh-cn/visualstudio/gamedev/unity/extensibility/customize-project-files-created-by-vstu#%E6%A6%82%E8%A7%88
//        /// </summary>
//        public static string OnGeneratedCSProject(string path, string content)
//        {
//            return GenerateCustomProject(content);
//        }

//        /// <summary>
//        /// �Զ���C#��Ŀ����
//        /// �ο�����:
//        /// https://zhuanlan.zhihu.com/p/509046784
//        /// https://learn.microsoft.com/zh-cn/visualstudio/ide/reference/build-events-page-project-designer-csharp?view=vs-2022
//        /// https://learn.microsoft.com/zh-cn/visualstudio/ide/how-to-specify-build-events-csharp?view=vs-2022
//        /// </summary>
//        static string GenerateCustomProject(string content)
//        {
//            XmlDocument doc = new();
//            doc.LoadXml(content);
//            var newDoc = doc.Clone() as XmlDocument;
//            var rootNode = newDoc.GetElementsByTagName("Project")[0];

//            // ��ӷ���������
//            {
//                XmlElement itemGroup = newDoc.CreateElement("ItemGroup", newDoc.DocumentElement.NamespaceURI);
//                var projectReference = newDoc.CreateElement("ProjectReference", newDoc.DocumentElement.NamespaceURI);
//                projectReference.SetAttribute("Include", @"Tools\Analyzer\Analyzer.csproj");
//                projectReference.SetAttribute("OutputItemType", @"Analyzer");
//                projectReference.SetAttribute("ReferenceOutputAssembly", @"false");

//                var project = newDoc.CreateElement("Project", newDoc.DocumentElement.NamespaceURI);
//                project.InnerText = @"{d1f2986b-b296-4a2d-8f12-be9f470014c3}";
//                projectReference.AppendChild(project);

//                var name = newDoc.CreateElement("Name", newDoc.DocumentElement.NamespaceURI);
//                name.InnerText = "Analyzer";
//                projectReference.AppendChild(name);

//                itemGroup.AppendChild(projectReference);
//                rootNode.AppendChild(itemGroup);
//            }

//            // AfterBuild(�ַ����滻�������Ǳ�����Ƶ�CodeDir)
//            {
//                var target = newDoc.CreateElement("Target", newDoc.DocumentElement.NamespaceURI);
//                target.SetAttribute("Name", "AfterBuild");
//                rootNode.AppendChild(target);
//            }

//            using StringWriter sw = new();
//            using XmlTextWriter tx = new(sw);
//            tx.Formatting = Formatting.Indented;
//            newDoc.WriteTo(tx);
//            tx.Flush();
//            return sw.GetStringBuilder().ToString();
//        }
//    }
//}