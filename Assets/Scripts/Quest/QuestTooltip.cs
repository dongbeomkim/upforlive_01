using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestTooltip : MonoBehaviour
{
    public TextMeshProUGUI qeustName;
    public TextMeshProUGUI qeustDetail;

    int id;
    int index;

    private void Awake()
    {
        qeustName = transform.Find("QuestName").GetComponent<TextMeshProUGUI>();
        qeustDetail = transform.Find("QuestDetail").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Panel();
    }

    public void Panel()
    {

        int id = QuestManager.Instance.questId;
        int index = QuestManager.Instance.questActionIndex;

        if (id == 10 && index == 0)
        {
            qeustName.text = "����� ���� �����";
            qeustDetail.text = "����� ������ ��� ���� �����ϴ�. ���� ��θ� ã�ư�����.";
        }
        else if (id == 10 && index == 1)
        {
            qeustName.text = "����� ���� �����";
            qeustDetail.text = "�ٸ� �����ڴ� ������? ������ ���� �ѷ�����!";
        }
        else if (id == 20 && index == 0)
        {
            qeustName.text = "��ο��� �ٽ� ������";
            qeustDetail.text = "����� ������ ã�ƾ� �Ѵ�. ��ο��� �ٽ� ���ư���.";
        }
        else if (id == 20 && index == 1)
        {
            qeustName.text = "�����ڿ��� �ٽ� ���ư�����";
            qeustDetail.text = "�ֹ��� ��ó�� ��ο��� �๰�� �޾Ҵ�. �켱 �ֹο��� ��Ȳ�� ��������.";

        }
        if (id == 30 && index == 0)
        {
            qeustName.text = "���� �� ���� �� ��� óġ!";
            qeustDetail.text = $"���� ���� �����ϸ� ����� 5���� óġ����.\n��� ų�� : {QuestManager.Instance.killCount}";
            if (QuestManager.Instance.killCount == 5)
            {
                qeustName.text = "���� ���� �� ��� óġ �Ϸ�";
                qeustDetail.text = "��� 5������ óġ�ϸ� ������ �Ϸ��ߴ�. ��ο��� ���ư�����";
                QuestManager.Instance.NextQuest();
                QuestManager.Instance.killCount = 0;
            }
        }
        else if (id == 40 && index == 1)
        {
            qeustName.text = "��ΰ� �ڸ���� �ð�";
            qeustDetail.text = "���ȸ���� ����� ���õ� ������ �ִ��� �� �� �ѷ�����";
        }
        else if (id == 50 && index == 0)
        {
            qeustName.text = "������ ��� ���� ����";
            qeustDetail.text = "���ڸ� �������� �̻��� �������� �������Դ�. ������ Ǯ�� Ż������";
        }
        else if (id == 60 && index == 0)
        {
            qeustName.text = "Ȯ������ ����� ����";
            qeustDetail.text = "�����ϰ� ���� �ֹο��� �� ����� �˸���";
        }
        else if (id == 60 && index == 1)
        {
            qeustName.text = "��θ� ã�ƶ�";
            qeustDetail.text = "����� �˷����� ���� ���� ȸ������ ���ڸ� ȸ���ϰ� ��θ� ��ٸ���.";
        }
        else if (id == 70 && index == 0)
        {
            qeustName.text = "��θ� �����϶�";
            qeustDetail.text = "���� ��ó....�� �����Ϳ��� ���� �Ա��� ã�� ����.";
            if (QuestManager.Instance.bosskillCount == 1)
            {
                QuestManager.Instance.NextQuest();
                QuestManager.Instance.bosskillCount = 0;
                qeustName.text = "��� ���� �Ϸ�";
                qeustDetail.text = "������ ã�� ���Ͱ� �Ǿ���� ��θ� �����ߴ�....\n�ٽ� ������ ���ư���.";
            }
        }
        else if (id == 90 && index == 0)
        {
            qeustName.text = "�ٽ� ã�ƿ� ��ȭ";
            qeustDetail.text = "��ȭ�ο� ������ �ٽ� �������.";
        }

    }


    private void Start()
    {
        Close();
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
