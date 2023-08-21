using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitHelper.FillBars
{
    public class ColorFillBar : FillBar
    {
        private Color _fillColor = Color.white;

        public ColorFillBar()
        {
            fillArea.usageHints |= UsageHints.DynamicColor;
        }

        public Color fillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                SetColor(value);
            }
        }

        private void SetColor(Color color)
        {
            fillArea.style.backgroundColor = color;
        }

        public new class UxmlFactory : UxmlFactory<ColorFillBar, UxmlTraits>
        {
        }

        public new class UxmlTraits : FillBar.UxmlTraits
        {
            private readonly UxmlColorAttributeDescription _fillColor = new()
            {
                name = "fill-color",
                defaultValue = Color.white
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var bar = (ColorFillBar)ve;

                bar.fillColor = _fillColor.GetValueFromBag(bag, cc);
            }
        }
    }
}