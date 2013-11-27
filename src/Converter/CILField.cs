using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{

    public class CILField
    {

        public void Accept(IVisitor aVisitor)
        {
            aVisitor.Visit(this);
        }
    }
}
