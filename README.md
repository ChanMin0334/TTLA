# 20조 스파르타 아처
# 프로젝트 소개
🎮 2D 액션 게임 프로젝트
이 프로젝트는 Unity를 사용하여 개발된 탑다운(top-down) 방식의 2D 액션 게임입니다. 플레이어는 다양한 몬스터와 보스를 상대하며 공격, 이동을 수행을 합니다. 궁수의 전설의 게임을 바탕을하여 재구현한 게임입니다. 

주요 기능:

자동 조준 및 공격 시스템

다양한 몬스터 패턴 및 보스전 구현

방 클리어 조건에 따른 진행

아이템 장착 및 스탯 강화

보스 등장 전 연출 및 페이즈 변환 시스템


# 📖 목차 (Table of Contents)
팀 기획

요구 사항 및 호환성

프로젝트 구조

알려진 문제점

# 기획

플로우차트
<img width="1076" height="934" alt="image" src="https://github.com/user-attachments/assets/4d28a03f-6b6b-46c7-8545-e3a56e8a752d" />


와이어프레임
<img width="1220" height="844" alt="image" src="https://github.com/user-attachments/assets/b4c5bc7d-72ee-4d23-bbbf-0a2cdd86dd1e" />


UML
<img width="1056" height="706" alt="image" src="https://github.com/user-attachments/assets/e78cb44b-b7aa-4a5f-95a3-15c43ab1fb33" />


# 💻 요구 사항 및 호환성
Unity 버전: 2021.3 이상

지원 플랫폼: Windows

필요한 패키지:

TextMeshPro

2D Sprite

h8man/NavMeshPlus: https://github.com/h8man/NavMeshPlus.git#master


# 프로젝트 구조
일부분의 프로젝트 파일과, 구조. 
📦Assets
├─ 📁Scripts
│  ├─ Player.cs : 플레이어 캐릭터의 이동, 공격, 피격, 활 조준 등 주요 기능 구현
│  ├─ MonsterBoss.cs : 보스 몬스터의 AI 및 공격 패턴 관리
│  ├─ BossSpawner.cs : 보스 스폰 및 등장 연출 담당
│  ├─ Entity.cs : 플레이어 및 몬스터의 공통 기반 클래스
│  ├─ GameManager.cs : 싱글톤으로 게임 전반 흐름 및 상태 관리
│  ├─ UIManager.cs : UI 관련 처리를 담당 (게임 오버, 클리어 등)
├─ 📁Prefabs
│  ├─ Player.prefab : 플레이어 프리팹
│  ├─ Boss.prefab : 보스 프리팹
├─ 📁Scenes
│  ├─ MainScene.unity : 메인 메뉴 씬
│  ├─ Level1_Forest.unity : 1스테이지 전투 씬
├─ 📁Animations
│  ├─ 애니메이션 클립 및 컨트롤러
├─ 📁Audio
│  ├─ 사운드 효과 파일들
├─ 📁Sprites
│  ├─ 캐릭터 및 몬스터 스프라이트 이미지
|─────

# 앞으로 추가할 콘텐츠
게임을 더욱 풍부하게 만들기 위해 다음과 같은 기능들을 추가할 계획입니다:

🏹 무기 및 스킬 시스템
다양한 무기를 획득하고 장착할 수 있는 시스템과, 액티브/패시브 스킬 트리 구현

🧠 적 AI 개선
몬스터마다 개별적인 AI 패턴을 부여하고, 던전의 난이도에 따라 변화하는 지능적인 움직임

🗺️ 월드 맵 & 다중 스테이지
현재는 단일 스테이지이지만, 추후에는 월드 맵과 다양한 지역(사막, 얼음, 용암 등)을 추가할 예정

🎁 랜덤 아이템 드랍 및 인벤토리 시스템 확장
몬스터 처치 시 확률적으로 아이템을 드랍하고, 이를 인벤토리에서 장착/판매 가능하도록

🎮 보스 다양화 및 고유 페이즈
여러 종류의 보스와 고유한 공격 패턴을 추가하여 매 스테이지마다 새로운 도전 제공

🏆 도전 과제 & 업적 시스템
특정 조건을 달성 시 업적을 해금하고 보상을 얻는 시스템
