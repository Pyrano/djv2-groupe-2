using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public GameObject Bar;


    public float ratio;
    float backRatio;
    // Start is called before the first frame update
    void Start()
    {
        Bar = gameObject;
        ratio = 1;
        backRatio = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFiller();
    }

    public void SetRatio(float rat)
    {
        ratio = rat;
    }

    private void UpdateFiller()
    {

        RectTransform fillerTransform = transform.GetChild(1).GetComponent<RectTransform>();
        RectTransform backFillerTransform = transform.GetChild(0).GetComponent<RectTransform>(); 
        fillerTransform.anchorMax = new Vector2(ratio, fillerTransform.anchorMax.y);
        if (Mathf.Abs(backRatio - ratio) < 0.01)
        {
            backRatio = ratio;
        }
        else
        {
            backRatio -= (backRatio - ratio) / 100;
        }    
        backFillerTransform.anchorMax = new Vector2(backRatio, backFillerTransform.anchorMax.y);
    }
}
