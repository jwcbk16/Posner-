using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Dot_Appears : MonoBehaviour {
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;

    public GameObject arrow;
    public GameObject dot;
    public int quadr_arrow;
    public string status;
    public int valid_cues;
    public int only_valid_depth;
    public int only_valid_alignment;
    public int not_valid;
    public int quadr_target;
    public float reaction_time;
    public string stopper;
    public string response = "Start";
    public int correct;
    public int wrong;
    public string correct_status;
    public string letter;
    int[,] data = new int[2, 4];
    public bool trig = false;
    public GameObject masker; 










    public void random_assigner(ref int quadr_arrow)
    {
        int loc = Random.Range(1, 5);
        if (loc == 1) {
            arrow.transform.position = new Vector3(13, 40, 170);
            quadr_arrow = 2;
        } else if (loc == 2) {
            arrow.transform.position = new Vector3(13, 40, 100);
            quadr_arrow = 30;
        } else if (loc == 3) {
            arrow.transform.position = new Vector3(13, 40, 170);
            arrow.transform.Rotate(Vector3.forward, 180f, Space.World);
            quadr_arrow = 1;
        } else if (loc == 4) {
            arrow.transform.position = new Vector3(13, 40, 100);
            arrow.transform.Rotate(Vector3.forward, 180f, Space.World);
            quadr_arrow = 40;
        }

    }

    public void rand_letter()
    {
        int loc = Random.Range(1, 5);
        if (loc == 1) {
            rand_assign(ref quadr_target, ref A);
            letter = "A";
        }
        if (loc == 2) {
            rand_assign(ref quadr_target, ref B);
            letter = "B";
        }
        if (loc == 3) {
            rand_assign(ref quadr_target, ref C);
            letter = "C";
        }
        if (loc == 4) {
            rand_assign(ref quadr_target, ref D);
            letter = "D";
        }

    }
    public void rand_assign(ref int quadr_target, ref GameObject GO)
    {

        int loc = Random.Range(1, 5);
        if (loc == 1) {
            GO.transform.position = new Vector3(100, 30, 135);
            quadr_target = 1;
        } else if (loc == 2) {
            GO.transform.position = new Vector3(-75, 30, 135);
            quadr_target = 2;
        } else if (loc == 3) {
            GO.transform.position = new Vector3(100, 30, 85);
            quadr_target = 40;
        } else if (loc == 4) {
            GO.transform.position = new Vector3(-75, 30, 85);
            quadr_target = 30;
        }
    }
    public void mask()
    {
        if (quadr_target == 1)
        {
            masker.transform.position = new Vector3(100, 30, 135);
        }
        else if (quadr_target == 2)
        {
            masker.transform.position = new Vector3(-75, 30, 135);
        }
        else if (quadr_target == 40)
        {
            masker.transform.position = new Vector3(100, 30, 85);
        }
        else if (quadr_target == 30)
        {
            masker.transform.position = new Vector3(-75, 30, 85);
        }
    }

	public void cue_determine(ref int valid_cues, ref int only_valid_depth,ref int only_valid_alignment, ref int not_valid) 
	{

		int qd = quadr_target;
		int qa = quadr_arrow;
		if (qd == qa) { 
			status = "Valid Cue";
			valid_cues += 1; 
		} else if (qd - qa == 10 || qd - qa == -10 || qd - qa == 1 || qd - qa == -1) {
			status = "Only Valid Depth";
			only_valid_depth += 1;
		} else if (qd - qa == 39 || qd - qa == -39 || qd - qa == 28 || qd - qa == -28) {
			status = "Only Valid Alignment";
			only_valid_alignment += 1; 
		} else { 
			status = "Completely Invalid Cue";
			not_valid += 1;
		}

	 
	}


	public void Letter()
	{
		rand_letter (); 
		cue_determine (ref valid_cues, ref only_valid_depth, ref only_valid_alignment, ref not_valid); 
	} 


	


	public IEnumerator Main_Coroutine () {
		for (int i = 0; i < 5; i++) { 
			stopper = "stop";
			response = "0";
			yield return new WaitForSeconds (2); 
			random_assigner (ref quadr_arrow); 
			yield return new WaitForSeconds (.15f);
			arrow.transform.position = new Vector3 (-300, -300, -300); 
			Letter ();
            yield return new WaitForSeconds(.1f);
            A.transform.position = new Vector3(-500, 0, 0);
            B.transform.position = new Vector3(-500, 0, 0);
            C.transform.position = new Vector3(-500, 0, 0);
            D.transform.position = new Vector3(-500, 0, 0);
            mask(); 
            print ("Please enter your response."); 
			while (response == "0" || !(Input.GetKey(KeyCode.Space))) {
				stopper = "go"; 
				yield return null;
			}
			data_collect ();
            masker.transform.position = new Vector3(-2000, 0, 0);
			
		}
	}

	public void Start(){
        

        StartCoroutine (Main_Coroutine()); 

	}

    public void Update() {
        if (stopper == "go") {
            if (Input.GetKey(KeyCode.A)) {
                response = "A";
                print(response);
            } else if (Input.GetKey(KeyCode.B)) {
                response = "B";
                print(response);
            } else if (Input.GetKey(KeyCode.C)) {
                response = "C";
                print(response);
            } else if (Input.GetKey(KeyCode.D)) {
                response = "D";
                print(response);
            }
           



        }
				
   
}
	public void data_collect() 
	{
		if (quadr_arrow == 30 || quadr_arrow == 40){
			quadr_arrow /= 10;
		}
		
		if (response == letter) {
			data [0, quadr_arrow - 1] += 1;
			correct_status = "Correct";
		}
		else 
		{
			data [1, quadr_arrow - 1] += 1; 
			correct_status = "Wrong";
		
	}

		print (correct_status); 
}

  
   
  

    }



