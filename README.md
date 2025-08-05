# 20조 스파르타 아처
Sparta Archer

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
//

와이어프레임
//

UML
//

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

# 알려진 문제점


# 앞으로 추가할 콘텐츠

