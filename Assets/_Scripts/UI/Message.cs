using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] float defaultDuration = 1f;
    [SerializeField] Animator animator;


    public void Init(string text, Color? color = null, float? duration = null)
    {
        messageText.text = text;
        if (color != null)
        {
            messageText.color = color.GetValueOrDefault();
        }
        StartCoroutine(Stay(duration));
    }

    IEnumerator Stay(float? duration = null)
    {
        yield return new WaitForSecondsRealtime(duration.GetValueOrDefault(defaultDuration));
        animator.SetTrigger("Leave");
    }
}
