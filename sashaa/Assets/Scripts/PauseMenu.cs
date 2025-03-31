using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenu;      // Панель паузы
    public GameObject levelSelectMenu; // Панель выбора уровня
    public Button continueButton;    // Кнопка "Продолжить"
    public Button levelSelectButton; // Кнопка "Выбрать уровень"
    public Button[] levelButtons;    // Кнопки уровней (назначьте в инспекторе)

    private bool isPaused = false;

    void Start()
    {
        // Назначаем обработчики кнопок
        continueButton.onClick.AddListener(ContinueGame);
        levelSelectButton.onClick.AddListener(ShowLevelSelect);

        // Назначаем кнопкам уровней загрузку сцен
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Нумерация с 1
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }

        // Скрываем меню при старте
        pauseMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
    }

    void Update()
    {
        // Пауза по ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ContinueGame();
            else PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Останавливаем время
        pauseMenu.SetActive(true);
    }

    void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Возобновляем время
        pauseMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
    }

    void ShowLevelSelect()
    {
        pauseMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1; // Возобновляем время
        SceneManager.LoadScene("Level_" + levelIndex); // Или ваш формат названий сцен
    }
}