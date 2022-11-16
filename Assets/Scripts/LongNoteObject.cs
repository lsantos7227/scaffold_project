using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteObject : MonoBehaviour
{
    public bool canBePressed;
    public Sprite whileActivated,whileActivatedEnd;
    public Sprite whileDeActivated,whileDeActivatedEnd;
    public KeyCode keyToPress;
    public Conductor Conductor;
    public Vector2 spawnPos;
    public Vector2 removePos;
    private SpriteRenderer note_Renderer;
    private NoteObject attachedNoteObject;
    public float beatOfThisNote;
    public bool Activated = false;
    public bool isEndNote;
    // Start is called before the first frame update
    public void initialize(float beat,NoteObject spawnedFrom,bool isEnd)
    {
        beatOfThisNote = beat;
        attachedNoteObject = spawnedFrom;
        Activated = true;
        isEndNote = isEnd;
        note_Renderer = GetComponent<SpriteRenderer>();
        if (isEndNote)
        {
            note_Renderer.sprite = whileActivatedEnd;
        }
        else{
            note_Renderer.sprite = whileActivated;
        }
    }
    void Start()
    {
        spawnPos = this.transform.position;
        removePos = new Vector2(this.transform.position.x, -1f);
        note_Renderer = GetComponent<SpriteRenderer>();
        if (isEndNote)
        {
            note_Renderer.sprite = whileActivatedEnd;
        }
        else{
            note_Renderer.sprite = whileActivated;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyUp(keyToPress) && Activated && attachedNoteObject.pressed) || Activated && attachedNoteObject.missed)
        {
            if (isEndNote){
                note_Renderer.sprite = whileDeActivatedEnd;
            }
            else
            {
                note_Renderer.sprite = whileDeActivated;
            }
        }
        if (Activated)
        {
            this.transform.position = Vector2.Lerp(
            spawnPos,removePos, (Conductor.beatsShownInAdvance - (beatOfThisNote - Conductor.songPositionInBeats)) / Conductor.beatsShownInAdvance);
        }
        if (transform.position.y <= 0f && Activated)
        {
            Destroy(gameObject);
        }
    }
}
