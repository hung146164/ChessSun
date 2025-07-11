using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContentString : MonoBehaviour
{
    private TMP_Text content;

    private void Awake()
    {
        content = GetComponent<TMP_Text>();
    }

    public void ChangeText(string newtext)
    {
        content.text = newtext;
    }

}
