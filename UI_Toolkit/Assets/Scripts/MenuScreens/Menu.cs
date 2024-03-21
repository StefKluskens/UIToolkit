using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private StyleSheet _styleSheet;

    public VisualElement Root { get; private set; }
    public VisualElement StartContainer { get; private set; }
    public Button StartButton { get; private set; }

    public static event Action StartClicked;
    public static event Action OptionsClicked;
    public static event Action QuitClicked;

    private void OnEnable()
    {
        StartClicked += StartGame;
    }

    private void OnDisable()
    {
        StartClicked -= StartGame;
    }

    private void Start()
    {
        StartCoroutine(GenerateUI());
    }

    private void OnValidate()
    {
        if(Application.isPlaying)
            return;

        StartCoroutine(GenerateUI());
    }

    private IEnumerator GenerateUI()
    {
        yield return null;

        Root = _uiDocument.rootVisualElement;
        Root.Clear();

        Root.styleSheets.Add(_styleSheet);

        GenerateStartScreen(Root);
    }

    private void GenerateStartScreen(VisualElement root)
    {
        StartContainer = Create("start-container");

        StartButton = Create<Button>("start-button");
        StartButton.text = "Start";
        StartButton.clicked += StartClicked;
        StartContainer.Add(StartButton);

        var optionsButton = Create<Button>();
        optionsButton.text = "Options";
        optionsButton.clicked += OptionsClicked;
        StartContainer.Add(optionsButton);

        var quitButton = Create<Button>();
        quitButton.text = "Quit";
        quitButton.clicked += QuitClicked;
        StartContainer.Add(quitButton);

        root.Add(StartContainer);
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

    private void StartGame()
    {
        //if (Application.isPlaying)
        //{
        //    if (_root == null)
        //    {
        //        return;
        //    }

        //    if (_startButton != null)
        //    {
        //        var startPosition = _startContainer.transform.position;
        //        var halfScreenWidth = Screen.width / 2;
        //        var halfContainerWidth = _startContainer.contentRect.width / 2;

        //        var endPosition = new Vector2(-halfScreenWidth - halfContainerWidth, startPosition.y);

        //        _startContainer.experimental.animation.Position(endPosition, 2000);
        //    }
        //}
    }
}