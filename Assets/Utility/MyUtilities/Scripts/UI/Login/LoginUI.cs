using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("Username")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_Text emailWarn;

    [Header("Password")]
    [SerializeField] private TMP_InputField passwordInput;

    [SerializeField] private TMP_Text passwordWarn;

    [Header("Login")]
    [SerializeField] private Button loginButton;

    private bool isValidEmail = false;
    private bool isvalidPassword = false;

    private bool isLogging = false;

    private string email;
    private string password;

    private void Awake()
    {
        UsernameInit();
        PasswordInit();
        LoginInit();
    }

    #region Username

    private void UsernameInit()
    {
        emailInput.onValueChanged.AddListener(EmailUpdate);
    }

    private void EmailUpdate(string val)
    {
        if (AuthValidator.IsValidEmail(val))
        {
            email = val;
            isValidEmail = true;
        }
        else
        {
            isValidEmail = false;
        }
        WarnEmail();
        UpdateLoginButton();
    }

    private void WarnEmail()
    {
        if (isValidEmail)
        {
            emailWarn.gameObject.SetActive(false);
        }
        else
        {
            emailWarn.gameObject.SetActive(true);
        }
    }

    #endregion Username

    #region Password

    private void PasswordInit()
    {
        passwordInput.onValueChanged.AddListener(PasswordUpdate);
        passwordInput.contentType = TMP_InputField.ContentType.Password;
    }

    private void PasswordUpdate(string val)
    {
        if (AuthValidator.IsValidPassword(val))
        {
            password = val;
            isvalidPassword = true;
        }
        else
        {
            isvalidPassword = false;
        }
        WarnPassword();
        UpdateLoginButton();
    }

    private void WarnPassword()
    {
        if (isvalidPassword)
        {
            passwordWarn.gameObject.SetActive(false);
        }
        else
        {
            passwordWarn.gameObject.SetActive(true);
        }
    }

    #endregion Password

    #region Login

    private void LoginInit()
    {
        loginButton = transform.Find("LoginBut").GetComponent<Button>();
    }

    private void UpdateLoginButton()
    {
        var image = loginButton.GetComponent<Image>();
        var color = image.color;
        if (isvalidPassword && isValidEmail && !isLogging)
        {
            loginButton.interactable = true;
            color.a = 1f;
        }
        else
        {
            loginButton.interactable = false;
            color.a = 0.3f;
        }
        image.color = color;
    }

    #endregion Login
}