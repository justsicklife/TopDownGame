VAR intelligence = 0

-> car_scene

=== car_scene ===
# 차 안 - 조용한 도로 위

(엔진 소리와 함께, 라디오가 켜진다.)

오늘의 주제는 '가정용 AI의 위험성'입니다. # speaker: radio_host
전 인공지능이 인간의 일자리를 대체할 거라 봅니다. 이미 배우, 모델, 심지어 뉴스 진행자까지...  # speaker: radio_panel_A
하지만 효율성과 편리함은 무시할 수 없죠. 감정 없는 로봇이 오히려 인간보다 낫다는 사람들도 있습니다.  # speaker: radio_panel_B

(주인공은 담배를 꺼내 손에 쥔다.) # speaker: my

* [담배를 핀다]
    (라이터 소리) # speaker: cigarette_ligheter
    불길이 어둠 속에서 번쩍였다.  # speaker: narrator
    머릿속이 맑아지는 기분이다.  # speaker: narrator
    ~ intelligence += 1
    -> after_choice

* [피지 않는다]
    이젠 끊을 때도 됐지. # speaker: my
    창밖의 불빛들이 조금 흐릿해 보였다. # speaker: narrator
    ~ intelligence -= 1
    -> after_choice

=== after_choice ===
자, 이제 청취자 의견을 받아볼까요?  # speaker: radio_host
라디오 소리에 잠시 집중했다. 하지만 머릿속 한구석이 이상하게 무거웠다. # speaker: narrator

-> END
