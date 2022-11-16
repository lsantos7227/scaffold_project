using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public Conductor Conductor;
    public bool startPlaying;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 50;
    public int scorePerGoodNote = 100;
    public int scorePerPerfectNote = 300; 
    public int scorePerLongNoteSection= 5;

    public Text scoreText;
    public Text multiText;
    public float totalNotes;
    public float normalHits;
    public float perfectHits;
    public float goodHits;
    public float missedHits;
    private float display_score;
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        display_score = 0;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (display_score < currentScore)
        {
            if ((currentScore - display_score) <= 25)
            {
                display_score++;
            }
            else if ((currentScore - display_score) <= 350)
            {
                display_score += 6;
            }
            else if ((currentScore - display_score) <= 750)
            {
                display_score += 12;
            }
            else
            {
                display_score += 24;
            }
        }
        scoreText.text = "Score: " + display_score;
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theMusic = Conductor.GetComponent<AudioSource>();

                
            }
        }else
         {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
             {
                 resultsScreen.SetActive(true);

                 normalsText.text = "" + normalHits;
                 goodsText.text = goodHits.ToString();
                 perfectsText.text = perfectHits.ToString();
                 missesText.text = "" + missedHits;

                 float totalHit = normalHits * 0.33f + goodHits * 0.66f + perfectHits * 1f;
                 float percentHit = (totalHit / (normalHits+goodHits+perfectHits+missedHits)) * 100f;
                 percentHitText.text = percentHit.ToString("F1") + "%";

                 string rankVal = "F";
                 if(percentHit > 60)
                 {
                     rankVal = "D";
                     if(percentHit > 70)
                     {
                         rankVal = "C";
                         if(percentHit > 80)
                         {
                             rankVal = "B";
                             if(percentHit > 90)
                             {
                                 rankVal = "A";
                                 if(percentHit > 95)
                                 {
                                     rankVal = "S";
                                 }
                             }
                         }
                     }

                 }
                 rankText.text = rankVal;
                 finalScoreText.text = currentScore.ToString();
             }
         }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        multiplierTracker++;
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                
                currentMultiplier++;
            }
        }

        multiText.text = "Combo: " + multiplierTracker + "x";

       // currentScore += scorePerNote * currentMultiplier;
        
    }
   
    public void NormalHit()
    { 
    currentScore += scorePerNote * currentMultiplier;
    NoteHit();
    normalHits++;
    }
    
    public void GoodHit()
    {
    currentScore += scorePerGoodNote * currentMultiplier;
    NoteHit();
    goodHits++;
    }
   
    public void PerfectHit()
    {
    currentScore += scorePerPerfectNote * currentMultiplier;
    NoteHit();
    perfectHits++;
    }
    public void longNoteHit()
    {
        currentScore += scorePerLongNoteSection * currentMultiplier;
        NoteHit();
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Combo: " + multiplierTracker + "x";

        missedHits++;
    }
}
