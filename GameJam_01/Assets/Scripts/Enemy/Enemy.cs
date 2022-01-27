using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DIFFICULTY
{
    EASY,
    NORMAL,
    HARD
}


public class Enemy : MonoBehaviour
{
    [SerializeField] public DIFFICULTY difficulty;
    [SerializeField] protected float hp;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] public bool isDead;
    [SerializeField] protected Bone weaknessBone;
    [SerializeField] protected int score;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDelay;
    
    [SerializeField] protected MeshRenderer[] meshes_head;
    [SerializeField] protected MeshRenderer[] meshes_body;
    [SerializeField] protected MeshRenderer[] meshes_lowerBody;

    protected GameObject target;
    protected Rigidbody rigid;

    protected float currentHp;
    protected float currentSpeed;

    [SerializeField] protected Transform test;
    [SerializeField] protected List<GameObject> enemyBones = new List<GameObject>();

    virtual protected void Start()
    {
        target = GameManager.Instance.playerController.gameObject;
        rigid = this.GetComponent<Rigidbody>();

        if (weaknessBone == Bone.NONE)
        {
            weaknessBone = (Bone)Random.Range((int)Bone.NONE + 1, (int)Bone.LOWERBODY + 1);
            SetPartColor();
        }
    }

    void SetPartColor()
    {
        switch(weaknessBone)
        {
            case Bone.HEAD:
              foreach(MeshRenderer m in meshes_head)
                {
                    m.material.color = Color.red;
                }
                foreach (MeshRenderer m in meshes_body)
                {
                    m.material.color = Color.white;
                }
                foreach (MeshRenderer m in meshes_lowerBody)
                {
                    m.material.color = Color.white;
                }
                break;
            case Bone.BODY:
                foreach (MeshRenderer m in meshes_head)
                {
                    m.material.color = Color.white;
                }
                foreach (MeshRenderer m in meshes_body)
                {
                    m.material.color = Color.red;
                }
                foreach (MeshRenderer m in meshes_lowerBody)
                {
                    m.material.color = Color.white;
                }
                break;
            case Bone.LOWERBODY:
                foreach (MeshRenderer m in meshes_head)
                {
                    m.material.color = Color.white;
                }
                foreach (MeshRenderer m in meshes_body)
                {
                    m.material.color = Color.white;
                }
                foreach (MeshRenderer m in meshes_lowerBody)
                {
                    m.material.color = Color.red;
                }

                break;
        }
    }
  

    public void DecreaseHP(float damage, Transform part, Vector3 velocity)
    {
        if (isDead || velocity.magnitude < 6f) return;

        currentHp -= damage;

        currentHp = Mathf.Clamp(currentHp, 0, hp);

        if(part.GetComponent<EnemyBone>().bone == weaknessBone)
        {
            GameManager.Instance.scoreManager.IncreaseScore(score * 2);
        }
        else
        {
            GameManager.Instance.scoreManager.IncreaseScore(score);
        }

        if(currentHp <= 0)
        {
            isDead = true;

            foreach(EnemyBone e in this.transform.GetComponentsInChildren<EnemyBone>())
            {
                //enemyBones.Add(e);
                e.GetComponent<Rigidbody>().useGravity = true;
                e.GetComponent<Rigidbody>().isKinematic = false;
                e.GetComponent<Rigidbody>().velocity = Vector3.zero;
                e.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                e.gameObject.layer = LayerMask.NameToLayer("Ragdoll");
                //e.transform.parent = null;
            }

            test.parent = null;
    
            part.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(velocity * 2, 30), ForceMode.VelocityChange);

            StartCoroutine(ResetDelay());
        }
    }

    virtual protected IEnumerator ResetDelay(float time = 2.0f)
    {
        yield return new WaitForSeconds(time);

        //isDead = false;
        currentHp = hp;
        
        weaknessBone = (Bone)Random.Range(0, (int)Bone.LOWERBODY + 1);
        SetPartColor();
        //foreach (GameObject e in enemyBones)
        //{
        //    //e.gameObject.SetActive(false);
        //}
        test.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
