using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIElement<TElement> : MonoBehaviour
    {
        private readonly Dictionary<Type, Action<Element>> typeActions = new Dictionary<Type, Action<Element>>();

        protected void InitializeElements(Element[] elements)
        {
            var rootElement = GetComponent<UIDocument>().rootVisualElement;

            typeActions.Add(typeof(Button), e =>
            {
                var data = (ButtonElement)e;
                rootElement.Q<Button>(data.Name).clicked += data.OnInteract.Invoke;
            });
            typeActions.Add(typeof(Toggle), e =>
            {
                var data = (Element<bool>)e;
                rootElement.Q<Toggle>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(DropdownField), e =>
            {
                var data = (Element<string>)e;
                rootElement.Q<DropdownField>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(RadioButton), e =>
            {
                var data = (Element<bool>)e;
                rootElement.Q<RadioButton>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(TextField), e =>
            {
                var data = (Element<string>)e;
                rootElement.Q<TextField>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(Scroller), e =>
            {
                var data = (Element<float>)e;
                rootElement.Q<Scroller>(data.Name).valueChanged += data.OnInteract.Invoke;
            });
            typeActions.Add(typeof(Slider), e =>
            {
                var data = (Element<float>)e;
                rootElement.Q<Slider>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(SliderInt), e =>
            {
                var data = (Element<int>)e;
                rootElement.Q<SliderInt>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });
            typeActions.Add(typeof(MinMaxSlider), e =>
            {
                var data = (Element<Vector2>)e;
                rootElement.Q<MinMaxSlider>(data.Name).RegisterValueChangedCallback(data.RegisterNewValue);
            });

            if (!typeActions.ContainsKey(typeof(TElement)))
                return;
            var appropriateAction = typeActions[typeof(TElement)];

            foreach (var element in elements)
                appropriateAction(element);
        }

        [Serializable] protected class Element
        {
            [SerializeField] protected string name;

            public string Name
            {
                get
                {
                    return name;
                }
            }
        }
        [Serializable] protected class ButtonElement : Element
        {
            [SerializeField] private UnityEvent onInteract;

            public UnityEvent OnInteract
            {
                get
                {
                    return onInteract;
                }
            }
        }
        [Serializable] protected class Element<TValue> : Element
        {
            [SerializeField] private UnityEvent<TValue> onInteract;

            public UnityEvent<TValue> OnInteract
            {
                get
                {
                    return onInteract;
                }
            }

            public void RegisterNewValue(ChangeEvent<TValue> changeEvent)
            {
                onInteract.Invoke(changeEvent.newValue);
            }
        }
    }
}
