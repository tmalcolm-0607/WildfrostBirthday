// WildfrostBirthday/Helpers/StatusEffectDataBuilderExtensions.cs
using System;

namespace WildfrostBirthday.Helpers
{
    public static class StatusEffectDataBuilderExtensions
    {
        /// <summary>
        /// Sets the icon sprite path for the status effect.
        /// </summary>
        public static StatusEffectDataBuilder WithIcon(this StatusEffectDataBuilder builder, string iconPath)
        {
            // Use SubscribeToAfterAllBuildEvent to set the icon on the built data
            return builder.SubscribeToAfterAllBuildEvent(data =>
            {
                // Try to set a field or property for the icon path if it exists
                var field = data.GetType().GetField("spritePath") ?? data.GetType().GetField("iconPath");
                if (field != null)
                    field.SetValue(data, iconPath);
                var prop = data.GetType().GetProperty("spritePath") ?? data.GetType().GetProperty("iconPath");
                if (prop != null && prop.CanWrite)
                    prop.SetValue(data, iconPath);
            });
        }
    }
}
