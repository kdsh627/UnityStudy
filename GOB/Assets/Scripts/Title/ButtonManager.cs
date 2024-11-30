using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadScnene() //Start 버튼
    {
        SceneManager.LoadScene("Stage");
    }

    public void Quit() //Exit 버튼
    {
        Application.Quit();
    }
}
