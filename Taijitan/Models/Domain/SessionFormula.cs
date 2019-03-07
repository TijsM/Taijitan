using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class SessionFormula
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int FormulaId { get; set; }
        public Formula Formula { get; set; }
        

        public SessionFormula(int sessionId, Session session, int formulaId, Formula formula)
        {
            SessionId = sessionId;
            Session = session;
            FormulaId = formulaId;
            Formula = formula;
        }
        public SessionFormula()
        {

        }
    }
}
