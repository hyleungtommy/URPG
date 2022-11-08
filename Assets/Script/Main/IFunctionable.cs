using System.Collections;
using System.Collections.Generic;

namespace RPG
{
    public interface IFunctionable
    {
        List<BattleMessage> use(Entity user, Entity[] target);
    }
}