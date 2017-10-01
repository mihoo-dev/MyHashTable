using System;

namespace Donghoon
{
    public class HashTable
    {
        private int MAX_SIZE;
        private LinkedList[] bucket;

        public HashTable( int capacity)
        {
            this.MAX_SIZE = capacity;
            this.bucket = new LinkedList[MAX_SIZE];
        }

        public void Add(string key, string value)
        {
            int index = HashFunction(key);
            bool check = false;
            
            while(true)
            {
                if(bucket[index] == null)
                {
                    bucket[index] = new LinkedList(key, value);
                    bucket[index].head = bucket[index];
                    bucket[index].tail = bucket[index];
                    bucket[index].count++;
                    Console.WriteLine($"(key : {key}) is Added, Bucket[{index}]");
                    break;
                }
                else
                {
                    if(bucket[index].count >= 5)
                    {
                        if(check == false)
                        {
                            Console.WriteLine($"(key : {key}) is searching for another bucket[count : {bucket[index].count}]");
                            index = CollisionHashFunction(key);
                            check = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("cannot be added");
                            break;
                        }
                    }
                    LinkedList newNode = new LinkedList(key, value);
                    bucket[index].tail.Next = newNode;
                    bucket[index].tail = newNode;
                    bucket[index].count++;
                    Console.WriteLine($"(key : {key}) is Added, Bucket[{index}]");
                    break;
                }
            }
        }

        public class LinkedList
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public LinkedList Next { get; set; }
            public LinkedList head;
            public LinkedList tail;
            public int count;

            public LinkedList(string key, string value)
            {
                this.Key = key;
                this.Value = value;
                this.Next = null;  
                this.head = null;
                this.tail = null; 
                this.count = 0;
            }
        }

        public int HashFunction(string key)
        {
            //Console.WriteLine(key.GetHashCode2() % MAX_SIZE);
            return key.GetHashCode2() % MAX_SIZE;
        }

        public int CollisionHashFunction(string key)
        {
            return (key.GetHashCode2() >> 6) % MAX_SIZE;
        }
    }

    public static class HashCode2ExtensionMethods
    {
        public static int GetHashCode2(this string input)
        {
            int hashCode = 0;
            for(int i = 0; i < input.Length; i++)
            {
                hashCode += input[i];
            }
            return hashCode;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HashTable ht = new HashTable(100);
            ht.Add("kang1", "dong");
            ht.Add("kang1", "dong");
            ht.Add("kang1", "dong");
            ht.Add("kang1", "dong");
            ht.Add("kang1", "dong");
            ht.Add("kang1", "dong");
            ht.Add("kang8", "dong");
            ht.Add("kang8", "dong");
            ht.Add("kang8", "dong");
            ht.Add("kang8", "dong");
            ht.Add("kang8", "dong");
            ht.Add("kang8", "dong");
            
            

            
           
        }
    }
}
