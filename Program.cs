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
                    Console.WriteLine($"({key}, {value}) is Added (Bucket[{index}], count : {bucket[index].count})");
                    break;
                }
                else
                {
                    if(bucket[index].count >= 5)
                    {
                        if(check == false)
                        {
                            Console.WriteLine($"Bucket[{index}] has no space left...Searching for another bucket");
                            index = CollisionHashFunction(key);
                            check = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("You have already tried twice...Cannot be added");
                            break;
                        }
                    }
                    LinkedList newNode = new LinkedList(key, value);
                    bucket[index].tail.Next = newNode;
                    bucket[index].tail = newNode;
                    bucket[index].count++;
                    Console.WriteLine($"({key}, {value}) is Added (Bucket[{index}], count : {bucket[index].count})");
                    break;
                }
            }
        }

        public string Search(string key)
        {
            int index = HashFunction(key);
            LinkedList Find = bucket[index];

            while(Find != null)
            {
                if(Find.Key == key)
                {
                    Console.WriteLine($"Found (Key : {key}, Value : {Find.Value}) in (Bucket[{index}])");
                    return Find.Value;
                }
                Find = Find.Next;
            }

            index = CollisionHashFunction(key);
            Find = bucket[index];

            while(Find != null)
            {
                if(Find.Key == key)
                {
                    Console.WriteLine($"Found (Key : {key}, Value : {Find.Value}) in (Bucket[{index}])");
                    return Find.Value;
                }
                Find = Find.Next;
            }

            Console.WriteLine($"Cannot find the key...{key}");
            return null;

        }

        public string Delete(string key, string value)
        {
            int index = HashFunction(key);
            LinkedList Find = bucket[index];
            LinkedList temp = bucket[index];

            while(Find != null)
            {
                if(Find.Key == key && Find.Value == value)
                {
                    bucket[index].count--;
                    Console.WriteLine($"Delete (Key : {key}, Value : {Find.Value}) from (Bucket[{index}], count : {bucket[index].count})");
                    if(Find == bucket[index])
                    {
                        bucket[index].Next.tail= bucket[index].tail;
                        bucket[index].Next.count = bucket[index].count;
                        bucket[index] = bucket[index].Next;
                        Find.Next = null;
                    }
                    else if(Find.Next != null)
                    {
                        temp.Next = Find.Next;
                    } 
                    else
                    {
                        temp.Next = null;
                        bucket[index].tail = temp;
                    }
                    
                    return Find.Value;
                }
                temp = Find;
                Find = Find.Next;
            }

            index = CollisionHashFunction(key);
            Find = bucket[index];
            temp = bucket[index];

            while(Find != null)
            {
                if(Find.Key == key && Find.Value == value)
                {
                    bucket[index].count--;
                    Console.WriteLine($"Delete (Key : {key}, Value : {Find.Value}) from (Bucket[{index}], count : {bucket[index].count})");

                    if(Find == bucket[index])
                    {
                        bucket[index].Next.tail= bucket[index].tail;
                        bucket[index].Next.count = bucket[index].count;
                        bucket[index] = bucket[index].Next;
                        Find.Next = null;
                    }
                    else if(Find.Next != null)
                        temp.Next = Find.Next;
                    else
                    {
                        temp.Next = null;
                        bucket[index].tail = temp;
                    }
                    
                    return Find.Value;
                }
                temp = Find;
                Find = Find.Next;
            }

            Console.WriteLine("Cannot find the key");
            return null;
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
            int capacity = 165;
            HashTable ht = null;
            if(capacity >= 100 && capacity <= 500)
            {
                 ht = new HashTable(capacity);
            }
            else
            {   
                Console.WriteLine("Capacity can only be between 100 and 500.");
                Console.WriteLine("Try Again..");
                 return;
            }
            ht.Add("kang1", "dong1");
            ht.Add("akng1", "dong2");
            ht.Add("angk1", "dong3");
            ht.Add("agnk1", "dong4");
            ht.Add("gnka1", "hoon");
            ht.Add("knga1", "dong5");
            ht.Add("kang7", "dong");
            ht.Add("kang8", "hoon");
            ht.Add("kang9", "dong");
            ht.Add("kang10", "dong");
            ht.Add("kang11", "dong");
            ht.Add("kang12", "dong");
            
            
            ht.Search("kang1");
            ht.Delete("kang1", "dong1");
            ht.Search("kang1");

            ht.Add("kang1", "dfsdf");
            ht.Add("gkan1", "dfsdf");

            ht.Search("kang1");
            ht.Search("gkan1");
            
            
            
        }
    }
}
