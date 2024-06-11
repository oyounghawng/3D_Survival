using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class UI_Title : UI_Scene
{

    int count = 0;
    int _index = 0;
    bool _isType;
    string _curScriptLine;
    float _interval;
    public int _charPerSecend = 15;

    enum GameObjects
    {
        BG
    }
    enum Texts
    {
        Title,
        TitleText,
        StoryText
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.Title).gameObject.BindEvent(Startevent);
        GetText((int)Texts.TitleText).text = "";
        GetText((int)Texts.StoryText).text = "";
    }
    string[] title = { "�ٰŸ� �Ұ�", "������ ����", "�÷��̾��� �ӹ�", "�ڿ��� ����", "����Ƽ ¯: �߼� ����Į����, ����� ������ ����, ������ ����Դϴ�." };
    string[] story = { "�������� ����Ƽ ¯�� ������ �߼��ô뿡 ������ ����Į������ ���踦 ü���� ���Դϴ�. ������ �밨�԰� ��ö�� ������ �ʿ��� �� ���迡��, ������ ���� ���� ���� ���� �տ� �޷� �ֽ��ϴ�.",
    "�������� �ǹ���ġ��, ������ ���簡 �ָ���ġ��, ��� ��ȭ�� �������� ���� �����մϴ�. �� ���� ������ ������ ������ �ı��� �����ϴ� ������ �����Դϴ�.","�׷��� ������ ���󿡼�, ����� ������ ����Դϴ�. ����, Ž��, �׸��� ��ȭ�� ���� �� ���踦 ȹ���ϰ�, ������ �������� �¸��ϴ� ���� ����� ��ǥ�Դϴ�.",
    "�ڿ��� ��ġ �ְ� ������ ���Ӿ����ϴ�. �׷��� �� ����� ���� ����� �� ���ϰ� ���� ���Դϴ�. �� �ӿ��� ��μ� ������ ��簡 �Ǿ�� ������ ��������.",
    ""
    };

    private void GameStart(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.SceneType.MainScene);
    }

    private void Startevent(PointerEventData data)
    {
        GetObject((int)GameObjects.BG).BindEvent(NextIndex);
        StartCoroutine(DelayText());
    }

    IEnumerator DelayText()
    {
        yield return new WaitForSeconds(1f);
        GetText((int)Texts.TitleText).text = title[count];
        SetLine(story[count]);
    }

    private void NextIndex(PointerEventData data)
    {
        if (count == 4)
        {
            GetText((int)Texts.Title).gameObject.BindEvent(GameStart);
            return;
        }
        if (!_isType)
        {
            count++;
            _curScriptLine = story[count];
            SetLine(_curScriptLine);
            GetText((int)Texts.TitleText).text = title[count];
        }
        else
        {
            SetLine(_curScriptLine);
        }
    }

    void SetLine(string script)
    {
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.StoryText).text = _curScriptLine;
            EndTyping();
        }
        else
        {
            _curScriptLine = script;
            StartCoroutine(StartTyping());
        }
    }
    IEnumerator StartTyping()
    {
        GetText((int)Texts.StoryText).text = "";
        _index = 0;
        _isType = true;

        yield return new WaitForSeconds(_interval);
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        if (GetText((int)Texts.StoryText).text == _curScriptLine)
        {
            EndTyping();
            yield break;
        }

        GetText((int)Texts.StoryText).text += _curScriptLine[_index];

        //Sound
        if (_curScriptLine[_index] != ' ' || _curScriptLine[_index] != '.')
            Managers.Sound.Play(Define.Sound.Effect, "Effects/Typing");

        _index++;

        yield return new WaitForSeconds(_interval);
        StartCoroutine(Typing());
    }
    void EndTyping()
    {
        _isType = false;
    }
}