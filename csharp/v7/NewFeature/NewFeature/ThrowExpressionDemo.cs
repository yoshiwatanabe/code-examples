using System;

namespace NewFeature
{
    class ThrowExpressionDemo
    {
        public static void Run()
        {
            var name1 = ToUpper("james");
            var name2 = ToUpper2("james");
        }

        public static string ToUpper(string name)
        {
            return name != null ? name.ToUpper() : throw new ArgumentNullException(nameof(name));
        }

        public static string ToUpper2(string name)
        {
            return (name ?? throw new ArgumentNullException(nameof(name))).ToUpper();
        }
    }
}
