using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
      if (transform.position.y < 4.5f)
      {
          this.transform.position += new Vector3(0,0.001f,0);

      }
      else
      {
        Destroy(gameObject);
      }
      
    }
    IEnumerator FadeOut()
    {
      for (float f = 1f; f >= -0.05f; f -= 0.05f)
      {
        Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
        float fadeAmount = f;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;
        yield return new WaitForSeconds(0.01f);
      }
      Destroy(gameObject);
      


    }
}
