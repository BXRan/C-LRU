using System;
using System.Collections.Generic;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LRUCache<int> cache = new LRUCache<int>(3);
            cache.Get(1);
            cache.Set(1, 1);
            cache.Set(2, 2);
            cache.Get(3);
            cache.Set(3, 3);
            cache.Set(4, 4);
            cache.Get(2);
            Console.ReadKey();
        }
    }
    public class LRUCache<T>
    {
        private int _size;//链表长度
        private int _capacity;//缓存容量 
        private Dictionary<int, ListNode<T>> _dic;//key +缓存数据
        private ListNode<T> _linkHead; //头
        public LRUCache(int capacity)
        {
            _linkHead = new ListNode<T>(-1, default);   //头
            _linkHead.Next = _linkHead.Prev = _linkHead;
            this._size = 0;
            this._capacity = capacity;
            this._dic = new Dictionary<int, ListNode<T>>();
        }

        public T Get(int key)
        {
            if (_dic.ContainsKey(key))
            {
                ListNode<T> n = _dic[key];
                MoveToHead(n);
                return n.Value;
            }
            else
            {
                return default(T);
            }
        }
        public void Set(int key, T value)
        {
            ListNode<T> n;
            if (_dic.ContainsKey(key))
            {
                n = _dic[key];
                n.Value = value;
                MoveToHead(n);
            }
            else
            {
                n = new ListNode<T>(key, value);
                AttachToHead(n);
                _size++;
            }
            if (_size > _capacity)
            {
                RemoveLast();// 如果更新节点后超出容量，删除最后一个
                _size--;
            }
            _dic.Add(key, n);
        }
        // 移出链表最后一个节点
        private void RemoveLast()
        {
            ListNode<T> deNode = _linkHead.Prev;
            RemoveFromList(deNode);
            _dic.Remove(deNode.Key);
        }
        // 将一个孤立节点放到头部
        private void AttachToHead(ListNode<T> n)
        {
            n.Prev = _linkHead;
            n.Next = _linkHead.Next;
            _linkHead.Next.Prev = n;
            _linkHead.Next = n;
        }
        // 将一个链表中的节点放到头部
        private void MoveToHead(ListNode<T> n)
        {
            RemoveFromList(n);
            AttachToHead(n);
        }
        private void RemoveFromList(ListNode<T> n)
        {
            //将该节点从链表删除
            n.Prev.Next = n.Next;
            n.Next.Prev = n.Prev;
        }
    }

    public class ListNode<T>
    {
        public ListNode<T> Prev;   //上一个
        public ListNode<T> Next;   //下一个
        public T Value; //值
        public int Key; //键

        public ListNode(int key, T val)  //构造
        {
            Value = val;
            Key = key;
            this.Prev = null;
            this.Next = null;
        }
    }
}
