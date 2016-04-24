using UnityEngine;
using System.Collections.Generic;

public class BacteriaCellUI : MonoBehaviour
{
    private const float MIN_SCALE = 1f;
    private const float MAX_SCALE = 1.05f;
    public float SPEED;

    private int direction = 1;
    private float scale;
    
    // Use this for initialization
    void Start()
    {
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= MAX_SCALE)
        {
            direction = -1;
        }
        if (transform.localScale.y <= MIN_SCALE)
        {
            direction = 1;
        }

        scale += direction * SPEED * Time.deltaTime;
        transform.localScale = Vector3.one * scale;

    }

    void OnMouseEnter()
    {
        GameObject.Find("Description").GetComponent<UnityEngine.UI.Text>().enabled = true;
    }

    void OnMouseExit()
    {
        GameObject.Find("Description").GetComponent<UnityEngine.UI.Text>().enabled = false;
    }
}
