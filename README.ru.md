# Основва для 2D Platformer (Unity)
➡️ [English](./README.ru.md) || [Скачать](link)

## Основные особенности

- Комфортный для дизайнеров пайплайн создания уровней (без кода)
- Собственные инструменты для создания уровней
- Runtime загрузка уровней основанная на Addressables
- Полностью функциональное главное меню и настройки
- Камера Cinemachine
- 2D-физика (rigidbodies, colliders, layer masks)
- Легко добавлять новые механики и фичи (гибкая архитектура)

## Инструменты для создания уровней

### Инструмент для установки блоков по гриду
![repo](https://github.com/user-attachments/assets/067e36f9-0392-4658-a498-55ca11e3150a)
Спавнит выбранные префабы (или наполняет весь грид только одним префабом)


### Инструмент для установки платформ
![repo1](https://github.com/user-attachments/assets/16ceb96f-b594-4567-b063-e3c35f428692)
Автоматически устанавливает путь для платформы (из точки А в точку Б), позволяя также настраивать параметры (скорость, длительность паузы)

## Геймплей 
![repo2](https://github.com/user-attachments/assets/525eedb6-5537-49ed-a008-58ae619aac6f)
## Особенности добавления нового контента
- Каждый уровень - отдельная сцена Addressables
- Уровни описаны в ScriptableObjects (name, thumbnail, scene reference)
- Меню с уровнями наполняется во время запуска игры
- Возможность добавлять новые уровни не изменяя код игры

## Цели проекта
- Испытать полный производственный цикл: от первого комита до релиза
- Создать фундамент, позволяющий легко масштабировать игру, добавляя новые механики и контент
- Создать простой пайплайн для дизайнеров

## Использованные ассеты
- [Bird 16x16 by ma9ici4n, CC0 license](https://ma9ici4n.itch.io/pixel-art-bird-16x16)
- [Bold Pixels font by Yuki, CC BY-SA 4.0 license](https://yukipixels.itch.io/boldpixels)
- [Brackeys' Platformer Bundle by Brackeys, CC0 license](https://brackeysgames.itch.io/brackeys-platformer-bundle)
- [Cozy Tunes by Pizza Doggy, CC-BY 4.0 license](https://pizzadoggy.itch.io/cozy-tunes)
- [Flag with animation by ankousse26, CC0 license](https://ankousse26.itch.io/free-flag-with-animation)
- [Nature Landscapes by CraftPix.net, CraftPix.net license](https://free-game-assets.itch.io/nature-landscapes-free-pixel-art)
- [Parallax Backgrounds by Bongseng, CC0 license](https://bongseng.itch.io/parallax-forest-desert-sky-moon)
- [RPG Essentials SFX by Leohpaz, Custom Free License (No Redistribution)](https://leohpaz.itch.io/rpg-essentials-sfx-free)
