using System;

using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

namespace Hyun
{
    using Extensions;

    public class Serialization
    {

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                // 2. Construct a SurrogateSelector object
                SurrogateSelector ss = new SurrogateSelector();
                Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
                Vector2SerializationSurrogate v2ss = new Vector2SerializationSurrogate();
                ss.AddSurrogate(typeof(Vector3),
                                new StreamingContext(StreamingContextStates.All),
                                v3ss);
                ss.AddSurrogate(typeof(Vector2),
                                new StreamingContext(StreamingContextStates.All),
                                v2ss);
                // 5. Have the formatter use our surrogate selector
                formatter.SurrogateSelector = ss;

                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static void Compile<T>(T obj, string writePath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = null;
            try
            {
                // 2. Construct a SurrogateSelector object
                SurrogateSelector ss = new SurrogateSelector();
                Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
                Vector2SerializationSurrogate v2ss = new Vector2SerializationSurrogate();
                ss.AddSurrogate(typeof(Vector3),
                                new StreamingContext(StreamingContextStates.All),
                                v3ss);
                ss.AddSurrogate(typeof(Vector2),
                                new StreamingContext(StreamingContextStates.All),
                                v2ss);
                // 5. Have the formatter use our surrogate selector
                bf.SurrogateSelector = ss;

                file = File.Create(writePath);
                if (file != null)
                {
                    bf.Serialize(file, obj);
                }

            }
            catch (Exception ex)
            {
                Hyun.Debug.Log.Exception(ex);

            }
            finally
            {
                file.Close();
            }

        }

        /// <summary>
        /// 버퍼 로 부터 디컴파일한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static T DeccompileFromBuffer<T>(byte[] buffer)
            where T : class
        {
            T ret = null;


            try
            {

                BinaryFormatter bf = new BinaryFormatter();
                // 2. Construct a SurrogateSelector object
                SurrogateSelector ss = new SurrogateSelector();
                Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
                Vector2SerializationSurrogate v2ss = new Vector2SerializationSurrogate();
                ss.AddSurrogate(typeof(Vector3),
                                new StreamingContext(StreamingContextStates.All),
                                v3ss);
                ss.AddSurrogate(typeof(Vector2),
                                new StreamingContext(StreamingContextStates.All),
                                v2ss);
                // 5. Have the formatter use our surrogate selector
                bf.SurrogateSelector = ss;



                using (MemoryStream reader = new MemoryStream(buffer))

                {
                    ret = (T)bf.Deserialize(reader);

                }




            }
            catch (Exception ex)
            {
                Hyun.Debug.Log.Exception(ex);
            }

            return ret;

        }

        public static T DecompileFromPath<T>(string readPath)
            where T : class
        {
            T ret = null;

            //if (File.Exists(readPath))
            try
            {

                BinaryFormatter bf = new BinaryFormatter();
                // 2. Construct a SurrogateSelector object
                SurrogateSelector ss = new SurrogateSelector();
                Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
                Vector2SerializationSurrogate v2ss = new Vector2SerializationSurrogate();
                ss.AddSurrogate(typeof(Vector3),
                                new StreamingContext(StreamingContextStates.All),
                                v3ss);
                ss.AddSurrogate(typeof(Vector2),
                                new StreamingContext(StreamingContextStates.All),
                                v2ss);
                // 5. Have the formatter use our surrogate selector
                bf.SurrogateSelector = ss;

                if (Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
                {

                    WWW reader = new WWW(readPath);
                    while (!reader.isDone) { }

                    using (MemoryStream stream = new MemoryStream(reader.bytes))
                    {
                        ret = (T)bf.Deserialize(stream);
                    }

                }
                else
                {
                    using (StreamReader reader = new StreamReader(readPath))
                    {

                        ret = (T)bf.Deserialize(reader.BaseStream);
                    }


                }



            }
            catch (Exception ex)
            {
                Hyun.Debug.Log.Exception(ex);
            }

            return ret;
        }

    }
}