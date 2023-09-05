using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitHelper.FillBars
{
    public class TintedImageFillBar : ImageFillBar
    {
        private Color _imageTintColor = Color.white;

        public TintedImageFillBar()
        {
            FillImage.usageHints |= UsageHints.DynamicColor;
        }

        public Color imageTintColor
        {
            get => _imageTintColor;
            set
            {
                _imageTintColor = value;
                SetColor(value);
            }
        }

        private void SetColor(Color color)
        {
            FillImage.style.unityBackgroundImageTintColor = color;
        }


        public new class UxmlFactory : UxmlFactory<TintedImageFillBar, UxmlTraits>
        {
        }

        public new class UxmlTraits : ImageFillBar.UxmlTraits
        {
            private readonly UxmlColorAttributeDescription _imageTintColor = new()
            {
                name = "image-tint-color",
                defaultValue = Color.white
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var bar = (TintedImageFillBar)ve;
                bar.imageTintColor = _imageTintColor.GetValueFromBag(bag, cc);
            }
        }
    }
}