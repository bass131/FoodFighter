using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;
namespace Croquette
{
    public class MemoryPool : Singleton<MemoryPool>
    {

        // 아이템 클래스
        class Item
        {
            public bool active; // 사용중인지 여부
            public GameObject gameObject;
        }
        Item[] table;


        //열거자 기본 재정의
        public IEnumerator GetEnumerator()
        {
            if (table == null)
                yield break;

            int count = table.Length;

            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item.active)
                    yield return item.gameObject;
            }
        }


        // 메모리풀 생성
        // original : 미리 생성해 둘 원본 소스
        // count : 풀 최고 개수
        public void Create(Object _original, int _count)
        {
            Dispose();
            table = new Item[_count];
            for (int i = 0; i < _count; i++)
            {
                Item item = new Item();
                item.active = false;
                item.gameObject = GameObject.Instantiate(_original) as GameObject;
                item.gameObject.SetActive(false);
                table[i] = item;
            }
        }

        // 메모리풀 생성
        // original : 메모리풀을 실행할 오브젝트 이름 [대신 속성은 없다]
        // count : 풀 최고 개수
        public void Create(string _original, int _count)
        {
            Dispose();
            table = new Item[_count];
            for (int i = 0; i < _count; i++)
            {
                Item item = new Item();
                item.active = false;
                item.gameObject = new GameObject(_original + i.ToString());
                item.gameObject.SetActive(false);
                table[i] = item;
            }
        }

        // 새 아이템 요청 - 쉬고 있는 객체를 반납
        public GameObject NewItem()
        {
            if (table == null)
                return null;
            int count = table.Length;
            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item.active == false)
                {
                    item.active = true;
                    item.gameObject.SetActive(true);
                    return item.gameObject;
                }

            }
            return null;
        }

        // 아이템 사용 종료 - 사용하던 객체를 쉬게한다
        // gameObject : NewItem으로 얻었던 객체
        public void RemoveItem(GameObject _gameObject)
        {
            if (table == null || _gameObject == null)
                return;
            int count = table.Length;

            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item.gameObject == _gameObject)
                {
                    item.active = false;
                    item.gameObject.SetActive(false);
                    break;
                }
            }
        }

        // 모든 아이템 사용종료 - 모든 객체를 쉬게한다
        public void ClearItem()
        {
            if (table == null)
                return;
            int count = table.Length;

            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item != null && item.active)
                {
                    item.active = false;
                    item.gameObject.SetActive(false);
                }
            }
        }

        // 메모리 풀 삭제
        public void Dispose()
        {
            if (table == null)
                return;

            int count = table.Length;

            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                GameObject.Destroy(item.gameObject);
            }
            table = null;
        }

        /// <summary>
        /// Pool Length Return
        /// </summary>
        /// <returns></returns>
        public int LengthPoolItem()
        {
            if (table == null)
            {
                Debug.Log("PoolMemoryEmpty!");
                return 0;
            }

            return table.Length;
        }
    }
}