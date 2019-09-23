using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SprintMeterUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    Image image;
    PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = playerMovement.SprintTimeLeft / playerMovement.SprintMaxTime;
        if (playerMovement.IsSprintLocked)
        {
            if (image.canvasRenderer.GetColor() == Color.white)
            {
                image.CrossFadeColor(Color.red, 0.4f, true, true);
            }
            else if (image.canvasRenderer.GetColor() == Color.red)
            {
                image.CrossFadeColor(Color.white, 0.4f, true, true);
            }
        }
        if (image.canvasRenderer.GetColor() == Color.red)
        {
            image.CrossFadeColor(Color.white, 0.4f, true, true);
        }
    }
}
