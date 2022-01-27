using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float speed;
    [SerializeField] protected bool isDead;
    [SerializeField] protected Bone weaknessBone;

    protected float currentHp;
    protected float currentSpeed;

    public void DecreaseHP(float damage, Transform part, Vector3 velocity)
    {
        if (isDead || velocity.magnitude < 5f) return;

        currentHp -= damage;

        currentHp = Mathf.Clamp(currentHp, 0, hp);

        if(part.GetComponent<EnemyBone>().bone == weaknessBone)
        {
            Debug.Log("Critical!!");
        }
        else
        {
            Debug.Log("Damaged");
        }

        if(currentHp <= 0)
        {
            isDead = true;

            //part.gameObject.SetActive(false);
            

            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform childTrs = transform.GetChild(i);

                if (childTrs.gameObject.activeSelf)
                {
                    CharacterJoint cj = transform.GetChild(i).GetComponent<CharacterJoint>();
                    if (cj != null)
                    {
                        if (!cj.connectedBody.gameObject.activeSelf)
                           cj.connectedBody = null;
                    }

                    childTrs.GetComponent<Rigidbody>().useGravity = true;
                    childTrs.GetComponent<Rigidbody>().isKinematic = false;
                    childTrs.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    childTrs.GetComponent<Rigidbody>().angularVelocity =Vector3.zero;
                }
            }

            part.GetComponent<Rigidbody>().AddForce(velocity * 4, ForceMode.VelocityChange);
            transform.DetachChildren();
        }
    }
}
