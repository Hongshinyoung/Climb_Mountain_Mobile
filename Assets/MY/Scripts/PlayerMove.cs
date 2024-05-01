using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    //public Button button1; // ������
    //public Button button2; // ���ڹ�
    //public Button button3; // ����ƾ
    //public Button button4; // �����
    //public Button button5; // ���
    //public Button button6; // ��������
    //public Button button7; // ���丮��
    //public Button button8; //�λ�
    private bool isSlowWalk = true;
    private float transitionTime = 3f;

    public ParticleSystem buff;

    public RepeatMap map;
    public MountainInfo mountainInfo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // ��ư�� �̺�Ʈ ������ �߰�
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

    // ���ο� �� ��ư�� ������ �� ����� �ڷ�ƾ
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

    // ���� �� ��ư�� ������ �� ����� �ڷ�ƾ
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

    // ���ο� �� ��ư�� ������ �� ȣ��� �Լ�
    public void SRun()
    {
        StartCoroutine(SRunCoroutine());
        // ResetToWalk();
    }

    // ���� �� ��ư�� ������ �� ȣ��� �Լ�
    public void FRun()
    {
        StartCoroutine(FRunCoroutine());
        // ResetToWalk();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //�ڷ� ���� �̵��ߴ� �ٽ� ���ڸ��� ���ƿ���
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


    // ��ŷ ���·� �����ϴ� �Լ�
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
