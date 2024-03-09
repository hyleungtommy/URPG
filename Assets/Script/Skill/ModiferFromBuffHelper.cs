namespace RPG
{
    //Skill damage/ mp usage modifier from Special Buffs
    public class ModifierFromBuffHelper
    {
        public static float getMPUseModifierFromBuff(Entity user)
        {
            float modifier = 1.0f;
            if (user.buffState.isBuffExists(8))//Mana Brust
            {
                modifier = 1.5f;
            }
            return modifier;
        }

        public static float getMagicModifierFromSpecialBuff(Entity user, string skillName)
        {
            float modifier = 1f;
            if (user.buffState.isBuffExists(8))//Mana Brust
            {
                modifier = 2f;
                if (skillName.Equals("Metorite") || skillName.Equals("Eternal Frozen"))
                {
                    modifier = 4f;
                }
            }
            return modifier;
        }

        public static float getAttackModifierFromSpecialBuff(Entity user, string skillName)
        {
            float modifier = 1f;
            if (user.buffState.isBuffExists(3))//Berserking
            {
                modifier = 1.5f;
                if (skillName.Equals("Berserking"))
                {
                    modifier = 2.0f;
                }
            }
            if (user.buffState.isBuffExists(12) && skillName.Equals("Shadow Assault"))//Shadow Form
            {
                modifier = 2f;
            }
            return modifier;
        }

        public static float getTargetDefenseModifierFromSpecialBuff(Entity target)
        {
            float modifier = 1f;
            if (target.buffState.isBuffExists(32))
            {// Summon Zombie
                modifier = 2f;
            }
            return modifier;
        }

        public static int getExtraTurnFromSummonSkeleton(Entity user)
        {
            int turn = 0;
            if (user.buffState.isBuffExists(33))
            {
                turn = 1;
            }
            return turn;
        }

        public static int getExtraDebuffChanceFromSummonDarkSpirit(Entity user)
        {
            int chance = 1;
            if (user.buffState.isBuffExists(34))
            {
                chance = 2;
            }
            return chance;
        }

    }
}
