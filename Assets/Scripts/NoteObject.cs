using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public GameObject longNoteEffect;
    public Conductor Conductor;
    public Vector2 spawnPos;
    public Vector2 removePos;
    public float beatOfThisNote;
    public bool Activated;
    public bool isLongNote;
    public bool pressed;
    public float endOfThisNote;
    private float timeTillTick;
    private float beatTimeSpawn = 0;
    public bool missed;
    // Start is called before the first frame update
    public void initialize(float beat, bool LongNote = false,float endNote = 0)
    {
        beatOfThisNote = beat;
        endOfThisNote = endNote;
        Activated = true;
        isLongNote = LongNote;
        pressed = false;
        missed = false;
        
    }
    void Start()
    {
        spawnPos = this.transform.position;
        removePos = new Vector2(this.transform.position.x, -1.1f);
        timeTillTick = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        beatTimeSpawn += Time.deltaTime * 6;
        if(Input.GetKeyDown(keyToPress) && !pressed && !missed)
        {
            if(canBePressed)
            {
                if (!isLongNote)
                {
                    gameObject.SetActive(false);
                }
               // GameManager.instance.NoteHit();
                if(Mathf.Abs(transform.position.y) > 0.80f)
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
                else if (Mathf.Abs(transform.position.y) <= 0.35f)
                {
                   Debug.Log("Perfect");
                   GameManager.instance.PerfectHit();
                   Instantiate(perfectEffect, new Vector2(0,4), perfectEffect.transform.rotation);
                }
                pressed = true;
                if (isLongNote == false)
                {
                    Destroy(gameObject);
                }
                else
                {
                    this.GetComponent<Renderer>().enabled = false;
                    timeTillTick = 0f;
                }
            }

        }
        else if (!Input.GetKeyUp(keyToPress) && isLongNote && pressed && !missed && (Conductor.songPositionInBeats < endOfThisNote))
        {
            timeTillTick += Time.deltaTime;
            if ((timeTillTick * Conductor.secPerBeat * 1.0f) >= 0.05f)
            {
                GameManager.instance.longNoteHit();
                timeTillTick = 0;
            }
        }
        else if (Input.GetKeyUp(keyToPress) && pressed && !missed && isLongNote && (Conductor.songPositionInBeats < endOfThisNote))
        {
            gameObject.SetActive(false);
            if ((endOfThisNote - Conductor.songPositionInBeats) >= 0.5f)
            {
                Debug.Log("Miss");
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, new Vector2(0,4), missEffect.transform.rotation);
                
            }
            else if ((endOfThisNote - Conductor.songPositionInBeats) >= 0.25f)
            {
                Debug.Log("Hit");
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, new Vector2(0,4), hitEffect.transform.rotation);
            }
            else if ((endOfThisNote - Conductor.songPositionInBeats) >= 0.125f)
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
        else if ((Conductor.songPositionInBeats >= endOfThisNote) && !missed && isLongNote && pressed && !Input.GetKeyUp(keyToPress))
        {
            gameObject.SetActive(false);
            Debug.Log("Perfect");
            GameManager.instance.PerfectHit();
            Instantiate(perfectEffect, new Vector2(0,4), perfectEffect.transform.rotation);
            Destroy(gameObject);
        }
        if (Activated && pressed == false)
        {
            this.transform.position = Vector2.Lerp(
            spawnPos,removePos, (Conductor.beatsShownInAdvance - (beatOfThisNote - Conductor.songPositionInBeats)) / Conductor.beatsShownInAdvance);
        }
        if (isLongNote && (beatTimeSpawn + beatOfThisNote) < endOfThisNote)
        {
            bool isEnd = false;
            if ((beatTimeSpawn + Time.deltaTime * 6f + beatOfThisNote) >= endOfThisNote)
            {
                Debug.Log("End Note");
                isEnd = true;
            }
            LongNoteObject noteobject = ((GameObject) Instantiate(longNoteEffect, new Vector3(transform.position.x,10f,1f),longNoteEffect.transform.rotation)).GetComponent<LongNoteObject>();
            noteobject.initialize((beatTimeSpawn + beatOfThisNote),this.GetComponent<NoteObject>(),isEnd);
        }
        if (missed && Conductor.songPositionInBeats >= endOfThisNote)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
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
                if (!isLongNote)
                {
                    canBePressed = false;
                    Instantiate(missEffect, new Vector2(0,4), missEffect.transform.rotation);
                    GameManager.instance.NoteMissed();
                    Destroy(gameObject);
                }
                else
                {
                    missed = true;
                    Instantiate(missEffect, new Vector2(0,4), missEffect.transform.rotation);
                    GameManager.instance.NoteMissed();
                    GameManager.instance.NoteMissed();
                    this.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

}
