using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{



    private void Awake()
    {
    	print("Dayım");
    }

    // Start is called before the first frame update
    void Start()
    {
    	StartCoroutine(FrameThree());
       
    }

    IEnumerator FrameThree()
    {
    	yield return new WaitForSeconds(Time.deltaTime*3);
    	print("Bubum");
    }

    // Update is called once per frame
    void Update()
    {
        print("Dayım2");
    }

    


}
