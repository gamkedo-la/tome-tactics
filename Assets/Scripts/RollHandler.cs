using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollHandler : MonoBehaviour
{
	[SerializeField] private GameObject die;
	private GameObject[] dieInst = null;
	private Rigidbody rig;

    void Start() { }

    void Update()
    {
        if (dieInst == null)
            return;

        foreach (GameObject die in dieInst)
        {
            rig = die.GetComponent<Rigidbody>();
            if (rig != null)
            {
                if (rig.IsSleeping())
                {
                    getResult();
                    rig = null;
                    dieInst = null;
                }
            }
        }
    }

    public void rollDice(int numOfDice = 1)
    {
    	Vector3 pos = new Vector3(5.0f, 7.5f, 5.0f);

        dieInst = new GameObject[numOfDice];

        for (int i = 0; i < numOfDice; ++i)
        {
            dieInst[i] = Instantiate(die, pos, Random.rotation);
            rig = dieInst[i].GetComponent<Rigidbody>();

            rig.AddForce(-5.0f, 0.0f, -5.0f, ForceMode.Impulse);
            rig.AddTorque(Random.Range(30.0f, 40.0f), 
                        Random.Range(30.0f, 40.0f),
                        Random.Range(30.0f, 40.0f));
        }
    }

    public void getResult()
    {
    	int diceCount = 0;

    	// Debug.Log(Vector3.Dot (dieInst.transform.forward, Vector3.up));
    	// Debug.Log(Vector3.Dot (dieInst.transform.up, Vector3.up));
    	// Debug.Log(Vector3.Dot (dieInst.transform.right, Vector3.up));

        foreach (GameObject die in dieInst)
        {

        	if (Vector3.Dot (die.transform.forward, Vector3.up) > 0.9)
        		diceCount = 4; // 5
        	else if (Vector3.Dot (-die.transform.forward, Vector3.up) > 0.9)
        		diceCount = 5; // 2
        	else if (Vector3.Dot (die.transform.up, Vector3.up) > 0.9)
        		diceCount = 3; // 3
        	else if (Vector3.Dot (-die.transform.up, Vector3.up) > 0.9)
        		diceCount = 1; // 4
        	else if (Vector3.Dot (die.transform.right, Vector3.up) > 0.9)
        		diceCount = 6; // 6
        	else if (Vector3.Dot (-die.transform.right, Vector3.up) > 0.9)
        		diceCount = 2; // 1

        	Debug.Log ("diceCount :" + diceCount);
            Destroy(die);
        }

    }
}
