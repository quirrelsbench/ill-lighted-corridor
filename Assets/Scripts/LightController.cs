using Oculus.Interaction.Body.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightController : MonoBehaviour
{
    public GameObject[] lights;

    public bool startSequence = false;
    private float timer = 0.0f;
    private float sequenceDelay = 0.5f;

    private void Start()
    {
        // makes sure there is no duplicate listener
        LightLeverController.leverFlipped -= OnLeverFlipped;

        // add event for when lever is flipped
        LightLeverController.leverFlipped += OnLeverFlipped;
    }

    private void Update()
    {
        if (startSequence)
        {
            timer += Time.deltaTime;

            lights[0].SetActive(true);

            if (timer >= sequenceDelay)
            {
                lights[1].SetActive(true);
                timer = 0.0f;
                startSequence = false;
            }
        }
    }

    void OnLeverFlipped()
    {
        if (!lights[0].activeSelf)
        {
            startSequence = true;
        }
        else
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
        }
    }
}
