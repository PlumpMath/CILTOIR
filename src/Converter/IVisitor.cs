using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    // CHECK CIL formar in 
    // parse in context
    public interface IVisitor
    {
        //    public void VisitCIL(ICILElement akid);

        void Open(ILClass aClass);
        void Open(CILCompilationUnit aCompilationUnit);
        void Open(ILMethod aMethod);
        void Close(ILClass aClass);
        void Close(CILCompilationUnit aCompilationUnit);
        void Close(ILMethod aMethod);
        void Visit(CILField aField);
        //void Visit(ILOpCode aStatement);

    }







  
    public class CILCompilationUnit
    {
        IEnumerable<ILClass> classes;

        public void Accept(IVisitor aVisitor)
        {
            aVisitor.Open(this);

            foreach ( var ilClass in classes) 
                ilClass.Accept(aVisitor);
            aVisitor.Close(this);
        }
    }
}
