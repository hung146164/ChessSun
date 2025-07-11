using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisUI : MonoBehaviour
{
    [Header("Username")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_Text usernameWarn;

    [Header("Password")]
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text passwordWarn;

    [Header("Email")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_Text emailWarn;

    [Header("Confirm Password")]
    [SerializeField] private TMP_InputField passConfirmInput;
    [SerializeField] private TMP_Text passConfirmWarn;

    [Header("Registration")]
    [SerializeField] private Button regisButton;


    private bool isvalidUsername = false;
    private bool isvalidPassword = false;
    private bool isvalidEmail = false;
    private bool isRegisting = false;
    private bool isvalidConfirmPassword=false;

    private string username;
    private string password;
    private string email; 

    private void Awake()
    {
        UsernameInit();
        PasswordInit();
        EmailInit();
        RegisInit();
        PasswordConfirmInit();
    }

    #region Username

    private void UsernameInit()
    {
        usernameInput.onValueChanged.AddListener(UsernameUpdate);
    }

    private void UsernameUpdate(string val)
    {
        if (AuthValidator.IsValidUsername(val))
        {
            username = val;
            isvalidUsername = true;
        }
        else
        {
            isvalidUsername = false;
        }
        WarnUsername();
        UpdateRegisButton();
    }

    private void WarnUsername()
    {
        if (isvalidUsername)
        {
            usernameWarn.gameObject.SetActive(false);
        }
        else
        {
            usernameWarn.gameObject.SetActive(true);
        }
    }

    #endregion Username

    #region Email
    private void EmailInit()
    {
        emailInput.onValueChanged.AddListener(EmailUpdate);
    }
    private void EmailUpdate(string val)
    {
        if (AuthValidator.IsValidEmail(val))
        {
            email = val;
            isvalidEmail = true;
        }
        else
        {
            isvalidEmail = false;
        }
        WarnEmail();
        UpdateRegisButton();
    }
    private void WarnEmail()
    {
        if(isvalidEmail)
        {
            emailWarn.gameObject.SetActive(false);
        }
        else
        {
            emailWarn.gameObject.SetActive(true);
        }
    }
    #endregion

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
        UpdateRegisButton();
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

    #region Password Confirm
    private void PasswordConfirmInit()
    {
        passConfirmInput.onValueChanged.AddListener(PasswordConfirmUpdate);
        passConfirmInput.contentType = TMP_InputField.ContentType.Password;
    }

    private void PasswordConfirmUpdate(string val)
    {
        if (AuthValidator.IsValidConfirmPassword(password,val))
        {
            
            isvalidConfirmPassword = true;
        }
        else
        {
            isvalidConfirmPassword = false;
        }
       
        WarnPasswordConfirm();
        UpdateRegisButton();
    }

    private void WarnPasswordConfirm()
    {
        if (isvalidConfirmPassword)
        {
            passConfirmWarn.gameObject.SetActive(false);
        }
        else
        {
            passConfirmWarn.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Regis

    private void RegisInit()
    {
        regisButton = transform.Find("Register").GetComponent<Button>();
    }

    private void UpdateRegisButton()
    {
        var image = regisButton.GetComponent<Image>();
        var color = image.color;
        if (isvalidPassword && isvalidUsername && isvalidEmail && isvalidConfirmPassword && !isRegisting)
        {
            regisButton.interactable = true;
            color.a = 1f;
        }
        else
        {
            regisButton.interactable = false;
            color.a = 0.3f;
        }
        image.color = color;
    }

    #endregion Login
}
