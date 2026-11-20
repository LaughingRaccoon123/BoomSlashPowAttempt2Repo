using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Net.Http.Headers;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject orbPrefab;

    [SerializeField] private InputActionReference basicAttack;

    private Animator anim;

    private string playerClass;

    [SerializeField] private float warriorLongSwordRange = 2.5f;

    [SerializeField]
    private float upwardAngleOffset = 30f;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim.runtimeAnimatorController.name == "magi_earthen_v1_0")
        {
            playerClass = "Mage";
        }
        else if (anim.runtimeAnimatorController.name == "warrior")
        {
            playerClass = "Warrior";
        }
    }
    void Update()
    {
        if (basicAttack.action.triggered)
        {

            anim.ResetTrigger("attackTrigger");
            anim.SetTrigger("attackTrigger");

            if (playerClass == "Mage")
            {
                StartCoroutine(SpawnOrbAfterDelay(8));
                StartCoroutine(ResetAttackAfterDuration(0.58f));
            }
            else if (playerClass == "Warrior")
            {
                
            }
        }
    }

    IEnumerator ResetAttackAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndAttackAnimation();
    }

    void MageSpawnOrb()
    {
        Vector3 playerOffset = new Vector3(0.05f, 1.5f, 0.6f);
        Vector3 rotatedPlayerOffset = transform.TransformDirection(playerOffset);
        Vector3 spawnPosition = transform.position + rotatedPlayerOffset;

        Quaternion adjustedRotation = Camera.main.transform.rotation * Quaternion.Euler(-upwardAngleOffset, 0f, 0f);

        GameObject spawnedObject = Instantiate(orbPrefab, spawnPosition, adjustedRotation);
    }

    IEnumerator SpawnOrbAfterDelay(int framesToWait)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        if (playerClass == "Mage")
        {
            MageSpawnOrb();
        }
    }

    public void EndAttackAnimation()
    {
        anim.ResetTrigger("attackTrigger");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, warriorLongSwordRange);
    }
}
