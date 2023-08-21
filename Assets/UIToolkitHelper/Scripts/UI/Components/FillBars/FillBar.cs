using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitHelper.FillBars
{
    public class FillBar : VisualElement, IFillable
    {
        protected const string rootUssClassName = "fill-bar";
        private const string fillUssClassName = rootUssClassName + "__area";
        private FillDirection _fillDirection;

        private float _fillAmount;

        public FillBar()
        {
            AddToClassList(rootUssClassName);
            fillArea = new VisualElement
            {
                name = "fill-bar-area",
                usageHints = UsageHints.DynamicTransform
            };
            fillArea.AddToClassList(fillUssClassName);
            Add(fillArea);
        }

        protected VisualElement fillArea { get; }

        public FillDirection fillDirection
        {
            get => _fillDirection;
            set
            {
                _fillDirection = value;
                ApplyElement();
            }
        }

        public float fillAmount
        {
            get => _fillAmount;
            set
            {
                _fillAmount = Mathf.Clamp01(value);
                ApplyElement();
            }
        }

        protected (float width, float height, float top, float left) CalcLayout(float rate)
        {
            var fillWidth = 1.0f;
            var fillHeight = 1.0f;
            var fillLeft = 0.0f;
            var fillTop = 0.0f;
            switch (fillDirection)
            {
                case FillDirection.LeftToRight:
                    fillWidth = rate;
                    break;
                case FillDirection.RightToLeft:
                    fillWidth = rate;
                    fillLeft = 1.0f - rate;
                    break;
                case FillDirection.TopToBottom:
                    fillHeight = rate;
                    break;
                case FillDirection.BottomToTop:
                    fillHeight = rate;
                    fillTop = 1.0f - rate;
                    break;
            }

            return (fillWidth, fillHeight, fillTop, fillLeft);
        }

        protected virtual void ApplyElement()
        {
            var (fillWidth, fillHeight, fillTop, fillLeft) = CalcLayout(fillAmount);

            fillArea.style.width = new StyleLength(new Length(fillWidth * 100, LengthUnit.Percent));
            fillArea.style.height = new StyleLength(new Length(fillHeight * 100, LengthUnit.Percent));
            fillArea.style.left = new StyleLength(new Length(fillLeft * 100, LengthUnit.Percent));
            fillArea.style.top = new StyleLength(new Length(fillTop * 100, LengthUnit.Percent));
        }


        public new class UxmlFactory : UxmlFactory<FillBar, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<FillDirection> _direction =
                new() { name = "fill-direction", defaultValue = FillDirection.LeftToRight };

            private readonly UxmlFloatAttributeDescription _fillAmount = new()
                { name = "fill-amount", defaultValue = 0.5f };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var bar = ve as FillBar;

                bar.fillAmount = _fillAmount.GetValueFromBag(bag, cc);
                bar.fillDirection = _direction.GetValueFromBag(bag, cc);
            }
        }
    }
}