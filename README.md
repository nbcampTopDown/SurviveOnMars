## 🧑‍💼 Sirvive On Mars

Unity 심화 주차 팀프로젝트 문영오 육수들IV 의 팀프로젝트 입니다.

## 💁‍♂️ 소개

### 머나먼 미래에 내배캠, 문영오 매니저님의 명에 따라 TIL을 쓰지 않는 외계인들을 징벌하기 위해 화성까지 쫓아왔다. 각오하라.

## ⏰ 개발 기간

- 24.02.26 ~ 24.03.04

## 👨‍👨‍👧‍👦 멤버

- 문정현(팀장) : Player, Enemy, EnemySpawn, EndingLogic
- 김준하 : Weapon, Map, Audio, ObjectPool
- 김철우 : StoreUI, Upgrade Item
- 김상민 : UI, StartScene,EndingScene

## 💻 개발 환경

- Engine : Unity 2022.3.2f1
- Language : C#

## 📑 구성

### StartScene

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/ca691ed0-d7ff-4a69-a5e3-b54cc9d1c541)


### GameScene

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/db35dbfd-bcd6-4324-a034-14187d2c8167)

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/96b88d59-3394-4b1e-9049-12926994916a)

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/82d3c315-714e-49be-9b09-17e377ccf44d)

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/30864c13-756e-4d0b-843d-b2509866b36a)


### EndingScene

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/72f4a328-93fd-47eb-b432-6f8408af3bba)


## 구현사항

### 1. 게임 맵 생성 및 배치

- Terrain을 사용하여 게임 맵을 생성하고, NavMesh를 사용해 적들의 맵 경로를 설정합니다.

### 2. 플레이어

- 플레이어 유닛이 맵의 스폰 포인트에 동적으로 생성되고, 플레이어의 조작에 따라 유닛은 특정 애니메이션을 취하고 행동합니다.
- 플레이어는 걷기, 달리기, 조준, 사격, 재장전 등의 행동이 가능합니다.

### 3. 적의 움직임과 스폰

- 둥지에서 일정한 양의 적 캐릭터 오브젝트를 일정한 시간 간격에 따라 스폰합니다.
- 적 캐릭터 오브젝트는 Idle State와 Chase State로 이루어져 있습니다.
- Idle State에서는 자신의 위치에서 서성이는 움직임을 보입니다.
- Chase State는 NavMesh로 플레이어를 추적하는 움직임을 보입니다.

### 4. 플레이어의 공격

- 플레이어는 우클릭을 통해 총을 조준할 수 있습니다.
- 플레이어는 조준한 상태에서만 총을 사격할 수 있습니다.
- 플레이어는 G키를 눌러 수류탄을 던질 수 있습니다.

### 5. 적과 플레이어 유닛의 체력 및 공격력

- 등장하는 적, 플레이어 유닛의 공격력을 지정하고 데미지 계산 및 체력 감소 로직을 구현했습니다.

### 6. 사운드 시스템

- 오디오 믹서를 만들어, 오디오 그룹의 소리를 조절하는 걸 구현했습니다.
- 풀링이 필요한 오디오는 오브젝트 풀링으로 구현했습니다.

### 7. 오브젝트 풀링

- PoolManager를 만들어, 오브젝트 풀링이 필요한 오브젝트들을 관리해줬습니다.

### 8. 업그레이드 시스템

- 플레이어는 적을 처치하거나 둥지를 파괴하면 일정량의 돈을 얻을 수 있습니다.
- Tap을 눌러 업그레이드에서 Player의 공격력, 연사속도 등을 구매하여 플레이어를 강하게 만들 수 있습니다.

### 9. 게임 진행 상태 표시

- GameScene에서 플레이어는 UI를 통해 남은시간과 플레이어의 정보를 확인할 수 있습니다.
- Tab을 눌러 Player의 정보, 임무목표, 플레이어의 스탯을 올릴수있습니다.

### 10. 게임 오버 및 승리 조건

- 플레이어 HP가 0이 되면 패배합니다
- 지정된 시간을 버텨 우주선에 탑승하면 승리합니다.

### 11. 엔딩

- Player가 지정된 시간을 모두 버티고 도착한 우주선에 탑승하면 엔딩씬으로 넘어가 엔딩크레딧이 재생됩니다.

### 12. 동적 생성

- 동적 생성을 통해 맵과 플레이어, 적, UI 등의 거의 모든 오브젝트를 생성하도록 구현했습니다.

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/5eedc40c-7187-41be-914c-b1bdb3361a35)


## 🖌️ 와이어프레임

![image](https://github.com/nbcampTopDown/SurviveOnMars/assets/154484912/2aa11205-f282-4607-a48f-f941e8ea344c)


## 🪄 Asset

### 사용 에셋 및 이미지 :

- https://assetstore.unity.com/packages/3d/environments/landscapes/mars-landscape-3d-175814 - 화성
- https://assetstore.unity.com/packages/3d/characters/insectoid-crab-monster-lurker-of-the-shores-20-animations-107223#description - 적
- https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913 - 건물
- https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/sci-fi-space-soldier-polygonr-66384 - 캐릭터 & 무기1
- https://assetstore.unity.com/packages/3d/props/realistic-grenades-pack-53211 - 수류탄
- https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325 - 유니티 파티클 팩
- https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633 - 스카이박스
- https://assetstore.unity.com/packages/3d/vehicles/space/destructor-spaceship-3229 - 우주선
- https://assetstore.unity.com/packages/2d/textures-materials/space-star-field-backgrounds-109689 - StartScene - BackGround
- https://assetstore.unity.com/packages/2d/gui/sci-fi-gui-skin-15606 - UI
- https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676 - DotWeen
- Song : Scott Buckley - Midsommar [/ scottbuckley](https://www.youtube.com/redirect?event=video_description&redir_token=QUFFLUhqbGtSWHNCYVF5bHpiWTdJRlhxU0xnM2dQVGIxUXxBQ3Jtc0tuTlJsb1NOTGFiWENrMFlKek13NGZJSEFrZkZSc3hWZklaSVNYM20xaFJDbWhkbXloX1pJRmJFX2ZoVWJEOU5pejFzTHQ5UkJqTnhZOGdBWWM4NmliR3BMWUNad2FIeUR3VUtkTTZub3l5ZlVhYUxrMA&q=https%3A%2F%2Fsoundcloud.com%2Fscottbuckley&v=sbb-y745cwc) Creative Commons — Attribution 3.0 Unported  — CC BY 3.0 Music promoted by Vlog Copyright Free Music 
Link :  [• Scott Buckley - Midsommar [Vlog No Co...](https://www.youtube.com/watch?v=sbb-y745cwc&t=0s)

https://assetstore.unity.com/publishers/13489 - weapon icon
