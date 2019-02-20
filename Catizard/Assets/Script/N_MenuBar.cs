using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class N_MenuBar : MonoBehaviour
{

    public Text CardSum, GoldAmount;
    public GameObject ExitUI;

    // Start is called before the first frame update
    void Start()
    {
        GoldAmount.text = "" + N_PlayerInfo.Gold;
        CardSum.text = "" + N_PlayerInfo.CardSum;
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GotoStore()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void GotoStore_Remove()
    {
        SceneManager.LoadScene("Store_1");
    }

    public void GotoStore_Purchase()
    {
        SceneManager.LoadScene("Store_2");
    }

    public void GotoStore_Strengthen()
    {
        SceneManager.LoadScene("Store_3");
    }

    public void GotoDeck()
    {
        SceneManager.LoadScene("DeckScene");
    }

    public void GotoStoryBook()
    {
        SceneManager.LoadScene("StoryBookScene");
    }

    public void GotoHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void ExitUI_On()
    {
        ExitUI.SetActive(true);
    }

    public void ExitUI_Off()
    {
        ExitUI.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

        Application.Quit();   // 종료한다

#endif
    }

}
