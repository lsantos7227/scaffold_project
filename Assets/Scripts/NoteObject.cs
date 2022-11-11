using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);
               // GameManager.instance.NoteHit();
                if(Mathf.Abs(transform.position.y) > 0.35 )
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                } 
                else if(Mathf.Abs(transform.position.y) > 0.1f)
                {
                   Debug.Log("GoodHit");
                   GameManager.instance.GoodHit();
                   Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                } 
                else
                {
                   Debug.Log("Perfect");
                   GameManager.instance.PerfectHit();
                   Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;

                Instantiate(missEffect, transform.position, missEffect.transform.rotation);

                GameManager.instance.NoteMissed();
            }
        }
    }

}
