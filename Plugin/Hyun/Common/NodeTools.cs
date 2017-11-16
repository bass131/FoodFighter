using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

using UnityEngine;

namespace Hyun
{
    using Extensions;

    public static class NodeTools
    {
        public static void AddEventListener<T>(ref Action<T> self, Action<T> adder)
        {
            self -= adder;
            self += adder;
        }
        public static void RemoveEventListener<T>(ref Action<T> self, Action<T> removee)
        {
            self -= removee;
        }
        public static void AddEventListener<T, U>(ref Action<T, U> self, Action<T, U> adder)
        {
            self -= adder;
            self += adder;
        }
        public static void RemoveEventListener<T, U>(ref Action<T, U> self, Action<T, U> removee)
        {
            self -= removee;
        }
        public static void AddEventListener<T, U, V>(ref Action<T, U, V> self, Action<T, U, V> adder)
        {
            self -= adder;
            self += adder;
        }
        public static void RemoveEventListener<T, U, V>(ref Action<T, U, V> self, Action<T, U, V> removee)
        {
            self -= removee;
        }


        public static void TryAction(Action ac)
        {
            if (ac != null)
                ac();

        }
        public static void TryAction<T1>(Action<T1> ac, T1 arg1)
        {
            if (ac != null)
                ac(arg1);
        }
        public static void TryAction<T1, T2>(Action<T1, T2> ac, T1 arg1, T2 arg2)
        {
            if (ac != null)
                ac(arg1, arg2);
        }
        public static void TryAction<T1, T2, T3>(Action<T1, T2, T3> ac, T1 arg1, T2 arg2, T3 arg3)
        {
            if (ac != null)
                ac(arg1, arg2, arg3);
        }


        public static void SafeAction(Action ac, Action<Exception> except)
        {
            try
            {
                if (ac != null)
                    ac();
            }
            catch (Exception ex)
            {
                except(ex);
            }
            finally
            {

            }


        }
        public static void SafeAction<T1>(Action<T1> ac, T1 arg1, Action<Exception> except)
        {
            try
            {
                if (ac != null)
                    ac(arg1);
            }
            catch (Exception ex)
            {
                except(ex);
            }
            finally
            {

            }
        }
        public static void SafeAction<T1, T2>(Action<T1, T2> ac, T1 arg1, T2 arg2, Action<Exception> except)
        {
            try
            {
                if (ac != null)
                    ac(arg1, arg2);
            }
            catch (Exception ex)
            {
                except(ex);
            }
            finally
            {

            }
        }
        public static void SafeAction<T1, T2, T3>(Action<T1, T2, T3> ac, T1 arg1, T2 arg2, T3 arg3, Action<Exception> except)
        {
            try
            {
                if (ac != null)
                    ac(arg1, arg2, arg3);
            }
            catch (Exception ex)
            {
                except(ex);
            }
            finally
            {

            }
        }


        public static bool IsActive<TComponent>(TComponent t) where TComponent : UnityEngine.Component
        {
            if (t != null)
                return IsActive(t.gameObject);
            return false;
        }
        public static bool IsActive(GameObject go)
        {
            if (go != null && !go.IsDestroyed())
                return go.activeSelf;
            return false;
        }
        /// <summary>
        /// 있으면 얻고 없으면 생성해서 얻고 부모를 셋팅하고 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static GameObject StableGameObject(Transform parent, string name, params Type[] components)
        {
            Transform itr = null;
            // 인자로 받은 parent가 null이 아니면 변수 itr에 parent의 Transform을 대입
            if (parent != null)
                itr = parent.Find(name);  // parent의 자식노드에 인자name의 이름을 가진 오브젝트가 있으면 불러옴 없으면 null(아래에서 새로 생성함)
            else
            {
                // parent가 null이라면 GameObject형 변수 gtr에 인자로받은 이름을 가진 오브젝트 대입
                GameObject gtr = GameObject.Find(name);
                // gtr에 성공적으로 대입이 되었으면 위에서 생성한 itr에 
                //gtr(인자로받은 오브젝트)의 Transform을 대입(부모 노드)
                if (gtr != null)
                    itr = gtr.transform;
            }

            // itr이 null이라면 (매개변수 parent가 null이고 하이어라키뷰에 매개변수 name을 가진 오브젝트가 없었다는 뜻)
            // 새로운 오브젝트를 생성해서 itr에 transform할당 (오브젝트 이름 = 매개변수로 받은 name)
            if (itr == null)
                itr = (new GameObject(name)).transform;

            // 부모설정 해주고  
            itr.transform.parent = parent;

            // 이미 존재하면 Type 배치만 확인해준다.
            if (components != null)
            {
                foreach (Type tyComponent in components)
                {
                    // itr에 인자로받은 components 적용 (이미 있으면 적용안함)
                    itr.GetOrAddComponent(tyComponent);
                }
            }

            // transform적용되고 component 추가된 itr.gameobject를 반환
            return itr.gameObject;
        }
        public static TComponent Find<TComponent>(Transform self, string relative_path) where TComponent : UnityEngine.Component
        {
            TComponent pRet = null;
            Hyun.Debug.Log.AssertFormat(self != null, "NodeTools.Find Failed. self is null ");

            if (self != null)
            {
                var trFind = self.Find(relative_path);
                if (trFind != null)
                {
                    pRet = trFind.GetComponent<TComponent>();
                }
                else
                {
                    Hyun.Debug.Log.ErrorFormat("Cannot Found '{0}/{1}'", self.name, relative_path);

                }
            }

            return pRet;

        }

