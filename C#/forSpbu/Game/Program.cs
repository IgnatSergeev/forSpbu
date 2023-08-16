using Game;

var eventLoop = new EventLoop();
var game = new Game.Game("../../../map.txt", movePlayer);

void movePlayer(int oldXPosition, int oldYPosition, int newXPosition, int newYPosition) {
    Console.SetCursorPosition(oldXPosition + 1, oldYPosition + 1);
    Console.Write(' ');
    Console.SetCursorPosition(oldXPosition + 1, oldYPosition + 1);
    Console.SetCursorPosition(newXPosition + 1, newYPosition + 1);
    Console.Write('@');
    Console.SetCursorPosition(newXPosition + 1, newYPosition + 1);
}

eventLoop.StartHandler += game.OnStart;
eventLoop.EndHandler += Game.Game.OnEnd;

eventLoop.LeftHandler += game.OnLeft;
eventLoop.RightHandler += game.OnRight;
eventLoop.DownHandler += game.OnDown;
eventLoop.UpHandler += game.OnUp;

eventLoop.Run();