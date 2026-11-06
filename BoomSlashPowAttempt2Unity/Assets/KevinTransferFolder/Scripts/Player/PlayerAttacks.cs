using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject orbPrefab;

    [SerializeField] private InputActionReference basicAttack;

    private Animator anim;

    private bool isAttacking = false;

    private bool canUseOrbAttack = false;

    private string playerClass;

    [SerializeField] private float warriorLongSwordRange = 2.5f;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim.runtimeAnimatorController.name == "magi_earthen_v1_0")
        {
            playerClass = "Mage";
            canUseOrbAttack = true;
        }
        else if (anim.runtimeAnimatorController.name == "warrior")
        {
            playerClass = "Warrior";
            canUseOrbAttack = false;
        }
    }
    void Update()
    {
        if (basicAttack.action.triggered && !isAttacking)
        {
            isAttacking = true;

            anim.SetBool("attacking", true);

            if (playerClass == "Mage")
            {
                StartCoroutine(SpawnOrbAfterDelay(8));
            }
            else if (playerClass == "Warrior")
            {

            }
            StartCoroutine(ResetAttackAfterDuration(0.6f));
        }

    }

    IEnumerator ResetAttackAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndAttackAnimation();
    }

    void WarriorMeleeAttack()
    {

    }

    void MageSpawnOrb()
    {
        Vector3 playerOffset = new Vector3(0.05f, 1.25f, 0.6f);
        Vector3 rotatedPlayerOffset = transform.TransformDirection(playerOffset);
        Vector3 spawnPosition = transform.position + rotatedPlayerOffset;

        GameObject spawnedObject = Instantiate(orbPrefab, spawnPosition, Camera.main.transform.rotation);
    }

    IEnumerator WaitForFramesThenSpawnOrb(int framesToWait)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        MageSpawnOrb();

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

    IEnumerator SpawnOrbAfterDelay(int framesToWait)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }
        MageSpawnOrb();
    }


    public void EndAttackAnimation()
    {
        anim.SetBool("attacking", false);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, warriorLongSwordRange);
    }
}
