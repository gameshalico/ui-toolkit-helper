namespace UIToolkitHelper.Sections
{
    public static class VisibilityToggleUtilities
    {
        public const string visibilityToggleUssClassName = "visibility-toggle";
        public const string visibilityToggleShownModifierUssClassName = visibilityToggleUssClassName + "--shown";

        public static string ToAnimationClassName(this VisibilityToggleAnimationType type)
        {
            return type switch
            {
                VisibilityToggleAnimationType.Fade => visibilityToggleUssClassName + "--fade",
                VisibilityToggleAnimationType.SlideUp => visibilityToggleUssClassName + "--slide-up",
                VisibilityToggleAnimationType.Custom => visibilityToggleUssClassName + "--custom",
                _ => visibilityToggleUssClassName
            };
        }
    }
}