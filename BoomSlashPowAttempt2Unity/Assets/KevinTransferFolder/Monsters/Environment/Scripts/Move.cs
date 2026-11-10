using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Move : MonoBehaviour
{
    public string playerTag = "Player";
    public float detectionRange = 15f;
    public float stopDistance = 2f;
    public float rotationSpeed = 5f;

    private Transform playerTransform;

    private float move = 20;
    private bool stop = false;
    private float blend;
    private float delay = 0;
    public float AddRunSpeed = 1;
    public float AddWalkSpeed = 1;
    private bool hasAniComp = false;
    private Animation anim;


    void Start()
    {

        if (null != GetComponent<Animation>())
        {
            hasAniComp = true;
        }

        GameObject playerObj = GameObject.FindWithTag(playerTag);
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }


    }

    void MoveActive()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > stopDistance)
        {

            Vector3 direction = (playerTransform.position - transform.position).normalized;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            float speed = 0.0f;
            float add = 0.0f;

            if (hasAniComp == true)
            {
                move *= 1.015F;

                if (move > 250 && CheckAniClip("run") == true)
                {
                    GetComponent<Animation>().CrossFade("run");
                    add = 20 * AddRunSpeed;
                }
                else
                {
                    GetComponent<Animation>().Play("walk");
                    add = 5 * AddWalkSpeed;
                }
                speed = Time.deltaTime * add;
                transform.Translate(0, 0, speed);
            }
            else
            {
                add = 5 * AddWalkSpeed;
                speed = Time.deltaTime * add;
                transform.Translate(0, 0, speed);
            }

        }
        else
        {
            if (hasAniComp == true)
            {
                if (GetComponent<Animation>().IsPlaying("walk"))
                {
                    GetComponent<Animation>().CrossFade("idle01", 0.3f);
                }
                if (GetComponent<Animation>().IsPlaying("run"))
                {
                    GetComponent<Animation>().CrossFade("idle01", 0.5f);
                    stop = true;
                }
                move = 20;

                if (stop == true)
                {
                    float max = Time.deltaTime * 20 * AddRunSpeed;
                    blend = Mathf.Lerp(max, 0, delay);
                    if (blend > 0)
                    {
                        transform.Translate(0, 0, blend);
                        delay += 0.025f;
                    }
                    else
                    {
                        stop = false;
                        delay = 0.0f;
                    }
                }
            }
        }
    }
    
    private bool CheckAniClip(string clipName)
    {
        if (!hasAniComp)
        {
            return false;
        }

        foreach (AnimationState state in anim)
        {
            if (state.clip != null &  state.clip.name == clipName)
            {
                return true;
            }
        }
        return false;
    }
}

