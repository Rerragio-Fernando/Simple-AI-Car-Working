using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Metrics : MonoBehaviour
{
    public TextMeshProUGUI timerTMP, speedTMP, distanceTMP;
    public Rigidbody rb;

    private string minutes = "00";
    private string seconds = "00";
    private float timer = 0, distance;
    private int speed, dis = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerTMP.text = $"{minutes}:{seconds}";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        timerTMP.text = $"{minutes}:{seconds}";

        speed = (int)rb.velocity.magnitude;
        speedTMP.text = $"{speed}m/s";

        distance = speed * Time.deltaTime;
        dis += (int)distance;
        distanceTMP.text = $"{dis}m";
    }
}
