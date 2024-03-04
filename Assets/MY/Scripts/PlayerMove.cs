using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public Button sRunButton; // ���ο� �� ��ư
    public Button fRunButton; // ���� �� ��ư
    private bool isSlowWalk = true;
    private float transitionTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // ��ư�� �̺�Ʈ ������ �߰�
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

    // ���ο� �� ��ư�� ������ �� ����� �ڷ�ƾ
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

    // ���� �� ��ư�� ������ �� ����� �ڷ�ƾ
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

    // ���ο� �� ��ư�� ������ �� ȣ��� �Լ�
    void SRun()
    {
        StartCoroutine(SRunCoroutine());
    }

    // ���� �� ��ư�� ������ �� ȣ��� �Լ�
    void FRun()
    {
        StartCoroutine(FRunCoroutine());
    }

    // ��ŷ ���·� �����ϴ� �Լ�
    void ResetToWalk()
    {
        isSlowWalk = true;
        animator.SetBool("is_sRun", false);
        animator.SetBool("is_fRun", false);
        animator.SetBool("is_Walk", true);
    }
}
