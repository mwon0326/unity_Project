using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_action : MonoBehaviour
{
    public ParticleSystem trap_particle; // 재생할 함정 파티클
    public float trap_playTime; // 함정이 발동되는 시간
    public float trap_stopTime; // 함정이 멈춰있는 시간

    private Collider trap_collider;
    private bool is_trap = false;
    private float trap_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        trap_collider = GetComponent<Collider>();
        StartCoroutine(TrapEffect());
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (is_trap)
        {
            trap_time += Time.deltaTime;
            
            if (trap_time >= 2f)
            {
                Debug.Log("Dead");
                trap_time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (is_trap)
            trap_time = 0;
    }

    private IEnumerator TrapEffect()
    {
        while (true)
        {
            is_trap = true;
            trap_particle.Play();
            yield return new WaitForSeconds(trap_playTime);

            is_trap = false;
            trap_particle.Stop();
            trap_particle.Clear();
            yield return new WaitForSeconds(trap_stopTime);
        }
    }
}
