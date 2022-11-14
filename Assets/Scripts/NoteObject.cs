using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public Conductor Conductor;
    public Vector2 spawnPos;
    public Vector2 removePos;
    public float beatOfThisNote;
    // Start is called before the first frame update
    public void initialize(float beat)
    {
        beatOfThisNote = beat;
        
        
    }
    void Start()
    {
        spawnPos = this.transform.position;
        removePos = new Vector2(this.transform.position.x, -1f);
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
                if(Mathf.Abs(transform.position.y) > 0.6f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, new Vector2(0,4), hitEffect.transform.rotation);
                } 
                else if(Mathf.Abs(transform.position.y) > 0.35f)
                {
                   Debug.Log("GoodHit");
                   GameManager.instance.GoodHit();
                   Instantiate(goodEffect, new Vector2(0,4), goodEffect.transform.rotation);
                } 
                else
                {
                   Debug.Log("Perfect");
                   GameManager.instance.PerfectHit();
                   Instantiate(perfectEffect, new Vector2(0,4), perfectEffect.transform.rotation);
                }
                Destroy(gameObject);
            }

        }
        this.transform.position = Vector2.Lerp(
        spawnPos,removePos, (Conductor.beatsShownInAdvance - (beatOfThisNote - Conductor.songPositionInBeats)) / Conductor.beatsShownInAdvance);
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

                Instantiate(missEffect, new Vector2(0,4), missEffect.transform.rotation);

                GameManager.instance.NoteMissed();
                Destroy(gameObject);
            }
        }
    }

}
