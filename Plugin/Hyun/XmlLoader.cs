using System;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using System.IO;
using System.ComponentModel;


namespace Hyun
{
    public class XmlLoader 
    {
        public static void serializer_UnknownNode(object _sender, XmlNodeEventArgs _e)
        {
            Console.WriteLine("Unknown Node:" + _e.Name + "\t" + _e.Text);


        }

        public static void serializer_UnknownAttribute(object _sender, XmlAttributeEventArgs _e)
        {
            System.Xml.XmlAttribute attr = _e.Attr;
            Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }

        public static void serializer_UnknownElement(object _sender, XmlElementEventArgs _e)
        {
            System.Xml.XmlElement elem = _e.Element;
            Console.WriteLine("Unknown Element:" + elem.Name + "='" + elem.Value + "'");
        }

        public static void Serialize<T>(T o, string writePath) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);

            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);

            using (TextWriter writer = new StreamWriter(writePath))
            {
                serializer.Serialize(writer, o);
            }
        }

        public static T DeserializeFromBuffer<T>(string text) where T : class
        {
            T pRet = null;

            if (string.IsNullOrEmpty(text))
                return pRet;

            using (System.IO.StringReader reader = new System.IO.StringReader(text))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);

                    serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

                    serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);


                    pRet = serializer.Deserialize(reader) as T;
                }
                catch (Exception ex)
                {
                    Hyun.Debug.Log.Exception(ex);
                }
                finally
                {

                }

            }

            return pRet;

        }
        /// <summary>
        /// 파일 로 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string path) where T : class
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return DeserializeFromBuffer<T>(reader.ReadToEnd());
            }

        }
    }
}