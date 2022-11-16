using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteObject : MonoBehaviour
{
    public bool canBePressed;
    public Sprite whileActivated;
    public Sprite whileDeActivated;
    public KeyCode keyToPress;
    public Conductor Conductor;
    public Vector2 spawnPos;
    public Vector2 removePos;
    private SpriteRenderer renderer;
    private NoteObject attachedNoteObject;
    public float beatOfThisNote;
    public bool Activated = false;
    // Start is called before the first frame update
    public void initialize(float beat,NoteObject spawnedFrom)
    {
        beatOfThisNote = beat;
        attachedNoteObject = spawnedFrom;
        Activated = true;
    }
    void Start()
    {
        spawnPos = this.transform.position;
        removePos = new Vector2(this.transform.position.x, -1f);
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = whileActivated;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(keyToPress) && Activated && attachedNoteObject.pressed)
        {
            renderer.sprite = whileDeActivated;
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
