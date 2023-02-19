using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text clockDial;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        clockDial.text = Clock.loopTime.ToString("0.00");
    }
}
