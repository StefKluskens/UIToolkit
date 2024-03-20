using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private StyleSheet _styleSheet;

    public static event Action<float> ScaleChanged;
    public static event Action SpinClicked;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if(Application.isPlaying)
            return;

        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return null;

        //Root of the UI, all UI elements will be added to this
        var root = _uiDocument.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        var container = Create("container");

        var vieuwbox = Create("view-box", "bordered-box");

        container.Add(vieuwbox);

        var controlBox = Create("control-box", "bordered-box");

        var spinBtn = Create<Button>();
        spinBtn.text = "Spin";
        spinBtn.clicked += SpinClicked;
        controlBox.Add(spinBtn);

        var scaleSlider = Create<Slider>();
        scaleSlider.lowValue = 0.5f;
        scaleSlider.highValue = 2f;
        scaleSlider.value = 1f;
        scaleSlider.RegisterValueChangedCallback(v => ScaleChanged?.Invoke(v.newValue));
        controlBox.Add(scaleSlider);

        container.Add(controlBox);

        root.Add(container);

        if(Application.isPlaying)
        {
            var targetPosition = container.worldTransform.GetPosition();
            var startPosition = targetPosition + Vector3.up * 100;

            controlBox.experimental.animation.Position(targetPosition, 2000).from = startPosition;
            controlBox.experimental.animation.Start(0, 1, 2000, (e, v) => { e.style.opacity = new StyleFloat(v); });
        }
    }

    private VisualElement Create(params string[] classNames)
    {
        return Create<VisualElement>(classNames);
    }

    T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        var element = new T();
        foreach (string className in classNames)
        {
            element.AddToClassList(className);
        }
        return element;
    }
}
