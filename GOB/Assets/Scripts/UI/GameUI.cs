using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject clear;
    [SerializeField] private TMP_Text BossHp;
    void Start()
    {
        SetBossHp(GameManager.Instance.Boss.Hp);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            MenuUIControl(); //메뉴 여닫기
        }
    }

    //메뉴 여닫기
    public void MenuUIControl()
    {
        bool open = menu.activeSelf;
        if (!open) //열 때
        {
            menu.SetActive(true);
        }
        else //닫을 때
        {
            menu.GetComponent<Animator>().SetBool("Close", true);
        }
    }

    public void SetBossHp(int hp)
    {
        BossHp.text = "X " + hp;
    }

    public void ClickHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void ClickRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage");
    }

    public void OnGameOver()
    {
        gameOver.SetActive(true);
    }

    public void OnClear()
    {
        clear.SetActive(true);
    }
}
