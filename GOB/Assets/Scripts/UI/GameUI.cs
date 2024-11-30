using NUnit.Framework.Constraints;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;

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
}
