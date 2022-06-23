using System;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public GameObject TopUpInput, LogInButton;
    public GameObject UserName, Password;
    public GameObject RepeatTip, RepeatInput;
    
    public static int balance;
    public static string usr, pwd;
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
        if (CheckEmpty(username, password) && SqlCache.QuaryAccount(username, password))
        {
            StartCoroutine(PanelManager.MakeDialog("登录成功！"));
            LogInSuccessfully(username, password);
        }
        else StartCoroutine(PanelManager.MakeDialog("用户名或密码错误！"));
    }
    public void SignUp()
    {
        if (RepeatTip.activeInHierarchy == false)
        {
            RepeatTip.SetActive(true);
            RepeatInput.SetActive(true);
            LogInButton.SetActive(false);
            return;
        }

        string username = UserName.GetComponent<InputField>().text;
        string password = Password.GetComponent<InputField>().text;
        string repeat = RepeatInput.GetComponent<InputField>().text;

        if (! CheckEmpty(username, password)) return;
        if (password != repeat)
        {
            StartCoroutine(PanelManager.MakeDialog("两次密码不一致！"));
            return;
        }
        if (SqlCache.QuaryAccount(username, password))
        {
            StartCoroutine(PanelManager.MakeDialog("用户已存在！"));
            return;
        }
        SqlCache.CreateAccount(username, password);
        StartCoroutine(PanelManager.MakeDialog("欢迎使用快餐订购系统！"));
        LogInSuccessfully(username, password);
    }
    private bool CheckEmpty(string username, string password)
    {
        if (username.Length <= 0)
        {
            StartCoroutine(PanelManager.MakeDialog("请输入用户名！"));
            return false;
        }
        if (password.Length <= 0)
        {
            StartCoroutine(PanelManager.MakeDialog("密码不能为空！"));
            return false;
        }
        return true;
    }
    public void Return()
    {
        if (RepeatTip.activeInHierarchy == false)
        {
            WelcomeSlide.slideDirection = false;
            GameObject.Find("欢迎界面").GetComponent<RectTransform>()
                .anchoredPosition = new Vector2(0, Screen.height - 1);
        }
        else
        {
            RepeatTip.SetActive(false);
            RepeatInput.SetActive(false);
            LogInButton.SetActive(true);
        }
    }
    void Update() { SlideManager.SlideLeft(slideDirection, panelTransform); }
    private void LogInSuccessfully(string username, string password)
    {
        slideDirection = InnerSlide.slideEnable = InnerSlide.logInEnable = true;
        LoadMyOrders.LoadOrders(username, password);
        balance = SqlCache.QuaryBalance(username, password);
        GameObject.Find("金额").GetComponent<Text>().text = balance.ToString()+"元";
        usr = username;
        pwd = password;
    }
    public void TopUp()
    {
        try
        {
            int num = int.Parse(TopUpInput.GetComponentsInChildren<Text>()[1].text);
            balance += num;
            SqlCache.UpdateBalance(usr, pwd, balance);
            StartCoroutine(PanelManager.MakeDialog("充值成功！"));
            GameObject.Find("金额").GetComponent<Text>().text = balance.ToString()+"元";
        }
        catch (Exception)
        {
            StartCoroutine(PanelManager.MakeDialog("请输入数字！"));
        }
    }
    public void Withdraw()
    {
        try
        {
            int num = int.Parse(TopUpInput.GetComponentsInChildren<Text>()[1].text);
            if (balance < num)
            {
                StartCoroutine(PanelManager.MakeDialog("余额不足！"));
            }
            else
            {
                balance -= num;
                SqlCache.UpdateBalance(usr, pwd, balance);
                StartCoroutine(PanelManager.MakeDialog("提现成功！"));
                GameObject.Find("金额").GetComponent<Text>().text = balance.ToString()+"元";
            }
        }
        catch (Exception)
        {
            StartCoroutine(PanelManager.MakeDialog("请输入数字！"));
        }
    }
}
