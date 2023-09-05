using UnityEngine.UIElements;

namespace UIToolkitHelper.FillBars
{
    public class ImageFillBar : FillBar
    {
        private const string ImageUssClassName = rootUssClassName + "__image";
        protected readonly VisualElement FillImage;

        public ImageFillBar()
        {
            FillImage = new VisualElement
            {
                name = "fill-bar-image",
                usageHints = UsageHints.DynamicTransform
            };
            FillImage.AddToClassList(ImageUssClassName);
            FillImage.usageHints |= UsageHints.DynamicTransform;
            fillArea.usageHints |= UsageHints.MaskContainer;
            fillArea.Add(FillImage);
            fillArea.style.overflow = Overflow.Hidden;

            RegisterCallback<GeometryChangedEvent>(OnImageGeometryChanged);
        }

        public Background image
        {
            get => FillImage.style.backgroundImage.value;
            set => FillImage.style.backgroundImage = value;
        }

        protected override void ApplyElement()
        {
            base.ApplyElement();
            ApplyImageLayout();
        }

        private void OnImageGeometryChanged(GeometryChangedEvent evt)
        {
            ApplyImageLayout();
        }

        private void ApplyImageLayout()
        {
            var (_, _, fillTop, fillLeft) = CalcLayout(fillAmount);

            var width = contentRect.width;
            var height = contentRect.height;

            FillImage.style.width = new StyleLength(new Length(width, LengthUnit.Pixel));
            FillImage.style.height = new StyleLength(new Length(height, LengthUnit.Pixel));
            FillImage.style.top = new StyleLength(new Length(-fillTop * height, LengthUnit.Pixel));
            FillImage.style.left = new StyleLength(new Length(-fillLeft * width, LengthUnit.Pixel));
        }

        public new class UxmlFactory : UxmlFactory<ImageFillBar, UxmlTraits>
        {
        }
    }
}