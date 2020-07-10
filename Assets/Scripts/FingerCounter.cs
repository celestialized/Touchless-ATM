using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerCounter : MonoBehaviour
{
    public Text Number_UIText;
    int fingerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.Number_UIText.text = "Numberfgfgh of vvvfingers: " + this.fingerCount;

    }

    // Update is called once per frame
    void Update()
    {
        this.Number_UIText.text = "Numberfgfgh of fingers: " + this.fingerCount;
    }

    public void NoExtendedFingers()
    {
        this.fingerCount = 0;
    }

    public void OneExtendedFingers()
    {
        this.fingerCount = 1;
    }
    public void TwoExtendedFingers()
    {
        this.fingerCount = 2;
    }
    public void ThreeExtendedFingers()
    {
        this.fingerCount = 3;
    }
    public void FourExtendedFingers()
    {
        this.fingerCount = 4;
    }
    public void FiveExtendedFingers()
    {
        this.fingerCount = 5;
    }
}
