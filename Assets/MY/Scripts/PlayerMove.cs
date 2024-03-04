using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public Button sRunButton; // 슬로우 런 버튼
    public Button fRunButton; // 빠른 런 버튼
    private bool isSlowWalk = true;
    private float transitionTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // 버튼에 이벤트 리스너 추가
        sRunButton.onClick.AddListener(SRun);
        fRunButton.onClick.AddListener(FRun);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowWalk)
        {
            animator.SetBool("is_Walk", true);
        }
    }

    // 슬로우 런 버튼을 눌렀을 때 실행될 코루틴
    IEnumerator SRunCoroutine()
    {
        if (isSlowWalk)
        {
            isSlowWalk = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_sRun", true);
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();
        }
    }

    // 빠른 런 버튼을 눌렀을 때 실행될 코루틴
    IEnumerator FRunCoroutine()
    {
        if (isSlowWalk)
        {
            isSlowWalk = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_fRun", true);
            yield return new WaitForSeconds(transitionTime);
            ResetToWalk();
        }
    }

    // 슬로우 런 버튼을 눌렀을 때 호출될 함수
    void SRun()
    {
        StartCoroutine(SRunCoroutine());
    }

    // 빠른 런 버튼을 눌렀을 때 호출될 함수
    void FRun()
    {
        StartCoroutine(FRunCoroutine());
    }

    // 워킹 상태로 변경하는 함수
    void ResetToWalk()
    {
        isSlowWalk = true;
        animator.SetBool("is_sRun", false);
        animator.SetBool("is_fRun", false);
        animator.SetBool("is_Walk", true);
    }
}
