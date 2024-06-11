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
    string[] title = { "줄거리 소개", "세계의 상태", "플레이어의 임무", "자원과 위험", "유니티 짱: 중세 아포칼립스, 당신은 최후의 전사, 최후의 희망입니다." };
    string[] story = { "여러분은 유니티 짱의 눈으로 중세시대에 떨어진 아포칼립스의 세계를 체험할 것입니다. 무모한 용감함과 냉철한 지혜가 필요한 이 세계에서, 생존은 오직 가장 강한 자의 손에 달려 있습니다.",
    "전염병이 판박이치고, 마법의 잔재가 휘몰아치며, 고대 신화의 괴물들이 땅을 유랑합니다. 이 곳은 과거의 영광과 현재의 파괴가 공존하는 무모한 세계입니다.","그렇게 조각난 세상에서, 당신은 유일한 희망입니다. 생존, 탐험, 그리고 진화를 통해 이 세계를 획득하고, 마지막 전투에서 승리하는 것이 당신의 목표입니다.",
    "자원은 가치 있고 위험은 끊임없습니다. 그러나 각 위험과 고난은 당신을 더 강하게 만들 것입니다. 고난 속에서 비로소 진정한 용사가 되어가는 여정을 떠나세요.",
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