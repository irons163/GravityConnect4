![Build Status](https://img.shields.io/badge/build-%20passing%20-brightgreen.svg)
![Platform](https://img.shields.io/badge/Platform-%20Unity%20-blue.svg)

# GravityConnect4 

- GravityConnect4 is a game in Unity.

## Features

- GravityConnect4:After the board is clicked, the flag(ball) will fall from the air, and the person who first connects the four flags into a line will win. The game has two modes: 1. Human to computer 2. Human to human. The computer has a considerable AI, you can enjoy the fun of playing chess, you can also switch the game mode to play with friends.
- 重力四子棋:點擊棋盤後旗子會從空中掉落，先將四個旗子連成一線的人獲勝。遊戲有兩種模式: 1.人類對電腦 2.人類對人類。電腦擁有可觀的AI，可以享受下棋的樂趣，也可以切換遊戲模式改成和朋友一起同樂。

## Usage

```obj-c
#import "Log.h"

- (NSString*)AddHeaderToMessage:(NSString*)message {
    counter++;
    return [NSString stringWithFormat:@"%@ %@%d %@", MyLivewViewStatusLogkey, @"Index:",counter, message];
}

[self AddHeaderToMessage:@"My custom log string."];
```
