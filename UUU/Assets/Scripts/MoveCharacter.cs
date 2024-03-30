using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.UI;

public class MoveCharacter : MonoBehaviour
{
    public int speed = 0;
    public bool onleft = false, mid = true, onright = false;
    public float TransSpeed = 10f;
    public int coin = 0;
    public int heart = 2;
    public float Score = 0f;
    public int ScorePenalty = 1;

    public TextMeshProUGUI coinText, healthText, scoreText;
    public GameObject failpanel;
    public GameObject winpanel;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    
     

    void Start()
    {
    	
    }
    // Update is called once per frame
    void Update()
    {

    	 if (heart>0)
        	  
        	  Score = coin*heart/ScorePenalty;
        	 
        	  else 
        	  Score = coin*1/ScorePenalty;

    	if (heart == 3)
    	{
    	heart1.SetActive(true);
    	heart2.SetActive(true);
    	heart3.SetActive(true);
        }
        else if (heart == 2)
    	{
    	heart1.SetActive(true);
    	heart2.SetActive(true);
    	heart3.SetActive(false);
        }

      else if (heart == 1)
    	{
    	heart1.SetActive(true);
    	heart2.SetActive(false);
    	heart3.SetActive(false);
        }

       else if (heart == 0)
    	{
    	heart1.SetActive(false);
    	heart2.SetActive(false);
    	heart3.SetActive(false);
        }



    	

        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("Run", true);
            speed = 11;
        }

         if(Input.GetKeyDown(KeyCode.Space))
         {
             GetComponent<Animator>().SetTrigger("Jump");
             //transform.DOJump(new Vector3(transform.position.x,2,transform.position.z),0.19f,1,0.5f);

             transform.DOMoveY(5,0.5f);
             transform.DOMoveY(0.19f,0.5f).SetDelay(0.5f);
         }

        if (Input.GetKeyDown(KeyCode.A) && onleft == false && mid == true)
        {
           if(SceneManager.GetActiveScene().buildIndex ==0)
           { //transform.position = new Vector3(-3, transform.position.y, transform.position.z);
            transform.DOMoveX(-3,TransSpeed);}
            else
            {
            	transform.DOMoveX(-4,TransSpeed);
            }
            
            onleft = true;
            mid = false;
            GetComponent<Animator>().SetTrigger("GoLeft");

        }
        else if (Input.GetKeyDown(KeyCode.A) && onright == true && mid == false)
        {

            //transform.position = new Vector3(0, transform.position.y, transform.position.z);
            transform.DOMoveX(0, TransSpeed);

            mid = true;
            onright = false;
            GetComponent<Animator>().SetTrigger("GoLeft");
        }

        if (Input.GetKeyDown(KeyCode.D) && onright == false && mid == true)
        {
        	if(SceneManager.GetActiveScene().buildIndex ==0)
        	{ 
            //transform.position = new Vector3(3, transform.position.y, transform.position.z);
            transform.DOMoveX(3, TransSpeed);}
            else
            {
            	transform.DOMoveX(4, TransSpeed);
            }

            onright = true;
            mid = false;
            GetComponent<Animator>().SetTrigger("GoRight");

        }
        else if (Input.GetKeyDown(KeyCode.D) && onleft == true && mid == false)
        {
            //transform.position = new Vector3(0, transform.position.y, transform.position.z);
            transform.DOMoveX(0, TransSpeed);

            mid = true;
            onleft = false;
            GetComponent<Animator>().SetTrigger("GoRight");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
        	speed =0;
        	winpanel.SetActive(true);
        	 GetComponent<Animator>().SetBool("Victory", true);
        	 
              scoreText.text = "Score: " + Score;
        }

        if(other.tag == "Stop")
        {
             GetComponent<Animator>().SetBool("Run", false);
            speed = 0;

            
        }
        else if(other.tag == "Bum")
        {
        	if(heart <= 1)
        	{
             GetComponent<Animator>().SetBool("Death", true);

             failpanel.SetActive(true);
        	
            speed = 0;}
            else
             GetComponent<Animator>().SetTrigger("Hitr");
            

            heart -=1;
            ScorePenalty +=1;
            
            if(heart >= 1)
            Destroy(other.gameObject);


        
        }
        if(other.CompareTag("Coin"))
        {
           coin += 10;
           Destroy(other.gameObject);

           coinText.text =  coin.ToString();
        }

        if(other.CompareTag("Heart"))
        {
        	Destroy(other.gameObject);

        	if(heart<3)
        	{
        		heart += 1;
        		
        	}
        	if(heart ==3)
        	{
        	coin += 30;
        	 coinText.text =  coin.ToString();
        	}
        }
    }


    public void RestartLevel()
    {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }

    public void NextLevel()
    {
    	
    	if(SceneManager.GetActiveScene().buildIndex ==0)
    	{
    		SceneManager.LoadScene(1);
    	}
    		if(SceneManager.GetActiveScene().buildIndex ==1)
    	{
    		SceneManager.LoadScene(0);
    	}
    }
      


}
