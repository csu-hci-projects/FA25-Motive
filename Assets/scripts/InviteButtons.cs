using UnityEngine;
using UnityEngine.SceneManagement;

public class InviteButtons : MonoBehaviour
{
    public void OnAcceptClicked()
    {
        SceneManager.LoadScene(2);
    }
    public void OnDeclineClicked()
    {
        SceneManager.LoadScene(0);
    }
}
