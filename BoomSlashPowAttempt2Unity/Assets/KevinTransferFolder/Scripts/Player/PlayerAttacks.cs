using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject orbPrefab;

    [SerializeField] private InputActionReference orbAttack;

    private Animator anim;

    private bool isAttacking = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (orbAttack.action.triggered && !isAttacking)
        {
            anim.SetBool("attacking", true);
            StartCoroutine(WaitForFramesThenSpawnOrb(8));
        }
    }

    void SpawnOrb()
    {
        Vector3 playerOffset = new Vector3(0.05f, 1.25f, 0.6f);
        Vector3 rotatedPlayerOffset = transform.TransformDirection(playerOffset);
        Vector3 spawnPosition = transform.position + rotatedPlayerOffset;

        GameObject spawnedObject = Instantiate(orbPrefab, spawnPosition, transform.rotation);
    }

    IEnumerator WaitForFramesThenSpawnOrb(int framesToWait)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        SpawnOrb();

        anim.SetBool("attacking", false);

        yield return null;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        isAttacking = false;
    }
}
