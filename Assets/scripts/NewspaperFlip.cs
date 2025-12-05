using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewspaperFlip : MonoBehaviour
{
    public GameObject startButton;
    public Sprite[] newspaperPages;
    public Image pageDisplay;
    private int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPage();
    }

    public void NextPage()
    {
        if (index < newspaperPages.Length - 1)
        {
            index++;
            ShowPage();
        }
    }

    public void BackPage()
    {
        if (index > 0)
        {
            index--;
            ShowPage();
        }
    }

    void ShowPage()
    {
        pageDisplay.sprite = newspaperPages[index];

        if (index == newspaperPages.Length - 1)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(3);
    }
}
