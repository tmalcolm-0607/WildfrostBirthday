// Helper class for creating Status Effect and Trait stacks
using static CardData;

namespace WildfrostBirthday.Helpers
{
    public static class StackHelpers
    {
        /// <summary>
        /// Creates a StatusEffectStacks object for the given status effect name and amount
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="name">The name of the status effect</param>
        /// <param name="amount">The amount/stack count</param>
        /// <returns>A StatusEffectStacks object</returns>
        public static StatusEffectStacks SStack(this WildFamilyMod mod, string name, int amount) 
            => new StatusEffectStacks(mod.TryGet<StatusEffectData>(name), amount);

        /// <summary>
        /// Creates a TraitStacks object for the given trait name and amount
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="name">The name of the trait</param>
        /// <param name="amount">The amount/stack count</param>
        /// <returns>A TraitStacks object</returns>
        public static TraitStacks TStack(this WildFamilyMod mod, string name, int amount) 
            => new TraitStacks(mod.TryGet<TraitData>(name), amount);
    }
}
