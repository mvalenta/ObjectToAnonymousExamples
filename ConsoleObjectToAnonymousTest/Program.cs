using Newtonsoft.Json;
using System;

namespace ConsoleObjectToAnonymousTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var result = TestMethod_CastTo(new { Val1 = "Test", Val2 = "Test2" });
            Console.WriteLine($"Attempt to use CastTo to convert object to anonymous type was {(result ? "Successful" : "Unsuccessful")}");

            var result2 = TestMethodWithObjects_CastTo(new { Val1 = new User(), Val2 = new Status() });
            Console.WriteLine($"Attempt to use CastTo to convert object to anonymous type was {(result2 ? "Successful" : "Unsuccessful")}");

            Console.WriteLine(); //Spacer

            var result3 = TestMethod_AnonymizeExtension(new { Val1 = "Test", Val2 = "Test2" });
            Console.WriteLine($"Attempt to use extension method to convert object to anonymous type was {(result3 ? "Successful" : "Unsuccessful")}");

            var result4 = TestMethodWithObjects_AnonymizeExtension(new { Val1 = new User(), Val2 = new Status() });
            Console.WriteLine($"Attempt to use extension method to convert object to anonymous type was {(result4 ? "Successful" : "Unsuccessful")}");

            Console.WriteLine(); //Spacer

            var result5 = TestMethod_JsonConvert(new { Val1 = "Test", Val2 = "Test2" });
            Console.WriteLine($"Attempt to use JsonConvert to convert object to anonymous type was {(result5 ? "Successful" : "Unsuccessful")}");

            var result6 = TestMethodWithObjects_JsonConvert(new { Val1 = new User(), Val2 = new Status() });
            Console.WriteLine($"Attempt to use JsonConvert to convert object to anonymous type was {(result6 ? "Successful" : "Unsuccessful")}");

            Console.Read();
        }

        public static bool TestMethod_CastTo(object inObject)
        {
            var anonType = new { Val1 = "", Val2 = "" };
            var anonObject = CastTo(inObject, anonType);

            if (anonObject.Val1 == "Test")
                return true;
            return false;
        }

        public static bool TestMethodWithObjects_CastTo(object inObject)
        {
            var anonType = new { Val1 = new User(), Val2 = new Status() };
            var anonObject = CastTo(inObject, anonType);

            if (anonObject.Val1.Id == 1 && anonObject.Val1.Name == "Mitch" && 
                anonObject.Val2.Code == 200 && anonObject.Val2.Error == "None")
                return true;
            return false;
        }

        public static bool TestMethod_AnonymizeExtension(object inObject)
        {
            var anonType = new { Val1 = "", Val2 = "" };
            var anonObject = inObject.Anonymize(anonType);

            if (anonObject.Val1 == "Test")
                return true;
            return false;
        }

        public static bool TestMethodWithObjects_AnonymizeExtension(object inObject)
        {
            var anonType = new { Val1 = new User(), Val2 = new Status() };
            var anonObject = inObject.Anonymize(anonType);

            if (anonObject.Val1.Id == 1 && anonObject.Val1.Name == "Mitch" &&
                anonObject.Val2.Code == 200 && anonObject.Val2.Error == "None")
                return true;
            return false;
        }

        public static bool TestMethod_JsonConvert(object inObject)
        {
            string resultBody = JsonConvert.SerializeObject(inObject);
            var anonType = new { Val1 = "", Val2 = "" };
            var anonObject = JsonConvert.DeserializeAnonymousType(resultBody, anonType);

            if (anonObject.Val1 == "Test")
                return true;
            return false;
        }

        public static bool TestMethodWithObjects_JsonConvert(object inObject)
        {
            string resultBody = JsonConvert.SerializeObject(inObject);
            var anonType = new { Val1 = new User(), Val2 = new Status() };
            var anonObject = JsonConvert.DeserializeAnonymousType(resultBody, anonType);

            if (anonObject.Val1.Id == 1 && anonObject.Val1.Name == "Mitch" &&
                anonObject.Val2.Code == 200 && anonObject.Val2.Error == "None")
                return true;
            return false;
        }


        private static T CastTo<T>(object value, T targetType)
        {
            // targetType above is just for compiler magic
            // to infer the type to cast x to

            return (T)value;
        }
    }

    public static class AnonymousExtension
    {
        public static T Anonymize<T>(this object value, T targetType)
        {
            return (T)value;
        }
    }

    public class User
    {
        public User()
        {
            Id = 1;
            Name = "Mitch";
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Status
    {
        public Status()
        {
            Code = 200;
            Error = "None";
        }
        public int Code { get; set; }
        public string Error { get; set; }
    }
}
