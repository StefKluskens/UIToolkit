using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    private void OnEnable()
    {
        Menu.StartClicked += StartGame;
        Menu.OptionsClicked += OpenOptions;
        Menu.QuitClicked += QuitGame;
    }

    private void OnDisable()
    {
        Menu.StartClicked -= StartGame;
        Menu.OptionsClicked -= OpenOptions;
        Menu.QuitClicked -= QuitGame;
    }

    private void StartGame()
    {
        if (Application.isPlaying)
        {
            if (_menu.Root == null)
            {
                return;
            }

            if (_menu.StartButton != null)
            {
                var startPosition = _menu.StartContainer.transform.position;
                var halfScreenWidth = Screen.width / 2;
                var halfContainerWidth = _menu.StartContainer.contentRect.width / 2;

                var endPosition = new Vector2(-halfScreenWidth - halfContainerWidth, startPosition.y);

                _menu.StartContainer.experimental.animation.Position(endPosition, 2000);
            }
        }
    }

    private void OpenOptions()
    {
        if (Application.isPlaying)
        {
            if (_menu.Root == null)
            {
                return;
            }

            if (_menu.StartButton != null)
            {
                var startPosition = _menu.StartContainer.transform.position;
                var halfScreenWidth = Screen.width / 2;
                var halfContainerWidth = _menu.StartContainer.contentRect.width / 2;

                var endPosition = new Vector2(halfScreenWidth + halfContainerWidth, startPosition.y);

                _menu.StartContainer.experimental.animation.Position(endPosition, 2000);
            }
        }
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
