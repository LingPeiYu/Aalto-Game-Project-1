using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class TextShower : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float pauseTime;
    [SerializeField]
    private UnityEvent finishEvent;

    private Text txt;
    private string content;
    private int index;

    private void Start()
    {
        txt = GetComponent<Text>();
        content = txt.text;
        txt.text = "_";
        index = 0;
        timer.ActivateTimer(pauseTime);
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || timer.IsZero()) && index < content.Length)
        {
            txt.text = txt.text.Replace('_', content[index]);
            timer.ActivateTimer(pauseTime);
            index++;
            if (index < content.Length)
                txt.text += '_';
        }
        if (index == content.Length)
        {
            finishEvent.Invoke();
            index++;
        }
    }
}
