using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {

        static void Main()
        {
            Mob slime = new Mob("Slime", 50, 0, 5, 5, MobState.Aggressive);
            Mob ent = new Mob("Ent, Protector of the Forest", 200, 50, 60, 10, MobState.Netural);

            Type slimeType = slime.GetType();
            Console.WriteLine(slimeType);

            TypeInfo entTypeInfo = ent.GetType().GetTypeInfo();
            Console.WriteLine("\n" + entTypeInfo.Name);
            Console.WriteLine(entTypeInfo.IsClass);


            MemberInfo[] members = typeof(Mob).GetMembers();
            foreach (MemberInfo member in members)
            {
                Console.WriteLine(member.Name);
            }
            Console.WriteLine("\n");
            FieldInfo[] fields = typeof(Mob).GetFields();
            FieldInfo privateField = typeof(Mob).GetField("mana", BindingFlags.NonPublic | BindingFlags.Instance);
            
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine(field.Name);
            }
            Console.WriteLine($"Private filed: {privateField}");


            MethodInfo decreaseMethodHPMethod = typeof(Mob).GetMethod("DecreaseHP");
            object[] parameters = { 10 };
            Console.WriteLine(ent);
            decreaseMethodHPMethod.Invoke(ent, parameters);

        }
    }
}
