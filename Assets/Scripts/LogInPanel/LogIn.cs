using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public GameObject UserName, Password;
    public GameObject RepeatTip, RepeatInput;
    
    void Start()
    {
        RepeatTip.SetActive(false);
        RepeatInput.SetActive(false);
    }
    public void SignIn()
    {
        string username = UserName.GetComponent<InputField>().text;
        string password = Password.GetComponent<InputField>().text;
        if (username.Length <= 0) StartCoroutine(PanelManager.MakeDialog("请输入用户名！"));
        else if (password.Length <= 0) StartCoroutine(PanelManager.MakeDialog("密码不能为空！"));
        
    }
    public void SignUp()
    {

    }
    public void Return()
    {
        WelcomeSlide.slideDirection = false;
        GameObject.Find("欢迎界面").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height - 1);
    }
}
