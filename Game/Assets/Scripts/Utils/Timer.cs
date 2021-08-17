using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


class TimerData//cria essa classe para tipagem do timer
{

    public string timerTag;

    public float initialTime;
    public float timeStamp;
    public float endTime;

    public bool isActive = true;
    public bool isDone = false;

    public Action timerCallback;
    public bool loopTimer;
}

public class Timer : MonoBehaviour
{

    List<TimerData> timers = new List<TimerData>();//cria uma lista para guardar os timers, lista de TimerDatas, classe acima para tipagem

    void Update()
    {
        for (var i = 0; i < timers.Count; i++)//loopa pela lista de timers
        {
            //logica do timer, ve quanto tempo passou e desconto do objeto no index tal da lista
            if (timers[i].isActive)
            {
                timers[i].timeStamp -= Time.deltaTime;

                if (timers[i].timeStamp <= timers[i].endTime)//se o tempo acabou
                {
                    try//tenta executar o callback, essa funcao e passada quando vc cria o timer, entao, quando o timer acaba ele executa ela
                    {   //o try ta aqui, caso o usuario passe uma funcao errada que nao e do tipo certo, quando vc passa a funcao, vc passa a referencia dela, entao caso essa referencia seja destruida, tipo quando o fantasma morre, vai da erro, por isso e bom ter o try aq
                        timers[i].timerCallback();
                    }
                    catch (Exception e)//se der alguma coisa errado printa a mensagem de erro
                    {
                        Debug.LogError("Timer callback error: " + e.Message);
                    }
                    if (timers[i].loopTimer)//ve se e loop, ai volta dnv
                    {
                        timers[i].timeStamp = timers[i].initialTime;
                    }
                    else
                    {
                        timers[i].isActive = false;
                    }
                }
            }
        }
    }

    public void CreateTimer(string tag, float start, float end, bool loop, Action callbackMethod)//funcao para adiiconar um timer, voce precisa passar uma tag q e como se fosse um id, o tempo de duracao, o tempo quando for acabar, se e para ser loop, e uma funcao para excecutar quando o timer acabar
    {
        timers.Add(new TimerData { timerTag = tag, initialTime = start, timeStamp = start, endTime = end, timerCallback = callbackMethod, loopTimer = loop });
    }

    private TimerData FindTimer(string tag)//voce passa a tag e ele retorna o objeto de timer
    {
        var t = new TimerData();
        foreach (var timer in timers)
        {
            if (timer.timerTag == tag)
            {
                t = timer;
            }
        }
        return t;
    }

    public void PauseTimer(string tag)//pausa o timer
    {
        FindTimer(tag).isActive = false;
    }

    public void ResumeTimer(string tag)//volta o timer
    {
        FindTimer(tag).isActive = true;
    }

    public void CancelTimer(string tag)//cancela o timer
    {
        timers.Remove(FindTimer(tag));
    }

    public float GetTimeStamp(string tag)//pega quando ja passou do timer
    {
        return FindTimer(tag).timeStamp;
    }
}
