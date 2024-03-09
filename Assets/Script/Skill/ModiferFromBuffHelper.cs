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
            return modifier;
        }

    }
}
