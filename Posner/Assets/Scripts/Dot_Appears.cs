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

     int arrow_pos_left = -50;
     int arrow_pos_right = 50;
     int arrow_y = 43;
     int arrow_pos_forward = 100;
     int arrow_pos_back = 170;
     int arrow_x;
     int arrow_z;

     int targ_pos_left = -100;
     int targ_pos_right = 100;
     int targ_pos_middle = 0;
     int targ_pos_forward = 0;
     int targ_pos_back = 150;
     int targ_y = 43;
     int targ_x;
     int targ_z;
     int left_or_right;

    public void random_assigner(ref int quadr_arrow)
    {
        int left_or_right = Random.Range(1, 3);
        if (left_or_right == 1)
        {
            arrow_x = arrow_pos_left;
        } else {
            arrow_x = arrow_pos_right;
        }
        int loc = Random.Range(1, 5);
        if (loc == 1) {
            quadr_arrow = 2;
        } else if (loc == 2) {
            quadr_arrow = 30;
        } else if (loc == 3) {
            quadr_arrow = 1;
        } else {
            quadr_arrow = 40;
        }
        arrow.transform.position = new Vector3(arrow_x, arrow_y, arrow_z);
        if  (loc == 3 || loc == 4) { 
                            arrow.transform.Rotate(Vector3.forward, 180f, Space.World);
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
        if (left_or_right == 1)
        {
            targ_x = targ_pos_left;
        }
        else
        {
            targ_x = targ_pos_right;
        }
        int loc = Random.Range(1, 5);
        if (loc == 1) {
            targ_x = targ_pos_left; targ_z = targ_pos_forward;
            quadr_target = 1;
        } else if (loc == 2) {
            targ_x = targ_pos_left; targ_z = targ_pos_back;
            quadr_target = 2;
        } else if (loc == 3) {
            targ_x = targ_pos_middle; targ_z = targ_pos_forward;
            quadr_target = 40;
        } else {
            targ_x = targ_pos_middle; targ_z = targ_pos_back;
            quadr_target = 30;
        }
        GO.transform.position = new Vector3(targ_x,targ_y,targ_z);
            }

    public void mask()
    {
        masker.transform.position = new Vector3(targ_x, targ_y, targ_z);
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