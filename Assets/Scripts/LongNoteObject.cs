using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteObject : MonoBehaviour
{
    public bool canBePressed;
    public Sprite whileActivated,whileActivatedEnd;
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
        if (Activated && attachedNoteObject.missed)
        {
            Color objectColor = note_Renderer.color;
            objectColor.a = 1f;
            note_Renderer.material.color = objectColor;
        }
    }
    void Start()
    {
        spawnPos = this.transform.position;
        removePos = new Vector2(this.transform.position.x, -1f);
        note_Renderer = GetComponent<SpriteRenderer>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndNote)
        {
            note_Renderer.sprite = whileActivatedEnd;
        }
        else{
            note_Renderer.sprite = whileActivated;
        }
        if((Input.GetKeyUp(keyToPress) && Activated && attachedNoteObject.pressed) || Activated && attachedNoteObject.missed)
        {
            Color objectColor = note_Renderer.color;
            objectColor.a = 1f;
            note_Renderer.material.color = objectColor;
            
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
