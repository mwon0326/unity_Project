using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_niddle_action : MonoBehaviour
{
    public float max_height; // 최고 도달점
    public float min_height; // 최저 도달점
    public float velocity; // 움직이는 속도
    public float mass;
    public GameObject niddle;
    
    private bool isUP = true; // 올라가고 있는지 내려가고 있는지
    private bool isWait = false;
    private Collider niddle_collider;

    // Start is called before the first frame update
    void Start()
    {
        niddle_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (niddle.transform.position.y < max_height && isUP)
        {
            float acc = (9.8f * mass) / mass;
            float vel = velocity + acc * Time.deltaTime;
            velocity = vel;

            niddle.transform.Translate(Vector3.up * vel);
            
            if (niddle.transform.position.y >= max_height)
            {
                isUP = false;
                velocity = 5f;
            }
        }
        else if (niddle.transform.position.y > min_height && !isUP)
        {
            niddle.transform.Translate(Vector3.down * velocity);

            if (niddle.transform.position.y <= min_height)
            {
                velocity = 5f;
                if (!isWait)
                    StartCoroutine(WaitTime());
            }
        }
    }

    IEnumerator WaitTime()
    {
        isWait = true;
        yield return new WaitForSeconds(5f);
        isUP = true;
        isWait = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWait)
            Debug.Log("Dead");
    }
}

