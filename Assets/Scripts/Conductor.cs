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
    public float[,] notes = {{4,1,0,1,1},{4.5f,1,0,1,1},{5f,1,1,1,1},{5.5f,1,0,1,0},{6,1,0,1,0},{7,1,1,0,0},{9,0,0,1,1},{11f,0,0,1,1},{12f,0,0,0,1},{13f,1,0,0,0},{13.1f,0,1,0,0},{13.2f,0,0,1,0},{13.3f,0,0,0,1}};
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
            if (notes[nextIndex,2] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_2, new Vector3(-0.5f,9f,1f),lane_2.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            if (notes[nextIndex,3] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_3, new Vector3(0.5f,9f,1f),lane_3.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            if (notes[nextIndex,4] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_4, new Vector3(1.5f,9f,1f),lane_4.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);
            //initialize the fields of the music note
            }
            nextIndex++;
        }
    }
    
}
