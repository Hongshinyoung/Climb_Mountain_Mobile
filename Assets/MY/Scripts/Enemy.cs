using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public MeshRenderer mesh;
    public Animator animator;
    public CapsuleCollider col;
    public RepeatMap map;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();

        mesh.enabled = false;
        // 5�ʿ��� 30�� ���̿� �� ���� isVisible�� true�� ����
        InvokeRepeating("AppearRandomly", 20f, Random.Range(150f, 300f));
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null && mesh.enabled)
        {
            animator.SetBool("isWalk", true);
            transform.Translate(Vector3.down * 9 * Time.deltaTime);
        }
    }

    void AppearRandomly()
    {
        mesh.enabled = true;
        col.enabled = true;

        // 3�� �ڿ� isVisible�� �ٽ� false�� ����
        Invoke("Disappear", 6f);
    }

    void Disappear()
    {
        mesh.enabled = false;
        col.enabled = false;
        //transform.position = map.enemyRootPos.position;
        //   transform.position = new Vector3(-10,0,0.7f);
    }

    //void ResetPosition()
    //{
    //    if(map.isReset == true)
    //    {
    //        transform.position = map.endPoint.position;
    //    }
    //}
}
