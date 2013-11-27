using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class ILMethod
    {

        IEnumerable<ILOpCode> instructions;


        public void Accept(IVisitor aVisitor)
        {
            aVisitor.Open(this);
            foreach (var instruction in instructions) //FIXME we dont visit instructions , methods are generated as a unit.. we need to add context!
                instruction.Accept(aVisitor);

            aVisitor.Close(this);
        }
    }
}
