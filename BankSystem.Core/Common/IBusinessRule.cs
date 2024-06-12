using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Common
{
    public interface IBusinessRule
    {
        RuleResult Check();
    }
}
