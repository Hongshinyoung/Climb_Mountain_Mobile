using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    private bool isWalk = true;
    private float transitionTime = 3f;

    public ParticleSystem buff;
    public RepeatMap map;
    public MountainInfo mountainInfo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // 버튼에 이벤트 리스너 추가
     //   button2.onClick.AddListener(SRun);
     //   button4.onClick.AddListener(FRun);

    }

    // Update is called once per frame
    void Update()
    {
        if (isWalk)
        {
            animator.SetBool("isHappyWalk", true);
        }
        if (map.moveSpeed > 2)
        {
            buff.gameObject.SetActive(true);
        }
        else buff.gameObject.SetActive(false);
    }

    // 슬로우 런 버튼을 눌렀을 때 실행될 코루틴
    public IEnumerator SRunCoroutine()
    {
        if (isWalk)
        {
            isWalk = false;
            animator.SetBool("isHappyWalk", false);
            animator.SetBool("isSlowRun", true);
            buff.gameObject.SetActive(true);
            map.moveSpeed = 3.5f;
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();

        }
    }

    // 빠른 런 버튼을 눌렀을 때 실행될 코루틴
    public IEnumerator FRunCoroutine()
    {
        if (isWalk)
        {
            isWalk = false;
            animator.SetBool("isHappyWalk", false);
            animator.SetBool("isFastRun", true);
            buff.gameObject.SetActive(true);
            map.moveSpeed = 5f;
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();
        }
    }

    // 슬로우 런 버튼을 눌렀을 때 호출될 함수
    public void SRun()
    {
        StartCoroutine(SRunCoroutine());
    }

    // 빠른 런 버튼을 눌렀을 때 호출될 함수
    public void FRun()
    {
        StartCoroutine(FRunCoroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //뒤로 조금 이동했다 다시 제자리로 돌아오게
            animator.SetTrigger("doDie");
            StartCoroutine(StopWalk());
        }

    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    ResetToWalk();
    //}

    public IEnumerator StopWalk()
    {
        map.moveSpeed = 0;
        yield return new WaitForSeconds(2.5f);
        ResetToWalk();
    }


    // 워킹 상태로 변경하는 함수
    public void ResetToWalk()
    {
        isWalk = true;
        animator.SetBool("isSlowRun", false);
        animator.SetBool("isFastRun", false);
        animator.SetBool("isHappyWalk", true);
        animator.SetBool("realFast", false);
        buff.gameObject.SetActive(false);
        map.moveSpeed = 1.5f;
    }

}
