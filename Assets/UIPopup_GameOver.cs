using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopup_GameOver : MonoBehaviour
{
    [SerializeField] private Button Retry_Button;
    [SerializeField] private Button Exit_Button;

    private void Awake()
    {
        Retry_Button.onClick.AddListener(() =>
        {
            // TODO : 임시로 빌드 0번 실행
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        Exit_Button.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
