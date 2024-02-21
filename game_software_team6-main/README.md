# 게임 소프트웨어 팀 6 - 좀비와 스켈레톤

## Unity Version
2022.3.9f1

## 게임 설명
턴 제로 진행되는 포트리스류 포격게임이다.

게임의 진행 과정은 다음과 같다.

1. 게임을 실행한다. (싱글플레이 클릭)
2. 각자의 턴이 주어졌을 때 상대를 맞춘다.
3. 두 플레이어 중 한 명이라도 HP가 0에 도달하면 게임이 종료된다.

## 유니티 프로젝트 설치 시 주의 사항
1. 게임 배포과정 예상치 못한 버그가 발생하여 배포가 불가능합니다. 첨부된 unityPakage를 통해 설치하여 실행해야 합니다.
2. 새 유니티 프로젝트 생성 후 Import Package 후 Package Manager에서 Input System을 다운받아야 한다.

## 게임 방법 및 주의 사항
1. 게임을 실행하고 싱글플레이 버튼을 누른다. (멀티 플레이는 미구현)
2. 게임 진행 방법
    1. 마우스를 이용하여 상하좌우 조준선 방향을 이동시킬 수 있다.
    2. 마우스 왼쪽 버튼을 클릭하게 되면 화면에 Press Bar의 상태를 확인하여 원하는 세기를 설정하고 클릭한 왼쪽 버튼을 때어 물체를 던질 수 있다.
3. 아이템 사용방법
    1. 아이템 사용은 키보드의 (1,2,3,4) 를 이용하여 설정할 수 있다.
    2. 아이템 사용횟수는 1회로 제한되어 있어 한 번 사용한 아이템은 다시 사용이 불가능하다.
4. 바람 고려
    1. 게임 내 필드에는 바람이 존재한다.
    2. 화면 내에 존재하는 십자가의 방향을 통해 바람을 확인하고 이를 계산하여 상대방을 맞춰야 한다.
5. 게임 승리 조건
    1. 상대의 HP를 0으로 만들거나 자신의 HP가 0이되면 종료된다.
6. 발생할 수 있는 버그 사항
    1. 특수한 경우에 던진 무기가 바닥의 터레인을 뚫고 사라지는 현상이 발생할 수 있음. 해당 상황에서는 게임 재시작 필요.

## 사용한 Asset
### 무기
[1. 무기(무료)](https://assetstore.unity.com/packages/3d/props/exterior/medieval-barrels-and-boxes-137474)
        - 무기의 종류로 사용함 (원통 무기/ 박스 무기)

### 캐릭터
[1. 스캘레톤(무료)](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/skeleton-pbr-animated-low-poly-30659)
        - 플레이어 구현을 위해 사용
        - 에셋 내의 애니메이션을 파라미터를 이용하여 구현하여 피격 시 모습을 보여줌.
        
[2. 좀비(무료)](https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232)
        - 플레이어 구현을 위해 사용
        - 에셋 내의 애니메이션을 파라미터를 이용하여 구현하여 피격 시 모습을 보여줌.

### 맵 배경
[1. 나무 및 지형(무료)](https://assetstore.unity.com/packages/tools/terrain/vegetation-spawner-177192)
        - 맵 배경 (나무, 지형 지물)을 위해 사용

[2. 바람 안내 지형(무료)](https://assetstore.unity.com/packages/3d/environments/fantasy/halloween-pack-cemetery-snap-235573)
        - 바람을 사용자에게 시각적으로 제공하기 위해 십자가 형태로 풍향계를 구
