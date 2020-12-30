using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherMonster : MonoBehaviour
{
    [SerializeField]
    private float moveRange = 3f;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private Transform topRight;
    [SerializeField]
    private Transform bottomLeft;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private MirrorStudent[] students;

    public bool isMoving { get; private set; }

    public void Move()
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_FaceScarToButterfly && Inventory.instance.IsPlayerHasItem(typeof(Lighter)))
        {
            isMoving = true;
            MoveRandom();
        }
    }

    public void BurnAndMoveToStudents()
    {
        LeanTween.cancel(gameObject);
        StartCoroutine(IBurnAndMoveToStudents());
    }

    private IEnumerator IBurnAndMoveToStudents()
    {
        // 효과음 
        fire.SetActive(true);                                    // 얼굴이 불타오르는 애니메이션
        // 얼굴에 흉터가 남음
        yield return new WaitForSeconds(1f);
        Vector3 dest = (students[0].transform.position + students[1].transform.position) / students.Length;
        LeanTween.move(gameObject, dest, 1f).setSpeed(moveSpeed);               // 학생들을 향해서 이동
        yield return new WaitForSeconds(0.7f);
        students[0].Move();
        students[1].Move();
        Lighter lighter = ObjectManager.GetObject<Lighter>();
        lighter.canInteractWith = "Tree";
        lighter.useType = UseType.Interact;
        PlayerScan.instance.progressStatus = ProgressStatus.E_EndMirrorRoom;
    }

    private void MoveRandom()
    {
        Vector3 dest = transform.position + (Vector3)Random.insideUnitCircle * moveRange;
        dest.x = Mathf.Clamp(dest.x, bottomLeft.position.x, topRight.position.x);
        dest.y = Mathf.Clamp(dest.y, bottomLeft.position.y, topRight.position.y);
        LeanTween.move(gameObject, dest, 1f).setSpeed(moveSpeed).setOnComplete(MoveRandom);
    }
}
