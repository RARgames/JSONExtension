using System;

namespace testProject
{
    class Program
    {
         void Main(string[] args)
        {
            LoadJSON();
            Test();
        }

        void Test()
        {
            //It's not fully implemented code, just a sample of JSON Extension usage
            //In order to use it in real functions, you need to implement loading json and calling value by key
            //
            //Main functionality:
            //Open settings at Tools/JSON Extension Settings and select JSON path - language.json - with a structure declared in the file (especially "en" table)
            //When you hover over any key you will see it's value in quick info
            //If you want to edit, right click on the key and open JSON edit window. If you change the value/key, it will be automatically changed in language.json and every occurrence of old key will be replaced with new key in the whole solution.
            //If the key is not existent, it will ask you to create new key

            //Existing keys
            Console.WriteLine(ConvertKey("key1"));
            //"key1x"
            //"xkey1"
            //key1
            //"key1 xx"
            Console.WriteLine(ConvertKey("key2"));
            Console.WriteLine(ConvertKey("key3"));
            Console.WriteLine(ConvertKey("key4"));
            Console.WriteLine(ConvertKey("key5"));
            Console.WriteLine(ConvertKey("key6"));
            Console.WriteLine(ConvertKey("key7"));
            Console.WriteLine(ConvertKey("key8"));
            Console.WriteLine(ConvertKey("key9"));
            Console.WriteLine(ConvertKey("key10"));
            Console.WriteLine(ConvertKey("key11"));
            Console.WriteLine(ConvertKey("key12"));
            Console.WriteLine(ConvertKey("key13"));
            Console.WriteLine(ConvertKey("key14"));
            Console.WriteLine(ConvertKey("key15"));
            Console.WriteLine(ConvertKey("key16"));
            Console.WriteLine(ConvertKey("key17"));
            Console.WriteLine(ConvertKey("key18"));
            Console.WriteLine(ConvertKey("key19"));
            Console.WriteLine(ConvertKey("key20"));
            //
            //Not existing keys
            Console.WriteLine(ConvertKey("key21"));
            Console.WriteLine(ConvertKey("key22"));
            Console.WriteLine(ConvertKey("key23"));
            Console.WriteLine(ConvertKey("key24"));
            //
            //Other functions
            Empty();
            Empty();
            Empty();
            Empty();
        }

        void LoadJSON()
        {
            //method that loads the given JSON
        }
        void Empty()
        {

        }

        string ConvertKey(string key)
        {
            //it should return the value of given key
            return key;
        }
    }
}
