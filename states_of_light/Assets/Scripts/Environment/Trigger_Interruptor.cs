using UnityEngine;
using System.Collections;

public class Trigger_Interruptor : MonoBehaviour
{
    //public SpriteRenderer sprite_Status;
    public LightEmitter light_emitter;

    public float activation_Time;
    public int id;

    void Start()
    {
        //sprite_Status.color = Color.red;
    }

    IEnumerator Activate()
    {
        //sprite_Status.color = Color.green;
        light_emitter.InterruptorIsActive(id);

        yield return new WaitForSeconds(activation_Time);

        //sprite_Status.color = Color.red;
        light_emitter.InterruptorIsInactive(id);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") || other.tag.Equals("PlayerSmall"))
        {
            StopAllCoroutines();
            StartCoroutine(Activate());
        }
    }
}
