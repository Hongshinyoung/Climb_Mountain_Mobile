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
        // 5초에서 30초 사이에 한 번씩 isVisible을 true로 변경
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

        // 3초 뒤에 isVisible을 다시 false로 변경
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
