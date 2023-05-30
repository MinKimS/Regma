using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Damage : MonoBehaviour
{
    public Image hpScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   //if()

    IEnumerator ShowBloodScreen()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
