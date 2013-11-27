using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IKVM.Reflection;

namespace Converter
{
    public class Loader
    {
        const BindingFlags AllDeclared =
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance |
    BindingFlags.Static |
    BindingFlags.DeclaredOnly;
        private Assembly assembly;

        public void Load(string filename)
        {
            var universe = new Universe();
           // universe.AssemblyResolve += AssemblyResolve;
            assembly = universe.LoadFile(filename);
          
        }

          //foreach (var type in assembly.GetTypes())
          //  {
          //      Console.WriteLine(type.FullName);
          //      WriteMembers(type.GetFields(AllDeclared));
          //      WriteMembers(type.GetProperties(AllDeclared));
          //      WriteMembers(type.GetEvents(AllDeclared));
          //      WriteMembers(type.GetConstructors(AllDeclared));
          //      WriteMembers(type.GetMethods(AllDeclared));

          //      foreach (var method in type.GetMethods())
          //      {
          //          method.GetMethodBody().
          //      }
          //  }

        //DEBUG
        //static void WriteMembers(MemberInfo[] members)
        //{
        //    foreach (var member in members)
        //        Console.WriteLine(" {0}", member);
        //}

        //static Assembly AssemblyResolve(object sender, IKVM.Reflection.ResolveEventArgs args)
        //{
        //    return ((Universe)sender).CreateMissingAssembly(args.Name);
        //}
    }
}
