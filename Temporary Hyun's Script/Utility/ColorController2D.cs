using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
namespace Croquette
{

    public class ColorController2D
    {
        enum State { waiting = 0, proceeding, complete }
        State curState = State.waiting;

        float time = 0;
        Color curColor;
        Color startColor;
        Color destColor;
        float startAlpha;
        float destAlpha;
        float duration;

        SpriteRenderer spriteRenderer;
        Action completeCallback;

        public ColorController2D(SpriteRenderer _spriteRenderer) { spriteRenderer = _spriteRenderer; }

        /// <summary>
        /// 미완 추후 수정
        /// </summary>
        public void ColorControl(Color _startColor, Color _destColor, float _duration, Action _completeCallback)
        {
            curState = State.proceeding;
            time = 0;
            duration = _duration;

            curColor = spriteRenderer.color;
            startColor = _startColor;
            destColor = _destColor;
            spriteRenderer.color = startColor;
            completeCallback = _completeCallback;

            UniRx.MainThreadDispatcher.StartCoroutine(ControlRoutine());
        }

        public void AlphaControl(float _startAlpha, float _destAlpha, float _duration, Action _completeCallback)
        {
            curState = State.proceeding;
            time = 0;
            duration = _duration;
            startAlpha = _startAlpha;
            destAlpha = _destAlpha;


            spriteRenderer.color = new Color(1, 1, 1, _startAlpha / 255);
            completeCallback = _completeCallback;

            UniRx.MainThreadDispatcher.StartFixedUpdateMicroCoroutine(AlphaControlRoutine());
        }

        IEnumerator ControlRoutine()
        {
            while (curState != State.complete)
            {
                switch (curState)
                {
                    case State.waiting:
                        break;
                    case State.proceeding:
                        time += Time.deltaTime;
                        if (time / duration < 1)
                        {
                            // 미완 코드 추후 수정
                            float increaseRed = destColor.r - startColor.r;
                            float increaseGreen = destColor.g - startColor.g;
                            float increaseBlue = destColor.b - startColor.b;
                            float increaseAlpha = destColor.a - startColor.a;

                            increaseRed *= time / duration;
                            increaseGreen *= time / duration;
                            increaseBlue *= time / duration;
                            increaseAlpha *= time / duration;

                            curColor = new Color(curColor.r + increaseRed
                                , curColor.g + increaseGreen
                                , curColor.b + increaseBlue
                                , curColor.a + increaseAlpha);
                            spriteRenderer.color = curColor;
                        }
                        else if (time / duration >= 1)
                        {
                            curColor = new Color(destColor.r, destColor.g, destColor.b, destColor.a);

                            curState = State.complete;
                        }
                        break;
                }
                yield return new WaitForEndOfFrame();
            }
            if (curState == State.complete)
            {
                completeCallback();
                time = 0;
                curState = State.waiting;
            }


            yield return null;
        }
        IEnumerator AlphaControlRoutine()
        {
            while (curState != State.complete)
            {
                switch (curState)
                {
                    case State.waiting:
                        break;
                    case State.proceeding:
                        time += Time.deltaTime;
                        if (time / duration < 1)
                        {
                            float increaseAlpha = destAlpha - startAlpha;
                            increaseAlpha *= time / duration;
                            spriteRenderer.color = new Color(1, 1, 1, (startAlpha + increaseAlpha) / 255);
                        }
                        else if (time / duration >= 1)
                        {
                            spriteRenderer.color = new Color(1, 1, 1, destAlpha / 255);

                            curState = State.complete;
                        }
                        break;
                }
                yield return new WaitForEndOfFrame();
            }
            if (curState == State.complete)
            {
                completeCallback();
                time = 0;
                curState = State.waiting;
            }


            yield return null;
        }
    }

}