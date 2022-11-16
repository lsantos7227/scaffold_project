using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Start is called before the first frame update
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    public float bpm;
    
    public int nextIndex;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    public int beatsShownInAdvance;
    //How many seconds have passed since the song started
    public float dspSongTime;
    public bool started;
    public float firstBeatOffset;
    public GameObject lane_1;
    public GameObject lane_2;
    public GameObject lane_3;
    public GameObject lane_4;
    [System.NonSerialized]
    public float[,] notes = {{4,1,0,2,2},{6,0,1,3,3},{8,2,2,2,2},{9,3,3,3,3}};
    //an AudioSource attached to this GameObject that will play the music.
public AudioSource musicSource;
    void Start()
    {
         //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        //Start the music
        //add a delay
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        if (nextIndex < notes.GetLength(0) && notes[nextIndex,0] < songPositionInBeats + beatsShownInAdvance)
        {   
            if (notes[nextIndex,1] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_1, new Vector3(-1.5f,9f,1f),lane_1.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);
            
            //initialize the fields of the music note
            }
            else if (notes[nextIndex,1] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_1, new Vector3(-1.5f,9f,1f),lane_1.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(1,nextIndex));
            }
            if (notes[nextIndex,2] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_2, new Vector3(-0.5f,9f,1f),lane_2.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            else if (notes[nextIndex,2] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_2, new Vector3(-0.5f,9f,1f),lane_2.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(2,nextIndex));
            }
            if (notes[nextIndex,3] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_3, new Vector3(0.5f,9f,1f),lane_3.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            else if (notes[nextIndex,3] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_3, new Vector3(0.5f,9f,1f),lane_3.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(3,nextIndex));
            }
            if (notes[nextIndex,4] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_4, new Vector3(1.5f,9f,1f),lane_4.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);
            //initialize the fields of the music note
            }
            else if (notes[nextIndex,4] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_4, new Vector3(1.5f,9f,1f),lane_4.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(4,nextIndex));
            }
            nextIndex++;
        }
    }
    public float find_nearest_three(int column,int startingPosition)
    {
        int scroll = startingPosition;
        while (scroll < notes.GetLength(0))
        {
            if (notes[scroll,column] == 3)
            {
                return notes[scroll,0];
            }
            scroll++;
        }
        return -1f;
    }

    
}
