using System;
using CodeBase.Hero;

namespace CodeBase.Stats
{
    class HeroHealth : NpcHealth, IPlayerTeam
    {
        public void ResetHealth() => 
            Give(Max);
    }
}