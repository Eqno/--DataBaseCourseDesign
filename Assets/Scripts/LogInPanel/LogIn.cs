using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public int SlideStep = 5000;
    public GameObject LogInPanel;
    public GameObject UserName, Password;
    public GameObject RepeatTip, RepeatInput;
    
    private static bool slideDirection;
    private RectTransform panelTransform;
    void Start()
    {
        slideDirection = false;
        RepeatTip.SetActive(false);
        RepeatInput.SetActive(false);
        panelTransform = GetComponent<RectTransform>();
    }
    public void SignIn()
    {
        string username = UserName.GetComponent<InputField>().text;
        string password = Password.GetComponent<InputField>().text;
        if (username.Length <= 0) StartCoroutine(PanelManager.MakeDialog("请输入用户名！"));
        else if (password.Length <= 0) StartCoroutine(PanelManager.MakeDialog("密码不能为空！"));
        else if (SqlCache.QuaryAccount(username, password)) slideDirection = true;
    }
    public void SignUp()
    {

    }
    public void Return()
    {
        WelcomeSlide.slideDirection = false;
        GameObject.Find("欢迎界面").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height - 1);
    }
    void Update()
    {
        if (slideDirection && panelTransform.anchoredPosition.x > -Screen.width)
        {
            panelTransform.anchoredPosition = new Vector2(
                LimitValue(
                    panelTransform.anchoredPosition.x
                    - SlideStep * Time.deltaTime,
                    -Screen.width, 0
                ), 0
            );
        }
        else if (! slideDirection && panelTransform.anchoredPosition.x < 0)
        {
            panelTransform.anchoredPosition = new Vector2(
                LimitValue(
                    panelTransform.anchoredPosition.x
                    + SlideStep * Time.deltaTime,
                    -Screen.width, 0
                ), 0
            );
        }
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}
