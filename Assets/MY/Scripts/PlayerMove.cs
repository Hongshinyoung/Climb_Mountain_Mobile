using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    //public Button button1; // 아이젠
    //public Button button2; // 초코바
    //public Button button3; // 프로틴
    //public Button button4; // 레드불
    //public Button button5; // 산삼
    //public Button button6; // 초코파이
    //public Button button7; // 도토리묵
    //public Button button8; //인삼
    private bool isSlowWalk = true;
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
        if (isSlowWalk)
        {
            animator.SetBool("is_Walk", true);
        }
        if (map.moveSpeed > 1.5)
        {
            buff.gameObject.SetActive(true);
        }
        else buff.gameObject.SetActive(false);
    }

    // 슬로우 런 버튼을 눌렀을 때 실행될 코루틴
    public IEnumerator SRunCoroutine()
    {
        if (isSlowWalk)
        {
            isSlowWalk = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_sRun", true);
            buff.gameObject.SetActive(true);
            map.moveSpeed = 3.0f;
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();

        }
    }

    // 빠른 런 버튼을 눌렀을 때 실행될 코루틴
    public IEnumerator FRunCoroutine()
    {
        if (isSlowWalk)
        {
            isSlowWalk = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_fRun", true);
            buff.gameObject.SetActive(true);
            map.moveSpeed = 4.5f;
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();
        }
    }

    // 슬로우 런 버튼을 눌렀을 때 호출될 함수
    public void SRun()
    {
        StartCoroutine(SRunCoroutine());
        // ResetToWalk();
    }

    // 빠른 런 버튼을 눌렀을 때 호출될 함수
    public void FRun()
    {
        StartCoroutine(FRunCoroutine());
        // ResetToWalk();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //뒤로 조금 이동했다 다시 제자리로 돌아오게
            animator.SetTrigger("is_die");
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
        yield return new WaitForSeconds(3);
        ResetToWalk();
    }


    // 워킹 상태로 변경하는 함수
    public void ResetToWalk()
    {
        isSlowWalk = true;
        animator.SetBool("is_sRun", false);
        animator.SetBool("is_fRun", false);
        animator.SetBool("is_Walk", true);
        animator.SetBool("realFast", false);
        buff.gameObject.SetActive(false);
        map.moveSpeed = 1.5f;
    }
}
