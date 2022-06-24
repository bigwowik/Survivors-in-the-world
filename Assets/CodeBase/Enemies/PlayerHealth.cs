using System;
using CodeBase.Hero;

namespace CodeBase.Stats
{
    class PlayerHealth : NpcHealth, IPlayerTeam
    {
        public void ResetHealth()
        {
            Give(Max);
        }
    }
}