        public static T Find<T>(string path) where T : UnityEngine.Component
        {
            GameObject go = GameObject.Find(path);
            if (go != null)
            {
                return go.GetComponent<T>();
            }
            return null;
        }
        // 이거 는 인액티브까지 찾는다.
        public static T Find<T>(GameObject parent, string path) where T : UnityEngine.Component
        {
            // tr에 매개변수 parent를 기준으로 path에 있는 오브젝트를 찾아서 대입
            Transform tr = parent.transform.Find(path);
            // tr이 null이면 null반환, null이 아니면 tr에 컴포넌트 T를 반환
            return tr ? tr.GetComponent<T>() : null;

        }


        public static void DestroyAllChildrens(GameObject parent)
        {
            List<Transform> children = parent.transform.Cast<Transform>().ToList();
            foreach (Transform child in children)
            {
                NodeTools.SafeDestroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy하고 나서 바로 다시 생성하려할경우 밑에서 존재하는것으로 검출된다.
        /// 이름을 바꿔버려서 바로 다시 생성하려할때에 문제없도록 변경됬다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T SafeDestroy<T>(T obj, float delay = 0.0f) where T : UnityEngine.Object
        {

            if (obj == null)
            {
                Hyun.Debug.Log.WarningFormat("SafeDestroy is Failed.");
                return default(T);
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                // 이름 변경해서 삭제하는거 
                obj.name = obj.GetHashCode().ToString(); // 이름 변경 
                (obj as GameObject).SetActive(false);
                UnityEngine.GameObject.Destroy(obj, delay);


            }
            else
            {
                if (Application.isEditor)
                {
                    if (Application.isPlaying)
                    {
                        obj.name = obj.GetHashCode().ToString(); // 이름 변경 
                        if (obj is GameObject)
                            (obj as GameObject).SetActive(false);

                        UnityEngine.GameObject.Destroy(obj, delay);
                    }
                    else
                    {
                        /// OnValidate에서 이렇게 안하면 에러난다
                        /// https://forum.unity3d.com/threads/onvalidate-and-destroying-objects.258782/
                        //UnityEditor.EditorApplication.delayCall += () =>
                        {

                            obj.name = obj.GetHashCode().ToString(); // 이름 변경 
                            if (obj is GameObject)
                                (obj as GameObject).SetActive(false);

                            UnityEngine.GameObject.DestroyImmediate(obj);
                        };

                    }


                }
                else
                {
                    //Imzeehc.Debug.Log.Message(string.Format("SafeDestroy -> {0} ", obj.name));
                    obj.name = obj.GetHashCode().ToString(); // 이름 변경 
                    if (obj is GameObject)
                        (obj as GameObject).SetActive(false);

                    UnityEngine.GameObject.Destroy(obj, delay);
                }
            }





            return null;
        }
        public static T SafeDestroyGameObject<T>(T component) where T : UnityEngine.Component
        {
            if (component != null)
                SafeDestroy(component.gameObject);
            return null;
        }

        public static GameObject SafeFind(string hierachy, Action<GameObject> postAction)
        {
            GameObject go = UnityEngine.GameObject.Find(hierachy);
            if (go != null)
            {
                postAction(go);
            }
            else
            {
                Hyun.Debug.Log.ErrorFormat("Cannot Found '{0}'", hierachy);
            }
            return go;
        }


        public static bool IsSameOf<T>(object obj)
        {
            return obj is T;
        }


        public static bool IsKindOf<T>(object obj)
            where T : class

        {

            Type objType = obj.GetType();

            // 값타입인경우 단순비교
            if (objType.IsGenericType)
            {
                if (TypeDescriptor.GetConverter(typeof(T)).IsValid(obj))
                    return true;
            }
            // 클래스타입인 경우 단순비교해보고 다른경우 상속관계 비교 
            else if (objType.IsClass)
                return IsSameOf<T>(obj) ? true : objType.IsSubclassOf(typeof(T));//     InheritsFrom(objType, typeof(T));


            return false;
        }

        static bool IsInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }


        public static bool IsSubclassOf(Type type, Type baseType)
        {
            if (type == null || baseType == null || type == baseType)
                return false;

            if (baseType.IsGenericType == false)
            {
                if (type.IsGenericType == false)
                    return type.IsSubclassOf(baseType);
            }
            else
            {
                baseType = baseType.GetGenericTypeDefinition();
            }

            type = type.BaseType;
            Type objectType = typeof(object);

            while (type != objectType && type != null)
            {
                Type curentType = type.IsGenericType ?
                    type.GetGenericTypeDefinition() : type;
                if (curentType == baseType)
                    return true;

                type = type.BaseType;
            }

            return false;
        }


        // 현재 타입의 베이스 타입인지 재귀적으로 비교 
        public static bool InheritsFrom(this Type t, Type baseType)
        {
            Type cur = t.BaseType;

            while (cur != null)
            {
                if (cur.Equals(baseType))
                {
                    return true;
                }

                cur = cur.BaseType;
            }

            return false;
        }


        public static TComponent GetOrAddComponent<TComponent>(GameObject self, Action<TComponent, bool> check)
            where TComponent : UnityEngine.Component
        {
            TComponent result = null;
            if (self.HasComponent<TComponent>())
            {
                result = self.GetComponent<TComponent>();

                check(result, false);
            }
            else
            {
                result = self.AddComponent<TComponent>();


                check(result, true);
            }
            return result;
        }
    }
}