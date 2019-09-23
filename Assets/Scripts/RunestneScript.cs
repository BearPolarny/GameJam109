using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunestneScript : MonoBehaviour, IUsable
{
    Text text;
    bool fading = false;
    private bool done;

    void Start()
    {
        text = GameObject.FindObjectOfType<RunestoneText>().GetComponent<Text>();
    }

    void Update()
    {
        if (fading)
        {
            text.CrossFadeAlpha(0.5f, 1f, true);
            text.CrossFadeAlpha(0f, 0.2f, true);
            if (text.canvasRenderer.GetAlpha() < 0.1f)
            {
                text.CrossFadeAlpha(1f, 1f, true);

                fading = false;
                text.canvasRenderer.SetAlpha(1f);
                text.color = Color.black;
                text.text = "";
            }
        }
        if (done && !fading)
        {
            Destroy(text);
        }
    }

    [SerializeField]
    private char letter;

    public void PerformAction()
    {

        if (text.text != null)
        {
            text.color = Color.black;
            text.text += letter;
            if (text.text.Length == 4)
            {
                if (text.text.Equals("SHIT"))
                {

                    GameObject key = GameObject.Find("Kluczyk");
                    key.GetComponent<MeshCollider>().enabled = true;
                    key.GetComponent<MeshRenderer>().enabled = true;
                    text.color = Color.green;
                    fading = true;
                    done = true;
                }
                else
                {
                    text.color = Color.red;
                    fading = true;
                }
            }
        }
        else
        {
            Debug.Log("nowy");
            text.text = "" + letter;
        }
    }

}
