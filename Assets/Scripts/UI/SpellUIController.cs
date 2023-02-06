using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellUIController : MonoBehaviour
{
    public GameObject shadow;
    public GameObject timer;

    public float ratio;
    public float timeCast;
    public float timerLen;

    // Start is called before the first frame update
    void Start()
    {
        timer.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        ratio = 0;
        timeCast = -timerLen;
    }

    // Update is called once per frame
    void Update()
    {
        UpShadow();
        UpTimer();
        if (Input.GetMouseButtonDown(2))
        {
            timeCast = Time.time;
        }
    }


    public void UpShadow()
    {
        ratio = 1 - ((Time.time - timeCast) / timerLen);
        if(ratio < 0)
        {
            ratio = 0;
        }
        RectTransform shadowTran = shadow.GetComponent<RectTransform>();
        shadowTran.anchorMax = new Vector2(shadowTran.anchorMax.x, ratio);
    }

    public void UpTimer()
    {
        float cd = timeCast + timerLen - Time.time;
        if (cd <= 0)
        {
            timer.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        else
        {
            timer.GetComponent<TMPro.TextMeshProUGUI>().text = cd.ToString("0.#") + " s";            
        }

    }

    public void OnSpellCast(object objData)
    {
        CustomEventData data = (CustomEventData) objData;
        timeCast = data.cdInfo.timeCast;
    }
}
