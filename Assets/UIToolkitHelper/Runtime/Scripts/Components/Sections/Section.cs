using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine.UIElements;

namespace UIToolkitHelper.Sections
{
    public class Section : VisualElement
    {
        private const string rootUssClassName = "section";
        private const string contentUssClassName = rootUssClassName + "__content-container";

        private readonly VisualElement _content;

        private readonly ReactiveProperty<bool> _isShownReactiveProperty = new();
        private readonly Subject<Unit> _showSubject = new();
        private readonly Subject<Unit> _hideSubject = new();

        private bool _isShown;
        private VisibilityToggleAnimationType _animationType;

        public Section()
        {
            AddToClassList(rootUssClassName);

            _content = new VisualElement
            {
                name = "section-content-container",
                usageHints = UsageHints.GroupTransform,
                pickingMode = PickingMode.Ignore
            };
            _content.AddToClassList(contentUssClassName);
            hierarchy.Add(_content);

            _isShownReactiveProperty.Value = true;
            SetShown(_isShownReactiveProperty.Value);
        }

        public IObservable<Unit> ShowAsObservable => _showSubject;
        public IObservable<Unit> HideAsObservable => _hideSubject;

        public IReadOnlyReactiveProperty<bool> IsShownReactiveProperty => _isShownReactiveProperty;

        public override VisualElement contentContainer => _content;

        public bool isShown
        {
            get => _isShownReactiveProperty.Value;
            set
            {
                if (_isShownReactiveProperty.Value == value) return;

                _isShownReactiveProperty.Value = value;
                SetShown(value);
            }
        }

        public VisibilityToggleAnimationType animationType
        {
            get => _animationType;
            set
            {
                if (_animationType == value) return;

                _content.RemoveFromClassList(_animationType.ToAnimationClassName());
                _animationType = value;
                _content.AddToClassList(_animationType.ToAnimationClassName());
            }
        }

        private void SetShown(bool shown)
        {
            _content.EnableInClassList(VisibilityToggleUtilities.visibilityToggleShownModifierUssClassName, shown);
            if (shown)
                _showSubject.OnNext(Unit.Default);
            else
                _hideSubject.OnNext(Unit.Default);
        }


        public new class UxmlFactory : UxmlFactory<Section, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlBoolAttributeDescription _isShown = new()
            {
                name = "is-shown",
                defaultValue = true
            };

            private readonly UxmlEnumAttributeDescription<VisibilityToggleAnimationType> _animationType = new()
            {
                name = "animation-type",
                defaultValue = VisibilityToggleAnimationType.None
            };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield return new UxmlChildElementDescription(typeof(VisualElement)); }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var section = (Section)ve;
                section.isShown = _isShown.GetValueFromBag(bag, cc);
                section.animationType = _animationType.GetValueFromBag(bag, cc);
            }
        }
    }
}