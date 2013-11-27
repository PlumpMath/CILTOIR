using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class ILClass
    {

        IEnumerable<ILMethod> methods;
        IEnumerable<CILField> fields;

        public void Accept(IVisitor aVisitor)
        {
            aVisitor.Open(this);
            foreach (var method in methods)
                method.Accept(aVisitor);


            foreach (var field in fields)
                field.Accept(aVisitor);


            aVisitor.Close(this);
        }
    }
}
