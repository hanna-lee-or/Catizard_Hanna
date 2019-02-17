﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N_CardSystem : MonoBehaviour
{

    public bool isGame = true, isCurse = false, isProvoke = false;
    public bool isSOS = false, isWild = false, isCatnip = false, isCatnipOn = false;
    public int GameMinute = 5, HeroSpeed = 1, cat_wait = 4, Cat_num_prev = 0, Cat_num_curr = 0, provoke_time = 18;
    public Slider HeroSlider;
    public Animator HeroAnimator;
    public GameObject HeroSOS, HeroCurse, HeroDual;
    public RectTransform Hero;
    public RectTransform[] HeroABC;
    public Transform Cat, SP_bar;
    public GridView gridView;
    public Slider SP_Slider;
    public GameObject[] Cat_graphic;
    public GameObject CardCover;
    public GameObject[] UIArray_N, UIArray_E;
    public Image[] UIImage_N, UIImage_E;
    public GameObject[] catnip;
    public Transform[] catnipXY;

    private int catnipIndex = 0, maxCatnip, SOS_repeat = 0, Provoke_repeat = 0;
    private float blockSize, blockBuffer;
    private Point next = new Point(6, 0);

    // Start is called before the first frame update
    void Awake()
    {
        blockSize = gridView.blockSize;
        blockBuffer = gridView.blockBuffer;
        HeroSlider.maxValue = GameMinute * 120;
        maxCatnip = catnip.Length;
        StartCoroutine("HeroTimer");
        StartCoroutine("CatMove");
        graphic_change(0);
        CardCover.SetActive(false);
        for(int i = 0; i < maxCatnip; i++)
        {
            catnip[i].SetActive(false);
        }
    }

    private void Update()
    {
        Vector3 temp = Cat.position;
        SP_bar.position = new Vector3(temp.x + 0.05f, temp.y - 0.4f);
    }

    IEnumerator HeroTimer()
    {
        while (isGame)
        {
            if (HeroSlider.value <= 0)
            {
                isGame = false;
            }
            if (HeroSlider.value < HeroSlider.maxValue && isCurse)
            {
                HeroSlider.value += HeroSpeed;
            }
            else
            {
                HeroSlider.value -= HeroSpeed;
            }

            for(int i = 0; i < 3; i++)
            {
                HeroABC[i].anchorMin = new Vector2(Hero.anchorMin.x, 0);
                HeroABC[i].anchorMax = new Vector2(Hero.anchorMax.x, 0);
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    // 카드 기능 함수
    public void CardFunction(int num)
    {
        switch (num)
        {
            case 4:
                provoke();
                break;
            case 6:
                On_Catnip();
                break;
            case 7:
                On_SOS();
                break;
        }
    }

    // 비둘기 전갈 카드 (시간 누적 O)
    public void On_SOS()
    {
        isSOS = true;
        SOS_repeat++;

        if (SOS_repeat == 1)
        {
            HeroAnimator.SetBool("run", true);
            HeroSpeed = 2;
            HeroSOS.SetActive(true);
            StartCoroutine("Off_SOS");
        }
        
    }

    IEnumerator Off_SOS()
    {

        while (SOS_repeat > 0)
        {
            yield return new WaitForSecondsRealtime(10f);
            SOS_repeat--;
        }
        isSOS = false;
        HeroDual.SetActive(false);
        HeroAnimator.SetBool("run", false);
        HeroSpeed = 1;
        HeroSOS.SetActive(false);
    }

    // 도발 카드 (시간 누적O)
    public void provoke()
    {
        isProvoke = true;
        Provoke_repeat++;
        if (Provoke_repeat == 1)
        {
            cat_wait = cat_wait / 2;
            graphic_change(2);
            SP_Slider.value = 0;
            StartCoroutine("After_provoke");
        }

    }

    IEnumerator After_provoke()
    {
        while (Provoke_repeat > 0)
        {
            yield return new WaitForSecondsRealtime(18f);
            Provoke_repeat--;
        }
        cat_wait = cat_wait * 2;
        SP_Slider.value = 0;
        graphic_change(0);
        isProvoke = false;
    }

    // JPS
    IEnumerator CatMove()
    {
        // 게임 시작 후 잠시동안은 움직이지 않음
        Cat.position = new Vector3(blockSize - 7.4f, 6 * 0.5f * -(blockSize * 7f + blockBuffer) - blockSize + 2.3f);
        yield return new WaitForSecondsRealtime(4f);

        while (isGame)
        {

            // 캣잎에 닿았다면
            if (isCatnipOn)
            {
                isCatnipOn = false;
                int stopTime = Random.Range(4, 7);
                print("캣잎 : 4 + " + stopTime + "초 추가 정지");
                yield return new WaitForSecondsRealtime(4 + stopTime);
            }
            else
            {
                // 마법사가 움직이는 타이밍 조절
                for (int i = 0; i < cat_wait; i++)
                {
                    if (SP_Slider.value < 100 && !isProvoke)
                    {
                        SP_Slider.value++;
                    }
                    yield return new WaitForSecondsRealtime(1f);
                }
            }

            // 다음 순서의 길이 있다면 다음 노드로 이동
            if (gridView.minIndex>=0&&gridView.isPath && gridView.CatIndex < gridView.CatPath[gridView.minIndex].Count)
            {
                gridView.CatIndex++;
                next = gridView.CatPath[gridView.minIndex][gridView.CatIndex];
                bool isColumn = next.column % 2 == 1 ? true : false;
                bool isRow = next.row % 2 == 1 ? true : false;
                float xSize = 0, ySize = 0;

                // 위치 지정
                if (isColumn)
                {
                    xSize = (next.column + 1) * 0.5f * (blockSize * 7f + blockBuffer) - blockSize * 3f;
                }
                else
                {
                    xSize = next.column * 0.5f * (blockSize * 7f + blockBuffer) + blockSize;
                }
                if (isRow)
                {
                    ySize = (next.row + 1) * 0.5f * -(blockSize * 7f + blockBuffer) + blockSize * 3f;
                }
                else
                {
                    ySize = next.row * 0.5f * -(blockSize * 7f + blockBuffer) - blockSize;
                }

                // 시작 위치 변경
                gridView.temp_x = next.column;
                gridView.temp_y = next.row;

                Cat.position = new Vector3(xSize-7.4f, ySize+2.3f, 5); // 수동으로 변경할 부분 좌표계
                yield return new WaitForSeconds(0.000000001f);
            }

        }
    }

    // 마법사 sprite 변경
    void graphic_change(int curr)
    {
        for (int i = 0; i < Cat_graphic.Length; i++)
        {
            Cat_graphic[i].SetActive(false);
        }
        Cat_graphic[curr].SetActive(true);
    }

    // 휴식 스킬
    public void Cat_rest()
    {
        graphic_change(1);
        cat_wait = 12;
        Invoke("Cat_SPplus", 12f);
    }
    
    void Cat_SPplus()
    {
        if (SP_Slider.value <= 70)
        {

            SP_Slider.value += 30;
        }
        else
            SP_Slider.value = 100;
        cat_wait = 4;
        graphic_change(0);
    }

    // 저주 스킬
    public void Cat_curse()
    {
        if (SP_Slider.value < 30)
            return;

        graphic_change(1);
        isCurse = true;
        Hero.localScale = new Vector3(-1, 1, 1);
        HeroCurse.SetActive(true);
        SP_Slider.value -= 30;
        if (isSOS)
        {
            HeroSOS.SetActive(false);
            HeroDual.SetActive(true);
        }
        Invoke("After_curse", 15f);

    }

    void After_curse()
    {
        isCurse = false;
        HeroCurse.SetActive(false);
        HeroDual.SetActive(false);
        if (isSOS)
            HeroSOS.SetActive(true);
        Hero.localScale = new Vector3(1, 1, 1);
        graphic_change(0);
    }

    // UI관련 함수
    IEnumerator On_UI(int num)
    {
        float valueA = 1;
        // num에 해당하는 UI를 킨다.
        for (int i = 0; i < UIArray_E.Length; i++)
        {
            UIArray_E[i].SetActive(false);
        }
        UIArray_E[num].SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        // 그 UI 이미지가 점점 투명해지다가 꺼지게 한다.
        for (int i = 1; i <= 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            valueA -= 0.1f;
            UIImage_E[num].color = new Color(1, 1, 1, valueA);
        }
        UIArray_E[num].SetActive(false);
    }

    // 에러UI 띄우기
    public void On_ErrorUI()
    {
        StopCoroutine("On_UI");
        StartCoroutine("On_UI", 0);
    }

    // 뇌물 카드 함수
    public void On_Catnip()
    {
        isCatnip = true;
        CardCover.SetActive(true);
        UIArray_N[0].SetActive(true);
    }

    public void CreateCatnip(int column, int row)
    {
        // 고양이 주변에는 캣닢 설치 불가
        if (next.column - 2 <= column && column <= next.column + 2 && next.row - 2 <= row && row <= next.row + 2)
        {
            On_ErrorUI();
            return;
        }

        isCatnip = false;
        bool isColumn = column % 2 == 1 ? true : false;
        bool isRow = row % 2 == 1 ? true : false;
        float xSize = 0, ySize = 0;

        // 위치 지정
        if (isColumn)
        {
            xSize = (column + 1) * 0.5f * (blockSize * 7f + blockBuffer) - blockSize * 3f;
        }
        else
        {
            xSize = column * 0.5f * (blockSize * 7f + blockBuffer) + blockSize;
        }
        if (isRow)
        {
            ySize = (row + 1) * 0.5f * -(blockSize * 7f + blockBuffer) + blockSize * 3f;
        }
        else
        {
            ySize = row * 0.5f * -(blockSize * 7f + blockBuffer) - blockSize;
        }
        catnip[catnipIndex].SetActive(true);
        catnipXY[catnipIndex].position = new Vector3(xSize-7.3f, ySize+2.3f, 5f);

        StartCoroutine("Check_Catnip", catnipIndex);

    }

    IEnumerator Check_Catnip(int index)
    {
        yield return new WaitForSeconds(0.0000000001f);
        print("Check_Catnip : " + isCatnip);
        // 캣닢이 설치된 곳에 캣닢 또는 허수아비가 있지 않다면 정상작동
        if (!isCatnip)
        {
            UIArray_N[0].SetActive(false);
            CardCover.SetActive(false);
            catnipIndex = (catnipIndex + 1) % maxCatnip;
            yield return new WaitForSecondsRealtime(15f);
            catnip[index].SetActive(false);
        }
        // 아니라면 캣닢설치 취소 및 에러창 생성
        else
        {
            catnip[index].SetActive(false);
            On_ErrorUI();
        }
    }
    
}
