using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLibrary
{
    public enum AccountState { SignedIn, SignedOut }
    public enum TriggerAccountState { SignIn, SignOut }
    public class AccountTransitioncs
    {
        public AccountState prevState;
        public AccountState nextState;
        public AccountState currentState;

    }
}
